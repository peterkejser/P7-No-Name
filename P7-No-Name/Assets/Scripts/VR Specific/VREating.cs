using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VREating : MonoBehaviour{

    public GameObject scriptHolder;
    public GameObject LTouch;
    public GameObject RTouch;
    public bool moveInPlace;
    // Use this for initialization
    void Start()
    {
        moveInPlace = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartEating()
    {
        StartCoroutine("Eating");
    }

    IEnumerator Eating()
    {
        gameObject.GetComponent<Animation>().Play();
        Debug.Log("check chec2");
        if(moveInPlace == true)
        {
            scriptHolder.GetComponent<VRTK_MoveInPlace>().maxSpeed = 0;
            yield return new WaitForSeconds(1f);
            scriptHolder.GetComponent<VRTK_MoveInPlace>().maxSpeed = 4;
        }
        if(moveInPlace == false)
        {
            LTouch.SetActive(false);
            RTouch.SetActive(false);
            yield return new WaitForSeconds(1f);
            LTouch.SetActive(true);
            RTouch.SetActive(true);
        }
    }
}
