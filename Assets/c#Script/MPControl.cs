using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MPControl : MonoBehaviour {
   static float mp = 0.5f;
   static float decreasePer;
    Slider mpslider;
   static bool isdecreaseCon = false;

	// Use this for initialization
	void Start () {
        mpslider = GameObject.Find("MPSlider").GetComponent<Slider>();

	}
	
	// Update is called once per frame
	void Update () {
        mpslider.value = mp;

        if (isdecreaseCon == true)
        {
            if (0 < mp)
            {
                mp -= decreasePer * Time.deltaTime;
            }
            else
            {
                GUIManager.Instance.ifSpeedupreseet = true;
            }
        }
    }

    public static void increaseMP(float value)
    {
        if (mp <= 1)
        {
            mp += value;
        }
    }
    public static void decreaseMP(float value)
    {
        if (mp >= 0)
        {
            mp -= value;
        }
    }

    public static void decreaseMPC(float value)
    {
        if (value < mp)
        {
            decreasePer = value;
            isdecreaseCon = true;
        }
        else
        {
            GUIManager.Instance.ifSpeedupreseet = true;
        }
    }

    public static void resetMPC()
    {
        decreasePer = 0;
        isdecreaseCon = false;
    }
    public static void Reset()
    {
        mp = 0.5f;
    }
}
