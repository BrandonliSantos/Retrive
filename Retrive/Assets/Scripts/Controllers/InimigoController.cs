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
        
        Mover();
        
    }


}
