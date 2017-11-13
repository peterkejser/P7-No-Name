using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointSystemVR : MonoBehaviour {

    public int[] totalPoints = new int[2];
    public int wolfHungerCap;
    string hungerDescription;
    bool tooBig;
    GameObject wolf;
    GameObject hlDescription;
    GameObject scriptHolder;

    // Use this for initialization
    void Start()
    {
        totalPoints[1] = 0;
        totalPoints[0] = 0;
        tooBig = false;
        scriptHolder = GameObject.FindGameObjectWithTag("ScriptHolder");
        wolf = GameObject.FindGameObjectWithTag("Wolf");
        hlDescription = GameObject.FindGameObjectWithTag("Text");
    }

    // Update is called once per frame
    void Update()
    {
        if (totalPoints[1] > 100)
        {
            scriptHolder.GetComponent<Master>().PlayAudioClip(4);
            if(!scriptHolder.GetComponent<AudioSource>().isPlaying)
            {
                SteamVR_LoadLevel.Begin("StartSceneVR");
            }
        }
    }

    public void CheckHungerLevel()
    {
        int value = totalPoints[1];
        if (value < 40)
        { hungerDescription = "starving"; }
        else if (value > 39 && value < 60)
        { hungerDescription = "hungry"; }
        else if (value > 59 && value < 80)
        { hungerDescription = "a little hungry"; }
        else if (value > 79 && value < 101)
        { hungerDescription = "feeling good"; }
        else if (value > 100)
        { }
        StartCoroutine("TempDispText", hungerDescription);
    }

    IEnumerator TempDispText(string hungerDescription)
    {
        hlDescription.GetComponent<TextMesh>().text = "You are " + hungerDescription;
        yield return new WaitForSeconds(1f);
        hlDescription.GetComponent<TextMesh>().text = "";
    }

    public void WolfSize()
    {
        int value = totalPoints[0] + wolfHungerCap;
        Vector3 sizeIncrease = new Vector3(0.3f, 0.3f, 0.3f);
        Debug.Log("inside WolfSize()");
        if (value > 39 && value < 60)
        { wolf.transform.localScale = new Vector3(2.3f, 2.3f, 2.3f); wolf.GetComponent<WolfVR>().speed = 4; }
        if (value > 59 && value < 80)
        { wolf.transform.localScale = new Vector3(2.6f, 2.6f, 2.6f); wolf.GetComponent<WolfVR>().speed = 4; }
        if (value > 79 && value < 101)
        { wolf.transform.localScale = new Vector3(2.9f, 2.9f, 2.9f); wolf.GetComponent<WolfVR>().speed = 4; }
        if (value > 100)
        { wolf.transform.localScale = new Vector3(3.2f, 3.2f, 3.2f); wolf.GetComponent<WolfVR>().speed = 4; }
    }
}
