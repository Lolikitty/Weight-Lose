using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class M9DayGoal {
	
	// Use this for initialization
	void Start () {
		//Debug.Log("Test");
		//Debug.Log(TheDayGoal(DateTime.Now));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public bool TheDayGoal(DateTime theDay){
		//取出Mission2的NowLv
		string path = Application.persistentDataPath + "/Mission.txt"; 
		string itemName = "Mission9";
		int nowLv = 1;
		if (File.Exists (path)) {
			
			JArray ja = JsonConvert.DeserializeObject<JObject> (File.ReadAllText (path)) ["Mission"] as JArray;
			
			for (int i = 0; i < ja.Count; i++) {
				if (itemName == ja [i] ["Item"].ToString ()) {
					nowLv = Int32.Parse(ja[i]["NowLv"].ToString()) ;
				}
			}
		}

		DateTime thisDay = theDay;
		string fileName = "Sports.txt";
		string rootIndex = "Sports"; //根索引
		
		//取得所有符合日期的項目集合
		List<JObject> set = new GetDateCollection ().getDateItem(thisDay, fileName, rootIndex);
		
		//Debug.Log("set = " + set);
		//Debug.Log("set.Count = " + set.Count);
		bool thisDayGoal = false;

		int minute = 0;
		int standardMinute = 30;

		int lv = nowLv; 
		//保留(取得當前困難度等級)...

		switch(lv){
		case 2:case 3:case 4:
			standardMinute += 10;
			break;
		case 5:case 6:case 7:
			standardMinute += 20;
			break;
		case 8:case 9:case 10:
			standardMinute += 30;
			break;
		}

		foreach(JObject tmp in set){ //先保留
			//DateTime day = (DateTime)tmp["Date"];
			
			//Debug.Log("day.ToShortDateString = " + day.ToShortTimeString());
//			MonoBehaviour.print(tmp["Minute"]);
//			MonoBehaviour.print("tmp = " + tmp.Count);

			int thisTmpMinute = Int32.Parse(tmp["Minute"].ToString());
			minute += thisTmpMinute;
			if(minute >= standardMinute){
				thisDayGoal = true;
				break;
			}
		}
		
		return thisDayGoal;
	}
}
