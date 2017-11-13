using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitVR : MonoBehaviour {
    int userScore;
    GameObject scriptHolder;
    GameObject jackal;
    Text textOverlay;
    string textDes;
    bool exiting;

    // Use this for initialization
    void Start()
    {
        exiting = true;
        scriptHolder = GameObject.FindGameObjectWithTag("ScriptHolder");
        jackal = GameObject.FindGameObjectWithTag("Player");
        textOverlay = GameObject.FindGameObjectWithTag("TextOverlay").GetComponent<Text>();
    }

    private void OnTriggerExit(Collider other)
    {
        userScore = scriptHolder.GetComponent<PointSystemVR>().totalPoints[1];
        if (exiting == false)
        {
            exiting = true;
        }
        if (userScore >= 80 && exiting == true)
        {
            scriptHolder.GetComponent<Master>().PlayAudioClip(2);
            exiting = false;
        }
        if (userScore < 40 && exiting == true)
        {
            scriptHolder.GetComponent<Master>().PlayAudioClip(3);
            exiting = false;
        }
        if (userScore > 39 && userScore < 60 && exiting == true)
        {
            scriptHolder.GetComponent<Master>().PlayAudioClip(3);
            exiting = false;
        }
        if (userScore > 59 && userScore < 80 && exiting == true)
        {
            scriptHolder.GetComponent<Master>().PlayAudioClip(3);
            exiting = false;
        }
    }
}
