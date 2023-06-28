using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpController : MonoBehaviour
{

    int quantidadeExp = 5;
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

        var playerLevel = player.GetComponent<PlayerController>().ObterLevel();
        player.GanharXp(quantidadeExp + Mathf.RoundToInt(playerLevel/2));
        Destroy(transform.parent.gameObject);
    }
}
