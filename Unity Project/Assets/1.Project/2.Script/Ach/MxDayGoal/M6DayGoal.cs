using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class M6DayGoal {
	
	// Use this for initialization
	void Start () {
		//Debug.Log("Test");
//		Debug.Log(TheDayGoal(DateTime.Now));
	}
	

	public bool TheDayGoal(DateTime theDay){
		
		DateTime thisDay = theDay;
		string fileName = "FWater.txt";
		string rootIndex = "FWater"; //根索引
		
		//取得所有符合日期的項目集合
		List<JObject> set = new GetDateCollection ().getDateItem(thisDay, fileName, rootIndex);
		
		//Debug.Log("set = " + set);
		//Debug.Log("set.Count = " + set.Count);
		bool thisDayGoal = false;
		
		if (set.Count > 0) {
			thisDayGoal = true;
//			Debug.Log("count > 0");
		}

//		Debug.Log ("thisDayGoal = " + thisDayGoal);

		return thisDayGoal;
	}
}
