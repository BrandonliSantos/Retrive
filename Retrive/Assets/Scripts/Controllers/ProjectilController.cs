using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilController : Projeteis
{
    
    [SerializeField] private float timer = 3f;   
    
    void Start()
    {
        myRB.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Disparo();
        timer -= Time.deltaTime;
        Debug.Log(timer);
        
    }

    void Disparo()
    {
        if(timer <= 0)
        {

            GameObject tiro = Instantiate(projetil, transform.position, Quaternion.identity);
            tiro.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadeMovimento, 0f);
            timer = 3f;

            Destroy(tiro, 5f);

        }
    }
}
