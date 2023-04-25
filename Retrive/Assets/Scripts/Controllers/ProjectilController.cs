using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilController : Projeteis
{

   [SerializeField] private float rotacaoVel = 200f;
    
    void Start()
    {
        myRB.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = (Vector2)alvo.position - myRB.position;
        Debug.Log(alvo.position);
        
        dir.Normalize();

        float rotacao = Vector3.Cross(dir, transform.up).z;
        Debug.Log(rotacao);
        
        myRB.angularVelocity = -rotacao * rotacaoVel;
        
        myRB.velocity = transform.up * velocidadeMovimento;
    }

}
