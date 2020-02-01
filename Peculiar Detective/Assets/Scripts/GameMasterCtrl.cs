using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterCtrl : MonoBehaviour 
{
	void Start ()
	{
		DontDestroyOnLoad(gameObject);

	}

	public void Control(string option, string[] arguments)
	{
		if (option == "ChangeScene") 
		{
			ChangeScene (arguments);
		}
	}

	void ChangeScene (string[] arguments)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene (arguments[0]);
	}
}
