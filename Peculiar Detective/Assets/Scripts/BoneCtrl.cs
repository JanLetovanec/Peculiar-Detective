using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneCtrl : MonoBehaviour 
{
	public int x,y;
	private Transform boneTransform;
	private Vector3 startPosition;

	void Start () 
	{
		x = Random.Range (7, 15);
		y = Random.Range (-2, -5);

		boneTransform = GetComponent<Transform> ();
		//Move bone to start position
		startPosition = new Vector3 (-8f + 0.25f, 4f - 0.25f, 0f);
		Vector3 deltaPosition = new Vector3 (x*0.5f, y*0.5f, 0f);
		boneTransform.position = startPosition + deltaPosition;
		//Update bone array at every dog
		foreach (DogCtrl dog in GameObject.FindObjectsOfType<DogCtrl>()) 
		{
			dog.SetBones ();
		}
	}

	public void GetEaten()
	{
		Debug.Log ("yum yum");
		Destroy (gameObject);
		foreach (DogCtrl dog in GameObject.FindObjectsOfType<DogCtrl>()) 
		{
			dog.UpToDateBones = false;
		}
	}
}
