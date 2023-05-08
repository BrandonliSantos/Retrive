using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoedaController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        other.TryGetComponent<PlayerController>(out var player);

        if(player is null) return;

        player.GanharMoedas(10);
        Destroy(transform.parent.gameObject);
    }
}
