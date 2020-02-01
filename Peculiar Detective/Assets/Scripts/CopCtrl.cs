using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopCtrl : MonoBehaviour 
{
	public int x, y;
	private Transform copTransform;
	private MapCtrl map;
	private Vector3 startPosition;
	private const int vision = 5;

	// Use this for initialization
	void Start () 
	{
		x = Random.Range (7, 15);
		y = Random.Range (-4, -7);
		map = GameObject.FindObjectOfType<MapCtrl> ();
		copTransform = GetComponent<Transform> ();
		//Move cat to start position
		startPosition = new Vector3 (-8f + 0.25f, 4f - 0.25f, 0f);
		Vector3 deltaPosition = new Vector3 (x*0.5f, y*0.5f, 0f);
		copTransform.position = startPosition + deltaPosition;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void MakeTurn()
	{
		int direction = Random.Range (0, 5);
		Move (direction);
	}

	//Selects Move action - moves to next square, 0-down, 1-right, 2-up, 3-left
	bool Move ( int direction)
	{
		int tempX, tempY;
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
			return false;
		//Check, wether it is a valid move
		if (!map.IsFree (tempX, tempY)) {
			return false;
		} else 
		{
			x = tempX;
			y = tempY;
			Vector3 deltaPosition =  new Vector3 (x*0.5f, y*0.5f, 0f);
			copTransform.position = startPosition + deltaPosition;
			return true;
		}
	}
}
