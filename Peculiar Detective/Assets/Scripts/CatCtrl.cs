using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCtrl : MonoBehaviour 
{
	public int x, y;
	public bool UpToDateFish = true;
	private FishCtrl[] fish;
	private DogCtrl[] dogs;
	private Transform catTransform;
	private MapCtrl map;
	private Vector3 startPosition;
	private const int vision = 5;

	void Start ()
	{
		x = Random.Range (7, 15);
		y = Random.Range (-4, -7);
		fish = GameObject.FindObjectsOfType<FishCtrl> ();
		dogs = GameObject.FindObjectsOfType<DogCtrl> ();
		map = GameObject.FindObjectOfType<MapCtrl> ();
		catTransform = GetComponent<Transform> ();
		//Move cat to start position
		startPosition = new Vector3 (-8f + 0.25f, 4f - 0.25f, 0f);
		Vector3 deltaPosition = new Vector3 (x*0.5f, y*0.5f, 0f);
		catTransform.position = startPosition + deltaPosition;
	}

	void Update ()
	{
		if (!UpToDateFish)
			SetFish ();
	}

	public void SetFish ()
	{
		UpToDateFish = true;
		fish = null;
		fish = GameObject.FindObjectsOfType<FishCtrl> ();
	}

	public void MakeTurn()
	{
		//Away from dog
		int direction = SenseDog ();
		if (direction != 4) 
		{
			Move (direction);
			return;
		}
		//Towards fish
		direction = SenseFish ();
		if (direction < 0) 
		{
			fish [-direction - 1].GetEaten ();
			fish = null;
			return;
		}
		//Random
		if (direction == 4) {
			direction = Random.Range (0, 5);}
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
			catTransform.position = startPosition + deltaPosition;
			return true;
		}
	}

	int SenseDog ()
	{
		//If there is no dog => return
		if (dogs == null)
			return 4;
		int minimum = -1;
		int minIndex = -1;
		//Finds closest dog
		for (int i = 0; i < dogs.Length; i++) 
		{
			if ( Mathf.Abs (dogs [i].x - x) <= vision ) 
			{
				if (  Mathf.Abs (dogs [i].y - y) <= vision )
				{
					int distance = Mathf.Abs (dogs [i].x - x) + Mathf.Abs (dogs [i].y - y);
					if (minimum == -1 || distance < minimum) {
						minimum = distance;
						minIndex = i;}
				}
			}
		}
		//Return if there is no dog in sight
		if (minIndex == -1) {
			return 4;}
		//Determine what move should the cat do -should move along x axis?
		if (Mathf.Abs (dogs [minIndex].x - x) > Mathf.Abs (dogs [minIndex].y - y)) {
			if (dogs [minIndex].x - x > 0) {
				return 3;
			} else
				return 1;
		} 
		else //or rather y axis
		{
			if (dogs [minIndex].y - y < 0){
				return 2;
			}
			else {
				return 0;
			}
		}
	}

	int SenseFish()
	{
		//If there is no fish => return
		if (fish == null)
			return 4;
		int minimum = -1;
		int minIndex = -1;
		//Finds closest fish
		for (int i = 0; i < fish.Length; i++) 
		{
			if ( Mathf.Abs (fish [i].x - x) <= vision ) 
			{
				if (  Mathf.Abs (fish [i].y - y) <= vision )
				{
					int distance = Mathf.Abs (fish [i].x - x) + Mathf.Abs (fish [i].y - y);
					if (minimum == -1 || distance < minimum) {
						minimum = distance;
						minIndex = i;}
				}
			}
		}
		//Return if there is no fish in sight
		if (minIndex == -1) {
			return 4;}
		//Return if standing on fish
		if (fish [minIndex].x == x && fish [minIndex].y == y) {
			return -(minIndex +1);}
		//Determine what move should the cat do -should move along x axis?
		if (Mathf.Abs (fish [minIndex].x - x) > Mathf.Abs (fish [minIndex].y - y)) {
			if (fish [minIndex].x - x > 0) {
				return 1;
			} else 
				return 3;
		} 
		else //or rather y axis
		{
			if (fish [minIndex].y - y < 0){
				return 0;
			}
			else {
				return 2;
			}
		}
	}
}
