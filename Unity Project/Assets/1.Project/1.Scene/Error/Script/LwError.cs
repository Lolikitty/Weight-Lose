using UnityEngine;
using System.Collections;

public class LwError : MonoBehaviour {

	static string eMsg = "Good !!";

	public UILabel ErrorMsg;
	public GameObject buttonQuit;
	public GameObject buttonRestart;

	// Unity Override Methods ==============================================================================================================================

	void Awake () {
		ErrorMsg.text = eMsg;
		UIEventListener.Get(buttonQuit).onClick = ButtonQuit;
		UIEventListener.Get(buttonRestart).onClick = ButtonRestart;
	}

	// Custom Methods ======================================================================================================================================

	void ButtonQuit(GameObject button){
		Application.Quit ();
	}

	void ButtonRestart(GameObject button){
		Application.LoadLevel (0);
	}

	public static void Show (string errorMsg) {
		eMsg = errorMsg;
		Application.LoadLevel ("Error");
	}
}
