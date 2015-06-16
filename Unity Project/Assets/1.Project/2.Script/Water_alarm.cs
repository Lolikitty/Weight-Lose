using UnityEngine;
using System.Collections;
using System;

public class Water_alarm : MonoBehaviour {


	//0~9群
	public ChooseNumberNGUI st1;
	public ChooseNumberNGUI st2;
	public ChooseNumberNGUI st3;
	public ChooseNumberNGUI st4;
	public ChooseNumberNGUI et1;
	public ChooseNumberNGUI et2;
	public ChooseNumberNGUI et3;
	public ChooseNumberNGUI et4;
	public ChooseNumberNGUI ct1;
	public ChooseNumberNGUI ct2;

	public GameObject script;
	LwInit script_sc;

	//轉化09群
	string st_h;
	string st_m;
	string et_h;
	string et_m;
	string ct;

	//圈圈中間的數字
	public UILabel next_time;

	//離開
	public GameObject Exit;
	public GameObject error_check;


	public GameObject error;

	//文化時間
	IFormatProvider culture;


	//時間
	DateTime st;
	DateTime et;
	DateTime clock;
	DateTime now;

	//條件成立嗎





	void Start () {
		NativePlugin.RegisterForNotifications();

		try{
			script = GameObject.Find("Script");
			script_sc = script.GetComponent<LwInit>();

			culture = new System.Globalization.CultureInfo("zh-TW", true);

			UIEventListener.Get (Exit).onClick = Exit_d;
			UIEventListener.Get (error_check).onClick = error_check_f;

			if (script_sc.go == true) {
				string wc_f = PlayerPrefs.GetString("wc_from");
				string wc_e = PlayerPrefs.GetString("wc_end");
				string wc_c = PlayerPrefs.GetString("wc_ct");

				if(wc_f.Length > 2 && wc_e.Length >2 && wc_c.Length > 1){

					st1.Set_number(wc_f[0]-48);
					st2.Set_number(wc_f[1]-48);
					st3.Set_number(wc_f[2]-48);
					st4.Set_number(wc_f[3]-48);
					et1.Set_number(wc_e[0]-48);
					et2.Set_number(wc_e[1]-48);
					et3.Set_number(wc_e[2]-48);
					et4.Set_number(wc_e[3]-48);
					ct1.Set_number(wc_c[0]-48);
					ct2.Set_number(wc_c[1]-48);
				}

				//next_time.text = Convert.ToInt32(Math.Ceiling( script_sc.clock.Subtract(now).TotalMinutes)).ToString();

			}
		}catch(Exception e){
			LwError.Show("Setting.Start() : " + e);
		}
	
	}
	
	// Update is called once per frame

	void Update(){

		if (script_sc.go == true) {

			next_time.text = script_sc.next_time;
		
		}else{


			next_time.text = "0";
		}


	}

	void Exit_d(GameObject obj){

		//時間轉換成小時分鐘
		st_h = st1.chooseNumber.ToString() + st2.chooseNumber.ToString();
		st_m = st3.chooseNumber.ToString() + st4.chooseNumber.ToString();
		et_h = et1.chooseNumber.ToString() + et2.chooseNumber.ToString();
		et_m = et3.chooseNumber.ToString() + et4.chooseNumber.ToString();
		ct = ct1.chooseNumber.ToString() + ct2.chooseNumber.ToString();


		if (ct != "00") {
				
			now = DateTime.Now;
			
			//dateTime

			try{
				st = DateTime.ParseExact(st_h + st_m , "HHmm", culture);
				et = DateTime.ParseExact(et_h + et_m , "HHmm", culture);

				PlayerPrefs.SetString(  "wc_from" ,st_h + st_m);
				PlayerPrefs.Save();
				PlayerPrefs.SetString(  "wc_end" ,et_h + et_m);
				PlayerPrefs.Save();
				PlayerPrefs.SetString( "wc_ct" , ct);

				if(DateTime.Compare(now , et) >= 0 ){
					
					et =  et.AddDays(1);
					
					Debug.Log(et.ToString());
					
				} 
				
				
				//幫數字合理化
				if(DateTime.Compare(st , et) >= 0 ){
					
					et =  et.AddDays(1);
					
					Debug.Log(et.ToString());
					
				} 
				
				clock = st.AddMinutes (Convert.ToDouble(ct));
				
				
				while(DateTime.Compare(now ,clock)>= 0){
					
					clock =  clock.AddMinutes(Convert.ToDouble(ct));
					
					
				}
				
				Debug.Log (now.ToString());
				
				if(DateTime.Compare(clock , now) >= 0){

					script_sc.clock_start(st , et , clock , ct);

					NativePlugin.instance.ShowNotification("喝水提醒", 60 * int.Parse(ct), "減肥同學會", "10");

					Application.LoadLevel("Setting");
				}
			}catch(Exception e){
				Debug.LogError(e.Message);

				error.transform.localPosition = new Vector3(0,0,0);

			}

		}

	}

	void error_check_f(GameObject obj){
		script_sc.go = false;

		Application.LoadLevel ("Setting");

	}


}
