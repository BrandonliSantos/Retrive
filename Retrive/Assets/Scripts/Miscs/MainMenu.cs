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


    public void CenaEspecifica(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
        StartCoroutine(CenaAnimcao());
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