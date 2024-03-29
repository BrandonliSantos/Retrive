using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntidadeBase : MonoBehaviour
{
    [SerializeField] protected int vida;
    [SerializeField] protected int vidaMax;
    [SerializeField] protected float velocidadeAtaque;
    [SerializeField] protected int ataque;
    [SerializeField] protected float velocidadeMovimento;
    [SerializeField] protected bool estaVivo = true;

     protected abstract void Mover();

     protected abstract void Atacar();

     protected abstract void Morrer();

     public abstract void PerdeVida(int quantidade);
}
