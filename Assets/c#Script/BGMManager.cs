using UnityEngine;
using System.Collections;

public class BGMManager : MonoBehaviour {
    public AudioClip MainMenusound;
    public AudioClip GameSound;
    public static AudioSource thisaudio;
    public static bool ingame;
	// Use this for initialization
	void Start () {
        thisaudio = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (ingame&&thisaudio.clip!=GameSound)
        {
            //thisaudio.clip = GameSound;
            //thisaudio.Play();
            StartCoroutine(BgmChangeWait(GameSound));


        }
        else if(!ingame&&thisaudio.clip!=MainMenusound)
        {
            //thisaudio.clip = MainMenusound;
            StartCoroutine(BgmChangeWait(MainMenusound));
        }

    }

    IEnumerator BgmChangeWait(AudioClip sound)
    {
        thisaudio.clip = null;
        float wait = 0;
        while (wait < 1.5)
        {
            wait += Time.deltaTime;
            yield return 0;
        }
        thisaudio.clip = sound;
        thisaudio.Play();
    }


}
