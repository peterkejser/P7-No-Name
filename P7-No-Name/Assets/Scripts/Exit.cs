using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Exit : MonoBehaviour {
    int userScore;
    GameObject scriptHolder;
    GameObject jackal;
    Text textOverlay;
    string textDes;

    // Use this for initialization
    void Start () {
        scriptHolder = GameObject.FindGameObjectWithTag("ScriptHolder");
        jackal = GameObject.FindGameObjectWithTag("Player");
        textOverlay = GameObject.FindGameObjectWithTag("TextOverlay").GetComponent<Text>();
    }

    private void OnTriggerExit(Collider other)
    {
        userScore = scriptHolder.GetComponent<PointSystem>().totalPoints[1];
        if (userScore >= 80)
        {
            textDes = "You Won!";
        }
        if (userScore < 40)
        {
            textDes = "starving!";
        }
        if (userScore > 39 && userScore < 60)
        {
            textDes = "still hungry!";
        }
        if (userScore > 59 && userScore < 80)
        {
            textDes = "still a little hungry!";
        }
        StartCoroutine("TempDispTextDes", textDes);
    }

    IEnumerator TempDispTextDes(string hungerDescription)
    {
        textOverlay.text = "You are " + hungerDescription;
        yield return new WaitForSeconds(2f);
        textOverlay.text = "";
    }
}
