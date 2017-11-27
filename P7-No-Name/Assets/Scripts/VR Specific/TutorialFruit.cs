using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFruit : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Snout")
        {
            GameObject.FindGameObjectWithTag("Snout").GetComponent<VREating>().StartEating();
            Destroy(gameObject);
        }
    }
}
