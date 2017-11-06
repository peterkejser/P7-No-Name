using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VRTK;

public class StartGameVR : MonoBehaviour {

    Animation tranAni;
    GameObject jackal;
    int userScore;
    GameObject scriptHolder;
    Text textOverlay;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    void Start()
    {
        tranAni = GameObject.FindGameObjectWithTag("Text").GetComponent<Animation>();
        jackal = GameObject.FindGameObjectWithTag("Player");
        scriptHolder = GameObject.FindGameObjectWithTag("ScriptHolder");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Snout")
        {
            scriptHolder.GetComponent<VRTK_MoveInPlace>().speedScale = 0;
            Debug.Log("check");
            tranAni.Play();
            StartCoroutine("LoadYourAsyncScene", tranAni);
        }
    }

    IEnumerator LoadYourAsyncScene(Animation tranAni)
    {
        yield return new WaitForSeconds(9);
        // The Application loads the Scene in the background at the same time as the current Scene.
        //This is particularly good for creating loading screens. You could also load the scene by build //number.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(3);

        //Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        DestroyObject(gameObject);
    }
}
