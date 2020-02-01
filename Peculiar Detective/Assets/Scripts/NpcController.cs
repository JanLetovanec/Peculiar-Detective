using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour 
{
	public GameObject interactButton;

	private GameMasterCtrl GmControl;
	private Transform tf;
	private bool inRange;

	[SerializeField]
	private string option;
	[SerializeField]
	private string[] arguments;

	void Start ()
	{
		if (interactButton == null) {
			Debug.Log("NO INTERACT_BUTTON");}
		GmControl = GameObject.FindObjectOfType<GameMasterCtrl> ();
		inRange = false;
		tf = this.GetComponent<Transform> ();
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
			GmControl.Control (option, arguments);
		}
	}
}
