using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Snout")
        {
            GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<PointSystem>().CheckHungerLevel();
        }
    }
}
