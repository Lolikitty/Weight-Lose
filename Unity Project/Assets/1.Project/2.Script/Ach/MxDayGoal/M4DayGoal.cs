using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class M4DayGoal {
	
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
//		Debug.Log("M4 set.Count = " + set.Count);

		bool thisDayGoal = true;
		float standardKal = 2000.0f; //熱量標準
		float kal = 0;
		//process
		if (set.Count == 0) {
			thisDayGoal = false;
		}

		foreach (JObject tmp in set) {
//			if(tmp.Count==0)Debug.Log ("tmp = " + 0);
			//計算今日熱量總量
			kal += float.Parse(tmp["Kal"].ToString());
			if(kal > standardKal){ //若超過標準，則false
				thisDayGoal = false;
				break;
			}
		}
		return thisDayGoal;
	}
}
