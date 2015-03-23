using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class SaveMissionLv : MonoBehaviour
{

	void Start () {
		//Debug.Log("Test");
//		Save(1,1);
	}

	public int GetMissionNowLv(int MissionNumber){
		if (MissionNumber < 1 || MissionNumber > 10) {
			Debug.LogError("Error: MissionNumber = " + MissionNumber);
			return 0;
		}

		string itemName = "Mission" + MissionNumber.ToString(); //key
		string path = Application.persistentDataPath + "/Mission.txt"; //

		string lv = "1";
		if (File.Exists (path)) {
			JArray ja = JsonConvert.DeserializeObject<JObject> (File.ReadAllText (path)) ["Mission"] as JArray;
			
			for (int i = 0; i < ja.Count; i++) {
				if (itemName == ja [i] ["Item"].ToString ()) {
					lv = ja[i]["NowLv"].ToString();
				}
			}
		}

		int Lv = Int32.Parse (lv);

		return Lv;
	}

	public void Save(DateTime day, int MissionNumber,int NowLv){
		if (MissionNumber < 1 || MissionNumber > 10) {
			Debug.LogError("Error: MissionNumber = " + MissionNumber);
			return ;
		}

		int MaxLv = 10; 

		NowLv = (NowLv > MaxLv ? MaxLv : NowLv);

		string itemName = "Mission" + MissionNumber.ToString(); //key
		string path = Application.persistentDataPath + "/Mission.txt"; //

		//JArray ja = new JArray();
		if(File.Exists(path)){

			//Mission = (JsonConvert.DeserializeObject<JObject>(File.ReadAllText(path))["Mission"] as JArray).ToObject<List<object>>(); //

			JArray ja =JsonConvert.DeserializeObject<JObject>(File.ReadAllText(path))["Mission"] as JArray;

			int nlv = 1;
			for(int i = 0; i < ja.Count; i++){
				if(itemName == ja[i]["Item"].ToString()){
					nlv = Int32.Parse(ja[i]["NowLv"].ToString());
					ja[i]["NowLv"] = NowLv.ToString();
				}
			}
			if(NowLv != nlv){
				SaveLvUpdateHistory s = new SaveLvUpdateHistory();
				s.Save( day, MissionNumber, nlv, NowLv);
			}
//			object Mission = new object();
//			Mission = ja;

			object Mission = ja;
			File.WriteAllText(path, JsonConvert.SerializeObject(new{Mission},Formatting.Indented));
		}else{
			List<object> Mission = null;
			Mission = new List<object>();
			for(int i=1;i <=10;i++){
				string rootName = "Mission" + i.ToString();
				var lvlog = new{
					Date = day,
					Item = rootName,
					NowLv = "1"
				};
				Mission.Add(lvlog);
			}
			//Mission = new List<object> (){lvlog}; //
			File.WriteAllText(path, JsonConvert.SerializeObject(new{Mission},Formatting.Indented)); //
		}

//		if (File.Exists (path)) {
//			object Mission = ja;
//			File.WriteAllText(path, JsonConvert.SerializeObject(new{Mission},Formatting.Indented));
//		}

	}
}

