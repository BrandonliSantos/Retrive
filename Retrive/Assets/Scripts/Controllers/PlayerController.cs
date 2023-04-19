using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : EntidadeBase
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Slider barraVida;

    [SerializeField] List<GameObject> posAtaques;

    [SerializeField] GameObject espada;

    [SerializeField] SpriteRenderer sprite;

    [SerializeField] float debug;

    GameObject ultimoPontoAtaque;
    bool spriteInvertidaUltimoAtaque;

    float timer;

    /*
        LISTA DE ATAQUES
        0 - SUPERIOR DIREITA
        1 - SUPERIOR ESQUERDA
        2 - INFERIOR DIRETA
        3 - INFERIOR ESQUERDA
    */


    // Start is called before the first frame update
    void Start()
    {
        barraVida.maxValue = vidaMax;
        barraVida.value = vidaMax;
        ultimoPontoAtaque = posAtaques[0];
        spriteInvertidaUltimoAtaque = false;
        timer = velocidadeAtaque;
    }

    // Update is called once per frame
    void Update()
    {
        Atacar();
        Mover();

        debug = Input.GetAxis(Controle.HoldAttack);
    }

    protected override void Atacar()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            if(Input.GetAxis(Controle.HoldAttack) == 0)
            {
                bool andandoParaCima = rb.velocity.y >= 0;
                bool andandoParaDireita = rb.velocity.x >= 0;

                GameObject pontoAtaque =  andandoParaCima && andandoParaDireita ? posAtaques[0]
                                        : andandoParaCima && !andandoParaDireita ? posAtaques[1]
                                        : !andandoParaCima && andandoParaDireita ? posAtaques[2]
                                        : !andandoParaCima && !andandoParaDireita ? posAtaques[3]
                                        : null;

                ultimoPontoAtaque = pontoAtaque ?? posAtaques[0];

                InstanciarAtaque(ultimoPontoAtaque, !andandoParaDireita);
            }
            else
            {
                InstanciarAtaque(ultimoPontoAtaque, spriteInvertidaUltimoAtaque);
            }

            timer = velocidadeAtaque;  
        }
    }

    protected override void Morrer()
    {
        throw new System.NotImplementedException();
    }

    protected override void Mover()
    {
        float y = Input.GetAxis(Controle.UpDown) * velocidadeMovimento;
        float x = Input.GetAxis(Controle.LeftRight) * velocidadeMovimento;

        rb.velocity = new Vector2(x, y);

        if(Input.GetAxis(Controle.HoldAttack) < 0)
            AlterarLadoSprite(rb.velocity.x);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        other.TryGetComponent<Inimigo>(out var inimigo);

        if(inimigo is null) return;
        
         PerdeVida(5);
    }

    void AlterarLadoSprite(float velocidadeX) => sprite.flipX = velocidadeX < 0;

    void InstanciarAtaque(GameObject pontoAtaque, bool inverterSprite)
    {
        var ataque = Instantiate(espada, pontoAtaque.transform.position, Quaternion.identity);

        //Comportamento da sprite
        var spriteAtaque = ataque.GetComponentInChildren<SpriteRenderer>();
        spriteAtaque.flipX = inverterSprite;

        spriteInvertidaUltimoAtaque = inverterSprite;

        spriteAtaque.transform.position = pontoAtaque.transform.position;

        //Passando parâmetro para controller
        var controller = ataque.GetComponent<EspadaController>();
        controller.posAtaque = pontoAtaque;
        controller.danoAtaque = this.ataque;
    }

    public override void PerdeVida(int quantidade)
    {
        vida -= quantidade;
        barraVida.value = vida;
    }
}
