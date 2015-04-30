using UnityEngine;
using System.Collections;

public class Teacher02 : MonoBehaviour {
	
	public GameObject cancel;
	public GameObject agree;

	public GameObject errorMark;
	public GameObject errorExit_Button;

	void Awake(){
//		errorMark.SetActive (false);
		UIEventListener.Get (errorExit_Button).onClick = ErrorExit_Button;
		UIEventListener.Get (agree).onClick = Agree;
	}

//	void Agree(GameObject obj){
//
//		if(){
//
//		}else{
//			errorMark.SetActive (true);
//		}
//	}

	void ErrorExit_Button(GameObject obj){
		errorMark.SetActive (false);
	}


	// Use this for initialization
	void Start () {
		
//		UIEventListener.Get (cancel).onClick = Cancel;
//		UIEventListener.Get (agree).onClick = Agree;
		
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void Cancel(GameObject obj){
		
		Application.LoadLevel ("User");
		
	}
	
	void Agree(GameObject obj){

		Grade g = new Grade ();

		int grade = g.GetTotalGrade ();

		int coachCost = 0;
		SaveMissionLv sml = new SaveMissionLv ();
		int lv = sml.GetMissionNowLv (10);
		int standardBaseCost = 10000;
		coachCost = lv * standardBaseCost;

		if(grade >= coachCost){
			Application.LoadLevel ("Teacher03");
		}else{
			errorMark.SetActive (true);
		}
//		Application.LoadLevel ("Teacher03");
		
	}
	
}