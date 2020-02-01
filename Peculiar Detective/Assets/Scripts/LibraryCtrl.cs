using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryCtrl : MonoBehaviour 
{
	public GameObject interactButton;
	public GameObject SearchGUI;

	private Transform tf;
	private bool inRange;
	//private LibraryData libraryData;

	void Start ()
	{
		if (interactButton == null) {
			Debug.Log("NO INTERACT_BUTTON");}
		if (SearchGUI == null) {
			Debug.Log("NO SEARCH_GUI");}
		
		inRange = false;
		tf = this.GetComponent<Transform> ();
		//libraryData = gameObject.GetComponent<LibraryData> ();
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			Vector2 deltaPosition= new Vector2 (-0.4f + tf.position.x, 1.25f + tf.position.y);
			GameObject.Instantiate (interactButton, deltaPosition, Quaternion.identity); 
			inRange = true;
		}

	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player") ) 
		{
			inRange = false;
			Destroy (GameObject.Find("Button(Clone)").gameObject);
		}

	}

	void Update () 
	{
		if (Input.GetButtonDown ("Interact") && inRange) 
		{
			GameObject.Instantiate (SearchGUI);
		}
	}
}
