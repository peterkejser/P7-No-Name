using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class Master : MonoBehaviour {
    public Animator pawAnimationRight;
    public Animator pawAnimationLeft;
    public AudioClip[] narration;
    public GameObject touchLocoLeft;
    public GameObject touchLocoRight;
    public GameObject snout;
    public AudioSource audioSource;
    bool MoveInPlaceActive;
    bool currentlyChecking;
    bool noAudioIsPlaying;
    public bool wolfNowFatAudio;
    public bool playerHasWon;
    bool[] hasBeenEaten = new bool[] { false, false, false, false };
    string[] fruits = new string[] { "Orange", "Banana", "Pineapple", "Melon" };
    // Use this for initialization
    void Start () {
        noAudioIsPlaying = true;
        MoveInPlaceActive = false;
        audioSource = GetComponent<AudioSource>();
        int y = SceneManager.GetActiveScene().buildIndex;
        if (y == 1)
        {
            PlayAudioClip(2);
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("j"))
        {
            PlayAudioClip(0);
        }
        if (Input.GetKeyDown("k"))
        {
            PlayAudioClip(1);
        }
        if (Input.GetKeyDown("l"))
        {
            if (MoveInPlaceActive == true)
            {
                gameObject.GetComponent<VRTK_MoveInPlace>().enabled = false;
                MoveInPlaceActive = false;
                touchLocoLeft.SetActive(true);
                touchLocoRight.SetActive(true);
                snout.GetComponent<VREating>().moveInPlace = false;
            }
            else
            {
                gameObject.GetComponent<VRTK_MoveInPlace>().enabled = true;
                MoveInPlaceActive = true;
                touchLocoLeft.SetActive(false);
                touchLocoRight.SetActive(false);
                snout.GetComponent<VREating>().moveInPlace = true;
            }
        }
    }
    public void CurlLeftPaw()
    {
        pawAnimationLeft.SetBool("Curled", true);
    }
    public void CurlRightPaw()
    {
        pawAnimationRight.SetBool("Curled", true);
    }
    public void UncurlLeftPaw()
    {
        pawAnimationLeft.SetBool("Curled", false);
    }
    public void UncurlRightPaw()
    {
        pawAnimationRight.SetBool("Curled", false);
    }

    public void FirstTimeFruit(string typeOfFruit)
    {
        if (!audioSource.isPlaying)
        {
            for (int i = 0; i < 3; i++)
            {
                if (typeOfFruit == fruits[i] && hasBeenEaten[i] == false)
                {
                    PlayAudioClip(i + 3);
                    hasBeenEaten[i] = true;
                }
            }
        }
    }

    public void PlayAudioClip(int audioClip)
    {
        if(audioClip == 21)
        {
            playerHasWon = true;
            StartCoroutine("PlayNarration", audioClip);
        }
        else if(audioClip == 22)
        {
            StartCoroutine("PlayNarration", audioClip);
        }
        if (!playerHasWon)
        {
            int value = audioClip;
            if(audioClip == 20)
            {
                StartCoroutine("PlayNarration", audioClip);
            }
            else if (audioClip < 14 && audioClip > 6)
            {
                if (!currentlyChecking && wolfNowFatAudio == false)
                {
                    audioSource.clip = narration[value];
                    StartCoroutine("PlayNarration", audioClip);
                    currentlyChecking = true;
                }
            }
            else
            {
                audioSource.clip = narration[value];
                audioSource.Play();
            }
            //StartCoroutine("PlayNarration",value);
        }
    }
    IEnumerator PlayNarration(int clipNumber)
    {
        if (clipNumber == 21 || clipNumber == 20 || clipNumber == 22)
        {
            if (clipNumber == 20 && audioSource.clip == narration[22])
            {
                Debug.Log("lazy solution");
            }
            else
            {
                audioSource.Stop();
                audioSource.clip = narration[clipNumber];
                audioSource.Play();
                yield return new WaitForSeconds(audioSource.clip.length);
                SteamVR_LoadLevel.Begin("StartSceneVR");
            }
        }
        else if(clipNumber == 14)
        {
            audioSource.clip = narration[clipNumber];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            wolfNowFatAudio = false;
        }
        else
        {
            audioSource.clip = narration[clipNumber];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            currentlyChecking = false;
            noAudioIsPlaying = true;
            //SteamVR_LoadLevel.Begin("StartSceneVR");
        }
    }
}
