using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundboard : MonoBehaviour
{
    AudioSource audioSource;

    //Efeitos Sonoros
    [SerializeField] AudioClip PlayerMorte;
    [SerializeField] AudioClip PegarMoeda;
    [SerializeField] AudioClip InimigoMorte;
    [SerializeField] AudioClip PlayerDano;

    // instancia singleton
    public static Soundboard instance;

    void Awake() 
    {

        int quantidade = FindObjectsOfType<Soundboard>().Length;

        if(quantidade > 1) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);  
    }

    void Start()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    void Update() 
    {
        audioSource.transform.position = Camera.main.transform.position;
    }


    void TocarSom(AudioClip som)
    {
        audioSource.clip = som;
        audioSource.Play();
    }

    public void TocarSomPlayerMorte() => TocarSom(PlayerMorte);
    public void TocarSomPegarMoeda() => TocarSom(PegarMoeda);
    public void TocarSomInimigoMorte() => TocarSom(InimigoMorte);
    public void TocarSomPlayerDano() => TocarSom(PlayerDano);
}
