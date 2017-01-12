using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
    public Text[] text;
    public GameObject ready;
    public GameObject go;
    static GUIManager myInstance;
    private AudioSource MainMenuSound;
    public GameObject FinishMenut;
    public GameObject MainMenut;
    public GameObject FinishDis;
    public GameObject FinishCry;
    public GameObject SubMenu;
    public GameObject MainGUI;
    public AudioClip ReadyGo;
    public GameObject BestDistanceMainMenu;

    public bool isFinishMove;
    bool canClick=true;
    public bool ifSpeedupreseet;
    bool ifFB = false;

	// Use this for initialization
    public static GUIManager Instance
    {
        get
        {
            myInstance = FindObjectOfType(typeof(GUIManager)) as GUIManager;
            PlayerManager.Instance.TransBack();
            return myInstance;
        }
    }

	void Start () {
        SaveManager.CreatOrLoad();
        DisplayTextM(BestDistanceMainMenu.GetComponent<TextMesh>(), SaveManager.GetBestDistance(), 5);
        MainMenuSound = this.GetComponent<AudioSource>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (ifSpeedupreseet == true)
        {
            SpeedUpReset();
        }
        DisplayText(text[0],(int)LevelGenerater.Instance.distance,5);
        DisplayText(text[1], (int)PlayerManager.Instance.Crystals, 5);

    }

    void DisplayText(Text text,int data,int numbercount)
    {
        string target = "";
        int zero = numbercount - data.ToString().Length;
        while (zero > 0)
        {
            target += "0";
            zero--;
        }
        target += data;
        text.text = target;
    }

    void DisplayTextM(TextMesh text, int data, int numbercount)
    {
        print("ds");
        string target = "";
        int zero = numbercount - data.ToString().Length;
        while (zero > 0)
        {
            target += "0";
            zero--;
        }
        target += data;
        text.text = target;
    }

    public void ButtonDown(Transform button)
    {
        Debug.Log("i");
        print(canClick);

        if (canClick == false)
            return;
        Vector3 scale = button.transform.localScale;
        button.transform.localScale = scale * 0.8f;
        switch (button.name)
        {
            case "MagicButton1":
                PlayerManager.Instance.ifspeedup = true;
                LevelGenerater.Instance.SpeedUp();
                MPControl.decreaseMPC(0.08f);
                break;
            case "TransparencyButton":                
                PlayerManager.Instance.TransPanrecy();
                MPControl.decreaseMPC(0.15f);
                break;
        }

    }
    public void ButtonUp(Transform button)
    {
        if (canClick == false)
            return;
        Vector3 scale = button.transform.localScale;
        button.transform.localScale = scale * 1.25f;
        switch (button.name) {
            case "StartButton":
                SoundManager.Instace.ButtonSound(button.name);
                StartCoroutine(MoveMenu(button.parent, -22, 0, 1.5f));
                if (ifFB)
                {
                    LevelGenerater.Instance.ReStart();
                    ifFB = false;
                }
                BGMManager.ingame = true;
                MainGUI.SetActive(true);
                break;
            case "MagicButton1":
                SpeedUpReset();
                //PlayerManager.Instance.ifspeedup = false;
                //LevelGenerater.Instance.speedre();
                //MPControl.resetMPC();
                break;
            case "TransparencyButton":
                PlayerManager.Instance.TransBack();
                MPControl.resetMPC();
                ifSpeedupreseet = false;
                break;
            case "Restart":
                SoundManager.Instace.ButtonSound(button.name);
                StartCoroutine(GMoveMenu(FinishMenut.transform, FinishMenut.transform.position.x, 11f, 0.5f));
                PlayerManager.Instance.Restart();
                LevelGenerater.Instance.ClearMap();
                LevelGenerater.Instance.ReStart();
                StartCoroutine(StartToPlay());
                BGMManager.ingame = true;
                break;
            case "MainMenu":
                SoundManager.Instace.ButtonSound(button.name);
                StartCoroutine(FinishToMain());
                PlayerManager.Instance.Restart();
                LevelGenerater.Instance.ClearMap();
                MainGUI.SetActive(false);
                ifFB = true;
                SaveManager.CreatOrLoad();
                DisplayTextM(BestDistanceMainMenu.GetComponent<TextMesh>(), SaveManager.GetBestDistance(), 5);
                break;
            case "SubButton":
                SoundManager.Instace.ButtonSound(button.name);
                StartCoroutine(GMoveMenu(SubMenu.transform, SubMenu.transform.position.x, 0f, 0.5f));
                break;
            case "SubButtonI":
                SoundManager.Instace.ButtonSound(button.name);
                StartCoroutine(GMoveMenu(SubMenu.transform, SubMenu.transform.position.x, -14f, 0.5f));
                break;
            case "quitbutton":
                Application.Quit();
                break;



        }

    }
    void SpeedUpReset()
    {
        PlayerManager.Instance.ifspeedup = false;
        LevelGenerater.Instance.speedre();
        MPControl.resetMPC();
        ifSpeedupreseet = false;
    }

    IEnumerator MoveMenu(Transform menutransform,float endposx,float endposy,float time)
    {
        canClick = false;
        float rate = 1.0f / time;
        Vector3 startpos = menutransform.localPosition;
        Vector3 endpos = new Vector3(endposx, endposy, menutransform.localPosition.z);
        float i = 0f;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            menutransform.localPosition = Vector3.Lerp(startpos, endpos, i);

            yield return 0;
        }
        StartCoroutine(StartToPlay());
        //StartCoroutine(readygo());
        //float wait = 0;

        //while (wait < 1.5f)
        //{
        //    wait += Time.deltaTime;
        //    yield return 0;
        //}

        //Debug.Log("ds");
        //LevelGenerater.Instance.resume();
        //PlayerManager.Instance.resume();
        canClick = true;
    }

    IEnumerator StartToPlay()
    {
        StartCoroutine(readygo());
        float wait = 0;

        while (wait < 1.5f)
        {
            wait += Time.deltaTime;
            yield return 0;
        }


        LevelGenerater.Instance.resume();
        PlayerManager.Instance.resume();
    }

    IEnumerator GMoveMenu(Transform menutransform, float endposx, float endposy, float time)
    {
        canClick = false;
        float rate = 1.0f / time;
        Vector3 startpos = menutransform.localPosition;
        Vector3 endpos = new Vector3(endposx, endposy, menutransform.localPosition.z);
        float i = 0f;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            menutransform.localPosition = Vector3.Lerp(startpos, endpos, i);

            yield return 0;
        }
        canClick = true;
    }

    IEnumerator readygo()
    {
        ready.SetActive(true);
        this.GetComponent<AudioSource>().PlayOneShot(ReadyGo);
        float wait = 0;
        while (wait < 0.5f)
        {
            wait += Time.deltaTime;
            yield return 0;
        }

        ready.SetActive(false);
        go.SetActive(true);
        float waited = 0;
        while (waited < 1f)
        {
            waited += Time.deltaTime;
            yield return 0;
        }
        go.SetActive(false);
    }
    IEnumerator FinishToMain()
    {
        canClick = false;
        StartCoroutine(GMoveMenu(FinishMenut.transform, FinishMenut.transform.position.x, 11f, 0.5f));

        float wait = 0;
        while (wait < 0.5f)
        {
            wait += Time.deltaTime;
            yield return 0;
        }
        StartCoroutine(GMoveMenu(MainMenut.transform, 0f, 0f, 1.5f));
        canClick = true;
    }
    
    public void FinishMenu() {
        DisplayTextM(FinishCry.GetComponent<TextMesh>(), PlayerManager.Instance.Crystals, 5);
        DisplayTextM(FinishDis.GetComponent<TextMesh>(),(int)LevelGenerater.Instance.distance, 5);
        StartCoroutine(GMoveMenu(FinishMenut.transform, FinishMenut.transform.position.x, 0f, 0.5f));
    }
}
