using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class grapplingHook : MonoBehaviour
{
	public Transform spawnPoint;
	public Transform spawnPoint2;
	public Transform spawnPoint3;
	public Transform spawnPoint4;

	LineRenderer lr;
	Rigidbody2D rb;
    
	SpringJoint2D sj;
	Rigidbody2D currentBody;

	int inTrigger = 0;
	bool grappleActive = false;

	Vector2 currentTrigger;

	bool haveKey = false;

	public int checkpoints = 0;

    // Audio for collision with walls
    public AudioSource AudioSrc;
    public AudioClip CollisionSound;
    // Audio for grappling hook
    public AudioClip HookSound;

    bool prevSpace = false;

    void Start()
	{
		//Gets the Line rendere and rigidbody of the player
		lr = GetComponent<LineRenderer>();
		rb = GetComponent<Rigidbody2D>();

		//Sets the lines width to 0
		lr.startWidth = 0F;
		lr.endWidth = 0F;

		//Gets the spring joint component.
        sj = GetComponent<SpringJoint2D>();
    }


	void Update()
	{
		lr.SetPosition(0, this.transform.position);


        //Enables the players' grapple
        if (Input.GetKey(KeyCode.Space))
		{
            if (inTrigger > 0)
            {
                if(prevSpace == false)
                {
                    AudioSrc.clip = HookSound;
                    AudioSrc.Play();
                    prevSpace = true;
                }
                
                sj.enabled = true;

                grappleActive = true;
                lr.startWidth = 0.1F;
                lr.endWidth = 0.1F;

            }

            lr.SetPosition(1, currentTrigger);

			sj.connectedBody = currentBody;
        }
        else
        {
            prevSpace = false;
        }
		//Disables the players' grapple
		if (Input.GetKeyUp(KeyCode.Space))		
		{
			lr.startWidth = 0F;
			lr.endWidth = 0F;
			grappleActive = false;

            sj.connectedBody = null;
        }

		//Set the line behind the player to hide it																																																															Super janky, i know.
		if (grappleActive == false)
		{
			lr.SetPosition(1, this.transform.position);


			sj.enabled = false;
		}

		
	}



    private void OnTriggerEnter2D(Collider2D other)
	{
		//Allows the player to hook 
		if (other.tag == "Hook")
		{
            currentTrigger = other.transform.position;
			//Debug.Log("Player Enterd Trigger");
			inTrigger += 1;

			currentBody = other.attachedRigidbody;
		}

		//Respawn player if touch wall
		if (other.tag == "Walls")
		{
            AudioSrc.clip = CollisionSound;
            AudioSrc.Play();

            respawnPlayer();

        }

		//Give the player the key
		if (other.tag == "RedKey")
		{
			haveKey = true;
			Debug.Log("Player Took The Red Key");
			Destroy(other.gameObject);
		}

		//Check for key on door, remove key from player
		if (other.tag == "Door" && haveKey == true)
		{
			Destroy(other.gameObject);
			haveKey = false;
		}

		//Cehck for checkpoint
		if (other.tag == "Checkpoint")
		{
			checkpoints += 1;
			Destroy(other); //Removes the collider after impact
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		//Disables the players ability to hook
		if (other.tag == "Hook")
		{
			//Debug.Log("Player Left Trigger");
			inTrigger -= 1;
		}
	}

    public void respawnPlayer()
    {
        rb.velocity = new Vector2();
        this.transform.position = spawnPoint.position;

        switch (checkpoints)
        {
            case 1:
                rb.velocity = new Vector2(); ;
                this.transform.position = spawnPoint2.position; ;
                break;

            case 2:
                rb.velocity = new Vector2();
                this.transform.position = spawnPoint3.position;
                break;

            case 3:
                rb.velocity = new Vector2();
                this.transform.position = spawnPoint4.position;
                break;

            default:
                break;
        }
    }
}
