using UnityEngine;
using System.Collections;

public class LevelEdge : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
        
    {
        if (other.name == "SpawnerTrigger")
        {
            switch (other.tag)
            {
                case "Moutain":
                    LevelGenerater.Instance.GenerateMoutainLayer();
                    break;
                case "Cloud1":
                    LevelGenerater.Instance.GenerateCloudLayer1();
                    break;
                case "Cloud2":
                    LevelGenerater.Instance.GenerateCloudLayer2();
                    break;
                case "Obstacle":
                    LevelGenerater.Instance.GenerateObstacles();
                    break;
            }
        }else if (other.name == "ResetTrigger")
        {
            switch (other.tag)
            {
                case "Moutain":
                case "Cloud1":
                case "Cloud2":
                    LevelGenerater.Instance.SleepGameObject(other.transform.parent.gameObject);

                    break;
                case "Obstacle":
                    LevelGenerater.Instance.SleepGameObject(other.transform.parent.gameObject);
                    //other.transform.parent.GetComponent<Obstacles>().DisActive();
                    break;

            }
        }else if(other.name== "sadakom")
        {
            SadakoManager.Instance.ResetSadako();
        }
    }
}
