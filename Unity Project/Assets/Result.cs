using UnityEngine;
using System.Collections;
using System;

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
	string s_fat;
	string s_days;
	string s_goal;


	// Use this for initialization
	void Start () {


		set_inform ();

	
	}

	void set_inform(){
		height.text = s_height;
		weight.text = s_weight;
		fat.text = s_fat;
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
			eat.text =Convert.ToString(total_hot - day_lose);

			//喝水
			drink.text = Convert.ToString(Convert.ToInt32(s_weight)*30);

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

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
