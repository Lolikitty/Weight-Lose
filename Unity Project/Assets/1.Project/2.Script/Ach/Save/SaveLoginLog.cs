using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class SaveLoginLog : MonoBehaviour {
	
//	void Start(){
//		OnClick ();
//	}
//
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
		string JsonLoginLogDataPath = Application.persistentDataPath + "/LoginLog.txt"; //
		
		var loginglog = new{
			Date = d
		};
		
		List<object> LoginLog = null;
		
		if (File.Exists(JsonLoginLogDataPath)) {
			LoginLog = (JsonConvert.DeserializeObject<JObject>(File.ReadAllText(JsonLoginLogDataPath))["LoginLog"] as JArray).ToObject<List<object>>(); //
			LoginLog.Add(loginglog); //
		} else {
			LoginLog = new List<object> (){loginglog}; //
		}
		File.WriteAllText(JsonLoginLogDataPath, JsonConvert.SerializeObject(new{LoginLog},Formatting.Indented)); //
		//print ("scuess");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
