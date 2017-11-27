using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideFruit : MonoBehaviour {
     public GameObject EatenFruit;
	// Use this for initialization
	void Start () {
        EatenFruit = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void RespawnFruit()
    {
        if (EatenFruit != null)
        {
            EatenFruit.transform.position += new Vector3(0, 10, 0);
        }
    }
}
