using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class BodyFat : MonoBehaviour
{


	public float GetDayBodyFat(DateTime day){
		float weight = 0;
		float height = 0;
		float year = 0;
		int sex = 0;

		SaveWeight w = new SaveWeight ();
		weight = w.GetDayWeight (day);
		if (weight == 0) {
			weight = w.GetLastDayWeight(day);
		}
	

		SaveHeightWaistline hw = new SaveHeightWaistline ();
		height = hw.GetDayHeight (day);

//		Debug.Log (day.Date + " hw.GetDayHeight (day) = " + height); //

		if (height == 0) {
			height = hw.GetLastDayHeight(day);
		}

//		Debug.Log (day.Date + " hw.GetLastDayHeight(day) = " + height); //

		height /= 100.0f;
//		if (height == 0) {
//			return 0;
//		}	


		string JsonHeightWaistlineDataPath = Application.persistentDataPath + "/User.txt"; //

		if (File.Exists (JsonHeightWaistlineDataPath)) {
			JObject j = JsonConvert.DeserializeObject<JObject> (File.ReadAllText (JsonHeightWaistlineDataPath));
			DateTime d = (DateTime) (j["Birthday"]);

			DateTime today = DateTime.Now;
			int age = today.Year - d.Year;
			if (d > today.AddYears(-age)) age--;
			year = age;


			string s = j["Sex"].ToString();
			if(s=="Boy"){
				sex = 1;
			}
		}


	

		float fat = 0;
		if(height != 0){
			fat = 1.2f *(weight/height/height)+ 0.23f * year  - 5.4f - 10.8f * sex;
		}

		if(fat <= 0){
			fat = 0;
		}

//		Debug.Log (day.Date + "  fat = " + fat +" weight = " + weight + " height = " + height 
//		           + " year = " + year + " sex = " + sex);

		return fat;

	}
}

