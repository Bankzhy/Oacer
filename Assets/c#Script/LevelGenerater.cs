using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LevelGenerater : MonoBehaviour {
	public List<GameObject> MoutainLayer;
	public List<GameObject> CloudLayer1;
	public List<GameObject> CloudLayer2;
	public List<GameObject> ActiveElement;
    public List<GameObject> Obstacles;

	float LowSpeed;
	float MediumSpeed;
	float Highspeed;
    float obspeed ;
    float scrollspeed = 2.5f;
    float maxscrollspeed = 7f;
    float speeddist = 1500f;
    float defaultspeed;
    float extralspeed = 0;

    bool canGenerate = false;
    bool ifpaused = true;
    public float distance;

    int instances = 0;
    static LevelGenerater myInstance;

    public static LevelGenerater Instance
    {
        get
        {
            myInstance = FindObjectOfType(typeof(LevelGenerater)) as LevelGenerater;
            return myInstance;
        }
    }

	// Use this for initialization

	void Start () {

        LowSpeed = scrollspeed * 2;
        MediumSpeed = scrollspeed * 3;
        Highspeed = scrollspeed * 4;
        obspeed = scrollspeed * 5;
        defaultspeed = scrollspeed;

        instances++;

        if (instances > 1)
            Debug.Log("Warning: There are more than one Level Generator at the level");
        else
            myInstance = this;
        
        GenerateCloudLayer1();
        GenerateCloudLayer2();
        GenerateMoutainLayer();
        GenerateObstacles();

	
	}
	
	// Update is called once per frame
	void Update () {
        if (ifpaused == false)
        {
            ScrollView();
            distance += obspeed * 1 * Time.deltaTime;
        }
        if (distance > 100&&canGenerate==true)
        {
            StartCoroutine("GenerateSadako");
        }
    }

    public IEnumerator GenerateSadako()
    {
        canGenerate = false;
        SadakoManager.Instance.SadakoStart();
        float gensecdis = Random.Range(10, 20);
        float waited = 0;
        while (waited <= gensecdis)
        {
            waited += Time.deltaTime;
            yield return 0;
        }
        canGenerate = true;
    }

    public void GenerateSadasko()
    {
        SadakoManager.Instance.SadakoStart();
    }

    public void GenerateMoutainLayer(){
        //print("start");
        int i = Random.Range(0, MoutainLayer.Count);
        GameObject g = MoutainLayer[i];
        MoutainLayer.Remove(g);
        ActiveElement.Add(g);
	}
    public void GenerateCloudLayer1()
    {

        int i = Random.Range(0, CloudLayer1.Count);
        GameObject g = CloudLayer1[i];
        CloudLayer1.Remove(g);
        ActiveElement.Add(g);
    }
    public void GenerateCloudLayer2()
    {

        int i = Random.Range(0, CloudLayer2.Count);
        GameObject g = CloudLayer2[i];
        CloudLayer2.Remove(g);
        ActiveElement.Add(g);
    }

    public void GenerateObstacles()
    {
        int i = Random.Range(0, Obstacles.Count);
        GameObject g = (GameObject)Obstacles[i];
        Obstacles.Remove(g);
        ActiveElement.Add(g);

        g.GetComponent<Obstacles>().ActiveChild();
        //print(g.name);
    }

    void ScrollView()
    {
        scrollspeed =extralspeed+ defaultspeed + (speeddist - (speeddist - distance)) / speeddist * (maxscrollspeed - defaultspeed);

        LowSpeed = scrollspeed*2;
        MediumSpeed = scrollspeed*3;
        Highspeed = scrollspeed*4;
        obspeed = scrollspeed*5;
        //print("starts");
        for (int i = 0; i < ActiveElement.Count; i++)
        {
            switch (ActiveElement[i].tag)
            {
                case "Moutain":
                    ActiveElement[i].transform.position -= Vector3.right * LowSpeed*Time.deltaTime;
                    break;
                case "Cloud1":
                    ActiveElement[i].transform.position -= Vector3.right * Highspeed * Time.deltaTime;
                    break;
                case "Cloud2":
                    ActiveElement[i].transform.position -= Vector3.right * MediumSpeed * Time.deltaTime;
                    break;
                case "Obstacle":
                    ActiveElement[i].transform.position -= Vector3.right * obspeed * Time.deltaTime;
                    break;



            }
        }
    }

   public void SleepGameObject(GameObject g)
    {
        switch (g.tag) {
            case "Moutain":
                ActiveElement.Remove(g);
                MoutainLayer.Add(g);
                g.transform.position = new Vector3(25, transform.position.y, transform.position.z);
                break;
            case "Cloud1":
                ActiveElement.Remove(g);
                CloudLayer1.Add(g);
                g.transform.position = new Vector3(20, transform.position.y, transform.position.z);
                break;
            case "Cloud2":
                ActiveElement.Remove(g);
                CloudLayer2.Add(g);
                g.transform.position = new Vector3(50, transform.position.y, transform.position.z);
                break;
            case "Obstacle":
                ActiveElement.Remove(g);
                Obstacles.Add(g);
                foreach(Transform child in g.transform)
                {
                    if(child.name== "redfire" || child.name == "bluefire")
                    {
                        print(33);
                        child.gameObject.GetComponent<AudioSource>().enabled = false;
                    }
                }
                g.transform.position = new Vector3(20, transform.position.y, transform.position.z);
                break;

        }

    }


    public float SpeedMultiplier()
    {
        return scrollspeed / defaultspeed;
    }

    public void paused()
    {
        canGenerate = false;
        ifpaused = true;

    }
    public void resume()
    {
        canGenerate = true;
        ifpaused = false;

    }
    public void SpeedUp()
    {
        extralspeed = 3.0f;
    }

    public void speedre()
    {
        extralspeed = 0f;
    }
    public void Stop()
    {

    }
    public void ClearMap()
    {
        SaveManager.SetDistance((int)distance);
        distance = 0;
        SadakoManager.Instance.ResetSadako();
        SadakoManager.Instance.StopSadako();
        StopAllCoroutines();
        while (ActiveElement.Count > 0)
        {
            switch (ActiveElement[0].tag) {
                case "Moutain":
                    MoutainLayer.Add(ActiveElement[0]);
                    ActiveElement[0].transform.localPosition = new Vector3(25f, ActiveElement[0].transform.localPosition.y, ActiveElement[0].transform.localPosition.z);
                    ActiveElement.Remove(ActiveElement[0]);
                    break;
                case "Cloud1":
                    CloudLayer1.Add(ActiveElement[0]);
                    ActiveElement[0].transform.localPosition = new Vector3(20f, ActiveElement[0].transform.localPosition.y, ActiveElement[0].transform.localPosition.z);
                    ActiveElement.Remove(ActiveElement[0]);
                    break;
                case "Cloud2":
                    CloudLayer2.Add(ActiveElement[0]);
                    ActiveElement[0].transform.localPosition = new Vector3(50f, ActiveElement[0].transform.localPosition.y, ActiveElement[0].transform.localPosition.z);
                    ActiveElement.Remove(ActiveElement[0]);
                    break;
                case "Obstacle":
                    Obstacles.Add(ActiveElement[0]);
                    foreach (Transform child in ActiveElement[0].transform)
                    {
                        if (child.name == "redfire" || child.name == "bluefire")
                        {
                            print(33);
                            child.gameObject.GetComponent<AudioSource>().enabled = false;
                        }
                    }
                    ActiveElement[0].transform.position = new Vector3(20, transform.position.y, transform.position.z);
                    ActiveElement.Remove(ActiveElement[0]);
                    break;
            }

        }
    }

    public void ReStart()
    {
        GenerateCloudLayer1();
        GenerateCloudLayer2();
        GenerateMoutainLayer();
        GenerateObstacles();
    }
}
