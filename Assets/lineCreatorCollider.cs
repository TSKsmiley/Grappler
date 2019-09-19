using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineCreatorCollider : MonoBehaviour
{

	public LineRenderer lr;
    bool inTrigger = false;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Debug.Log("Player Enterd Trigger");
			inTrigger = true;
		}
	}

	void Update()
    {
		
		if (Input.GetKeyDown(KeyCode.Space) && inTrigger == true)
		{
			Debug.Log("Player is inside trigger and hit space");
			lr.SetPosition(1, this.transform.position);
		}
	}
}
