using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Transform spawnPos;
    [SerializeField] List<GameObject> Inimigos;

    float delay = 5;
    float timer = 0;

    float chanceSpawnEsqueleto = .7f; // 30%

    /*
        Lista Inimigos:
        0 - Slime
        1 - Esqueleto
    */

    // Start is called before the first frame update
    void Start()
    {
        timer = delay;
    }

    // Update is called once per frame
    void Update()
    {
        criarInimigosCasoForaDaTela();
    }

    void criarInimigosCasoForaDaTela()
    {
        if(sprite.isVisible) return;

        timer -= Time.deltaTime;

        if(timer > 0) return;

        float chance = Random.Range(0, 1f);
        var inimigo = chance >= chanceSpawnEsqueleto ? Inimigos[1] : Inimigos[0];
        Instantiate(inimigo, spawnPos.position, Quaternion.identity);

        timer = delay;
    }
}
