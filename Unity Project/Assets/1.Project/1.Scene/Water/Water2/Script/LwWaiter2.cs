using UnityEngine;
using System.Collections;

public class LwWaiter2 : MonoBehaviour {

	public UISlider uis;
	public GameObject WaiterBottomObj;
	public TextMesh NowCC;

	public GameObject buttonExit;
	public GameObject waiter;
	public GameObject sports;
	public GameObject weightMeasurements;
	public GameObject buttonOk;
	public GameObject buttonBack;


	public static int WaiterValue;
	public static int UseCupCC = 200;

	// Unity Override Methods ==============================================================================================================================

	void Awake () {
//		UIEventListener.Get(buttonExit).onClick = ButtonExit;
//		UIEventListener.Get(waiter).onClick = Waiter;
//		UIEventListener.Get(sports).onClick = Sports;
//		UIEventListener.Get(weightMeasurements).onClick = WeightMeasurements;
		UIEventListener.Get(buttonOk).onClick = ButtonOk;

		UIEventListener.Get(buttonBack).onClick = ButtonBack;
	}

	void Update () {
		WaiterBottomObj.SetActive(uis.value < 0.02f ? false : true);
		WaiterValue = (int)(uis.value * UseCupCC);
		NowCC.text = UseCupCC - WaiterValue + " c.c.";
	}

	// Custom Methods ======================================================================================================================================

	void ButtonExit(GameObject button){
		Application.LoadLevel ("MainMenu");
	}
	
	void Waiter(GameObject button){
		Application.LoadLevel ("Water");
	}
	
	void Sports(GameObject button){
		Application.LoadLevel ("Sports");
	}
	
	void WeightMeasurements(GameObject button){
		Application.LoadLevel ("WeightMeasurements");
	}


	void ButtonOk(GameObject button){
		print ("ok : " + WaiterValue + " c.c.");
		Application.LoadLevel ("Water3");
	}

	void ButtonBack(GameObject obj){
		Application.LoadLevel ("Water");
	}
}
