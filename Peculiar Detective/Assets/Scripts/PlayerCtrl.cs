using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {

	//private Transform playerTransform;
	private Rigidbody2D playerRigidBody;

	[SerializeField]
	private float speed = 2f;

	void Start () 
	{
		//playerTransform = gameObject.GetComponent<Transform> ();
		playerRigidBody = gameObject.GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () 
	{
		if (Input.GetAxis ("Horizontal") != 0f ) 
		{
			Vector2 force = new Vector2 (Input.GetAxis("Horizontal") * speed, playerRigidBody.velocity.y);
			playerRigidBody.velocity = force;
		}
	}
}
