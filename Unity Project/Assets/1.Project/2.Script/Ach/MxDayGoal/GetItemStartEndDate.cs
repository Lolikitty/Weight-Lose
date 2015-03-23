using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class GetItemStartEndDate {

	public DateTime getItemStartEndDate(int MissionNumber,int StartEndDate){

		if(MissionNumber < 1 || MissionNumber > 10 || (StartEndDate != 0 && StartEndDate != 1)){
			Debug.LogError("Error: MissionNumber or StartEndDate ... MissionNumber=" + MissionNumber + " ... StartEndDate = " + StartEndDate);
			return DateTime.Now;
		}

		string file = "";
		string itemIndex = "";
		switch(MissionNumber){ //設定p和standardDays
		case 1:
			file = "Food.txt";
			itemIndex = "Food";
			break;
		case 2:
			file = "Water.txt";
			itemIndex = "Water";
			break;
		case 3:
			file = "Weight.txt";
			itemIndex = "Weight";
			break;
		case 4:
			file = "Food.txt";
			itemIndex = "Food";
			break;
		case 5:
			file = "LoginLog.txt";
			itemIndex = "LoginLog";
			break;
		case 6:
			file = "FWater.txt";
			itemIndex = "FWater";
			break;
		case 7:
			file = "FWeight.txt";
			itemIndex = "FWeight";
			break;
		case 8:
			file = "MWeight.txt";
			itemIndex = "MWeight";
			break;
		case 9:
			file = "Sports.txt";
			itemIndex = "Sports";
			break;
		}
		string path = Application.persistentDataPath + "/" + file ; //
		
		List<object> set = new List<object>();
		
		//Debug.Log("File.Exists(path) = " + File.Exists(path));
		
		if (File.Exists (path)) {

			//MonoBehaviour.print(path);

			//取出所有符合日期的項目，加入至set
			set = (JsonConvert.DeserializeObject<JObject> (File.ReadAllText (path)) [itemIndex] as JArray).ToObject<List<object>> (); //

			//Debug.Log("set.GetType = " + set.GetType());
			//Debug.Log("set.Count = " + set.Count);

			DateTime startDate = new DateTime (2099, 1, 1);
			DateTime endDate = new DateTime (1999, 1, 1);

			foreach (object tmp in set) {
	
					JObject t = JObject.Parse (JsonConvert.SerializeObject (tmp));
					//MonoBehaviour.print("t[\"Date\"] = " + t["Date"]);
					DateTime day = (DateTime)t ["Date"];

				if (startDate > day) { //DateTime.Compare(startDate,day) > 0
							startDate = day;
				}
				if (endDate < day) {
							endDate = day;
				}
			}

//			Debug.Log("i=3  startDate = " + startDate);
//			Debug.Log("i=3  endDate = " + endDate);

			if (StartEndDate == 0) { //StartDate = 0
					return startDate;
	
			} else { //EndDate = 1
					return endDate;
			}
			
		} else {
			return DateTime.Now;
		}
	}
}
