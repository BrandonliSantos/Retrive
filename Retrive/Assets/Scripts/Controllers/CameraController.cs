using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;
    public Vector3 offset;

    [SerializeField] private float limMinX;
    [SerializeField] private float limMaxX;
    [SerializeField] private float limMinY;
    [SerializeField] private float limMaxY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SeguirPlayer();
    }

    void SeguirPlayer()
    {
        if(Target)
        {
            Vector3 posicaoPlayer = Target.position + offset;

            float myX = Mathf.Clamp(posicaoPlayer.x, limMinX, limMaxX);
            float myY = Mathf.Clamp(posicaoPlayer.y, limMinY, limMaxY);

            transform.position = new Vector3(myX, myY, transform.position.z);
        }
    }
}
