using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCtrl : MonoBehaviour 
{
	public int x,y;
	private Transform fishTransform;
	private Vector3 startPosition;

	void Start () 
	{
		x = Random.Range (7, 15);
		y = Random.Range (-2, -5);

		fishTransform = GetComponent<Transform> ();
		//Move fish to start position
		startPosition = new Vector3 (-8f + 0.25f, 4f - 0.25f, 0f);
		Vector3 deltaPosition = new Vector3 (x*0.5f, y*0.5f, 0f);
		fishTransform.position = startPosition + deltaPosition;
		//Register every fish
		foreach (CatCtrl cat in GameObject.FindObjectsOfType<CatCtrl>()) 
		{
			cat.SetFish ();
		}
	}

	public void GetEaten()
	{
		Debug.Log ("yum yum");
		Destroy (gameObject);
		foreach (CatCtrl cat in GameObject.FindObjectsOfType<CatCtrl>()) 
		{
			cat.UpToDateFish = false;
		}
	}
}
