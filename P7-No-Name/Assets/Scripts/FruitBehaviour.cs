using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FruitBehaviour : MonoBehaviour {
    public bool edible;
    GameObject snout;
    string[] fruits = new string[] { "Orange", "Melon", "Banana", "Pineapple"};
    // Use this for initialization
    void Start () {
        edible = false;
        snout = GameObject.FindGameObjectWithTag("Snout");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Wolf" && edible == true)
        {
            BeingConsumed();
            AssignPoints(0);
        }
        else if (collision.collider.tag == "Snout" && snout.GetComponent<Animation>().isPlaying)
        {
            GameObject.FindGameObjectWithTag("Snout").GetComponent<PlayEating>().startEating();
            BeingConsumed();
            AssignPoints(1);
        }
    }


    void BeingConsumed()
    {
        Collider[] hitColliders = null;
        Vector3 boxSize = new Vector3(12.5f, 2f, 12.5f);
        LayerMask fruitOnly = 1 << LayerMask.NameToLayer("Fruits");
        var fruitSpawnScript = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<FruitSpawning>();


        if (transform.position.x < 0 && transform.position.z < 0)
        {
            hitColliders = Physics.OverlapBox(new Vector3(-12.5f, 0, -12.5f), boxSize, Quaternion.identity, fruitOnly);
            if (hitColliders.Length < 4)
            {
                fruitSpawnScript.SpawnSingleFruit(0);
            }
            else
            {
                fruitSpawnScript.SpawnSingleFruit(4);
            }
        }
        else if (transform.position.x < 0 && transform.position.z > 0)
        {
            hitColliders = Physics.OverlapBox(new Vector3(-12.5f, 0, 12.5f), boxSize, Quaternion.identity, fruitOnly);
            if (hitColliders.Length < 4)
            {
                fruitSpawnScript.SpawnSingleFruit(1);
            }
            else
            {
                fruitSpawnScript.SpawnSingleFruit(4);
            }
        }
        else if (transform.position.x > 0 && transform.position.z > 0)
        {
            hitColliders = Physics.OverlapBox(new Vector3(12.5f, 0, 12.5f), boxSize, Quaternion.identity, fruitOnly);
            if (hitColliders.Length < 4)
            {
                fruitSpawnScript.SpawnSingleFruit(2);
            }
            else
            {
                fruitSpawnScript.SpawnSingleFruit(4);
            }
        }
        else if (transform.position.x > 0 && transform.position.z < 0)
        {
            hitColliders = Physics.OverlapBox(new Vector3(12.5f, 0, -12.5f), boxSize, Quaternion.identity, fruitOnly);
            if (hitColliders.Length < 4)
            {
                fruitSpawnScript.SpawnSingleFruit(3);
            }
            else
            {
                fruitSpawnScript.SpawnSingleFruit(4);
            }
        }
        Destroy(gameObject);
    }
    void AssignPoints(int owner)
    {
        int h = owner;
        var fruitSpawnScript = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<FruitSpawning>();
        string value = gameObject.tag;
        int debugValue = fruitSpawnScript.totalPoints[0];
        switch (value)
        {
            case "Orange":
                fruitSpawnScript.totalPoints[h] += Random.Range(1, 5);
                break;
            case "Banana":
                fruitSpawnScript.totalPoints[h] += Random.Range(3, 8);
                break;
            case "Pineapple":
                fruitSpawnScript.totalPoints[h] += Random.Range(5, 10);
                break;
            case "Melon":
                fruitSpawnScript.totalPoints[h] += Random.Range(7, 11);
                break;
        }
        Debug.Log("wolf just ate a(n) " + value + " worth " + (fruitSpawnScript.totalPoints[0]-debugValue) + " points");
    }
    void IncreaseSize()
    {

    }
}
