using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBehavVR : MonoBehaviour {
    public bool edible;
    GameObject snout;

    // Use this for initialization
    void Start()
    {
        edible = false;
        snout = GameObject.FindGameObjectWithTag("Snout");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Snout" && edible == false)
        {
            GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<Master>().FirstTimeFruit(gameObject.tag);
            GameObject.FindGameObjectWithTag("Snout").GetComponent<VREating>().StartEating();
            BeingConsumed(1);
        }
    }

    public void BeingConsumed(int owner)
    {
        Collider[] hitColliders = null;
        Vector3 boxSize = new Vector3(12.5f, 2f, 12.5f);
        LayerMask fruitOnly = 1 << LayerMask.NameToLayer("Fruits");
        var fruitSpawnScript = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<FruitSpawnVR>();

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
        var pointSystem = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<PointSystemVR>();
        string fruit = gameObject.tag;
        pointSystem.wolfPreviousScore = pointSystem.totalPoints[0];
        pointSystem.AssignPoints(owner, fruit);
        Destroy(gameObject);
    }
}
