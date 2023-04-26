using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeController : MonoBehaviour
{
    [SerializeField] private AnimationCurve Curva;
    [SerializeField] private float duracao = 1f;
    [SerializeField] private bool TremeSim;
    
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    
    public IEnumerator CameraShake()
    {
        Vector3 posInicial = transform.position;
        float tempoDeEspera = 0f;

        while (tempoDeEspera < duracao)
        {
            posInicial = transform.position;
            tempoDeEspera += Time.deltaTime;
            float forcaShake = Curva.Evaluate(tempoDeEspera / duracao);
            transform.position = posInicial + Random.insideUnitSphere * forcaShake;
            yield return null;
        }
        transform.position = posInicial;        
    }

}
