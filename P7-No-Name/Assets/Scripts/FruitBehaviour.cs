using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FruitBehaviour : MonoBehaviour {
    public bool edible;
    GameObject snout;
    // Use this for initialization
    void Start () {
        edible = false;
        snout = GameObject.FindGameObjectWithTag("Snout");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void WolfEating()
    {
        BeingConsumed();
        AssignPoints(0, true);
    }
    void OnTriggerEnter(Collider other)
    {
        /*
        if (other.tag == "Wolf" && edible == true)
        {
            BeingConsumed();
            AssignPoints(0,true);
        }*/
        if (other.tag == "Snout" && snout.GetComponent<PlayEating>().eating==true)
        {
            GameObject.FindGameObjectWithTag("Snout").GetComponent<PlayEating>().StartEating();
            BeingConsumed();
            AssignPoints(1,false);
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
            hitColliders = Physics.OverlapBox(new Vector3(-12.5f, 1, -12.5f), boxSize, Quaternion.identity, fruitOnly);
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
            hitColliders = Physics.OverlapBox(new Vector3(-12.5f, 1, 12.5f), boxSize, Quaternion.identity, fruitOnly);
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
            hitColliders = Physics.OverlapBox(new Vector3(12.5f, 1, 12.5f), boxSize, Quaternion.identity, fruitOnly);
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
            hitColliders = Physics.OverlapBox(new Vector3(12.5f, 1, -12.5f), boxSize, Quaternion.identity, fruitOnly);
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
    void AssignPoints(int owner, bool wSizeCheck)
    {
        int h = owner;
        var pointSystem = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<PointSystem>();
        string value = gameObject.tag;
        int debugValue = pointSystem.totalPoints[0];
        switch (value)
        {
            case "Orange":
                pointSystem.totalPoints[h] += Random.Range(1, 5);
                break;
            case "Banana":
                pointSystem.totalPoints[h] += Random.Range(3, 8);
                break;
            case "Pineapple":
                pointSystem.totalPoints[h] += Random.Range(5, 10);
                break;
            case "Melon":
                pointSystem.totalPoints[h] += Random.Range(7, 11);
                break;
        }
        if(wSizeCheck)
        {
            Debug.Log("inside wSizeCheck");
            pointSystem.WolfSize();
        }
        Debug.Log("wolf just ate a(n) " + value + " worth " + (pointSystem.totalPoints[0]-debugValue) + " points. Totalt points = "+ pointSystem.totalPoints[0]);
    }
}
