using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilController : Projeteis
{

   [SerializeField] private float rotacaoVel = 800f;
    
    void Start()
    {
        var player = FindAnyObjectByType<PlayerController>();

        if(player)
            alvo = player.transform;

        else
            alvo = transform;

        myRB.GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = (Vector2)alvo.position - myRB.position;
        
        dir.Normalize();

        //float rotacao = Vector3.Cross(dir, transform.up).z;
        
        myRB.angularVelocity = rotacaoVel;

        Vector3 newPosition = Vector3.MoveTowards(transform.position, alvo.position, velocidadeMovimento * Time.deltaTime);
        transform.position = newPosition;
        
        //myRB.velocity = transform.up * velocidadeMovimento;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        other.TryGetComponent<PlayerController>(out var player);

        if(player is null) return;

        player.PerdeVida(dano);
        Destroy(gameObject);
    }

    private void OnDestroy() 
    {
        var animacao = Instantiate(animacaoDestruir, transform.position, Quaternion.identity);
        Destroy(animacao, .3f);    
    }

}
