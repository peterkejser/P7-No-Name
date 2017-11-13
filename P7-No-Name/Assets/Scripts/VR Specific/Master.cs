using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour {
    public Animator pawAnimationRight;
    public Animator pawAnimationLeft;
    public AudioClip wolfBlocking;
    public AudioClip won;
    public AudioClip starving;
    public AudioClip tooFull;
    AudioSource audioSource;
    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
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

    public void PlayAudioClip(int audioClip)
    {
        int value = audioClip;
        switch (value)
        {
            case 1:
                audioSource.clip = wolfBlocking;
                break;
            case 2:
                audioSource.clip = won;
                break;
            case 3:
                audioSource.clip = starving;
                break;
            case 4:
                audioSource.clip = tooFull;
                break;
        }
        StartCoroutine("PlayNarration",value);
    }
    IEnumerator PlayNarration(int clipNumber)
    {
        if(clipNumber == 1 || clipNumber == 2)
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            SteamVR_LoadLevel.Begin("StartSceneVR");
        }
        else
        audioSource.Play();

    }
}
