using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCtrl : MonoBehaviour 
{
	public CatCtrl[] cats;
	public DogCtrl[] dogs;
	public int timer;
	public Text mapGUI;

	void Start()
	{
		timer = 0;
		cats = GameObject.FindObjectsOfType<CatCtrl> ();
		dogs = GameObject.FindObjectsOfType<DogCtrl> ();
	}

	public bool IsFree (int x, int y)
	{
		//Checks obstacles and buildings
		if ((x == 3 || x == 4) && (y == -1 || y == -2)) {
			return false;
		}
		//Checks Living creatures
		else if ( CheckFreeOfCats (x, y) ) 
		{
			if ( CheckFreeOfDogs (x, y) ) {
				return true;
			} 
			else {
				return false;
			}
		}
		else return false;
	}

	public bool CheckFreeOfCats(int x, int y)
	{
		foreach (CatCtrl cat in cats) 
		{
			if (cat.x == x && cat.y == y) {
				return false;}
		}
		return true;
	}

	bool CheckFreeOfDogs(int x, int y)
	{
		foreach (DogCtrl dog in dogs) 
		{
			if (dog.x == x && dog.y == y) {
				return false;}
		}
		return true;
	}

	public void TurnOrder ()
	{
		foreach (CatCtrl cat in cats) 
		{
			cat.MakeTurn ();		
		}
		foreach (DogCtrl dog in dogs) 
		{
			dog.MakeTurn ();
		}
		timer++;
		mapGUI.text = timer.ToString();
	}
}																	