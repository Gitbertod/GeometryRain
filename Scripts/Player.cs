using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Transform center;
	public float time = 0;
	float frecuencia = 3;
	float amplitud = 1;


    private void Start()
    {
        
    }
    void Update()
	{
		time += Time.deltaTime;
		float x = 0;
		float y = amplitud * Mathf.Sin(time * frecuencia);
		

		float lerpY = Mathf.Lerp(x, y, time);
		Vector3 Onda = new Vector3(x, lerpY, 0);
		transform.position = Onda + center.position;
	
	}

}
