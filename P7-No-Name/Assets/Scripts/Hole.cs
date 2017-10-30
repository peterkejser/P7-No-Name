using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hole : MonoBehaviour {
    int userScore;
    GameObject scriptHolder;
    GameObject jackal;
    Text textOverlay;
    
	// Use this for initialization
	void Start () {
        scriptHolder = GameObject.FindGameObjectWithTag("ScriptHolder");
        jackal = GameObject.FindGameObjectWithTag("Player");
        textOverlay = GameObject.FindGameObjectWithTag("TextOverlay").GetComponent<Text>();
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Snout")
        {
            GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<PointSystem>().CheckHungerLevel();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Snout")
        {
            userScore = scriptHolder.GetComponent<PointSystem>().totalPoints[1];
            if (userScore >= 80 && Input.GetMouseButtonDown(0))
            {
                textOverlay.text = "You Won!";
                jackal.transform.position -= new Vector3(0, 0, 3);
            }
        }
    }
}
