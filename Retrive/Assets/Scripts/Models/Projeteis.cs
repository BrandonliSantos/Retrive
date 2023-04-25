using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projeteis : MonoBehaviour
{
    [SerializeField] protected float velocidadeMovimento;
    [SerializeField] protected Rigidbody2D myRB;
    [SerializeField] protected Transform alvo;
    [SerializeField] protected GameObject animacaoDestruir;

    protected int dano = 0;

    public void DefinirDano(int dano) => this.dano = dano;
}
