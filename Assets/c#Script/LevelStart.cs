using UnityEngine;
using System.Collections;

public class LevelStart : MonoBehaviour {
    public AudioClip Sadako;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        if(other.name== "bluefire"||other.name== "redfire")
        {
            other.gameObject.GetComponent<AudioSource>().enabled = true;
        }else if(other.name== "sadakom")
        {
            other.transform.parent.GetComponent<AudioSource>().PlayOneShot(Sadako);
        }
    }
}
