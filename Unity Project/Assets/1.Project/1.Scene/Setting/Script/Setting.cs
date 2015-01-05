using UnityEngine;
using System.Collections;

public class Setting : MonoBehaviour {

	public GameObject buttonExit;

	void Start () {
		UIEventListener.Get (buttonExit).onClick = ButtonExit;
	}
	
	void ButtonExit(GameObject obj){
		Application.LoadLevel ("MainMenu");
	}
}
