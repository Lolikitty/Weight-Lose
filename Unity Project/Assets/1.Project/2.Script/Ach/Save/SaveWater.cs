using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class SaveWater : MonoBehaviour {

	// Use this for initialization
	void OnClick(){
		Save ();
	}

	public void Save(){
		int waterLiter = LwWaiter2.UseCupCC - LwWaiter2.WaiterValue ; 
		f(DateTime.Now,waterLiter);
	}
	
	public void Save(DateTime dt,int waterLiter){
		f(dt,waterLiter);
	}
	
	public void Save(DateTime start,DateTime end,int[] waterLiter){
		if (start.Date <= end.Date && (end.Subtract(start).Days + 1) == waterLiter.Length) { //...
			int i = 0;
			for (DateTime d = start; d.Date <= end.Date; d = d.AddDays(1),i++) {
				f (d,waterLiter[i]);
			}
		}
		else {
			Debug.LogError("Error: DateTime start,DateTime end");
		}
	}


	public void f (DateTime d,int waterLiter) {
//		int waterLiter = LwWaiter2.UseCupCC - LwWaiter2.WaiterValue ; 

		string JsonWaterDataPath = Application.persistentDataPath + "/Water.txt"; //

		var w = new{
			Date = d,
			Liter = waterLiter
		};

		List<object> Water = null;

		if (File.Exists(JsonWaterDataPath)) {
			Water = (JsonConvert.DeserializeObject<JObject>(File.ReadAllText(JsonWaterDataPath))["Water"] as JArray).ToObject<List<object>>(); //
			Water.Add(w); //
		} else {
			Water = new List<object> (){w}; //
		}
		File.WriteAllText(JsonWaterDataPath, JsonConvert.SerializeObject(new{Water},Formatting.Indented)); //
	}
	
	// Update is called once per frame
	public float GetDayWater(DateTime day) {
		DateTime d = day;
		string fileName = "Water.txt";
		string rootIndex = "Water"; //根索引
		
		//取得所有符合日期的項目集合
		List<JObject> set = new GetDateCollection ().getDateItem(d, fileName, rootIndex);

		float sum = 0;
		//process
		foreach (JObject tmp in set) {
			float L = float.Parse(tmp["Liter"].ToString());
			sum += L;
		}

		return sum;
	}
}
