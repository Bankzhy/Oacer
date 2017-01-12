using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
    public AudioClip buttonsound;
    private AudioSource buttonsource;
    public GameObject[] ButtonList;
    static SoundManager myinstace;

    public static SoundManager Instace
    {
        get
        {
            myinstace = FindObjectOfType(typeof(SoundManager)) as SoundManager;
            return myinstace;
        }
    } 
    
	// Use this for initialization
	void Start () {
        //print("sound");
        ButtonList = GameObject.FindGameObjectsWithTag("button");
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    //public void ReadySound()
    //{
    //    print("rs");
    //    BGMManager.thisaudio.PlayOneShot(ReadyGo);
    //}
    public void ButtonSound(string buttonname)
    {


        foreach(GameObject button in ButtonList)
        {

            if (button.name == buttonname)
            {

                buttonsource = button.GetComponent<AudioSource>();
                buttonsource.PlayOneShot(buttonsound);
            }
        }
    }
}
