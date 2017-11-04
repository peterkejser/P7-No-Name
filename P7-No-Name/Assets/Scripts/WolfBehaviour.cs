using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WolfBehaviour : MonoBehaviour {

    public Animation eatingAnimation;
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
        target.GetComponent<FruitBehaviour>().edible = true;
        var currentPos = transform.position;
        var targetPos = new Vector3(target.position.x, 4, target.position.z);
        float elapsedTime = 0;
        float journeyLength = Vector3.Distance(currentPos, targetPos);
        while (transform.position != targetPos && target != null)
        {
            float fracJourney = elapsedTime / journeyLength;
            transform.position = Vector3.Lerp(currentPos, targetPos, fracJourney * speed);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        eatingAnimation.Play("New Animation");
        yield return new WaitForSeconds(2f);

        if (scriptHolder.GetComponent<PointSystem>().totalPoints[0] < 100)
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
        textOverlay = GameObject.FindGameObjectWithTag("TextOverlay").GetComponent<Text>();
        textOverlay.text = "you couldn't espace because the wolf blocked your path and you go shot by the farmer or something XD";
    }
}
