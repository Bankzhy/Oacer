using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {
    float AndroidCameraSize = 5.75f;
    float iPadCameraSize = 7.3f;

	// Use this for initialization
	void Start () {
        if (Application.platform == RuntimePlatform.Android)
        {
            this.gameObject.GetComponent<Camera>().orthographicSize = AndroidCameraSize;

        }else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            this.gameObject.GetComponent<Camera>().orthographicSize = iPadCameraSize;
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
