using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VRTK;
public class ExitVR : MonoBehaviour {
    int userScore;
    GameObject scriptHolder;
    GameObject jackal;
    Text textOverlay;
    string textDes;
    public bool exiting;
    public GameObject HoleCheckCollider;

    // Use this for initialization
    void Start()
    {
        exiting = true;
        scriptHolder = GameObject.FindGameObjectWithTag("ScriptHolder");
        jackal = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Snout")
        {
            userScore = scriptHolder.GetComponent<PointSystemVR>().totalPoints[1];
            if (userScore >= 101)
            {
                scriptHolder.GetComponent<VRTK_MoveInPlace>().speedScale = 0;
                GameObject.FindGameObjectWithTag("Wolf").GetComponent<WolfVR>().gameIsEnding = true;
                gameObject.GetComponent<BoxCollider>().size += new Vector3(0,0,2);
                if (scriptHolder.GetComponent<Master>().audioSource.isPlaying == false)
                {
                    scriptHolder.GetComponent<Master>().PlayAudioClip(22);
                }
            }
            else
            {
                HoleCheckCollider.GetComponent<HoleCheckVR>().enterFromOutside = true;
                userScore = scriptHolder.GetComponent<PointSystemVR>().totalPoints[1];

                if (userScore >= 80 && userScore <= 100 && exiting == true)
                {
                    scriptHolder.GetComponent<Master>().PlayAudioClip(21);
                    GameObject.FindGameObjectWithTag("Wolf").GetComponent<WolfVR>().gameIsEnding = true;
                    exiting = false;
                }
                else if (userScore < 40 && exiting == true && scriptHolder.GetComponent<Master>().wolfNowFatAudio==false)
                {
                    scriptHolder.GetComponent<Master>().PlayAudioClip(24);
                    exiting = false;
                }
                else if (userScore > 39 && userScore < 60 && exiting == true && scriptHolder.GetComponent<Master>().wolfNowFatAudio == false)
                {
                    scriptHolder.GetComponent<Master>().PlayAudioClip(23);
                    exiting = false;
                }
                else if (userScore > 59 && userScore < 80 && exiting == true && scriptHolder.GetComponent<Master>().wolfNowFatAudio == false)
                {
                    scriptHolder.GetComponent<Master>().PlayAudioClip(23);
                    exiting = false;
                }
            }
        }

    }
}
