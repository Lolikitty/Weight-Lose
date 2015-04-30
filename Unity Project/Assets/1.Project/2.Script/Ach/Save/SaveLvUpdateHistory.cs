using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class SaveLvUpdateHistory : MonoBehaviour
{
	public bool IsUpdateAddLv(int MissionNumber, DateTime start,DateTime end){
		if (MissionNumber < 1 || MissionNumber > 10) {
			Debug.LogError("Error: MissionNumber = " + MissionNumber);
			
			return false;
		}
		string itemName = "Mission" + MissionNumber.ToString(); //key
		string path = Application.persistentDataPath + "/MissionLvUpdateHistory.txt"; //
//		string before = "BeforeUpdateLv";
//		string after = "AfterUpdateLv";
		bool reach = false;

		if (File.Exists (path)) {
			JArray ja = JsonConvert.DeserializeObject<JObject> (File.ReadAllText (path)) ["MissionLvUpdateHistory"] as JArray;

//			Debug.Log("ja.Count = " + ja.Count);

			for(DateTime d = start ; d.Date <= end.Date ; d = d.AddDays(1)){
				for(int i = 0;i < ja.Count; i++){
					DateTime tmp = (DateTime) ja[i]["Date"];
					int before = Int32.Parse(ja[i]["BeforeUpdateLv"].ToString());
					int after = Int32.Parse(ja[i]["AfterUpdateLv"].ToString());
					if( ja[i]["Item"].ToString() == itemName && tmp.Date == d.Date && before < after){
						reach = true;
						break;
					}
				}
				if(reach){
					break;
				}
			}

		}

		return reach;
	}


	public void Save(DateTime day, int MissionNumber, int BeforeUpdateLv,int AfterUpdateLv){
		if (MissionNumber < 1 || MissionNumber > 10) {
			Debug.LogError("Error: MissionNumber = " + MissionNumber);

			return ;
		}

		if(BeforeUpdateLv >= AfterUpdateLv){
			return;
		}

		string itemName = "Mission" + MissionNumber.ToString(); //key
		string path = Application.persistentDataPath + "/MissionLvUpdateHistory.txt"; //
		
		List<object> MissionLvUpdateHistory = null;


		string info = BeforeUpdateLv.ToString() + " -> " + AfterUpdateLv.ToString();

		var tmp = new{
			Date = day,
			Item = itemName,
			BeforeUpdateLv = BeforeUpdateLv.ToString(),
			AfterUpdateLv = AfterUpdateLv.ToString(),
			UpdateInfo = info
		};

		if(File.Exists(path)){

			JArray ja =JsonConvert.DeserializeObject<JObject>(File.ReadAllText(path))["MissionLvUpdateHistory"] as JArray;

			MissionLvUpdateHistory = (JsonConvert.DeserializeObject<JObject>(File.ReadAllText(path))["MissionLvUpdateHistory"] as JArray).ToObject<List<object>>(); //
			//
			bool isExist = false;
			for(int i=0 ;i < ja.Count ; i++){
				if(ja[i]["Item"].ToString() == itemName && ja[i]["BeforeUpdateLv"].ToString() == BeforeUpdateLv.ToString()
				   && ja[i]["AfterUpdateLv"].ToString() == AfterUpdateLv.ToString() ){
					isExist = true;
					break;
				}
			}
			if(!isExist){ //
				MissionLvUpdateHistory.Add(tmp); //
			}
		}else{
			MissionLvUpdateHistory = new List<object> (){tmp}; //
		}
		File.WriteAllText(path, JsonConvert.SerializeObject(new{MissionLvUpdateHistory},Formatting.Indented)); //
	
	}
}

