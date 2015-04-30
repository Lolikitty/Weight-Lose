using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class SaveMissionItemCount : MonoBehaviour {
	
	//	void Start(){
	//		OnClick ();
	//	}


	
	string JsonDataPath = Application.persistentDataPath + "/MissionItemCount.txt"; //

	// Use this for initialization
	public int GetMissionItemReachCount(int MissionNumber){
		if (MissionNumber < 6 || MissionNumber > 8) {
			Debug.LogError("Error: MissionNumber = " + MissionNumber);
			return 0;
		}

		string itemName = "Mission" + MissionNumber.ToString();

//		Debug.Log("JsonDataPath = " + File.Exists (JsonDataPath));

		if (File.Exists (JsonDataPath)) {
			JArray ja = JsonConvert.DeserializeObject<JObject> (File.ReadAllText (JsonDataPath)) ["Mission"] as JArray;

//			Debug.Log("ja.Count = " + ja.Count);

			string c="";
			for (int i = 0; i < ja.Count; i++) {
				if (itemName == ja [i] ["Item"].ToString ()) {
					c = ja [i] ["ReachCount"].ToString ();
				}
			}
			int count = 0;
			count = Int32.Parse (c);
			if(count >= 0)
				return count;
			else
				return 0;
		} else {
			Save(MissionNumber,0);
//			Debug.Log("MissionNumber = " + MissionNumber + " ,0");
			return 0;
		}
	}

	public void Save(int MissionNumber,int Count) {
		if (MissionNumber < 6 || MissionNumber > 8 || Count < 0) {
			Debug.LogError("Error: MissionNumber = " + MissionNumber + " and  Count = " + Count);
			return;
		}

		string itemName = "Mission" + MissionNumber.ToString();

//		Debug.Log(File.Exists(JsonDataPath));

		if (File.Exists(JsonDataPath)) {
			JArray ja =JsonConvert.DeserializeObject<JObject>(File.ReadAllText(JsonDataPath))["Mission"] as JArray;

//			if(MissionNumber == 5)
//				Debug.Log("ja.Count = " + ja.Count);

			for(int i = 0; i < ja.Count; i++){
				if(itemName == ja[i]["Item"].ToString()){
					ja[i]["ReachCount"] = Count.ToString();
				}
			}

			object Mission = ja;
			File.WriteAllText(JsonDataPath, JsonConvert.SerializeObject(new{Mission},Formatting.Indented)); //

		} else {
			List<object> Mission = null;
			Mission = new List<object>();
			for(int i=6;i <=8;i++){
				string tmpItemName = "Mission" + i.ToString();
				var tmp = new{
					Date = DateTime.Now,
					Item = tmpItemName,
					ReachCount = "0"
				};
				Mission.Add(tmp);
			}
			//Mission = new List<object> (){lvlog}; //
			File.WriteAllText(JsonDataPath, JsonConvert.SerializeObject(new{Mission},Formatting.Indented)); //
		}
		//File.WriteAllText(JsonDataPath, JsonConvert.SerializeObject(new{MissionItemCount},Formatting.Indented)); //
		//print ("scuess");
	}

}
