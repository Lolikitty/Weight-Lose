using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class M1DayGoal {

	// Use this for initialization
	void Start () {
		//Debug.Log("Test");
		Debug.Log(TheDayGoal(DateTime.Now));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public bool TheDayGoal(DateTime theDay){

		DateTime thisDay = theDay;
		string fileName = "Food.txt";
		string rootIndex = "Food"; //根索引

		//取得所有符合日期的項目集合
		List<JObject> set = new GetDateCollection ().getDateItem(thisDay, fileName, rootIndex);

		//Debug.Log("set = " + set);
		//Debug.Log("set.Count = " + set.Count);

		bool thisDayGoal = false;
		bool breakfast = false;
		bool lunch = false;
		bool dinner = false;

		foreach(JObject tmp in set){ //先保留
			DateTime day = (DateTime)tmp["Date"];

			//Debug.Log("day.ToShortDateString = " + day.ToShortTimeString());

			int hour = Int32.Parse(day.ToString("HH"));

			//Debug.Log("hour = " + hour);

			if(hour >= 5 && hour < 10){
				breakfast = true;
			}else if(hour >= 10 && hour < 14){
				lunch = true;
			}else if(hour >=16 && hour < 20){
				dinner = true;
			}
			if(breakfast && lunch && dinner){
				thisDayGoal = true;
				break;
			}
		}
//		Debug.Log ("breakfast = " + breakfast);
//		Debug.Log ("lunch = " + lunch);
//		Debug.Log ("dinner = " + dinner);

		return thisDayGoal;
	}
}
