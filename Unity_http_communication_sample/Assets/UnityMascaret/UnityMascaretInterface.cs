using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mascaret;
using System.IO;
using UnityEngine.UI;


public class UnityMascaretInterface : MonoBehaviour {
	
	public VRApplication mascaret;
	public GameObject gui_interface;
	private string textToShow;
//	private Dictionary<string, OSDWindow> windows = new Dictionary<string, OSDWindow> ();

	public GameObject text;
	public GameObject menu3D;

	public void printEntity(string entityName)
	{
		//OSDWindow window = new OSDWindow (entityName);
		//	windows.Add (entityName, window);
	}

	public void destroyObject(GameObject obj)
	{
		Destroy (obj);
	}

	public GameObject instantiateObject(string objName)
	{
		return (GameObject)(Resources.Load (objName));
	}
	
	public void hideEntity(string entityName)
	{
		//windows.Remove (entityName);
		//Debug.Log ("TOPRINT == FALSE");
	}
	
	void Start()
	{
		mascaret = VRApplication.Instance;
		//gui_interface = GameObject.Find("GUI");
		//text.SetActive (false);


		//gui_interface.SetActive (false);
		
	}
	
	public void OnGUI()
	{/*
		if (textToShow != "") 
		{
			Text t = text.GetComponent<Text> ();
			t.text = textToShow;
		}
		*/
	}

	public void showText(string text)
	{
		textToShow = text;
		this.text.SetActive (true);
	}

	public void hideText()
	{
		this.text.SetActive (false);
	}

	
}

