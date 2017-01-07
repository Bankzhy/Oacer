using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class SadakoManager : MonoBehaviour {
    static SadakoManager myinstance;
    public GameObject Indicator;
    public AudioClip SadakoBeforeSound;

    List<Sadako> ActiveElement=new List<Sadako>();

    List<Sadako> DeActiveElement=new List<Sadako>();

    bool canGen = false;
	// Use this for initialization

    public static SadakoManager Instance
    {
        get
        {
            myinstance = FindObjectOfType(typeof(SadakoManager)) as SadakoManager;
            return myinstance;
        }
    }
	void Start () {
	       foreach(Transform child in transform)
        {
            if (child.name != "sadakoIndicator")
            {
                //print("selecet");
                DeActiveElement.Add(child.GetComponent<Sadako>());
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        
        startToMove();
	
	}

    public void SadakoStart()
    {
        StartCoroutine("IndicatorAndStart");
    }

    IEnumerator IndicatorAndStart()
    {
        //print("start");
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(SadakoBeforeSound);
        Indicator.SetActive(true);
        double waitsec = 0;
        while (waitsec < 3)
        {
            waitsec += Time.deltaTime;
            yield return 0;
        }
        int count = Random.Range(1, 3);
        //print(count);
        canGen = true;
        GenerateSadako(count);
        Indicator.SetActive(false);
        canGen = false;
    }

   public void GenerateSadako(int gencount)
    {
        for (int i = 0; i < gencount; i++)
        {
            
            int n = Random.Range(0, DeActiveElement.Count);
            //print("s"+n);
            //print("list"+DeActiveElement.Count);
            if (DeActiveElement.Count == 0)
            {
                break;
            }
            else
            {
                if (DeActiveElement[n] != null&&canGen==true)
                {
                    Sadako sa = DeActiveElement[n];
                    DeActiveElement.Remove(sa);
                    ActiveElement.Add(sa);
                }
                else
                {

                    break;

                }
            }
            
        }

    }

    void startToMove()
    {
        if (ActiveElement.Count != 0)
        {
            foreach (Sadako sadako in ActiveElement)
            {
                if (sadako != null)
                {

                    sadako.Move();
                    //print("sd");
                }
            }
        }
    }

    public void ResetSadako()
    {
        //if (ActiveElement.Count != 0)
        //{
            for(int i = 0; i< ActiveElement.Count; i++)
            {
                //if (ActiveElement.Count == 0)
                //{
                //    break;
                //}
                //else
                //{

                    Sadako sa = ActiveElement[i];
                    sa.Reset();
                    sa.gameObject.transform.position = new Vector3(12f, sa.gameObject.transform.position.y, sa.gameObject.transform.position.z);
                    ActiveElement.Remove(sa);
                    DeActiveElement.Add(sa);
                //}
            }
        //}
    }
    public void StopSadako()
    {
        StopAllCoroutines();
        Indicator.SetActive(false);
    }
}
