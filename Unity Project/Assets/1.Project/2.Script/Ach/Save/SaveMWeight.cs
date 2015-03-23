using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class SaveMWeight : MonoBehaviour {
	
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
		string JsonMWeightDataPath = Application.persistentDataPath + "/MWeight.txt"; //
		
		var rmw = new{
			Date = d,
		};

		List<object> MWeight = null;
		
		if (File.Exists(JsonMWeightDataPath)) {
			MWeight = (JsonConvert.DeserializeObject<JObject>(File.ReadAllText(JsonMWeightDataPath))["MWeight"] as JArray).ToObject<List<object>>(); //
			MWeight.Add(rmw); //
		} else {
			MWeight = new List<object> (){rmw}; //
		}
		File.WriteAllText(JsonMWeightDataPath, JsonConvert.SerializeObject(new{MWeight},Formatting.Indented)); //
		//print ("scuess");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
