using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntidadeBase
{
    [SerializeField] private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
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
}
