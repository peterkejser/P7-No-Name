using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawning : MonoBehaviour {
    string[] fruits = new string[] { "Orange", "Melon", "Banana", "Pineapple" };
    Vector3[] regionFrom = new Vector3[] { new Vector3(-20f, 1.5f, -20f), new Vector3(-20f, 1.5f, 0f), new Vector3(0f, 1.5f, 0f), new Vector3(0f, 1.5f, -20f), new Vector3(-20f, 1.5f, -20f) };
    Vector3[] regionTo = new Vector3[] { new Vector3(0f, 1.5f, 0f), new Vector3(0f, 1.5f, 20f), new Vector3(20f, 1.5f, 20f), new Vector3(20f, 1.5f, 0f), new Vector3(20f, 1.5f, 20f) };
    public GameObject wolfObject;
    // Use this for initialization
    void Start()
    {
        SpawnFruit();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnFruit()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                string tempFruit = fruits[Random.Range(0, 4)];
                GameObject fruitInstance = Instantiate(Resources.Load(tempFruit, typeof(GameObject))) as GameObject;
                fruitInstance.transform.position = new Vector3(Random.Range(regionFrom[i].x, regionTo[i].x), 1.5f, Random.Range(regionFrom[i].z, regionTo[i].z));
            }
        }
        for (int h = 0; h < 13; h++)
        {
            string tempFruit = fruits[Random.Range(0, 4)];
            GameObject fruitInstance = Instantiate(Resources.Load(tempFruit, typeof(GameObject))) as GameObject;
            fruitInstance.transform.position = new Vector3(Random.Range(regionFrom[4].x, regionTo[4].x), 1.5f, Random.Range(regionFrom[4].z, regionTo[4].z));
        }
        wolfObject.GetComponent<WolfVR>().fruitsAreSpawning = false;
    }

    public void SpawnSingleFruit(int regionNumber)
    {
        string tempFruit = fruits[Random.Range(0, 4)];
        GameObject fruitInstance = Instantiate(Resources.Load(tempFruit, typeof(GameObject))) as GameObject;
        fruitInstance.transform.position = new Vector3(Random.Range(regionFrom[regionNumber].x, regionTo[regionNumber].x), 1.5f, Random.Range(regionFrom[regionNumber].z, regionTo[regionNumber].z));
    }
}
