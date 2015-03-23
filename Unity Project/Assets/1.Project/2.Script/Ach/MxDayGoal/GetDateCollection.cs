using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class GetDateCollection {

	public List<JObject> getDateItem(DateTime thisDay,string fileName,string rootIndex){

		string path = Application.persistentDataPath + "/" + fileName; //

		List<object> set = null;
		List<JObject> returnSet = new List<JObject>();

		//Debug.Log("File.Exists(path) = " + File.Exists(path));
//		if (rootIndex == "FWater") {
//			Debug.Log("FWater.txt is " + File.Exists(path));
//			Debug.Log("returnSet.Count = " + returnSet.Count);
//		}

		if (File.Exists(path)) {

			//取出所有符合日期的項目，加入至set
			set = (JsonConvert.DeserializeObject<JObject>(File.ReadAllText(path))[rootIndex] as JArray).ToObject<List<object>>(); 


			//Debug.Log("set.GetType = " + set.GetType());
			//Debug.Log("set.Count = " + set.Count);

			foreach (object tmp in set) {

				JObject t = JObject.Parse(JsonConvert.SerializeObject(tmp));

				//MonoBehaviour.print("t[\"Date\"] = " + t["Date"]);

				DateTime day = (DateTime)t["Date"];

				if (day.Date == thisDay.Date) { 
//					MonoBehaviour.print("day.Date = " + day + " thisDay.Date = " + thisDay);
//					MonoBehaviour.print("fileName = " + fileName + " day = " + day);
					returnSet.Add(t);
				}
			}

		}
		return returnSet;
	}
}
