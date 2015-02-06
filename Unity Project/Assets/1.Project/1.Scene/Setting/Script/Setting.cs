using UnityEngine;
using System.Collections;
using System;

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

	public ChooseNumberNGUI st1;
	public ChooseNumberNGUI st2;
	public ChooseNumberNGUI st3;
	public ChooseNumberNGUI st4;
	public ChooseNumberNGUI gt1;
	public ChooseNumberNGUI gt2;
	public ChooseNumberNGUI gt3;
	public ChooseNumberNGUI gt4;


	public GameObject cs;
	public GameObject cs_mail;
	public GameObject cs_ok;

	public GameObject time_error;
	public GameObject error_ok;


	public string st;
	public string gt;

	IFormatProvider culture;

	public bool time_good = true;

	void Awake(){



	}


	void Start () {


		culture = new System.Globalization.CultureInfo("zh-TW", true);

		try{

		

		st = PlayerPrefs.GetString ("st");
		gt = PlayerPrefs.GetString ("gt");


		if(st.Length>2 && gt.Length>2){
			st1.Set_number(st[0] -48);
			st2.Set_number(st[1] -48);
			st3.Set_number(st[2] -48);
			st4.Set_number(st[3]-48);
			gt1.Set_number(gt[0]-48);
			gt2.Set_number(gt[1]-48);
			gt3.Set_number(gt[2]-48);
			gt4.Set_number(gt[3]-48);
		}

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
		UIEventListener.Get (cs).onClick = Cs_click;
		UIEventListener.Get (cs_ok).onClick = Cs_mail_click;
		UIEventListener.Get (error_ok).onClick = Time_click;

		UIEventListener.Get (Sw_alarm).onDrag = Sw_drag;
		UIEventListener.Get (Sw_food).onDrag = Sw_drag;
		UIEventListener.Get (Sw_password).onDrag = Sw_drag;
		UIEventListener.Get (Sw_alarm).onDragEnd = Sw_dragend;
		UIEventListener.Get (Sw_food).onDragEnd = Sw_dragend;
		UIEventListener.Get (Sw_password).onDragEnd = Sw_dragend;

		}catch(Exception e){
			LwError.Show("Setting.Start() : " + e);
			
		}

		
	}

	void Time_click(GameObject obj){

		time_error.transform.localPosition = new Vector3 (-600 , 0 , 0);



	}

	void Cs_mail_click(GameObject obj){


		cs_mail.transform.localPosition = new Vector3 (600,0,0);


	}

	void Cs_click(GameObject obj){
		cs_mail.transform.localPosition = new Vector3 (0 , 0 ,0);


	}


	void Clock_click(GameObject obj){

		try{
			note_time ();


			Application.LoadLevel ("Water_clock");
		}catch(Exception e){


			LwError.Show("Setting.Start() : " + e);

			time_error.transform.localPosition = new Vector3(0 , 0 , 0);

		}

	}

	void Password_click(GameObject obj){

		try{
			note_time ();

			Application.LoadLevel ("InputPassword");
		}catch(Exception e){
			
			
			LwError.Show("Setting.Start() : " + e);
			
			time_error.transform.localPosition = new Vector3(0 , 0 , 0);
			
		}
	}

	void exit_click(GameObject obj){

		try{
			note_time ();

			Application.LoadLevel ("MainMenu");
		}catch(Exception e){
			
			
			LwError.Show("Setting.Start() : " + e);
			
			time_error.transform.localPosition = new Vector3(0 , 0 , 0);
			
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


			
			st = st1.chooseNumber.ToString()
				+st2.chooseNumber.ToString()
				+st3.chooseNumber.ToString()
				+st4.chooseNumber.ToString();
			gt = gt1.chooseNumber.ToString()
				+gt2.chooseNumber.ToString()
				+gt3.chooseNumber.ToString()
				+gt4.chooseNumber.ToString();

			DateTime tst = DateTime.ParseExact(st , "HHmm", culture);
			DateTime tet = DateTime.ParseExact(gt , "HHmm", culture);


			


			PlayerPrefs.SetString("st" , st);
			PlayerPrefs.Save();
			PlayerPrefs.SetString("gt" , gt);
			PlayerPrefs.Save();

	}

}
