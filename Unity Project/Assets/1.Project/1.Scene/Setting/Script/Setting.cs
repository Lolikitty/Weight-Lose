using UnityEngine;
using System.Collections;

public class Setting : MonoBehaviour {

	public GameObject clock;
	public GameObject password;
	public GameObject exit;

	public GameObject Sw_alarm;
	public GameObject Sw_food;
	public GameObject Sw_password;

	public UISprite Bg_alarm;
	public UISprite Bg_food;
	public UISprite Bg_password;

	public GameObject script;
	LwInit script_sc;

	public GameObject st1;
	public GameObject st2;
	public GameObject st3;
	public GameObject st4;
	public GameObject gt1;
	public GameObject gt2;
	public GameObject gt3;
	public GameObject gt4;


	//整合好的起床跟睡眠時間
	public string st;
	public string gt;

	public bool time_good = true;


	void Start () {

		script = GameObject.Find ("Script");
		script_sc = script.GetComponent<LwInit>();

		if (script_sc.go_clock == true) {
			Bg_alarm.spriteName = "open";

			Vector3 now = Sw_alarm.transform.localPosition;

			Sw_alarm.transform.localPosition = new Vector3(105 , now.y, now.z);
		}

		if (script_sc.go_food == true) {
			Bg_food.spriteName = "open";
			
			Vector3 now = Sw_food.transform.localPosition;
			
			Sw_food.transform.localPosition = new Vector3(105 , now.y, now.z);
		}

		if (script_sc.go_password == true) {
			Bg_password.spriteName = "open";
			
			Vector3 now = Sw_password.transform.localPosition;
			
			Sw_password.transform.localPosition = new Vector3(105 , now.y, now.z);
		}


		UIEventListener.Get (clock).onClick = Clock_click;
		UIEventListener.Get (password).onClick = Password_click;
		UIEventListener.Get (exit).onClick = exit_click;

		UIEventListener.Get (Sw_alarm).onDrag = Sw_drag;
		UIEventListener.Get (Sw_food).onDrag = Sw_drag;
		UIEventListener.Get (Sw_password).onDrag = Sw_drag;
		UIEventListener.Get (Sw_alarm).onDragEnd = Sw_dragend;
		UIEventListener.Get (Sw_food).onDragEnd = Sw_dragend;
		UIEventListener.Get (Sw_password).onDragEnd = Sw_dragend;
	}


	void Clock_click(GameObject obj){

		note_time ();

		if(time_good == true){

			Application.LoadLevel ("Water_clock");
		}
	}

	void Password_click(GameObject obj){

		note_time ();

		if (time_good == true) {
		
			Application.LoadLevel ("SetPassword");
		}
	}

	void exit_click(GameObject obj){

		note_time ();


		if(time_good == true){
			Application.LoadLevel ("MainMenu");
		}

	}

	//85 105

	void Sw_drag(GameObject obj , Vector2 delta){

		if(obj.transform.localPosition.x >= 85 || obj.transform.localPosition.x <= 105){

			obj.transform.localPosition += new Vector3 (delta.x, 0 , 0);

		}
		Vector3 now = obj.transform.localPosition;

		if (obj.transform.localPosition.x < 85) {



			obj.transform.localPosition = new Vector3(85 , now.y , now.z);
		}

		if (obj.transform.localPosition.x > 105) {
			
			
			
			obj.transform.localPosition = new Vector3(105 , now.y , now.z);
		}

		if (obj.transform.localPosition.x >= 95) {

			if(obj.name == "Wb"){

				script_sc.go_clock = true;

				PlayerPrefs.SetString("go_clock","true");
				
				PlayerPrefs.Save();

				Bg_alarm.spriteName = "open" ;



			}else if(obj.name == "Fb"){

				script_sc.go_food = true;

				PlayerPrefs.SetString("go_food","true");
				
				PlayerPrefs.Save();

				Bg_food.spriteName = "open";

			}else{

				script_sc.go_password = true;

				PlayerPrefs.SetString("go_password","true");
				
				PlayerPrefs.Save();

				Bg_password.spriteName = "open";

			}
		}

		if (obj.transform.localPosition.x < 95) {
			
			if(obj.name == "Wb"){

				script_sc.go_clock = false;

				PlayerPrefs.SetString("go_clock","false");

				PlayerPrefs.Save();

				Bg_alarm.spriteName = "close" ;
				
			}else if(obj.name == "Fb"){

				script_sc.go_food = false;

				PlayerPrefs.SetString("go_food","false");

				PlayerPrefs.Save();
				
				Bg_food.spriteName = "close";
				
			}else{

				script_sc.go_password = false;

				PlayerPrefs.SetString("go_clock","false");

				PlayerPrefs.Save();


				Bg_password.spriteName = "close";
				
			}
		}


	}

	void Sw_dragend(GameObject obj){

		Vector3 now = obj.transform.localPosition;

		if (obj.transform.localPosition.x >= 95) {
			obj.transform.localPosition = new Vector3(105 , now.y,now.z);
		}
		
		if (obj.transform.localPosition.x < 95) {
			obj.transform.localPosition = new Vector3(85 , now.y,now.z);
		}

	}


	void note_time(){


		if (st1.GetComponent<ChooseNumberNGUI> ().chooseNumber > 2 || st3.GetComponent<ChooseNumberNGUI> ().chooseNumber > 5
		|| gt1.GetComponent<ChooseNumberNGUI> ().chooseNumber > 2 || gt3.GetComponent<ChooseNumberNGUI> ().chooseNumber > 5) {
			time_good = false;

		}else{
			time_good = true;

			st = st1.GetComponent<ChooseNumberNGUI> ().chooseNumber.ToString()
				+st2.GetComponent<ChooseNumberNGUI> ().chooseNumber.ToString()
				+st3.GetComponent<ChooseNumberNGUI> ().chooseNumber.ToString()
				+st4.GetComponent<ChooseNumberNGUI> ().chooseNumber.ToString();
			gt = gt1.GetComponent<ChooseNumberNGUI> ().chooseNumber.ToString()
				+gt2.GetComponent<ChooseNumberNGUI> ().chooseNumber.ToString()
				+gt3.GetComponent<ChooseNumberNGUI> ().chooseNumber.ToString()
				+gt4.GetComponent<ChooseNumberNGUI> ().chooseNumber.ToString();





		}

	}

}
