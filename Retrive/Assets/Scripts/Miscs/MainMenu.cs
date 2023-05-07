using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transicao;


    public void Scenechange()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(CenaAnimcao());
    }


    public IEnumerator CenaEspecifica(int SceneIndex)
    {
        StartCoroutine(CenaAnimcao());
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneIndex);
    }


    public void Scenequit()
    {
        Application.Quit();
        StartCoroutine(CenaAnimcao());
    }

    public IEnumerator CenaAnimcao()
    {
        transicao.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
    }

    

}