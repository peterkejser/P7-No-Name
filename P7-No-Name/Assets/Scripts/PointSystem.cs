using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class PointSystem : MonoBehaviour {
    public int[] totalPoints = new int[2];
    public int wolfHungerCap;
    string hungerDescription;
    bool tooBig;
    GameObject wolf;
    GameObject hlDescription;
    Text textOverlay;
    int currentSize;

    // Use this for initialization
    void Start () {
        currentSize = 1;
        totalPoints[1] = 0;
        totalPoints[0] = 0;
        tooBig = false;
        wolf = GameObject.FindGameObjectWithTag("Wolf");
        hlDescription = GameObject.FindGameObjectWithTag("Text");
        textOverlay = GameObject.FindGameObjectWithTag("TextOverlay").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		if (totalPoints[1]>100)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().m_WalkSpeed = 2;
            textOverlay.text = "you couldn't escape because you got too big to fit through the hole and you go shot by the farmer or something XD";
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

    public void WolfSize(int previousValue)
    {
        int value = totalPoints[0]+wolfHungerCap;
        Vector3 sizeIncrease = new Vector3(0.3f, 0.3f, 0.3f);
        Debug.Log("inside WolfSize()");

        if (previousValue<40 && value>39)
        {
            Debug.Log("checked2");
          Destroy(wolf.transform.GetChild(0).gameObject);
            GameObject wolfModel2 = Instantiate(Resources.Load("wolf_2", typeof(GameObject))) as GameObject;
            wolfModel2.transform.SetParent(wolf.transform,false);
            wolf.GetComponent<WolfBehaviour>().findWolfAnimator();
            wolf.GetComponent<WolfBehaviour>().speed = 4;
        }
        if (previousValue<60 && value>59)
        {
            Destroy(wolf.transform.GetChild(0).gameObject);
            GameObject wolfModel3 = Instantiate(Resources.Load("wolf_3", typeof(GameObject))) as GameObject;
            wolfModel3.transform.SetParent(wolf.transform,false);
            wolf.GetComponent<WolfBehaviour>().findWolfAnimator();
            wolf.GetComponent<WolfBehaviour>().speed = 4;
        }
        if (previousValue<80 && value>79)
        {
            Destroy(wolf.transform.GetChild(0).gameObject);
            GameObject wolfModel4 = Instantiate(Resources.Load("wolf_4", typeof(GameObject))) as GameObject;
            wolfModel4.transform.SetParent(wolf.transform,false);
            wolf.GetComponent<WolfBehaviour>().findWolfAnimator();
            wolf.GetComponent<WolfBehaviour>().speed = 4;
        }
        if (previousValue<100 && value>99)
        {
            Destroy(wolf.transform.GetChild(0).gameObject);
            GameObject wolfModel5 = Instantiate(Resources.Load("wolf_5", typeof(GameObject))) as GameObject;
            wolfModel5.transform.SetParent(wolf.transform, false);
            wolf.GetComponent<WolfBehaviour>().findWolfAnimator();
            wolf.GetComponent<WolfBehaviour>().speed = 4;
        }
    }
}
