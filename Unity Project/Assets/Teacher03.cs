using UnityEngine;
using System.Collections;

public class Teacher03 : MonoBehaviour {

	public GameObject cancel;
	public GameObject agree;

	public UILabel phone_l;
	public UILabel line_l;
	public UILabel skype_l;
	public UILabel smallsin_l;
	
	string phone_number;
	string line_id;
	string skype_id;
	string smallsin_id;



	
	// Use this for initialization
	void Start () {
		
		UIEventListener.Get (cancel).onClick = Cancel;
		UIEventListener.Get (agree).onClick = Agree;
		
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void Cancel(GameObject obj){
		
	//	Application.LoadLevel ("User");
		
	}

	//須確認資料
	void Agree(GameObject obj){
		
	//	Application.LoadLevel ("Teacher04");
		
	}
}
