using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WolfVR : MonoBehaviour {

    public bool fruitsAreSpawning;
    public int speed;
    GameObject scriptHolder;
    Text textOverlay;

    void Start()
    {
        speed = 5;
        scriptHolder = GameObject.FindGameObjectWithTag("ScriptHolder");
    }

    void Update()
    {
        if (!fruitsAreSpawning)
        {
            FindNearestFruit();
            fruitsAreSpawning = true;
        }
    }

    void FindNearestFruit()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 50, 1 << LayerMask.NameToLayer("Fruits"));
        Transform[] fruitPos = new Transform[hitColliders.Length];
        int i = 0;
        while (i < hitColliders.Length)
        {
            fruitPos[i] = hitColliders[i].transform;
            i++;
        }
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in fruitPos)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        StartCoroutine("MoveToFruit", bestTarget);
    }

    IEnumerator MoveToFruit(Transform target)
    {
        target.GetComponent<FruitBehavVR>().edible = true;
        Vector3 targetPos = new Vector3(target.position.x, 1, target.position.z);
        Vector3 currentPos = transform.position;
        float elapsedTime = 0;
        float journeyLength = Vector3.Distance(currentPos, targetPos);
        gameObject.GetComponent<Animator>().SetBool("isWalking", true);

        while (transform.position != targetPos && target != null)
        {
            float fracJourney = elapsedTime / journeyLength;
            //rotation
            var lookPos = target.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.02f);
            //position
            transform.position = Vector3.Lerp(currentPos, targetPos, fracJourney * speed);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (target.tag == "Melon" || target.tag == "Pineapple")
        {
            gameObject.GetComponent<Animator>().SetTrigger("isMunching");
        }
        else
        {
            gameObject.GetComponent<Animator>().SetTrigger("isLowMunching");
        }
        yield return new WaitForSeconds(1f);
        target.GetComponent<FruitBehavVR>().WolfEating();
        yield return new WaitForSeconds(1.1f);

        if (scriptHolder.GetComponent<PointSystemVR>().totalPoints[0] < 100)
        {
            FindNearestFruit();
        }
        else
        {
            StartCoroutine("MoveToHole");
        }
    }
    IEnumerator MoveToHole()
    {
        var currentPos = transform.position;
        Vector3 targetPos = new Vector3(-3.66f, 2.02f, -24.5f);
        float elapsedTime = 0;
        float journeyLength = Vector3.Distance(currentPos, targetPos);
        while (transform.position != targetPos)
        {
            float fracJourney = elapsedTime / journeyLength;
            transform.position = Vector3.Lerp(currentPos, targetPos, fracJourney * speed);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("check!");
        scriptHolder.GetComponent<Master>().PlayAudioClip(1);
    }
}
