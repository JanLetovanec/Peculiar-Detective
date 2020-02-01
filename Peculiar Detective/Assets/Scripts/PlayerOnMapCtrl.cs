using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnMapCtrl : MonoBehaviour 
{
	[SerializeField]
	private GameObject sleepMarkPrefab;
	[SerializeField]
	private GameObject markPrefab;
	private GameObject mark;
	private MapCtrl map;
	private Transform playerTransform;
	[SerializeField]
	private int x, y ,tempX, tempY;
	private string action;
	private Vector3 startPosition;
	private bool selected = false;

	void Start ()
	{
		startPosition = new Vector3 (-8f + 0.25f, 4f - 0.25f, 0f); 
		map = GameObject.FindObjectOfType<MapCtrl> ();
		playerTransform = gameObject.GetComponent<Transform> ();
		playerTransform.position = startPosition;
		x = 0;
		y = 0;
		tempX = 0;
		tempY = 0;
		action = "";
		mark = null;

		if (markPrefab == null) {
			Debug.Log("NO MARK!");}
		if (sleepMarkPrefab == null) {
			Debug.Log("NO SLEEP MARK!");}
		if (map == null) {
			Debug.Log ("NO MAP!");}
	}

	// Checks for input
	void Update ()
	{
		if (!selected) 
		{
			if (Input.GetButtonDown ("Horizontal")) 
			{
				if (Input.GetAxis ("Horizontal") > 0f) {
					Move (1);
				} else {
					Move (3);}
			}
			if (Input.GetButtonDown ("Vertical")) 
			{
				if (Input.GetAxis ("Vertical") < 0f) {
					Move (0);
				} else {
					Move (2);}
			}
			if (Input.GetButtonDown ("Interact")) 
			{
				Interact ();
			}
		}
		if (Input.GetButtonDown ("Submit")) {
			SubmitTurn ();}

	}

	//Selects Move action - moves to next square, 0-down, 1-right, 2-up, 3-left
	void Move (int direction)
	{
		//Change temporary Co-ordinates
		if (direction == 0 && y > -15) {
			tempX = x;
			tempY = y - 1;
		} else if (direction == 1 && x < 31) {
			tempX = x + 1;
			tempY = y;
		} else if (direction == 2 && y < 0) {
			tempX = x;
			tempY = y + 1;
		} else if (direction == 3 && x > 0) {
			tempX = x - 1;
			tempY = y;
		} else
			return;
		if (!map.IsFree (tempX, tempY)) {
			return;}
		//create Mark
		if (mark != null) {
			Destroy(mark);
			mark = null;
		}
		Vector3 markPosition = 
			startPosition
			+ new Vector3 (0.5f * tempX, 0.5f * tempY, 0f);
		mark = Instantiate (markPrefab, markPosition, Quaternion.identity);
		//Flag as finished
		selected = true;
		action = "move";
	}

	//Submits turn, runs calculations, enemy moves and so on...
	void SubmitTurn ()
	{
		if (mark != null) {
			Destroy (mark);
			mark = null;
		}
		
		if (action == "move") 
		{
			x = tempX;
			y = tempY;
			Vector3 deltaPosition = new Vector3 (0.5f * tempX, 0.5f * tempY, 0f);
			playerTransform.position = startPosition + deltaPosition;
		}
		//Aftermath - other turns, reset vars
		if (true /*action != ""*/) 
		{
			action = "";
			map.TurnOrder();
			if (!map.CheckFreeOfCats (x, y)) {
				Sleep (5);
			}
			selected = false;
		}
	}

	//Faint / sleep
	void Sleep (int length)
	{
		selected = true;
		//Casts SleepMark
		Vector3 markPosition = 
			startPosition
			+ new Vector3 (0.5f*x - 0.2f, 0.5f*y + 0.2f, -0.5f);
		mark = Instantiate (sleepMarkPrefab, markPosition, Quaternion.identity);
		//Makes the world do its thing
		for (int i = 0; i < length; i++) 
		{
			map.TurnOrder ();
		}
		selected = false;
	}

	//Interact
	void Interact ()
	{
		Debug.Log ("INTERACT - finish GUI");
	}
}
