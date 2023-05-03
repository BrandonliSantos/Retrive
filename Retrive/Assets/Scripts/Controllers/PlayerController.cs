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

    [SerializeField] private GameObject TelaLevelUp;

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

    bool ultimoAtaqueRotacionado = false;

    float timer;

    [SerializeField] int XpParaProximoNivel = 100;
    [SerializeField] int XpAtual = 0;

    [SerializeField] int level = 1;
    [SerializeField] float Iframes;
    [SerializeField] float IframesFlashes;

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

                ultimoAtaqueRotacionado = pontoAtaque.Equals(posAtaques[2]) || pontoAtaque.Equals(posAtaques[3]);

                InstanciarAtaque(ultimoPontoAtaque, !andandoParaDireita, ultimoAtaqueRotacionado);
            }
            else
            {
                InstanciarAtaque(ultimoPontoAtaque, spriteInvertidaUltimoAtaque, ultimoAtaqueRotacionado);
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

        if(Input.GetAxis(Controle.HoldAttack) == 0)
            AlterarLadoSprite(rb.velocity.x);

        float myX = Mathf.Clamp(transform.position.x, limMinX, limMaxX);
        float myY = Mathf.Clamp(transform.position.y, limMinY, limMaxY);

        transform.position = new Vector3(myX, myY, 0f);
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent<InimigoController>(out var inimigo);

        if(inimigo is null) return;
       
         PerdeVida(inimigo.ObterAtaque());
    }

    void AlterarLadoSprite(float velocidadeX) => sprite.flipX = velocidadeX < 0;

    void InstanciarAtaque(GameObject pontoAtaque, bool inverterSprite, bool rotacionarAtaque)
    {
        var ataque = Instantiate(espada, pontoAtaque.transform.position, Quaternion.identity);

        //rotacionar
        if(rotacionarAtaque)
        {
            var z = inverterSprite ? 90 : -90;
            ataque.transform.rotation = Quaternion.Euler(0, 0, z);
        }

        //ajuste colisão caso sprite invertido
        if(inverterSprite)
        {
            var vectorOffset = ataque.GetComponent<BoxCollider2D>().offset;
            Vector2 novoVectorOffset = new Vector2(vectorOffset.x * -1, vectorOffset.y);
            ataque.GetComponent<BoxCollider2D>().offset = novoVectorOffset;
        }
            

        //Comportamento da sprite
        var spriteAtaque = ataque.GetComponentInChildren<SpriteRenderer>();
        spriteAtaque.flipX = inverterSprite;

        spriteInvertidaUltimoAtaque = inverterSprite;

        spriteAtaque.transform.position = pontoAtaque.transform.position;

        //dano de ataque + chance de critico
        var chanceCritico = Random.Range(0, 101);

        var danoAtaque = this.ataque + Random.Range(0, this.level + 1);

        var danoCritico = false;

        if(chanceCritico <= 10)
        {
            danoAtaque *= 2;
            danoCritico = true;
        }
        
        //Passando parâmetro para controller
        var controller = ataque.GetComponent<EspadaController>();
        controller.posAtaque = pontoAtaque;
        controller.danoAtaque = danoAtaque;
        controller.danoCritico = danoCritico;
    }

    public override void PerdeVida(int quantidade)
    {
        vida -= quantidade;
        barraVida.value = vida;
        var shake = FindObjectOfType<CameraShakeController>();
        StartCoroutine(shake.CameraShake());
        StartCoroutine(Invulnerabilidade());
        
        
    
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

        //Congela jogo e habilita tela de level up
        Time.timeScale = 0;
        TelaLevelUp.gameObject.SetActive(true);

        //Player recupera vida ao subir de level
        vida = vidaMax;
        barraVida.maxValue = vidaMax;
        barraVida.value = vidaMax;
    }
    
    public int ObterLevel() => level;

    //Métodos de Evolução
    public void IncrementarVida(int valor)
    {
        vidaMax += valor;
        vida = vidaMax;

        barraVida.maxValue = vidaMax;
        barraVida.value = vidaMax;
    }

    private IEnumerator Invulnerabilidade()
    {
        Physics2D.IgnoreLayerCollision(24, 25, true);
        
        for (int i = 0; i < IframesFlashes; i++)
        {
            sprite.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(Iframes / (IframesFlashes * 2));
            sprite.color = new Color(1,1,1,1);
            yield return new WaitForSeconds(Iframes / (IframesFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(24, 25, false);
    } 

    public void IncrementarVelocidadeAtaque(float valor) => velocidadeAtaque -= valor;
    public void IncrementarVelocidadeMovimento(float valor) => velocidadeMovimento += valor;
    public void IncrementarAtaque(int valor) => ataque += valor;

}
