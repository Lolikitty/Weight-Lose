using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class SaveWeight : MonoBehaviour {

	public ChooseNumber w1;
	public ChooseNumber w2;
	public ChooseNumber w3;
	public ChooseNumber w4;

	// Use this for initialization
	void OnClick () {
		Save ();
	}

	public void Save(){
		float nw1 = w1.chooseNumber * 100;
		float nw2 = w2.chooseNumber * 10;
		float nw3 = w3.chooseNumber;
		float nw4 = w4.chooseNumber * 0.1f;
		
		float weightKg = (nw1 + nw2 + nw3 + nw4);

//		print ("h = " + weightKg);


		f (DateTime.Now , weightKg);

	}

	public void Save(DateTime start,DateTime end,int[] weightKg){
		if (start.Date <= end.Date && (end.Subtract(start).Days + 1) == weightKg.Length) { //...
			int i = 0;
			for (DateTime d = start; d.Date <= end.Date; d = d.AddDays(1),i++) {
				f (d,weightKg[i]);
			}
		}
		else {
			Debug.LogError("Error: DateTime start,DateTime end");
		}
	}


	public void f(DateTime d,float weight){
		//print(weightKg);

//		Debug.Log ("d = " + d + ", w = " + weight);

		string JsonWeightDataPath = Application.persistentDataPath + "/Weight.txt"; 
		
		var w = new{
			Date = d,
			Kg = weight.ToString()
		};
		
		List<object> Weight = new List<object> ();
		
		if (File.Exists(JsonWeightDataPath)) {
			Weight = (JsonConvert.DeserializeObject<JObject>(File.ReadAllText(JsonWeightDataPath))["Weight"] as JArray).ToObject<List<object>>(); //
			Weight.Add(w); 
		} else {
			Weight = new List<object> (){w}; 
		}
		File.WriteAllText(JsonWeightDataPath, JsonConvert.SerializeObject(new{Weight},Formatting.Indented)); 

	}

	public float GetDayWeight(DateTime day){
		DateTime d = day;
		string fileName = "Weight.txt";
		string rootIndex = "Weight"; 
		
	
		List<JObject> set = new GetDateCollection ().getDateItem(d, fileName, rootIndex);
		float sum = 0;
		int count = 0;
		foreach (JObject tmp in set) {
			count ++;
			float w = float.Parse( tmp["Kg"].ToString() );
			sum += w;
		}

		float avg = 0;

		if (set.Count != 0) {
			avg = sum / (float)set.Count;
		}

		return avg;
	}

	public float GetLastDayWeight(DateTime day){

		string JsonWeightDataPath = Application.persistentDataPath + "/Weight.txt"; 
		DateTime tmp = new DateTime(1900,1,1); 

		float lastWeight = 0;
		if (File.Exists (JsonWeightDataPath)) {
			JArray ja = JsonConvert.DeserializeObject<JObject> (File.ReadAllText (JsonWeightDataPath)) ["Weight"] as JArray;
			for(int i=0 ;i < ja.Count ; i++){
				DateTime d = (DateTime)ja[i]["Date"];
				if(d > tmp && d.Date <= day.Date){
					tmp = d; 
					if(tmp.Date <= day.Date){
						lastWeight = float.Parse(ja[i]["Kg"].ToString()) ;
					}
				}
			}
		}

		return lastWeight;
	}
}
