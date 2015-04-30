using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class M2DayGoal {

	public bool TheDayGoal(DateTime theDay){
		//取出Mission2的NowLv
		string path = Application.persistentDataPath + "/Mission.txt"; 
		string itemName = "Mission2";
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
		string fileName = "Water.txt";
		string rootIndex = "Water"; //根索引
		
		//取得所有符合日期的項目集合
		List<JObject> set = new GetDateCollection ().getDateItem(thisDay, fileName, rootIndex);
		
		bool thisDayGoal = false;

		int lv = nowLv;// hardLv["現在難度等級"];

		float standardLiter = 2000.0f;

		switch(lv){
		case 1:
		case 2:
			standardLiter *= 0.7f;
			break;
		case 3:
		case 4:
			standardLiter *= 0.8f;
			break;
		case 5:
		case 6:
			standardLiter *= 0.9f;
			break;
		}

		float sum = 0;
		//process
		foreach (JObject tmp in set) {
			float L = float.Parse(tmp["Liter"].ToString());
			sum += L;
		}	
		if (sum >= standardLiter) {	
			thisDayGoal = true;
		}
		return thisDayGoal;
	}

}
