using UnityEngine;
using System.Collections;

public class LwSports2 : MonoBehaviour {

	public GameObject buttonExit;
	public GameObject waiter;
	public GameObject sports;
	public GameObject weightMeasurements;

	public GameObject buttonOk;

	// Unity Override Methods ==============================================================================================================================

	void Awake () {
		UIEventListener.Get(buttonExit).onClick = ButtonExit;
		UIEventListener.Get(waiter).onClick = Waiter;
		UIEventListener.Get(sports).onClick = Sports;
		UIEventListener.Get(weightMeasurements).onClick = WeightMeasurements;
		UIEventListener.Get(buttonOk).onClick = ButtonOk;
	}
	
	// Custom Methods ======================================================================================================================================
	
	void ButtonExit(GameObject button){
		Application.LoadLevel ("MainMenu");
	}
	
	void Waiter(GameObject button){
		Application.LoadLevel ("Waiter");
	}
	
	void Sports(GameObject button){
		Application.LoadLevel ("Sports");
	}
	
	void WeightMeasurements(GameObject button){
		Application.LoadLevel ("WeightMeasurements");
	}

	void ButtonOk(GameObject button){
		Application.LoadLevel ("WeightMeasurements");
	}

}
