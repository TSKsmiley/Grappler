﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineMaker : MonoBehaviour
{

	LineRenderer lr;
    Rigidbody2D rb;
    public float speed;

	bool inTrigger = false;
	bool grappleActive = false;
    
    
    void Start()
    {
		lr = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
		lr.startWidth = 0F;
		lr.endWidth = 0F;
	}

    
    void Update()
    {
		lr.SetPosition(0, this.transform.position);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			grappleActive = true;
			lr.startWidth = 0.1F;
			lr.endWidth = 0.1F;
		}
		if (Input.GetKey(KeyCode.Space) && inTrigger == true)
		{
			Debug.Log("Player is inside trigger and hit space");
			lr.SetPosition(1, this.transform.position);
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
		}
	}

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Debug.Log("Player Enterd Trigger");
			inTrigger = true;
		}
	}
}