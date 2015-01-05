using UnityEngine;
using System.Collections;

public class LwUser2 : MonoBehaviour {

	public GameObject buttonEdit;

	// Unity Override Methods ==============================================================================================================================

	void Awake () {
		UIEventListener.Get(buttonEdit).onClick = ButtonEdit;
	}

	// Custom Methods ======================================================================================================================================
	
	void ButtonEdit(GameObject button){
		Application.LoadLevel ("User3");
	}
}
