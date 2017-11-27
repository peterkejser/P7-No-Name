using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VRTK;

public class StartGameVR : MonoBehaviour {


    void Start()
    {
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Snout")
        {
            SteamVR_LoadLevel.Begin("GameSceneVR");
        }
    }
}
