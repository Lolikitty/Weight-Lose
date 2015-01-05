using UnityEngine;
using System.Collections;

public class LwSports : MonoBehaviour {

	public GameObject buttonExit;
	public GameObject waiter;
	public GameObject sports;
	public GameObject weightMeasurements;

	public GameObject button_1;
	public GameObject button_2;
	public GameObject button_3;
	public GameObject button_4;
	public GameObject button_5;
	public GameObject button_6;
	public GameObject button_7;
	public GameObject button_8;
	public GameObject button_9;

	// Unity Override Methods ==============================================================================================================================

	void Awake () {
		UIEventListener.Get(buttonExit).onClick = ButtonExit;
		UIEventListener.Get(waiter).onClick = Waiter;
		UIEventListener.Get(sports).onClick = Sports;
		UIEventListener.Get(weightMeasurements).onClick = WeightMeasurements;

		UIEventListener.Get(button_1).onClick = Button_1;
		UIEventListener.Get(button_2).onClick = Button_2;
		UIEventListener.Get(button_3).onClick = Button_3;
		UIEventListener.Get(button_4).onClick = Button_4;
		UIEventListener.Get(button_5).onClick = Button_5;
		UIEventListener.Get(button_6).onClick = Button_6;
		UIEventListener.Get(button_7).onClick = Button_7;
		UIEventListener.Get(button_8).onClick = Button_8;
		UIEventListener.Get(button_9).onClick = Button_9;
	}
	
	// Custom Methods ======================================================================================================================================

	void Button_1(GameObject button){
		Application.LoadLevel ("Sports2");
	}

	void Button_2(GameObject button){
		Application.LoadLevel ("Sports2");
	}

	void Button_3(GameObject button){
		Application.LoadLevel ("Sports2");
	}

	void Button_4(GameObject button){
		Application.LoadLevel ("Sports2");
	}

	void Button_5(GameObject button){
		Application.LoadLevel ("Sports2");
	}

	void Button_6(GameObject button){
		Application.LoadLevel ("Sports2");
	}

	void Button_7(GameObject button){
		Application.LoadLevel ("Sports2");
	}

	void Button_8(GameObject button){
		Application.LoadLevel ("Sports2");
	}

	void Button_9(GameObject button){
		Application.LoadLevel ("Sports2");
	}
	
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

}
