using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineMaker : MonoBehaviour
{

	LineRenderer lr;
    Rigidbody2D rb;
    public float speed;
	SpringJoint2D sj;
	Rigidbody2D currentBody;

	public bool inTrigger = false;
	bool grappleActive = false;

	Vector2 currentTrigger;

    void Start()
    {
		lr = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
		sj = GetComponent<SpringJoint2D>();
		lr.startWidth = 0F;
		lr.endWidth = 0F;
	}

    
    void Update()
    {
		lr.SetPosition(0, this.transform.position);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (inTrigger == true)
			{
				sj.enabled = true;
				grappleActive = true;
				lr.startWidth = 0.1F;
				lr.endWidth = 0.1F;
				
			}
		}
		if (Input.GetKey(KeyCode.Space))
		{
			lr.SetPosition(1, currentTrigger);
			sj.connectedBody = currentBody;
		}
		if (Input.GetKeyUp(KeyCode.Space))
		{
			lr.startWidth = 0F;
			lr.endWidth = 0F;
			grappleActive = false;
			
		}

		if (grappleActive == false)
		{
			lr.SetPosition(1, this.transform.position);
			sj.enabled = false;
		}
	}

	//void FixedUpdate()
	//{
	//	float moveHorizontal = Input.GetAxis("Horizontal");
	//	float moveVertical = Input.GetAxis("Vertical");

	//	Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
	//	rb.velocity = movement * speed;
	//}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Hook")
		{
			currentTrigger = other.transform.position;
			currentBody = other.attachedRigidbody;
			Debug.Log("Player Enterd Trigger");
			inTrigger = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Hook")
		{
			Debug.Log("Player Left Trigger");
			inTrigger = false;
		}
	}
}
