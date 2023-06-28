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
            if(TremeSim)
            {
                posInicial = transform.position;
                tempoDeEspera += Time.deltaTime;
                float forcaShake = Curva.Evaluate(tempoDeEspera / duracao);
                var vector = posInicial + Random.insideUnitSphere * forcaShake;
                vector.z = posInicial.z;
                transform.position = vector;
            }
            yield return null;
        }
        transform.position = posInicial;        
    }

    public void PodeTremer(bool podeTremer) => TremeSim = podeTremer;

}
