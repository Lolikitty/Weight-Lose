using UnityEngine;
using System.Collections;
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Sockets;

public class Result : MonoBehaviour {

	public UILabel height;
	public UILabel weight;
	public UILabel sex;
	public UILabel bmi;
	public UILabel basic;
	public UILabel fat;
	public UILabel eat;
	public UILabel drink;




	string s_height;
	string s_weight;
	string s_sex;
	string s_name;
	string s_days;
	string s_goal;


	// Use this for initialization
	void Start () {
		string JsonUserDataPath = Application.persistentDataPath + "/User.txt";

		string h = "";
		string w = "";
		string se = "";
		string bm = "";
		string bmr = "";
		string bodyfat = "";
		string kal = "";
		string water = "";

		if (File.Exists (JsonUserDataPath)) {
			JObject ja = JsonConvert.DeserializeObject<JObject> (File.ReadAllText (JsonUserDataPath));
			h = ja["HeightNow"].ToString();
			w = ja["WeightInit"].ToString();
			se = ja["Sex"].ToString();

			if(float.Parse(h) !=0 ){
//				bmi = (float.Parse(w)/ float.Parse(h)/100 / float.Parse(h)/100).ToString();
				float bmi_test = float.Parse(w)/ float.Parse(h)/100 / float.Parse(h)/100;
				if (bmi_test < 18.5f) {
					
					bmi.text = bmi_test.ToString("f1") + "(過輕)" ;
					
				}else if(bmi_test <24){
					
					bmi.text = bmi_test.ToString("f1") + "(正常)" ;
					
				}else if(bmi_test <27){
					
					bmi.text = bmi_test.ToString("f1") + "(過重)" ;
					
				}else if(bmi_test <30){
					
					bmi.text = bmi_test.ToString("f1") + "(輕度肥胖)" ;
					
				}else if(bmi_test <35){
					
					bmi.text = bmi_test.ToString("f1") + "(中度肥胖)" ;
					
				}else if(bmi_test >=35){
					
					bmi.text = bmi_test.ToString("f1") + "(重度肥胖)" ;
					
				}
			}

			DateTime bir = (DateTime) ja["Birthday"];
			DateTime today = DateTime.Now;
			int age = today.Year - bir.Year;
			if (bir > today.AddYears(-age)) age--;

			if(se == "Boy"){

				bmr = (66 + (13.7f * float.Parse(w)) + (5 * float.Parse(h)) - (6.8f * age)).ToString("f1");
			
			}else if(se == "Girl"){

				bmr = (655 + (9.6f * float.Parse(w)) + (1.8 * float.Parse(h)) - (4.7f * age)).ToString("f1");
			}
//			Women: BMR = 655 + ( 9.6 x weight in kilos ) + ( 1.8 x height in cm ) - ( 4.7 x age in years ) 
//			Men: BMR = 66 + ( 13.7 x weight in kilos ) + ( 5 x height in cm ) - ( 6.8 x age in years )

			BodyFat bf = new BodyFat();
			bodyfat = bf.GetDayBodyFat(DateTime.Now).ToString("f1");

			height.text = h;
			weight.text = w;
			sex.text = se;
//			bmi;
			basic.text = bmr ;
			fat.text = bodyfat;
			eat.text = "2000 kal";
			drink.text = "2000 c.c";
		}





//		if(File.Exists(JsonUserDataPath)){
//			Get_json ();
//		}
//
//
//		set_inform ();

	
	}
	/*
	void Get_json(){
	
		JObject ja = JsonConvert.DeserializeObject<JObject> (File.ReadAllText (JsonUserDataPath));

		s_height = (string)ja["HeightNow"];

		SaveWeight ss = new SaveWeight ();

//		ss.GetLastDayWeight (DateTime.Now);
//		s_weight = ss.GetLastDayWeight (DateTime.Now).ToString();

		s_weight = (string)ja["WeightInit"];

		s_sex = (string)ja["Sex"];

		s_name = (string)ja["Name"];

	}

	void set_inform(){
		height.text = s_height + "cm";
		weight.text = s_weight + "kg";
		sex.text = s_sex;
		double bmi_test;

		if (s_sex == "boy") {
			//基礎代謝率
			basic.text = Convert.ToString(24 * Convert.ToDouble(s_weight) * 1);

			//熱量攝取
			int total_hot = Convert.ToInt32(Convert.ToInt32(basic.text) /0.7) ; 
			int day_lose = 7000 * Convert.ToInt32(s_goal) / Convert.ToInt32(s_days) ; 
			eat.text =Convert.ToString(total_hot - day_lose);

			//喝水
			drink.text = Convert.ToString(Convert.ToInt32(s_weight)*30);

			//bmi計算
			double double_tall = Convert.ToDouble(s_height) * Convert.ToDouble(s_height);
			bmi_test = Math.Round ((Convert.ToDouble(s_weight) / double_tall), 1);

		}else{

			//基礎代謝率
			basic.text = Convert.ToString(24 * Convert.ToDouble(s_weight) * 0.9);

			//熱量攝取
			int total_hot = Convert.ToInt32(Convert.ToInt32(basic.text) /0.7) ; 
			int day_lose = 7000 * Convert.ToInt32(s_goal) / Convert.ToInt32(s_days) ; 
			eat.text =Convert.ToString(total_hot - day_lose) + "kal";

			//喝水
			drink.text = Convert.ToString(Convert.ToInt32(s_weight)*30) + "c.c.";

			//bmi計算
			double double_tall = Convert.ToDouble(s_height) * Convert.ToDouble(s_height);		
			bmi_test = Math.Round ((Convert.ToDouble(s_weight) / double_tall), 1);
		}

		if (bmi_test < 18.5) {

			bmi.text = Convert.ToString(bmi_test) + "(過輕)" ;

		}else if(bmi_test <24){

			bmi.text = Convert.ToString(bmi_test) + "(正常)" ;

		}else if(bmi_test <27){
			
			bmi.text = Convert.ToString(bmi_test) + "(過重)" ;
			
		}else if(bmi_test <30){
			
			bmi.text = Convert.ToString(bmi_test) + "(輕度肥胖)" ;
			
		}else if(bmi_test <35){
			
			bmi.text = Convert.ToString(bmi_test) + "(中度肥胖)" ;
			
		}else if(bmi_test >=35){
			
			bmi.text = Convert.ToString(bmi_test) + "(重度肥胖)" ;
			
		}

		double standard = 22 * Convert.ToInt32 (s_height) * Convert.ToInt32 (s_height) * 0.0001;

		if (s_sex == "boy") {

			double fat_d = Math.Round(((Convert.ToDouble(s_weight) - standard*0.88)/Convert.ToDouble(s_weight))*100,1);

			fat.text = Convert.ToString(fat);
		}else{

			double fat_d = Math.Round(((Convert.ToDouble(s_weight) - standard*0.82)/Convert.ToDouble(s_weight))*100,1);
			
			fat.text = Convert.ToString(fat);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	*/
}
