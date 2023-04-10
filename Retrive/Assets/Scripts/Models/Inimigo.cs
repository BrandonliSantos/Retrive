using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : EntidadeBase
{
    [SerializeField] protected Transform posPlayer;
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
        throw new System.NotImplementedException();
    }

    protected override void PerdeVida(int quantidade)
    {
        throw new System.NotImplementedException();
    }
}
