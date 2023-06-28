using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImaExpController : MonoBehaviour
{
    [SerializeField] Transform posPlayer;
    [SerializeField] float velocidadeMovimento = 3;

    bool emContatoComPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        var player = FindAnyObjectByType<PlayerController>();

        if(player)
            posPlayer = player.transform ?? transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(emContatoComPlayer)
            SeguirPlayer();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
           other.TryGetComponent<PlayerController>(out var player);

           emContatoComPlayer = player is not null;
    }

    private void OnTriggerExit2D(Collider2D other) {

        emContatoComPlayer = false;
        
    }

    void SeguirPlayer()
    {
        if(!posPlayer) return;

        Vector3 minhaposicao = transform.position;

        Vector3 targetPosition = posPlayer.position;

        Vector3 newPosition = Vector3.MoveTowards(minhaposicao, targetPosition, velocidadeMovimento * Time.deltaTime);

        transform.position = newPosition; 
    }
}
