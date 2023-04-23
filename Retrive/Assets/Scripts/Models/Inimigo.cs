using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : EntidadeBase
{

    [SerializeField] protected Transform posPlayer;
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected GameObject drop;
    [SerializeField] protected GameObject animMorte;
    [SerializeField] protected float duracaoAnimMorte;
    [SerializeField] protected float limMaxX;
    [SerializeField] protected float limMinX;
    [SerializeField] protected float limMaxY;
    [SerializeField] protected float limMinY;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Atacar()
    {
        throw new System.NotImplementedException();
    }

    protected override void Morrer()
    {
        if(vida <= 0 && estaVivo)
        {
            estaVivo = false;
            Instantiate(drop, transform.position, transform.rotation);
            
            Destroy(gameObject);
            var player = FindAnyObjectByType<PlayerController>();

            if(player)
            {
                player.AumentarNumeroMortes();
            }

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


        float myX = Mathf.Clamp(transform.position.x, limMinX, limMaxX);
        float myY = Mathf.Clamp(transform.position.y, limMinY, limMaxY);

        transform.position = new Vector3(myX, myY, 0f);         
    }

    public override void PerdeVida(int quantidade)
    {
        vida -= quantidade;       
    }
}
