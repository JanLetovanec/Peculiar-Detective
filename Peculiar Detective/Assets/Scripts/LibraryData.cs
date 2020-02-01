using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book 
{
	public string name;
	public string[] tags;
	public string GUIpath;

	public Book(string _name, string[] _tags, string _gui)
	{
		name = _name;
		tags = _tags;
		GUIpath = "Books/" + _gui;
	}
} 

public class LibraryData : MonoBehaviour 
{
	private List<Book> books;

	//Declare Books
	void Start()
	{
		books = new List<Book> ();
		books.Add
		(
			new Book
			(
				"Cats did it",
				new string[] {"cat","detective","fiction"},
				"CatsDidIt"
			)
		);
		books.Add
		(
			new Book
			(
				"Alergies",
				new string[] {"cat","dog","allergy","biology","illness","cough","animal"},
				"CatsDidIt"
			)
		);
		books.Add
		(
			new Book
			(
				"Cat - biology and cat-o-logy",
				new string[] {"cat","biology","anatomy"},
				"CatsDidIt"
			)
		);
		books.Add
		(
			new Book
			(
				"Memes",
				new string[] {"cat","meme","internet", "humour"},
				"CatsDidIt"
			)
		);
	}


	public List<Book> Search (string[] keywords)
	{
		List<Book> result = new List<Book>();

		foreach (Book book in books)
		{
			bool FoundMatchAll = true;
			foreach (string keyword in keywords)
			{
				bool FoundMatchSingle = false;
				foreach (string tag in book.tags)
				{
					if (tag == keyword)
					{	FoundMatchSingle = true;}
				}
				if (FoundMatchSingle == false)
				{	FoundMatchAll = false;}
			}
			if (FoundMatchAll)
			{	result.Add(book);}
		}

		return result;
	}
}
