using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
    public float maxDepth = 5.0f;
    public float minDepth = -3.0f;
    public float maxRotation = 4.0f;
    public float speeduppos = -4.0f;
    public float speedup = 5f;
    Vector3 newRotation = new Vector3(0, 0, 0);

    public float bufferDepth = 1.0f;

    float distanceToMin;
    float distanceToMax;
    public int Crystals;
    public float maxSpeed = 8f;
    float speed;
    float limitspeed;
    public float roatdiv;
    static int instances = 0;
    public Material TransP;
    public Material HeartP;
    public Material defaultM;
    public AudioClip diamondsound;
    public AudioClip fire;
    public AudioClip SpeedUpSound;
    public AudioClip TransPSound;
    public AudioClip floatghostsound;
    public AudioClip babyghostsound;
    bool updirection=false ;
    bool ifpaused = true;
    bool canheart=true;
    public bool ifspeedup = false;

    public static PlayerManager myInstance;
	// Use this for initialization

    public static  PlayerManager Instance
    {
        get
        {
            myInstance = FindObjectOfType(typeof(PlayerManager)) as PlayerManager;
            return myInstance;
        }
    }
	void Start () {
        instances++;

        if (instances > 1)
            Debug.Log("Warning: There are more than one Player Manager at the level");
        else
            myInstance = this;
        ;
        roatdiv = maxSpeed / maxRotation;
    }
	
	// Update is called once per frame
	void Update () {
        if (ifpaused == false)
        {
            calculateDistance();
            calculateMovement();
            moveAndRotation();
        }

        if (ifspeedup == true)
        {
            SpeedUp();

        }
        else
        {
            SpeedDown();

        }
    }

    void calculateDistance()
    {
        
        distanceToMax = Mathf.Round( maxDepth- transform.position.y);
        distanceToMin = Mathf.Round(transform.position.y - minDepth);



    }

    void calculateMovement()
    {
        if (updirection == true)
        {
            speed += Time.deltaTime * maxSpeed;
            if (distanceToMax<bufferDepth)
            {
                limitspeed =Mathf.Round( maxSpeed * ((maxDepth - transform.position.y) / bufferDepth));
                if (limitspeed < speed)
                {
                    speed = limitspeed;

                }
            }else if (distanceToMin<bufferDepth)
            {
                limitspeed = maxSpeed * (transform.position.y - minDepth) / bufferDepth;
                if (limitspeed > speed)
                {
                    speed = limitspeed;

                }
                    
            }

        }
        else
        {

            speed -= Time.deltaTime * maxSpeed;

            if (distanceToMin < bufferDepth)
            {
                limitspeed = Mathf.Round( maxSpeed * ((minDepth - transform.position.y) / bufferDepth));
                if (limitspeed > speed)
                {
                    speed = limitspeed;
                }
            }else if (distanceToMax < bufferDepth)
            {
                limitspeed = maxSpeed * (transform.position.y - maxDepth);
                if (limitspeed < speed)
                {
                    speed = limitspeed;
                        
                }
            }
        }
    }

    public void moveAndRotation()
    {
        newRotation.z = speed / roatdiv;
        transform.eulerAngles = newRotation;
        transform.position += Vector3.up * speed * Time.deltaTime;

    }

    public void moveUp()
    {
        updirection = true;


    }

    public void moveDown()
    {
        updirection = false;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            if (other.name == "diamondpink")
            {
                other.GetComponent<Renderer>().enabled = false;
                other.GetComponent<BoxCollider>().enabled = false;
                other.GetComponent<AudioSource>().PlayOneShot(diamondsound);
                other.transform.FindChild("ObParticle").gameObject.GetComponent<ParticleSystem>().Play();
                MPControl.increaseMP(0.005f);
                    
            }else if (other.name == "diamondred")
            {
                other.GetComponent<Renderer>().enabled = false;
                other.GetComponent<BoxCollider>().enabled = false;
                other.GetComponent<AudioSource>().PlayOneShot(diamondsound);
                other.transform.FindChild("ObParticle").gameObject.GetComponent<ParticleSystem>().Play();
                HPControl.increaseHP(0.015f);
            }else if (other.name == "redfire"||other.name=="bluefire")
            {
                other.GetComponent<Renderer>().enabled = false;
                other.GetComponent<BoxCollider>().enabled = false;
                other.GetComponent<AudioSource>().PlayOneShot(fire);
                if (other.name== "redfire")
                {
                    other.transform.FindChild("redfire").GetComponent<Renderer>().enabled = false;
                }
                else
                {
                    other.transform.FindChild("bluefire").GetComponent<Renderer>().enabled = false;
                }
                other.transform.FindChild("Particle System").gameObject.GetComponent<ParticleSystem>().Play();
                if (canheart == true)
                {
                    StartCoroutine("heart");
                }
                HPControl.decreaseHP(0.05f);



            }else if (other.name== "crystalpurple"||other.name== "crystalblue"||other.name== "crystalpink")
            {
                other.GetComponent<Renderer>().enabled = false;
                other.GetComponent<BoxCollider>().enabled = false;
                other.GetComponent<AudioSource>().PlayOneShot(diamondsound);
                other.transform.FindChild("ObParticle").gameObject.GetComponent<ParticleSystem>().Play();
                Crystals += 1;

            }else if (other.name== "babyghost")
            {
                other.transform.FindChild("babyghost").GetComponent<Renderer>().enabled = false;
                other.GetComponent<BoxCollider>().enabled = false;
                other.GetComponent<AudioSource>().PlayOneShot(babyghostsound);
                other.transform.FindChild("Particle System").gameObject.GetComponent<ParticleSystem>().Play();
                if (canheart == true)
                {
                    StartCoroutine("heart");
                }
                HPControl.decreaseHP(0.15f);
            }
            else if(other.name== "floatghost")
            {
                other.transform.FindChild("floatghost").GetComponent<Renderer>().enabled = false;
                other.GetComponent<BoxCollider>().enabled = false;
                other.GetComponent<AudioSource>().PlayOneShot(floatghostsound);
                other.transform.FindChild("Particle System").gameObject.GetComponent<ParticleSystem>().Play();
                if (canheart == true)
                {
                    StartCoroutine("heart");
                }
                HPControl.decreaseHP(0.3f);
            }
            else if(other.name== "sadakom")
            {
                if (canheart == true)
                {
                    StartCoroutine("heart");
                }
                HPControl.decreaseHP(0.5f);
            }
        }
    }

    IEnumerator heart()
    {
        canheart = false;
        transform.FindChild("wizardplane").GetComponent<Animator>().enabled = true;
        double wait = 0;
        while (wait < 1)
        {
            wait += Time.deltaTime;
            yield return 0;
        }
        transform.FindChild("wizardplane").GetComponent<Animator>().enabled = false;
        canheart = true;
    }

    public void paused()
    {
        ifpaused = true;

    }
    public void resume()
    {
        ifpaused = false;
    }

    public void SpeedUp()
    {
        this.transform.FindChild("Particle System").gameObject.SetActive(true);

        if (this.transform.position.x < speeduppos)
        {
            this.transform.position += Vector3.right * speedup * Time.deltaTime;
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(SpeedUpSound);
        }
        else
        {
            this.transform.position = new Vector3(speeduppos, this.transform.position.y, this.transform.position.z);
        }

    }

    public void SpeedDown()
    {

        this.transform.FindChild("Particle System").gameObject.SetActive(false);
        if (this.transform.position.x > -7f)
        {
            this.transform.position -= Vector3.right * speedup* Time.deltaTime;
        }
        else
        {
            this.transform.position = new Vector3(-7f, this.transform.position.y, this.transform.position.z);
        }
    }

    public void TransPanrecy()
    {
        this.GetComponent<BoxCollider>().enabled = false;
        this.transform.FindChild("wizardplane").GetComponent<MeshRenderer>().material = TransP;
        this.GetComponent<AudioSource>().PlayOneShot(TransPSound);
    }
    public void TransBack()
    {
        this.GetComponent<BoxCollider>().enabled = true;
        this.transform.FindChild("wizardplane").GetComponent<MeshRenderer>().material = defaultM;
    }

    public void Die()
    {
        LevelGenerater.Instance.paused();
        StopAllCoroutines();
        transform.FindChild("wizardplane").GetComponent<Animator>().enabled = false;
        this.gameObject.transform.position = new Vector3(this.transform.position.x, -8f, this.transform.position.z);
        ifpaused = true;
        GUIManager.Instance.FinishMenu();
        SadakoManager.Instance.ResetSadako();
        SadakoManager.Instance.StopSadako();
        BGMManager.ingame = false;
    }
    public void Revive()
    {
        this.gameObject.transform.position = new Vector3(this.transform.position.x, 1f, this.transform.position.z);
    }
    public void Restart()
    {
        //       SaveManager.SetCrystal(Crystals);
        Crystals = 0;
        this.gameObject.transform.position = new Vector3(-7f, 1f, this.transform.position.z);
        HPControl.restart();
        MPControl.Reset();
    }
  
}
