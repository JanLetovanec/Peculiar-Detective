using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LibraryGuiCtrl : MonoBehaviour 
{
	public string rawKeywords; 
	private LibraryData libData;

	private Transform panelTransform;
	public GameObject panelFirst;
	public GameObject panelSecond;
	public GameObject panelThird;
	public GameObject panelWarning;
	private GameObject[] panels = { null, null, null };
	private GameObject warning = null;

	void Start() 
	{
		rawKeywords = "";
		libData = FindObjectOfType<LibraryData> ();
		panelTransform = gameObject.GetComponent<Transform> ().Find ("Panel");

		if (libData == null) {
			Debug.Log ("NO LIBRARY_DATA!");}
		if (panelFirst == null) {
			Debug.Log ("NO PANEL_1!");}
		if (panelSecond == null) {
			Debug.Log ("NO PANEL_2!");}
		if (panelThird == null) {
			Debug.Log ("NO PANEL_3!");}
		if (panelThird == null) {
			Debug.Log ("NO PANEL_WARNING!");}
	}


	public void OnValueChange (string value)
	{
		rawKeywords = value;
	}

	//Parses raw input into keywords array
	string[] ParseRawKeywords (string _rawKeywords)
	{
		int i = 0;
		int k = 0;
		List <string> keywords = new List<string> ();

		//Keep adding keywords until there is no more spaces
		while (i != -1)
		{
			k = _rawKeywords.IndexOf (' ', i + 1);
			char[] tempKey;
			//If there is space, copy
			if (k != -1) 
			{
				if (i == 0) {
					tempKey = new char[k - i];
					_rawKeywords.CopyTo (i, tempKey, 0, k - i);
				} else {
					tempKey = new char[k - i -1];
					_rawKeywords.CopyTo (i + 1, tempKey, 0, k - i -1);
				}

			} 
			// Else,(if there is no space), copy the rest
			else 
			{
				if (i != 0) 
				{
					tempKey = new char[_rawKeywords.Length - i -1];
					_rawKeywords.CopyTo (i + 1, tempKey, 0, _rawKeywords.Length - i - 1);
				} else 
				{
					tempKey = new char[_rawKeywords.Length - i];
					_rawKeywords.CopyTo (i, tempKey, 0, _rawKeywords.Length - i);
				}
			}
			i = k;
			keywords.Add ( new string (tempKey) );
		}
		return ListToArray (keywords);
	}

	//Turns list into an array
	string [] ListToArray (List<string> list)
	{
		int i = 0;
		string[] array = new string[list.Count];
		foreach (string element in list) 
		{
			array [i] = element;
			i++;
		}
		return array;	
	}

	//Controls result display
	void DisplayResult (List<Book> resultBooks)
	{
		//Clear last search panels
		if (panels [0] != null) {
			Destroy (panels[0]);}
		if (panels [1] != null) {
			Destroy (panels[1]);}
		if (panels [2] != null) {
			Destroy (panels[2]);}
		if (warning != null) {
			Destroy (warning);}
		//Print new panels
		if (resultBooks.Count >= 1) {
			panels [0] = Instantiate (panelFirst, panelTransform);
			PrintPanel (0, resultBooks [0].name, resultBooks [0].tags, resultBooks[0].GUIpath);
		}
		if (resultBooks.Count >= 2) {
			panels [1] = Instantiate (panelSecond, panelTransform);
			PrintPanel (1, resultBooks [1].name, resultBooks [1].tags, resultBooks[1].GUIpath);
		}
		if (resultBooks.Count >= 3) {
			panels [2] = Instantiate (panelThird, panelTransform);
			PrintPanel (2, resultBooks [2].name, resultBooks [2].tags, resultBooks[2].GUIpath);
		}
		if (resultBooks.Count > 3) {
			warning = GameObject.Instantiate(panelWarning, panelTransform);}
	}
	// Refactored function - prints text on panels
	void PrintPanel (int panelID, string name, string[] tags, string _GUIpath)
	{
		//print name
		Text panelText;
		panelText = panels [panelID].GetComponentInChildren<Text> ();
		panelText.text = name + "\n";
		//print keywords
		for (int i = 0; i < tags.Length; i++) 
		{
			panelText.text = panelText.text + tags [i];
			if (i != tags.Length - 1) {
				panelText.text = panelText.text + " ,";}
		}
		//Assigns GUIpath
		panels[panelID].GetComponentInChildren<GoToButton>().path = _GUIpath;
	}

	//Calls search on LibraryData
	public void PerformSearch()
	{
		//prevent NullError
		if (rawKeywords == "") {
			return;}
		
		string[] keys = ParseRawKeywords (rawKeywords);
		List<Book> result = libData.Search (keys);
		DisplayResult(result);
	}

	//Cancel button
	public void Cancel()
	{
		Destroy (gameObject);
	}
}
