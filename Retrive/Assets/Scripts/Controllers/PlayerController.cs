using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : EntidadeBase
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Slider barraVida;
    [SerializeField] private TextMeshPro textoLevel;

    [SerializeField] private Slider barraXp;

    [SerializeField] List<GameObject> posAtaques;

    [SerializeField] GameObject espada;

    [SerializeField] SpriteRenderer sprite;

    private int mortes = 0;
    [SerializeField] TextMeshPro morteTexto;
    [SerializeField] private float limMinX;
    [SerializeField] private float limMaxX;
    [SerializeField] private float limMinY;
    [SerializeField] private float limMaxY;

    GameObject ultimoPontoAtaque;
    bool spriteInvertidaUltimoAtaque;

    float timer;

    [SerializeField] int XpParaProximoNivel = 100;
    [SerializeField] int XpAtual = 0;

    [SerializeField] int level = 1;

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
        barraXp.maxValue = XpParaProximoNivel;
        barraXp.value = XpAtual;

        ultimoPontoAtaque = posAtaques[0];
        spriteInvertidaUltimoAtaque = false;
        timer = velocidadeAtaque;

        
    }

    // Update is called once per frame
    void Update()
    {
        Atacar();
        Mover();

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

        float myX = Mathf.Clamp(transform.position.x, limMinX, limMaxX);
        float myY = Mathf.Clamp(transform.position.y, limMinY, limMaxY);

        transform.position = new Vector3(myX, myY, 0f);

        if(Input.GetAxis(Controle.HoldAttack) == 0)
            AlterarLadoSprite(rb.velocity.x);

            
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        other.TryGetComponent<InimigoController>(out var inimigo);

        if(inimigo is null) return;
       
         PerdeVida(inimigo.ObterAtaque());
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
        var shake = FindObjectOfType<CameraShakeController>();
        StartCoroutine(shake.CameraShake());
        
        
    
        if(vida <= 0)
            Destroy(gameObject);
    }

    public void AumentarNumeroMortes()
    {
        mortes++;
        var texto = $"Kills: {mortes}";
        morteTexto.GetComponent<TextMeshPro>().SetText(texto);
    }

    public void GanharXp(int quantidade)
    {
        XpAtual += quantidade;

        if(XpAtual >= XpParaProximoNivel)
            SubirLevel();

        barraXp.maxValue = XpParaProximoNivel;
        barraXp.value = XpAtual;
    }

    public void SubirLevel()
    {
        XpAtual = 0;
        level++;
        XpParaProximoNivel = XpParaProximoNivel + (20 * level);

        textoLevel.SetText($"LVL {level}");
    }
    
    public int ObterLevel() => level;
}
