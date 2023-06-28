using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : EntidadeBase
{

    [Header("Objetos dependentes")]
    [SerializeField] protected Transform posPlayer;
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected GameObject drop;
    [SerializeField] protected GameObject dropMoeda;
    [SerializeField] protected GameObject animMorte;
    [SerializeField] protected Animator animator;
    [SerializeField] protected GameObject projetil;

    [Header("Propriedades")]
    [SerializeField] protected float duracaoAnimMorte;
    [SerializeField] protected float limMaxX;
    [SerializeField] protected float limMinX;
    [SerializeField] protected float limMaxY;
    [SerializeField] protected float limMinY;
    [SerializeField] protected bool disparaProjetil = false;
    [SerializeField] protected float delayDisparo = 7f;
    [SerializeField] protected bool spriteOriginalViradoParaDireita = true;

    [SerializeField] protected float chanceDropMoedaPorcentagem = 1;
    protected float timer = 0;

    protected override void Atacar()
    {
        if(sprite.isVisible && disparaProjetil)
        {
            timer -= Time.deltaTime;

            if(timer > 0) return;

            var objProjetil = Instantiate(projetil, transform.position, transform.rotation);
            objProjetil.GetComponent<ProjectilController>().DefinirDano(ataque);
            timer = delayDisparo;
        }
    }

    protected override void Morrer()
    {
        if(vida <= 0 && estaVivo)
        {
            estaVivo = false;

            //Dropa EXP
            Instantiate(drop, transform.position, transform.rotation);

            //Dropa Moeda
            DroparMoeda();
            
            Destroy(gameObject);
            var player = FindAnyObjectByType<PlayerController>();

            if(player)
            {
                player.AumentarNumeroMortes();
            }

            Soundboard.instance.TocarSomInimigoMorte();

            //Criar animação morte
            var animacao = Instantiate(animMorte, transform.position, transform.rotation);
            animacao.GetComponentInChildren<SpriteRenderer>().flipX = sprite.flipX;
            Destroy(animacao, duracaoAnimMorte);
        }      
    }

    protected override void Mover()
    {
        if(!posPlayer) return;

        Vector3 minhaposicao = transform.position;

        Vector3 targetPosition = posPlayer.position;

        Vector3 newPosition = Vector3.MoveTowards(minhaposicao, targetPosition, velocidadeMovimento * Time.deltaTime);

        transform.position = newPosition;

        if(targetPosition.x < minhaposicao.x)
            sprite.flipX = true;

        else
            sprite.flipX = false;

        sprite.flipX = spriteOriginalViradoParaDireita ? sprite.flipX : !sprite.flipX;
           
        float myX = Mathf.Clamp(transform.position.x, limMinX, limMaxX);
        float myY = Mathf.Clamp(transform.position.y, limMinY, limMaxY);

        transform.position = new Vector3(myX, myY, 0f);         
    }

    public override void PerdeVida(int quantidade)
    {
        vida -= quantidade;
        animator.SetTrigger("RecebeDano");      
    }

    void DroparMoeda()
    {
        var chance = Random.Range(0, 101);

        if(chance < chanceDropMoedaPorcentagem)
        {
            //Setando a posição para a moeda não ficar por cima do EXP
            Vector3 posicaoSpawn = new Vector3(transform.position.x + .8f, transform.position.y, transform.position.z);
            Instantiate(dropMoeda, posicaoSpawn, transform.rotation);
        }
            
    }
}
