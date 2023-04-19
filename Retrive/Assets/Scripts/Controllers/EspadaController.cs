using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EspadaController : MonoBehaviour
{
    [SerializeField] private GameObject popUpDano;
    public GameObject posAtaque;

    public int danoAtaque;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, .6f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = posAtaque.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        other.TryGetComponent<Inimigo>(out var inimigo);

        if(inimigo is null) return;

        inimigo.PerdeVida(danoAtaque);

        //Criando popup de dano na cabeça do inimigo
        var popUp = Instantiate(popUpDano, other.transform.position, Quaternion.identity);
        popUp.transform.position = new Vector3(popUp.transform.position.x, popUp.transform.position.y + .5f, 10);

        //passando o dano causando para o popUp
        popUp.GetComponentInChildren<TextMeshPro>().SetText(danoAtaque.ToString());

        Destroy(popUp, 1f);
    }
}
