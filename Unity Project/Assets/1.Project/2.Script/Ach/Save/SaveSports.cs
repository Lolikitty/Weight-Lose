using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class SaveSports : MonoBehaviour {


	// Use this for initialization

	//sport1 = basketball
	//sport2 = swimming
	//sport3 = running
	//sport4 = badminton
	//sport5 = Yoga
	//sport6 = Weightlifting
	//sport7 = bike
	//sport8 = baseball
	//sport9 = volleyball
	//sport10 = rope skipping
	//sport11 = dancing

	public void Save(DateTime d,string sportItem,int time) {

		if (sportItem != "0") {
			f (d, sportItem, time);
		}
	}
	
	// Update is called once per frame
	public void f(DateTime d,string sportItem,int minutes) {
		string JsonSportsDataPath = Application.persistentDataPath + "/Sports.txt"; //

		float baseKalPerMinute = 1.0f;

		baseKalPerMinute = GetSportItemPerMinuteKal (sportItem);

		float k = minutes * baseKalPerMinute;

		var s = new{
			Date = d,
			Item = sportItem,
			Minute = minutes.ToString(),
			Kal = k.ToString()
		};
		
		List<object> Sports = null;
		
		if (File.Exists(JsonSportsDataPath)) {
			Sports = (JsonConvert.DeserializeObject<JObject>(File.ReadAllText(JsonSportsDataPath))["Sports"] as JArray).ToObject<List<object>>(); //
			Sports.Add(s); //
		} else {
			Sports = new List<object> (){s}; //
		}
		File.WriteAllText(JsonSportsDataPath, JsonConvert.SerializeObject(new{Sports},Formatting.Indented)); //
	}


	public float GetDayKal(DateTime day){
		string fileName = "Sports.txt";
		string rootIndex = "Sports";
		GetDateCollection gdc = new GetDateCollection ();
		List<JObject> ja = gdc.getDateItem(day, fileName, rootIndex);

		float c = 0;

		foreach(JObject tmp in ja){
			float k = float.Parse(tmp["Kal"].ToString());
			c += k;
		}
		return c;
	}

	public float GetSportItemPerMinuteKal(string sportsItem){
		float baseKalPerMinute = 1.0f;
		switch(sportsItem){
		case "0":
			break;
		case "sport1":
			baseKalPerMinute = 6.0f;
			break;
		case "sport2":
			baseKalPerMinute = 17.5f;
			break;
		case "sport3":
			baseKalPerMinute = 12.0f;
			break;
		case "sport4":
			baseKalPerMinute = 4.5f;
			break;
		case "sport5":
			baseKalPerMinute = 4.3f;
			break;
		case "sport6":
			baseKalPerMinute = 4.0f;
			break;
		case "sport7":
			baseKalPerMinute = 3.0f;
			break;
		case "sport8":
			baseKalPerMinute = 4.7f;
			break;
		case "sport9":
			baseKalPerMinute = 5.0f;
			break;
		case "sport10":
			baseKalPerMinute = 17.0f;
			break;
		case "sport11":
			baseKalPerMinute = 4.0f;
			break;
		}
		return baseKalPerMinute;
	}
}
