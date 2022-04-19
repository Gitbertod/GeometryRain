using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube2 : MonoBehaviour
{
    float time = 0;
    float speed = 0;
    float lifeTime = 0;

    void Start()
    {
        speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime; 
        transform.position += Vector3.down * speed * Time.deltaTime;
        transform.Rotate(0, 0,1);

        if (lifeTime > 5) Destroy(this.gameObject);

        if (transform.position.y <= -2.5f) 
        {
            float x = Mathf.Lerp(0.5f , 0 , time += Time.deltaTime);
            float y = Mathf.Lerp(0.5f , 0 , time += Time.deltaTime);

            transform.localScale = new Vector3(x, y, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") Invoke("Destroyer",0.1f);
    }

    void Destroyer() 
    {
        Destroy(gameObject);
    }
}
