using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Transform spawnPos;
    [SerializeField] List<GameObject> Inimigos;

    [SerializeField] GameObject player;
    float delay = 5;
    float timer = 0;

    int quantidadeInimigosPorSpawn = 1;

    int levelPlayerSpawn = 0;

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

        for(int i = 0; i < quantidadeInimigosPorSpawn;i++)
        {
            float chance = Random.Range(0, 1f);
            var inimigo = chance >= chanceSpawnEsqueleto ? Inimigos[1] : Inimigos[0];

            bool inimigoEsqueleto = chance >= chanceSpawnEsqueleto;
            
            inimigo = AdicionarAtributosAoInimigo(inimigo, inimigoEsqueleto);

            Instantiate(inimigo, spawnPos.position, Quaternion.identity);
        }

        //Aumenta a quantidade de inimigos spawnados a cada 5 leveis que o player sobe
        var levelPlayer = player.GetComponent<PlayerController>().ObterLevel();
        if(levelPlayer % 5 == 0 && levelPlayerSpawn != levelPlayer)
        {
            levelPlayerSpawn = levelPlayer;
            quantidadeInimigosPorSpawn++;
        }
            

        timer = delay;
    }

    GameObject AdicionarAtributosAoInimigo(GameObject inimigo, bool inimigoEsqueleto)
    {
        if(!player) return inimigo;

        var levelPlayer = player.GetComponent<PlayerController>().ObterLevel();

        if(inimigoEsqueleto)
        {
            inimigo.GetComponent<InimigoController>().DefinirAtaque(2 + levelPlayer);
            inimigo.GetComponent<InimigoController>().DefinirVelocidadeAtaque(1 + .2f * levelPlayer);
            inimigo.GetComponent<InimigoController>().DefinirVida(2 + 2 * levelPlayer);
            inimigo.GetComponent<InimigoController>().DefinirVelocidadeMovimento(1.2f);
        }
        else
        {
            inimigo.GetComponent<InimigoController>().DefinirAtaque(1 + levelPlayer);
            inimigo.GetComponent<InimigoController>().DefinirVelocidadeAtaque(1 + .1f * levelPlayer);
            inimigo.GetComponent<InimigoController>().DefinirVida(2 + 1 * levelPlayer);
            inimigo.GetComponent<InimigoController>().DefinirVelocidadeMovimento(1.8f);
        }
        
        return inimigo;
    }

}
