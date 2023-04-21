using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpController : MonoBehaviour
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

            player.GanharXp(5);
            Destroy(gameObject);
    }
}
