using UnityEngine;
using System.Collections;

public class LwWeightMeasurements2 : MonoBehaviour {

	public GameObject buttonOk;

	// Unity Override Methods ==============================================================================================================================

	void Awake () {
		UIEventListener.Get(buttonOk).onClick = ButtonOk;
	}

	// Custom Methods ======================================================================================================================================

	void ButtonOk(GameObject button){
		Application.LoadLevel ("MainMenu");
	}

}
