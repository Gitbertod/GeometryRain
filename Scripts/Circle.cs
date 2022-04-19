using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Circle : MonoBehaviour
{
    public Transform center;
    [Range(0,1)]
    public float time = 0; 
    float frecuencia = 3;
    float amplitud = 1;
    float speed = 0.5f;
    
    public bool anim = true;

    void Start()
    {

        

    }

    
    void Update()
    {
            float speedScale = Time.deltaTime * 0.5f;

            float y = 0;
            float x = 1;

            float lerpX = Mathf.Lerp(x, y,time);
            float lerpY = Mathf.Lerp(x,y,time ) ;
            Vector3 scale = new Vector3(lerpX, lerpY, 0);
    }

    float LinearTween(float t, float b, float c, float d)
    {
        return c * t / d + b;
    }
    


}
