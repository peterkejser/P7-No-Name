using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("1"))
        {
            SteamVR_LoadLevel.Begin("MiniProject1");
        }
        if (Input.GetKeyDown("2"))
        {
            SteamVR_LoadLevel.Begin("MiniProject2");
        }
        if (Input.GetKeyDown("3"))
        {
            SteamVR_LoadLevel.Begin("MiniProject3");
        }

    }
}
