using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HoleCheckVR : MonoBehaviour {

    int userScore;
    GameObject scriptHolder;
    GameObject jackal;
    Text textOverlay;

    void Update()
    {
    }
    // Use this for initialization
    void Start()
    {
        scriptHolder = GameObject.FindGameObjectWithTag("ScriptHolder");
        jackal = GameObject.FindGameObjectWithTag("Player");
        textOverlay = GameObject.FindGameObjectWithTag("TextOverlay").GetComponent<Text>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Snout")
        {
            GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<PointSystemVR>().CheckHungerLevel();
        }
    }
}
