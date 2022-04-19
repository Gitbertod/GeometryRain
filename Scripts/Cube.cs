using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    float speed = 2;
    float lifeTime = 0;
    public float scale;
    float speedScale = 0.10f;
    public EnemySpawner enemySpawner;
    [Range(0,1)]
    public float time = 0;
    
    void Update()
    {
       
        if (transform.position.y <= -2.5) 
        {
            float scaleX = Mathf.Lerp(0.5f, 0, time+=Time.deltaTime);
            float scaleY = Mathf.Lerp(0.5f, 0, time +=Time.deltaTime);
            Vector3 scale = new Vector3(scaleX, scaleY, 0);

            transform.position = new Vector3(transform.position.x,-2.5f,0);
            transform.localScale = scale;
        } 
       
        lifeTime += Time.deltaTime;
        
        if (lifeTime >= 6) Destroy(this.gameObject);

        transform.position += Vector3.down * Time.deltaTime * speed;
        transform.Rotate(0,0,0.5f); 

    }
}
