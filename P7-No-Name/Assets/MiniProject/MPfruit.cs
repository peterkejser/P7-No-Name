using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPfruit : MonoBehaviour {
    GameObject scriptHolder;
    public Transform player;
	// Use this for initialization
	void Start () {
        scriptHolder = GameObject.FindGameObjectWithTag("ScriptHolder");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Snout")
        {
            scriptHolder.GetComponent<HideFruit>().RespawnFruit();
            HideFruit();
            scriptHolder.GetComponent<Logging>().NewEntry(gameObject.tag,player.position.x,player.position.z);
        }
    }
    void HideFruit()
    {
        scriptHolder.GetComponent<HideFruit>().EatenFruit = gameObject;
        gameObject.transform.position -= new Vector3(0, 10, 0);

    }
}
