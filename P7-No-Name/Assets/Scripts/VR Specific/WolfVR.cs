using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class WolfVR : MonoBehaviour {

    public bool fruitsAreSpawning;
    public bool gameHasStarted;
    public bool gameIsEnding;
    public int speed;
    int wolfHungerCap;
    GameObject scriptHolder;
    Text textOverlay;
    Animator wolfAnimator;

    void Start()
    {
        gameHasStarted = false;
        speed = 3;
        scriptHolder = GameObject.FindGameObjectWithTag("ScriptHolder");
        scriptHolder.GetComponent<VRTK_MoveInPlace>().speedScale = 0;
        wolfHungerCap = scriptHolder.GetComponent<PointSystemVR>().wolfHungerCap;
        FindWolfAnimator();
    }

    public void FindWolfAnimator()
    {
        wolfAnimator = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!scriptHolder.GetComponent<AudioSource>().isPlaying)
        {
            gameHasStarted = true;
        }

        if (!fruitsAreSpawning && gameHasStarted)
        {
            scriptHolder.GetComponent<VRTK_MoveInPlace>().speedScale = 1;
            FindNearestFruit();
            fruitsAreSpawning = true;
            gameHasStarted = false;
            Debug.Log("newestCheck");
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
        Vector3 targetPos = new Vector3(target.position.x, 1, target.position.z);
        Vector3 currentPos = transform.position;
        //rotation
        var lookPos = target.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        //
        float elapsedTime = 0;
        float journeyLength = Vector3.Distance(currentPos, targetPos);
        FindWolfAnimator();
        wolfAnimator.SetBool("isWalking", true);

        while (target != null && transform.position != targetPos)
        {
            float fracJourney = elapsedTime / journeyLength;
            //rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.02f);
            //position
            transform.position = Vector3.Lerp(currentPos, targetPos, fracJourney * speed);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (target != null)
        {
            target.GetComponent<FruitBehavVR>().edible = true;
            if (target.tag == "Melon" || target.tag == "Pineapple")
            {
                wolfAnimator.SetTrigger("isMunching");
            }
            else
            {
                wolfAnimator.SetTrigger("isLowMunching");
            }
            var fruitBehavScript = target.GetComponent<FruitBehavVR>();
            yield return new WaitForSeconds(1f);
            fruitBehavScript.BeingConsumed(0);
            yield return new WaitForSeconds(1.2f);
            scriptHolder.GetComponent<PointSystemVR>().WolfSize();
            yield return new WaitForSeconds(0.15f);
        }
        if (scriptHolder.GetComponent<PointSystemVR>().totalPoints[0] < 100+wolfHungerCap)
        {
            FindNearestFruit();
        }
        else
        {
            scriptHolder.GetComponent<Master>().wolfNowFatAudio = true;
            scriptHolder.GetComponent<Master>().PlayAudioClip(14);
            StartCoroutine("MoveToHole");
        }
    }
    IEnumerator MoveToHole()
    {
        FindWolfAnimator();
        wolfAnimator.SetBool("isWalking", true);

        var currentPos = transform.position;
        Vector3 targetPos1 = new Vector3(-3.6097f, 1f, -23.624f);
        float elapsedTime = 0;
        float journeyLength = Vector3.Distance(currentPos, targetPos1);
        while (transform.position != targetPos1)
        {
            //rotation
            var lookPos = new Vector3(-3.6259f, 0.0f, -23.7f) - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.02f);
            //position
            float fracJourney = elapsedTime / journeyLength;
            transform.position = Vector3.Lerp(currentPos, targetPos1, fracJourney * speed);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if(gameIsEnding)
        {
            FindWolfAnimator();
            wolfAnimator.SetBool("isWalking", false);
            yield break;
        }
        var currentPos2 = transform.position;
        Vector3 targetPos2 = new Vector3(-3.52f, -0.16f, -25.975f);
        float elapsedTime2 = 0;
        float journeyLength2 = Vector3.Distance(currentPos2, targetPos2);
        wolfAnimator.SetBool("isLeaving", true);
        while (transform.position != targetPos2)
        {
            //rotation
            var lookPos2 = new Vector3(-3.52f, 0.0f, -28f) - transform.position;
            lookPos2.y = 0;
            var rotation2 = Quaternion.LookRotation(lookPos2);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation2, 0.02f);
            //position
            float fracJourney2 = elapsedTime2 / journeyLength2;
            transform.position = Vector3.Lerp(currentPos2, targetPos2, fracJourney2 * speed);
            elapsedTime2 += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("check!");
        scriptHolder.GetComponent<Master>().PlayAudioClip(20);
    }
}
