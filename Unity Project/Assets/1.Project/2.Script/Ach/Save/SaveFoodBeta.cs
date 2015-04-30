using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class SaveFoodBeta : MonoBehaviour
{

	public void Save(DateTime date, string name, int kal){
		string JsonFoodDataPath = Application.persistentDataPath + "/Food.txt";
		
		var food = new {
			Date = date,
			Name = name.ToString(),
			Kal = kal.ToString()
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

