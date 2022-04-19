using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject cube;
    public GameObject cube2;
    float time = 0;
    float time2 = 0;
    
    void Start()
    {
        cube = (GameObject)Resources.Load("cube");
        cube2 = (GameObject)Resources.Load("cube2");
    }

    void Update()
    {
        time += Time.deltaTime;
        time2 += Time.deltaTime;
        
        if (time >= 1) 
        {
            time = 0;
            Spawn();
        }

        if (time2 >= 2) 
        {
            time2 = 0;
            SpawnCube2();
        } 
    }

    void Spawn() 
    {
        Instantiate(cube);
        cube.transform.position = new Vector3(Random.Range(-2f,2f),transform.position.y,0);
    }

    void SpawnCube2() 
    {
        Instantiate(cube2);
        cube2.transform.position = new Vector3(Random.Range(-2, 2), transform.position.y);
    
    }
}
