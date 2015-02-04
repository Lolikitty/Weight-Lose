using UnityEngine;
using System.Collections;
using System;

public class Water_alarm : MonoBehaviour {


	//0~9群
	public GameObject st1;
	public GameObject st2;
	public GameObject st3;
	public GameObject st4;
	public GameObject et1;
	public GameObject et2;
	public GameObject et3;
	public GameObject et4;
	public GameObject ct1;
	public GameObject ct2;

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


		script = GameObject.Find("Script");
		script_sc = script.GetComponent<LwInit>();

		culture = new System.Globalization.CultureInfo("zh-TW", true);

		UIEventListener.Get (Exit).onClick = Exit_d;
		UIEventListener.Get (error_check).onClick = error_check_f;

		if (script_sc.go == true) {



		}
	
	}
	
	// Update is called once per frame

	void Update(){

		if (script_sc.go == true) {

			next_time.text = script_sc.next_time;
		
		}else{


			next_time.text = "0";
		}

		next_time.text = Convert.ToInt32(Math.Ceiling( clock.Subtract(now).TotalMinutes)).ToString();


	}

	void Exit_d(GameObject obj){

		//時間轉換成小時分鐘
		st_h = st1.GetComponent<ChooseNumberNGUI>().chooseNumber.ToString() + st2.GetComponent<ChooseNumberNGUI>().chooseNumber.ToString();
		st_m = st3.GetComponent<ChooseNumberNGUI>().chooseNumber.ToString() + st4.GetComponent<ChooseNumberNGUI>().chooseNumber.ToString();
		et_h = et1.GetComponent<ChooseNumberNGUI>().chooseNumber.ToString() + et2.GetComponent<ChooseNumberNGUI>().chooseNumber.ToString();
		et_m = et3.GetComponent<ChooseNumberNGUI>().chooseNumber.ToString() + et4.GetComponent<ChooseNumberNGUI>().chooseNumber.ToString();
		ct = ct1.GetComponent<ChooseNumberNGUI>().chooseNumber.ToString() + ct2.GetComponent<ChooseNumberNGUI>().chooseNumber.ToString();


		if (ct != "00") {
				
			now = DateTime.Now;
			
			//dateTime

			try{
				st = DateTime.ParseExact(st_h + st_m , "HHmm", culture);
				et = DateTime.ParseExact(et_h + et_m , "HHmm", culture);


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

					Application.LoadLevel("Setting");
				}
			}catch{

				error.transform.localPosition = new Vector3(0,0,0);

			}

		}

	}

	void error_check_f(GameObject obj){
		script_sc.go = false;

		Application.LoadLevel ("Setting");

	}


}
