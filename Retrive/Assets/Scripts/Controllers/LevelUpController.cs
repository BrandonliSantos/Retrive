using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpController : MonoBehaviour
{
    [SerializeField] TipoAtributo Atributo;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EvoluirAtributo()
    {
        var player = FindAnyObjectByType<PlayerController>();

        if(player)
        {
            switch(Atributo)
            {
                case TipoAtributo.Ataque:
                    player.IncrementarAtaque(1);
                break;

                case TipoAtributo.VidaMax:
                    player.IncrementarVida(10);
                break;

                case TipoAtributo.VelocidadeAtaque:
                    player.IncrementarVelocidadeAtaque(.1f);
                break;

                case TipoAtributo.VelocidadeMovimento:
                    player.IncrementarVelocidadeMovimento(.5f);
                break;

                default:
                    break;
            }
        }

        Time.timeScale = 1;
    }

    public enum TipoAtributo
    {
        Ataque = 0,
        VidaMax = 1,
        VelocidadeAtaque = 2,
        VelocidadeMovimento = 3
    }
}
