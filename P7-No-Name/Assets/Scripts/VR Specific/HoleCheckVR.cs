using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HoleCheckVR : MonoBehaviour {

    int userScore;
    public bool enterFromOutside;
    GameObject scriptHolder;

    void Update()
    {
    }
    // Use this for initialization
    void Start()
    {
        scriptHolder = GameObject.FindGameObjectWithTag("ScriptHolder");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Snout" && !enterFromOutside && GameObject.FindGameObjectWithTag("Wolf").GetComponent<WolfVR>().gameIsEnding==false)
        {
            if (scriptHolder.GetComponent<Master>().audioSource.isPlaying == false)
            {
                scriptHolder.GetComponent<PointSystemVR>().CheckHungerLevel();
                GameObject.FindGameObjectWithTag("Exit").GetComponent<ExitVR>().exiting = true;
            }
        }
        else if (other.tag == "Snout" && enterFromOutside)
        {
            enterFromOutside = false;
        }
    }
}
