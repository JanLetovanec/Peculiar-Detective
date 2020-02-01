using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogCtrl : MonoBehaviour 
{
	public int x, y;
	public bool UpToDateBones = true;
	private BoneCtrl[] bones;
	private Transform dogTransform;
	private MapCtrl map;
	private Vector3 startPosition;
	private const int vision = 10;

	void Start () 
	{
		x = Random.Range (7, 15);
		y = Random.Range (-4, -7);
		bones = GameObject.FindObjectsOfType<BoneCtrl> ();
		map = GameObject.FindObjectOfType<MapCtrl> ();
		dogTransform = GetComponent<Transform> ();
		//Move dog to start position
		startPosition = new Vector3 (-8f + 0.25f, 4f - 0.25f, 0f);
		Vector3 deltaPosition = new Vector3 (x*0.5f, y*0.5f, 0f);
		dogTransform.position = startPosition + deltaPosition;	
	}

	public void SetBones ()
	{
		UpToDateBones = true;
		bones = null;
		bones = GameObject.FindObjectsOfType<BoneCtrl> ();
	}

	void Update ()
	{
		if (!UpToDateBones) {
			SetBones ();
		}
	}

	public void MakeTurn()
	{
		int direction;
		//Towards bones
		direction = SenseBones ();
		if (direction < 0) 
		{
			bones [-direction - 1].GetEaten ();
			bones = null;
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
			dogTransform.position = startPosition + deltaPosition;
			return true;
		}
	}

	int SenseBones()
	{
		//If there is no fish => return
		if (bones == null)
			return 4;
		int minimum = -1;
		int minIndex = -1;
		//Finds closest fish
		for (int i = 0; i < bones.Length; i++) 
		{
			if ( Mathf.Abs (bones [i].x - x) <= vision ) 
			{
				if (  Mathf.Abs (bones [i].y - y) <= vision )
				{
					int distance = Mathf.Abs (bones [i].x - x) + Mathf.Abs (bones [i].y - y);
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
		if ( bones [minIndex].x == x && bones [minIndex].y == y) {
			return -(minIndex +1);}
		//Determine what move should the cat do -should move along x axis?
		if (Mathf.Abs ( bones [minIndex].x - x) > Mathf.Abs ( bones [minIndex].y - y)) {
			if ( bones [minIndex].x - x > 0) {
				return 1;
			} else 
				return 3;
		} 
		else //or rather y axis
		{
			if ( bones [minIndex].y - y < 0){
				return 0;
			}
			else {
				return 2;
			}
		}
	}
}
