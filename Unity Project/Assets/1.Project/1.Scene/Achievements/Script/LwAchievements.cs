using UnityEngine;
using System.Collections;

public class LwAchievements : MonoBehaviour {

	public GameObject buttonExit;

	// Unity Override Methods ==============================================================================================================================

	void Start () {
		UIEventListener.Get(buttonExit).onClick = ButtonExit;
	}

	void Update () {	
	}

	// Custom Methods ======================================================================================================================================

	void ButtonExit(GameObject obj){
		Application.LoadLevel ("MainMenu");
	}
}
