using UnityEngine;
using System.Collections;

public class Sadako : MonoBehaviour {
    float speed=0;

    bool canmove = false;
    //public GameObject Indicator;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (canmove ==true)
        {
            transform.position -= Vector3.right * speed * Time.deltaTime;
        }
	}

    public void Move()
    {
   
        speed = LevelGenerater.Instance.SpeedMultiplier()*20f;
        canmove = true;
        StartCoroutine("StartAnimation");
    }

    IEnumerator StartAnimation()
    {
        double wait = 3f;
        while (wait < 3f)
        {
            wait += Time.deltaTime;
            yield return 0;



        }

        this.gameObject.GetComponent<Animator>().enabled = true;




    }
    public void Reset()
    {
        canmove = false;
        //this.gameObject.GetComponent<Animator>().Stop();
        this.gameObject.GetComponent<Animator>().enabled = false;

    }

    //IEnumerator IdicatorAndGo()
    //{
    //    Indicator.SetActive(true);
    //    double waitsec = 0;
    //    while (waitsec < 2)
    //    {
    //        waitsec += Time.deltaTime;
    //        yield return 0;
    //    }

    //    speed = LevelGenerater.Instance.SpeedMultiplier();
    //}
}
