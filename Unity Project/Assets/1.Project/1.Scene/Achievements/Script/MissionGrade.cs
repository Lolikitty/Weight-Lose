using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class MissionGrade : MonoBehaviour
{

	int baseGrade = 50;

	// Use this for initialization
	public int GetMissionGrade(int MissionNumber)
	{
		if (MissionNumber < 1 || MissionNumber > 10) {
			return 0;
		}

		string itemName = "Mission" + MissionNumber.ToString(); //key
		string path = Application.persistentDataPath + "/Mission.txt"; //
		string lv="1";
		//JArray ja = new JArray();
		if(File.Exists(path)){
	
			JArray ja =JsonConvert.DeserializeObject<JObject>(File.ReadAllText(path))["Mission"] as JArray;
			
			for(int i = 0; i < ja.Count; i++){
				if(itemName == ja[i]["Item"].ToString()){
					lv = ja[i]["NowLv"].ToString();
				}
			}
		}
//		if (lv == null) {
//			lv = "1";
//		}
		int grade = baseGrade * Int32.Parse (lv);

		return grade;
	}
}

