using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : EntidadeBase
{
    [SerializeField] protected Transform posPlayer;
    [SerializeField] protected SpriteRenderer sprite;
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
        throw new System.NotImplementedException();
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
    }

    public override void PerdeVida(int quantidade)
    {
        vida -= quantidade;

        if(vida <= 0 && estaVivo)
        {
            estaVivo = false;
            Destroy(gameObject);
            var player = FindAnyObjectByType<PlayerController>();

            if(player)
            {
                player.AumentarNumeroMortes();
                player.GanharXp(5);
            }
        }
    }

}
