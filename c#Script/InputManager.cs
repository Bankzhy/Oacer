using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {
    Ray ray;
    RaycastHit hit;
    Transform[] button=new Transform[9];
    Transform defaultp;
    Dictionary<int, Transform> buttonMap = new Dictionary<int, Transform>();
    //Transform button1;
    //bool istouched;

    int mask = 1<<8;
    public bool usetouch;
    // Use this for initialization
	void Start () {
	  
	}
	
	// Update is called once per frame
	void Update () {
        if (usetouch == false)
        {
            Click();
        }
        else
        {
            touch();
        }
  //      command();
	}
    void command()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            print(SaveManager.GetBestDistance());
            SaveManager.Castdistance(0);
        }
    }
    void Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                button[0] = hit.transform;
                GUIManager.Instance.ButtonDown(button[0]);

            }
            else
            {
                button[0] = null;
                PlayerManager.Instance.moveUp();
                
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (button[0] == null)
            {
                PlayerManager.Instance.moveDown();
            }else
            {
                GUIManager.Instance.ButtonUp(button[0]);
            }

        }

    }

    void touch()
    {
        if (0 < Input.touchCount)
        {
            for(int i = 0; i < Input.touchCount; i++)
            {
                Touch t = Input.GetTouch(i);
                if (t.phase == TouchPhase.Began)
                {
                    ray = Camera.main.ScreenPointToRay(t.position);
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                    {
                        buttonMap.Add(t.fingerId, hit.transform);
                        GUIManager.Instance.ButtonDown(buttonMap[t.fingerId]);
                    }
                    else
                    {
                        buttonMap.Add(t.fingerId, defaultp);
                        PlayerManager.Instance.moveUp();
                    }
                }else if (t.phase == TouchPhase.Ended)
                {
                    if (buttonMap[t.fingerId] == defaultp)
                    {
                        PlayerManager.Instance.moveDown();
                        buttonMap.Remove(t.fingerId);
                    }else
                    {
                        GUIManager.Instance.ButtonUp(buttonMap[t.fingerId]);
                        buttonMap.Remove(t.fingerId);
                    }
            }
        }
        //if (0 < Input.touchCount)
        //{
        //    for(int i = 0; i < Input.touchCount; i++)
        //    {
        //        Touch t = Input.GetTouch(i);
        //        if (t.phase == TouchPhase.Began)
        //        {
        //            ray= Camera.main.ScreenPointToRay(t.position);
        //            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        //            {
        //                //for (int j = i; j < 9;)
        //                //{
        //                //    if (j > 0 && Input.GetTouch(j).phase != TouchPhase.Stationary)
        //                //    {
        //                        button[j] = hit.transform;
        //                        GUIManager.Instance.ButtonDown(button[j]);
        //                        //break;
        //                //    }
        //                //    else
        //                //    {
        //                //        j++;
        //                //    }

        //                //}
        //                 //GUIManager.Instance.ButtonDown(button[i]);

        //            }
        //            else
        //            {
        //                button[i] = null;
        //                PlayerManager.Instance.moveUp();
        //            }
        //        }else if (t.phase == TouchPhase.Ended)
        //        {
        //            if (button[i]==null)
        //            {
        //                PlayerManager.Instance.moveDown();

        //            }
        //            else
        //            {
        //                GUIManager.Instance.ButtonUp(button[i]);
        //                button[i] = null;
        //            }
        //        }
        //    }
        }
        //if (0 < Input.touchCount)
        //{
            //for(int i=0;i<Input.touchCount; i++)
            //{
            //    Touch t = Input.GetTouch(i);
                
            //    if (t.phase == TouchPhase.Began)
            //    {
            //        ray = Camera.main.ScreenPointToRay(t.position);
            //        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            //        {
            //            if (istouched == false)
            //            {
            //                button0 = hit.transform;
            //                GUIManager.Instance.ButtonDown(button0);
            //                istouched = true;
            //            }
            //            else
            //            {
            //                button1 = hit.transform;
            //                GUIManager.Instance.ButtonDown(button1);
            //                istouched = true;
            //            }

            //        }
            //        else
            //        {
            //            if (istouched == false)
            //            {
            //                button0 = null;
            //                PlayerManager.Instance.moveUp();
            //                istouched = true;
            //            }
            //            else
            //            {
            //                button1 = null;
            //                PlayerManager.Instance.moveUp();
            //                istouched = true;
            //            }

            //        }
            //    }
            //    else if (t.phase == TouchPhase.Ended)
            //    {
                    
            //        if (button0 == null&&button1==null)
            //        {
            //            PlayerManager.Instance.moveDown();
            //            istouched = false;
            //        }

            //        else if(button0==null&&button1!=null)
            //        {
            //            if (t.fingerId == Input.GetTouch(i - 1).fingerId)
            //            {
            //                PlayerManager.Instance.moveDown();
            //                istouched = false;
            //            }
            //            else
            //            {
            //                GUIManager.Instance.ButtonUp(button1);
            //                istouched = false;
            //            }
            //        }else if (button0 != null && button1 == null)
            //        {
            //            if (t.fingerId == Input.GetTouch(i - 1).fingerId)
            //            {
            //                GUIManager.Instance.ButtonUp(button0);
            //                istouched = false;
            //            }
            //            else
            //            {
            //                PlayerManager.Instance.moveDown();
            //                istouched = false;
            //            }
            //        }else if (button0 != null && button1 != null)
            //        {
            //            GUIManager.Instance.ButtonUp(button0);
            //            GUIManager.Instance.ButtonUp(button1);
            //            istouched = false;
            //        }

            //    }
            //}
        //}
        //foreach(Touch touch in Input.touches)
        //{
        //    if (touch.phase == TouchPhase.Began && touch.phase != TouchPhase.Canceled)
        //    {
        //        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        //        {
        //            button = hit.transform;
        //            GUIManager.Instance.ButtonDown(button);
        //        }
        //        else
        //        {
        //            button = null;
        //            PlayerManager.Instance.moveUp();
        //        }
        //    }else if (touch.phase==TouchPhase.Ended||touch.phase==TouchPhase.Canceled)
        //    {
        //        if (button == null)
        //        {
        //            PlayerManager.Instance.moveDown();
        //        }
        //        else
        //        {
        //            GUIManager.Instance.ButtonUp(button);
        //        }
        //    }
           
        //}

    }
}

