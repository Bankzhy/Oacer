using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HPControl : MonoBehaviour {
    static float hp =0.5f;
    Slider hpslider;
   static bool Pcon;
    // Use this for initialization
    void Start () {
       hpslider = GameObject.Find("HPSlider").GetComponent<Slider>();
	
	}
	
	// Update is called once per frame
	void Update () {
        hpslider.value = hp;

        if (Mathf.Ceil(hp) == 0&&Pcon==false)
        {
            PlayerManager.Instance.Die();
            Pcon = true;
        }
        
    }

    public static void increaseHP(float value)
    {
        if (hp <= 1)
        {
            hp += value;
        }
    }
    public static void decreaseHP(float value)
    {
        if (hp >= 0)
        {
            hp -= value;
        }
    }
    public static void restart()
    {
        hp = 0.5f;
        Pcon = false;
    }
}
