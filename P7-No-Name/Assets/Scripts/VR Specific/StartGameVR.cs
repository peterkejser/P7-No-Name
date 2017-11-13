using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VRTK;

public class StartGameVR : MonoBehaviour {

    GameObject jackal;
    int userScore;
    GameObject scriptHolder;
    Text textOverlay;

    void Start()
    {
        jackal = GameObject.FindGameObjectWithTag("Player");
        scriptHolder = GameObject.FindGameObjectWithTag("ScriptHolder");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Snout")
        {
            SteamVR_LoadLevel.Begin("GameSceneVR");
        }
    }
}
