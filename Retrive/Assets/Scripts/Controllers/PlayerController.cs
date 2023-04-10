using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : EntidadeBase
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Slider barraVida;


    // Start is called before the first frame update
    void Start()
    {
        barraVida.maxValue = vidaMax;
        barraVida.value = vidaMax;
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
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
        float y = Input.GetAxis(Controle.UpDown) * velocidadeMovimento;
        float x = Input.GetAxis(Controle.LeftRight) * velocidadeMovimento;

        rb.velocity = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        other.TryGetComponent<Inimigo>(out var inimigo);

        if(inimigo is null) return;
        
         PerdeVida(5);
    }

    protected override void PerdeVida(int quantidade)
    {
        vida -= quantidade;
        barraVida.value = vida;
    }
}
