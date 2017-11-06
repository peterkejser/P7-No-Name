using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VREating : MonoBehaviour{

    GameObject scriptHolder;
    // Use this for initialization
    void Start()
    {
        scriptHolder = GameObject.FindGameObjectWithTag("ScriptHolder");
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
        Debug.Log("check check");
        scriptHolder.GetComponent<VRTK_MoveInPlace>().speedScale = 0;
        yield return new WaitForSeconds(1f);
        scriptHolder.GetComponent<VRTK_MoveInPlace>().speedScale = 1;
    }
}
