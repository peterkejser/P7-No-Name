using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBehaviour : MonoBehaviour {

    float startTime;
    public Animation eatingAnimation;
    public bool fruitsAreSpawning;

    void Start()
    {
        startTime = Time.time;
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
        target.GetComponent<FruitBehaviour>().edible = true;
        var currentPos = transform.position;
        var targetPos = new Vector3(target.position.x, 3, target.position.z);
        //float time = 10;
        float elapsedTime = 0;
        float journeyLength = Vector3.Distance(currentPos, targetPos);
        while (transform.position != targetPos && target != null)
        {
            float fracJourney = elapsedTime / journeyLength;
            transform.position = Vector3.Lerp(currentPos, targetPos, fracJourney * 4);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        eatingAnimation.Play("New Animation");
        yield return new WaitForSeconds(2f);
        FindNearestFruit();
    }
}
