using UnityEngine;
using System.Collections;
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class test : MonoBehaviour {

	public GameObject ok;

	string[] food2 = new string[]{"egg","pig","cake","coke"};

	int i = 0;


	void Start(){


	//	JsonFoodSave ("ew" , food2);
		FoodGet ();

	}

	void FoodGet(){
		string JsonFoodDataPath = Application.dataPath + "/Food.txt";
		JArray ja = JsonConvert.DeserializeObject<JObject> (File.ReadAllText (JsonFoodDataPath)) ["Food"] as JArray;

		for(int i = 0; i < ja.Count; i++){

			JArray test = (JArray)ja[i]["Food2"];

			for(int a = 0 ; a < test.Count ; a++){

				Debug.Log(test[a]);

			}




		}
	}
	


	void JsonFoodSave(string name, string[] food2){
		string JsonFoodDataPath = Application.dataPath + "/Food.txt";
		
		var food = new {
			Name = name,

			Food2 = food2

		};
		
		List<object> Food = null;
		
		if(File.Exists(JsonFoodDataPath)){
			Food = (JsonConvert.DeserializeObject<JObject> (File.ReadAllText(JsonFoodDataPath))["Food"] as JArray).ToObject<List<object>>();
			Food.Add(food);
		}else{
			Food = new List<object> (){food};
		}
		File.WriteAllText(JsonFoodDataPath, JsonConvert.SerializeObject(new{Food},Formatting.Indented));
	}

}
