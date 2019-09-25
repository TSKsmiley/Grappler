using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class grapplingHook : MonoBehaviour
{
	public Transform spawn;
	public Transform spawnPoint1;
	public Transform spawnPoint2;
	public Transform spawnPoint3;
	public Transform spawnPoint4;
	public Transform spawnPoint5;
	public Transform spawnPoint6;
	//public Transform spawnPoint7;

	LineRenderer lr;
	Rigidbody2D rb;
    
	SpringJoint2D sj;
	Rigidbody2D currentBody;

	int inTrigger = 0;
	bool grappleActive = false;

	Vector2 currentTrigger;

	bool haveRedKey = false;
	bool haveGreenKey = false;
	bool haveBlueKey = false;

	public int checkpoints = 0;

    // Audio for collision with walls
    public AudioSource AudioSrc;
    public AudioClip CollisionSound;
    // Audio for grappling hook
    public AudioClip HookSound;

    bool prevSpace = false;

    // UI Death counter
    public Text deathsText;
    private int deathCount;

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

		if (Input.GetKeyDown(KeyCode.R))
		{
			respawnPlayer();
		}

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




		//Give the player red key
		if (other.tag == "RedKey")
		{
			haveRedKey = true;
			Debug.Log("Player Took The Red Key");
			Destroy(other.gameObject);
		}
		//Give the player red key
		if (other.tag == "GreenKey")
		{
			haveGreenKey = true;
			Debug.Log("Player Took The Green Key");
			Destroy(other.gameObject);
		}
		//Give the player red key
		if (other.tag == "BlueKey")
		{
			haveBlueKey = true;
			Debug.Log("Player Took The Blue Key");
			Destroy(other.gameObject);
		}





		//Check for RedKey on RedDoor, remove RedKey from player
		if (other.tag == "RedDoor" && haveRedKey == true)
		{
			Destroy(other.gameObject);
			haveRedKey = false;
		}
		//Check for GreenKey on RedDoor, remove RedKey from player
		if (other.tag == "GreenDoor" && haveGreenKey == true)
		{
			Destroy(other.gameObject);
			haveGreenKey = false;
		}
		//Check for RedKey on RedDoor, remove RedKey from player
		if (other.tag == "BlueDoor" && haveBlueKey == true)
		{
			Destroy(other.gameObject);
			haveBlueKey = false;
		}


		if (other.tag == "Finish")
		{
			//Add SomethingHere
			Debug.Log("Player Finished");
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
        deathCount += 1;
        deathsText.text = deathCount.ToString() + " deaths";

        switch (checkpoints)
        {
			case 0:
				rb.velocity = new Vector2(); ;
				this.transform.position = spawn.position; ;
				break;

			case 1:
                rb.velocity = new Vector2(); ;
                this.transform.position = spawnPoint1.position; ;
                break;

            case 2:
                rb.velocity = new Vector2();
                this.transform.position = spawnPoint2.position;
                break;

            case 3:
                rb.velocity = new Vector2();
                this.transform.position = spawnPoint3.position;
                break;

			case 4:
				rb.velocity = new Vector2();
				this.transform.position = spawnPoint4.position;
				break;

			case 5:
				rb.velocity = new Vector2();
				this.transform.position = spawnPoint5.position;
				break;

			case 6:
				rb.velocity = new Vector2();
				this.transform.position = spawnPoint6.position;
				break;

			default:
                break;
        }
    }
}
