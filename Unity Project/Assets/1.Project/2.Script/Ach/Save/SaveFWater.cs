using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class SaveFWater : MonoBehaviour {
	
	void Start(){
		//OnClick ();
		Save();
	}

	// Use this for initialization

	public void Save(){
		f(DateTime.Now);
	}
	
	public void Save(DateTime dt){
		f(dt);
	}
	
	public void Save(DateTime start,DateTime end){
		if (start.Date <= end.Date)
		for (DateTime d = start; d.Date <= end.Date; d = d.AddDays(1)) {
			f (d);
		}
		else {
			Debug.LogError("Error: DateTime start,DateTime end");
		}
	}


	public void f(DateTime d) {
		string JsonFWaterDataPath = Application.persistentDataPath + "/FWater.txt"; //
		
		var rfw = new{
			Date = d,
		};
		
		List<object> FWater = null;
		
		if (File.Exists(JsonFWaterDataPath)) {
			FWater = (JsonConvert.DeserializeObject<JObject>(File.ReadAllText(JsonFWaterDataPath))["FWater"] as JArray).ToObject<List<object>>(); //
			FWater.Add(rfw); //
		} else {
			FWater = new List<object> (){rfw}; //
		}
		File.WriteAllText(JsonFWaterDataPath, JsonConvert.SerializeObject(new{FWater},Formatting.Indented)); //
		//print ("scuess");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
