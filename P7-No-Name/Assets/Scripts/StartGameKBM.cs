using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameKBM : MonoBehaviour {
    Animation tranAni;
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    void Start()
    {
        tranAni = GameObject.FindGameObjectWithTag("Text").GetComponent<Animation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Snout")
        {
            Debug.Log("check");
            tranAni.Play();
            StartCoroutine("LoadYourAsyncScene",tranAni);
        }
    }

    IEnumerator LoadYourAsyncScene(Animation tranAni)
    {
        yield return new WaitForSeconds(9);
        // The Application loads the Scene in the background at the same time as the current Scene.
        //This is particularly good for creating loading screens. You could also load the scene by build //number.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        //Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
