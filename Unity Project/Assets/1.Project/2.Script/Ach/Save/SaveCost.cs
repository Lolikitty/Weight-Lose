using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class SaveCost : MonoBehaviour
{

	public void Save(DateTime day,int cost){
		string path = Application.persistentDataPath + "/Cost.txt"; 
		
		var w = new{
			Date = day,
			Cost = cost.ToString()
		};
		
		List<object> Weight = new List<object> ();
		
		if (File.Exists(path)) {
			Weight = (JsonConvert.DeserializeObject<JObject>(File.ReadAllText(path))["Cost"] as JArray).ToObject<List<object>>(); //
			Weight.Add(w); 
		} else {
			Weight = new List<object> (){w}; 
		}
		File.WriteAllText(path, JsonConvert.SerializeObject(new{Weight},Formatting.Indented)); 
	}

	public int GetTotalCost(){
		string path = Application.persistentDataPath + "/Cost.txt"; 
		int sum = 0;
		if (File.Exists (path)) {
			JArray ja = JsonConvert.DeserializeObject<JObject> (File.ReadAllText (path)) ["Cost"] as JArray;
			for(int i=0 ; i < ja.Count ;i++){
				int cost = Int32.Parse(ja[i]["Cost"].ToString());
				sum += cost;
			}
		}
		return sum;
	}
}

