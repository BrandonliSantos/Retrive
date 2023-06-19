using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundboard : MonoBehaviour
{
    //Musicas
    AudioSource audioSource;

    //Efeitos Sonoros

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
}
