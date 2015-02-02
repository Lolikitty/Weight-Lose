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

	//聲音
	public AudioSource audio_s;
	public AudioClip audio_c;

	//文化時間
	IFormatProvider culture;


	//時間
	DateTime st;
	DateTime et;
	DateTime clock;
	DateTime now;

	//條件成立嗎
	public bool go = false ;



	void Start () {

		audio_s = this.gameObject.GetComponent<AudioSource>();


		audio_s.Play ();

		culture = new System.Globalization.CultureInfo("zh-TW", true);

		UIEventListener.Get (Exit).onClick = Exit_d;

	}
	
	// Update is called once per frame
	void Update () {


		if (go == true) {

			now = DateTime.Now;


			next_time.text = Convert.ToInt32(Math.Ceiling( clock.Subtract(now).TotalMinutes)).ToString();

			//Debug.Log(clock.Subtract(now).TotalMinutes).ToString());

			if(DateTime.Compare(now , clock) >= 0 ){

				Debug.Log("time_out");

				Call() ;

			}

		}
	
	}

	void Call(){

		audio_s.Play ();
		//audio_s.SetScheduledEndTime (30);

		clock = clock.AddMinutes (Convert.ToDouble(ct));

		Debug.Log (clock.ToString ());

		if(DateTime.Compare(clock , et) >= 0 ){
			
			go = false;

			next_time.text = "0";
			
		}

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
			st = DateTime.ParseExact(st_h + st_m , "HHmm", culture);
			et = DateTime.ParseExact(et_h + et_m , "HHmm", culture);


			if(DateTime.Compare(st , et) >= 0 ){
				
				et =  et.AddDays(1);
				
				Debug.Log(et.ToString());
				
			} 
			
			clock = st.AddMinutes (Convert.ToDouble(ct));
			
			DateTime Dt ;
			
			while(DateTime.Compare(now ,clock)>= 0){
				
				clock =  clock.AddMinutes(Convert.ToDouble(ct));
				
				
			}
			
			Debug.Log (now.ToString());
			
			if(DateTime.Compare(clock , now) >= 0){
				
				Debug.Log("goooo");
				go = true;
			}
		
		
		
		
		}



	}
}
