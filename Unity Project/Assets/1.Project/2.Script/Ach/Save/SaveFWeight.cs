using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class SaveFWeight : MonoBehaviour {
	
//	void Start(){
//		OnClick ();
//	}
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
		string JsonFWeightDataPath = Application.persistentDataPath + "/FWeight.txt"; //

		var rfw = new{
			Date = d,
		};
		
		List<object> FWeight = null;
		
		if (File.Exists(JsonFWeightDataPath)) {
			FWeight = (JsonConvert.DeserializeObject<JObject>(File.ReadAllText(JsonFWeightDataPath))["FWeight"] as JArray).ToObject<List<object>>(); //
			FWeight.Add(rfw); //
		} else {
			FWeight = new List<object> (){rfw}; //
		}
		File.WriteAllText(JsonFWeightDataPath, JsonConvert.SerializeObject(new{FWeight},Formatting.Indented)); //
		//print ("scuess");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
