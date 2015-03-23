using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChooseFood2 : MonoBehaviour {

	public GameObject bg;

	public static List<GameObject> bgs = new List<GameObject>();
	
	void Awake () {
		bgs.Add (bg);
		bg.SetActive (false);
	}

	void OnClick () {
		ShowBG ();
	}

	void ShowBG(){
		foreach(GameObject g in bgs){
			g.SetActive(false);
		}
		bg.SetActive (true);
	}

}
