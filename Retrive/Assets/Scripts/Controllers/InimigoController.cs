using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoController : Inimigo
{
    [SerializeField] private Rigidbody2D myRB;
    

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        SeguirPlayer();
       
    }

    void SeguirPlayer()
    {

        Vector3 minhaposicao = transform.position;

        Vector3 targetPosition = posPlayer.position;

        Vector3 newPosition = Vector3.MoveTowards(minhaposicao, targetPosition, velocidadeMovimento * Time.deltaTime);

        transform.position = newPosition;

    }
}
