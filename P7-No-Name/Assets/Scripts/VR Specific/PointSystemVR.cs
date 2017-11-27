using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;
public class PointSystemVR : MonoBehaviour
{

    public int[] totalPoints = new int[2];
    public int wolfHungerCap;
    public int wolfPreviousScore;
    bool tooBig;
    bool eatenTooMuch;
    GameObject wolf;
    GameObject scriptHolder;

    // Use this for initialization
    void Start()
    {
        eatenTooMuch = false;
        totalPoints[1] = 0;
        totalPoints[0] = 0;
        tooBig = false;
        scriptHolder = gameObject;
        wolf = GameObject.FindGameObjectWithTag("Wolf");
    }

    // Update is called once per frame
    void Update()
    {
        /*if (eatenTooMuch)
        {
            if (!scriptHolder.GetComponent<AudioSource>().isPlaying && Input.GetKeyDown("n"))
            {
                scriptHolder.GetComponent<VRTK_MoveInPlace>().speedScale = 1;
                SteamVR_LoadLevel.Begin("StartSceneVR");
            }
        }*/
    }
    public void AssignPoints(int owner, string fruit)
    {
        int h = owner;
        wolfPreviousScore = totalPoints[0];
        string value = fruit;
        switch (value)
        {
            case "Orange":
                totalPoints[h] += Random.Range(1, 5);
                break;
            case "Banana":
                totalPoints[h] += Random.Range(3, 8);
                break;
            case "Pineapple":
                totalPoints[h] += Random.Range(5, 10);
                break;
            case "Melon":
                totalPoints[h] += Random.Range(7, 11);
                break;
        }
    }

    public void CheckHungerLevel()
    {
        int value = totalPoints[1];
        if (value < 40)
        { scriptHolder.GetComponent<Master>().PlayAudioClip(7); }
        else if (value > 39 && value < 60)
        { scriptHolder.GetComponent<Master>().PlayAudioClip(9); }
        else if (value > 59 && value < 80)
        { scriptHolder.GetComponent<Master>().PlayAudioClip(10); }
        else if (value > 79 && value < 101)
        { scriptHolder.GetComponent<Master>().PlayAudioClip(12); }
        else if (value > 100)
        { scriptHolder.GetComponent<Master>().PlayAudioClip(13); }
    }

    public void WolfSize()
    {
        int value = totalPoints[0];

        if (wolfPreviousScore < 40 + wolfHungerCap && value > 39 + wolfHungerCap)
        {
            Destroy(wolf.transform.GetChild(0).gameObject);
            GameObject wolfModel2 = Instantiate(Resources.Load("wolf_2", typeof(GameObject))) as GameObject;
            wolfModel2.transform.SetParent(wolf.transform, false);
            wolf.GetComponent<WolfVR>().FindWolfAnimator();
            wolf.GetComponent<WolfVR>().speed = 3;
        }
        if (wolfPreviousScore < 60 + wolfHungerCap && value > 59 + wolfHungerCap)
        {
            Destroy(wolf.transform.GetChild(0).gameObject);
            GameObject wolfModel3 = Instantiate(Resources.Load("wolf_3", typeof(GameObject))) as GameObject;
            wolfModel3.transform.SetParent(wolf.transform, false);
            wolf.GetComponent<WolfVR>().FindWolfAnimator();
            wolf.GetComponent<WolfVR>().speed = 3;
        }
        if (wolfPreviousScore < 80 + wolfHungerCap && value > 79 + wolfHungerCap)
        {
            Destroy(wolf.transform.GetChild(0).gameObject);
            GameObject wolfModel4 = Instantiate(Resources.Load("wolf_4", typeof(GameObject))) as GameObject;
            wolfModel4.transform.SetParent(wolf.transform, false);
            wolf.GetComponent<WolfVR>().FindWolfAnimator();
            wolf.GetComponent<WolfVR>().speed = 2;
        }
        if (wolfPreviousScore < 100 + wolfHungerCap && value > 99 + wolfHungerCap)
        {
            Destroy(wolf.transform.GetChild(0).gameObject);
            GameObject wolfModel5 = Instantiate(Resources.Load("wolf_5", typeof(GameObject))) as GameObject;
            wolfModel5.transform.SetParent(wolf.transform, false);
            wolf.GetComponent<WolfVR>().FindWolfAnimator();
            wolf.GetComponent<WolfVR>().speed = 2;
        }
    }
}
