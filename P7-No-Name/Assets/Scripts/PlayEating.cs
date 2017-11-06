using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayEating : MonoBehaviour {
    public bool eating;
    CapsuleCollider snoutCollider;
	// Use this for initialization
	void Start () {
        eating = false;
        snoutCollider = gameObject.GetComponent<CapsuleCollider>();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine("TryingToEat");
        }
	}

    public void StartEating()
    {
        StartCoroutine("Eating");
    }

    IEnumerator Eating()
    {
        gameObject.GetComponent<Animation>().Play();
        Debug.Log("check check");
        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().m_WalkSpeed = 0;
        yield return new WaitForSeconds(1f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().m_WalkSpeed = 5;
    }

    IEnumerator TryingToEat()
    {
        eating = true;
        snoutCollider.height = 0.14f;
        yield return new WaitForSeconds(0.2f);
        snoutCollider.height = 0f;
        eating = false;
    }
}
