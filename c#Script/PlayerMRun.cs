using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMRun : MonoBehaviour {
    public int speed = 20;
    public float jumppower = 500;
    bool isjump = false;
    public int MAX_JUMP_COUNT = 2;
    int jumcount = 0;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
     // Run();
        if (jumcount < MAX_JUMP_COUNT && Input.GetKeyDown(KeyCode.U))
        {
            //print("sss");
            //this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumppower);
            isjump = true;

        }

        bool isfall = this.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0.1 ? true : false;
        if (isfall == true)
        {
            this.gameObject.GetComponent<Animator>().SetBool("Fall", true);
        }
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        Run(x);
        if (isjump==true)
        {
            print("s");
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.gameObject.GetComponent<Rigidbody2D>().velocity=new Vector2(0,jumppower);
            this.gameObject.GetComponent<Animator>().SetTrigger("Jump");

            jumcount++;
            isjump = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            print("k");

            this.gameObject.GetComponent<Animator>().SetBool("Jump", false);
            jumcount = 0;
        }
    }

    void Run(float x)
    {

        if (x>0){
           this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
           // this.gameObject.transform.position += Vector3.right * 10 * Time.deltaTime;
            //this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up*1.5f, ForceMode.Force);
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, this.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            
            this.gameObject.GetComponent<Animator>().SetBool("Run", true);
        }
        else if(x==0)
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            this.gameObject.GetComponent<Animator>().SetBool("Run", false);
          
        }
        if (x<0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, this.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            this.gameObject.GetComponent<Animator>().SetBool("Run", true);
            
        }else if (x==0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            this.gameObject.GetComponent<Animator>().SetBool("Run", false);
            
        }
    }
}
