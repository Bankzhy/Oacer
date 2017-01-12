using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Obstacles : MonoBehaviour {
    List<GameObject> obelement = new List<GameObject>();
	// Use this for initialization
	void Start () {
	    foreach(Transform child in transform)
        {
            if (child.name != "SpawnerTrigger" && child.name != "ResetTrigger")
            {
                //print("in");
                obelement.Add(child.gameObject);
                //EnableOrDisable(child.gameObject, false);

            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void EnableOrDisable(GameObject go,bool eod)
    {
        #if UNITY_3_5
            go.SetActiveRecursively(activate);
        #else
        go.SetActive(eod);
         #endif
    }
    public void ActiveChild()
    {

        foreach(GameObject child in obelement)
        {
            //print("test");
            EnableOrDisable(child, true);
            child.GetComponent<Renderer>().enabled = true;
            child.GetComponent<Collider>().enabled = true;

            if (child.name == "redfire")
            {
                child.transform.FindChild("redfire").GetComponent<Renderer>().enabled = true;
            }
            else if (child.name == "bluefire")
            {
                child.transform.FindChild("bluefire").GetComponent<Renderer>().enabled = true;
            }else if(child.name== "babyghost")
            {
                child.transform.FindChild("babyghost").GetComponent<Renderer>().enabled = true;
            }else if(child.name== "floatghost")
            {
                child.transform.FindChild("floatghost").GetComponent<Renderer>().enabled = true;
            }
        }
    }
    public void DisActive()
    {
        foreach(GameObject child in obelement)
        {

            Transform go = child.transform.FindChild("ObParticle");
            if (go != null)
            {
                go.GetComponent<ParticleSystem>().Stop();
            }
            EnableOrDisable(child, false);
        }
    }
}
