using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameSave Save;

    string caminhoSave;

    int VALOR_BASE_UPGRADE = 3;

    bool AtributosPlayerSet = false;
    bool ComponentesObtidos = false;

    public static GameManager instance;

    List<UpgradeUI> UpgradeComponents = new List<UpgradeUI>();

    TextMeshProUGUI TextoMoedasPlayer;

    [SerializeField] GameObject TransicaoCena;

    
    private void Awake() 
    {
        //Aplicação do Singleton Pattern
        int quantidade = FindObjectsOfType<GameManager>().Length;

        if(quantidade > 1) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);  

        ObterSaveJogo(); 
    }

    void Start()
    {
        instance = this;
    }

    void Update()
    {

        //Cena: Menu
        if(SceneManager.GetActiveScene().buildIndex == 0 && !ComponentesObtidos)
        {
            GameObject.FindWithTag("TelaUpgrade").transform.GetChild(0).gameObject.SetActive(true);
            ObterComponentesUI();
            GameObject.FindWithTag("TelaUpgrade").transform.GetChild(0).gameObject.SetActive(false);
            CarregarComponentesUI();
            ComponentesObtidos = true;
        }

        //Cena: Menu
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            AtributosPlayerSet = false;
        }

        //Cena: Jogo
        if(SceneManager.GetActiveScene().buildIndex == 1 && !AtributosPlayerSet)
        {
            ComponentesObtidos = false;
            AtribuirUpgradesAoPlayer();
            AtributosPlayerSet = true;
        }
                    
    }

    public void VoltarParaMenu()
    {
        Camera mainCamera = Camera.main;
        Vector3 centroCamera = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, mainCamera.nearClipPlane));
        Instantiate(TransicaoCena, centroCamera, transform.rotation);
        Invoke("CarregarCenaMenu", 1f);
    }

    void CarregarCenaMenu() => SceneManager.LoadScene(0);

    void AtribuirUpgradesAoPlayer()
    {
        var player = FindAnyObjectByType<PlayerController>();

        if(!player) return;

        foreach(var upgrade in Save.Upgrades)
        {
            switch(upgrade.tipo)
            {
                case TipoAtributo.Ataque:
                    player.IncrementarAtaque(1 * upgrade.level);
                break;

                case TipoAtributo.VelocidadeAtaque:
                    player.IncrementarVelocidadeAtaque(.1f * upgrade.level);
                break;

                case TipoAtributo.VelocidadeMovimento:
                    player.IncrementarVelocidadeMovimento(.5f * upgrade.level);
                break;

                case TipoAtributo.VidaMax:
                    player.IncrementarVida(10 * upgrade.level);
                break;
            }
        }
    }

    void ObterSaveJogo()
    {
        caminhoSave = $"{Application.dataPath}/save.txt";

        // Se o save não existe, cria um
        if(!File.Exists(caminhoSave))
        {
            GameSave saveTemplate = new();

            saveTemplate.Upgrades.Add(new Upgrade{tipo = TipoAtributo.Ataque});
            saveTemplate.Upgrades.Add(new Upgrade{tipo = TipoAtributo.VelocidadeAtaque});
            saveTemplate.Upgrades.Add(new Upgrade{tipo = TipoAtributo.VelocidadeMovimento});
            saveTemplate.Upgrades.Add(new Upgrade{tipo = TipoAtributo.VidaMax});

            Save = saveTemplate;

            SalvarJogo();
        }
        else
        {
            string saveJson = File.ReadAllText(caminhoSave);
            Save = JsonUtility.FromJson<GameSave>(saveJson);
        }
    }

    void ObterComponentesUI()
    {
        TextoMoedasPlayer = GameObject.FindWithTag("MoedasPlayer").GetComponent<TextMeshProUGUI>();

        UpgradeComponents = new List<UpgradeUI>();

        UpgradeComponents.Add(
            new UpgradeUI
            {
                Tipo = TipoAtributo.Ataque,
                TextoLevelUpgrade = GameObject.FindWithTag("AtaqueLVL").GetComponent<TextMeshProUGUI>(),
                TextoValorUpgrade = GameObject.FindWithTag("AtaqueCusto").GetComponent<TextMeshProUGUI>(),
                BotaoUpgrade = GameObject.FindWithTag("AtaqueBotao").GetComponent<Button>()
            }
        );

        UpgradeComponents.Add(
            new UpgradeUI
            {
                Tipo = TipoAtributo.VelocidadeAtaque,
                TextoLevelUpgrade = GameObject.FindWithTag("VelocidadeLVL").GetComponent<TextMeshProUGUI>(),
                TextoValorUpgrade = GameObject.FindWithTag("VelocidadeCusto").GetComponent<TextMeshProUGUI>(),
                BotaoUpgrade = GameObject.FindWithTag("VelocidadeBotao").GetComponent<Button>()
            }
        );

        UpgradeComponents.Add(
            new UpgradeUI
            {
                Tipo = TipoAtributo.VelocidadeMovimento,
                TextoLevelUpgrade = GameObject.FindWithTag("MovimentoLVL").GetComponent<TextMeshProUGUI>(),
                TextoValorUpgrade = GameObject.FindWithTag("MovimentoCusto").GetComponent<TextMeshProUGUI>(),
                BotaoUpgrade = GameObject.FindWithTag("MovimentoBotao").GetComponent<Button>()
            }
        );

        UpgradeComponents.Add(
            new UpgradeUI
            {
                Tipo = TipoAtributo.VidaMax,
                TextoLevelUpgrade = GameObject.FindGameObjectWithTag("VidaLVL").GetComponent<TextMeshProUGUI>(),
                TextoValorUpgrade = GameObject.FindGameObjectWithTag("VidaCusto").GetComponent<TextMeshProUGUI>(),
                BotaoUpgrade = GameObject.FindGameObjectWithTag("VidaBotao").GetComponent<Button>()
            }
        );
    }

    void CarregarComponentesUI()
    {
        TextoMoedasPlayer.SetText(SetTextoMoedas(Save.QuantidadeMoedas));


        foreach(var componente in UpgradeComponents)
        {
            var upgrade = Save.Upgrades.Where(u => u.tipo == componente.Tipo).FirstOrDefault();

            componente.TextoLevelUpgrade.SetText(SetTextoLevel(upgrade.level));

            SetValorUpgrade(upgrade, componente);
        }
    }

    public void SalvarMoedasPlayer(int moedas)
    {
        ObterSaveJogo();

        Save.QuantidadeMoedas += moedas;

        string json = JsonUtility.ToJson(Save);
            File.WriteAllText(caminhoSave, json);
    }

    public void EvoluirUpgrade(int tipoAtributo)
    {
        ObterSaveJogo();

        TipoAtributo tipo = Enum.Parse<TipoAtributo>(tipoAtributo.ToString());

        var upgrade = Save.Upgrades.Where(u => u.tipo == tipo).First();

        Save.QuantidadeMoedas -= VALOR_BASE_UPGRADE * upgrade.level;
        Save.Upgrades.Where(u => u.tipo == tipo).First().level++;
             
        SalvarJogo();
        ObterComponentesUI();
        CarregarComponentesUI();
    }

    string SetTextoMoedas(int valor) => $"X {valor}";

    string SetTextoLevel(int level)
    {
        if(level >= 10) return $"LVL MAX";

        return $"LVL {level}";
    }

    void SetValorUpgrade(Upgrade upgrade, UpgradeUI componente)
    {
        var precoUpgrade = VALOR_BASE_UPGRADE * upgrade.level;
            componente.TextoValorUpgrade.SetText(SetTextoMoedas(precoUpgrade));

        if(upgrade.level >= 10 || precoUpgrade > Save.QuantidadeMoedas)
            componente.BotaoUpgrade.interactable = false;
    }

    void SalvarJogo()
    {
        string json = JsonUtility.ToJson(Save);
        File.WriteAllText(caminhoSave, json);
    }
}
