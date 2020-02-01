using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToButton : MonoBehaviour 
{
	public string path = "";

	public void GoTo()
	{
		Instantiate (Resources.Load (path));
	}
}
