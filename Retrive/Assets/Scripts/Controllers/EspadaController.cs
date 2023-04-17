using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspadaController : MonoBehaviour
{
    public GameObject posAtaque;

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
}
