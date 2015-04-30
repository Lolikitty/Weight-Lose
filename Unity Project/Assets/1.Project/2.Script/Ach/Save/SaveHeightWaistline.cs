using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class SaveHeightWaistline : MonoBehaviour {
	
	public ChooseNumberNGUI h1;
	public ChooseNumberNGUI h2;		
	public ChooseNumberNGUI h3;
	public ChooseNumberNGUI h4;

	public ChooseNumberNGUI w1;
	public ChooseNumberNGUI w2;
	public ChooseNumberNGUI w3;
	public ChooseNumberNGUI w4;
	// Use this for initialization
	void OnClick(){
		Save ();
	}

	public void Save() {

		float nh1 = h1.chooseNumber * 100;
		float nh2 = h2.chooseNumber * 10;
		float nh3 = h3.chooseNumber ;
		float nh4 = h4.chooseNumber * 0.1f;

		float nw1 = w1.chooseNumber * 100;
		float nw2 = w2.chooseNumber * 10;
		float nw3 = w3.chooseNumber ;
		float nw4 = w4.chooseNumber * 0.1f;


		float h = nh1 + nh2 + nh3 + nh4;
		float w = nw1 + nw2 + nw3 + nw4;

		string nh = h.ToString ();
		string nw = w.ToString ();

//		print ("h = " + nh);
//		print ("w = " + nw);

		f (DateTime.Now ,h,w);
//		string JsonHeightWaistlineDataPath = Application.persistentDataPath + "/HeightWaistline.txt"; //路徑
//		
//		var hw = new{
//			Date = DateTime.Now,
//			Height = nh ,
//			Waistline = nw
//		};
//		
//		List<object> HW = null;
//		
//		if (File.Exists(JsonHeightWaistlineDataPath)) {
//			HW = (JsonConvert.DeserializeObject<JObject>(File.ReadAllText(JsonHeightWaistlineDataPath))["HW"] as JArray).ToObject<List<object>>(); //
//			HW.Add(hw); //寫入
//
////			JArray ja = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(JsonHeightWaistlineDataPath))["HeightWaistline"] as JArray;
//
//		} else {
//			HW = new List<object> (){hw}; //新建
//		}
//		File.WriteAllText(JsonHeightWaistlineDataPath, JsonConvert.SerializeObject(new{HW},Formatting.Indented)); //存入

	}
	
	
	public void f(DateTime d,float h,float w) {
		

	
		string JsonHeightWaistlineDataPath = Application.persistentDataPath + "/HeightWaistline.txt"; //路徑
		
		var hw = new{
			Date = d,
			Height = h.ToString() ,
			Waistline = w.ToString()
		};
		
		List<object> HW = null;
		
		if (File.Exists(JsonHeightWaistlineDataPath)) {
			HW = (JsonConvert.DeserializeObject<JObject>(File.ReadAllText(JsonHeightWaistlineDataPath))["HW"] as JArray).ToObject<List<object>>(); //
			HW.Add(hw); //寫入
			
			//			JArray ja = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(JsonHeightWaistlineDataPath))["HeightWaistline"] as JArray;
			
		} else {
			HW = new List<object> (){hw}; //新建
		}
		File.WriteAllText(JsonHeightWaistlineDataPath, JsonConvert.SerializeObject(new{HW},Formatting.Indented)); //存入
		
	}

	public float GetDayHeight(DateTime day){
		string JsonHeightWaistlineDataPath = Application.persistentDataPath + "/HeightWaistline.txt"; //路徑
		float height = 0;
		if (File.Exists (JsonHeightWaistlineDataPath)) {
			JArray ja = JsonConvert.DeserializeObject<JObject> (File.ReadAllText (JsonHeightWaistlineDataPath)) ["HW"] as JArray;

//			Debug.Log("ja.Count = " + ja.Count); //

			for(int i=0;i < ja.Count ; i++){
				DateTime dt = (DateTime) ja[i]["Date"] ;

//				Debug.Log("dt = " + dt);

				if(dt.Date == day.Date ){

//					Debug.Log("dt.Date = " + dt.Date + " day.Date = " + day.Date);

					height = float.Parse(ja[i]["Height"].ToString());
					break;
				}
			}
		}
		return height;
	}

	public float GetLastDayHeight(DateTime day){
		string JsonHeightWaistlineDataPath = Application.persistentDataPath + "/HeightWaistline.txt"; //路徑
		float lastHeight = 0;
		DateTime tmp = new DateTime(1900,1,1); 


		if (File.Exists (JsonHeightWaistlineDataPath)) {
			JArray ja = JsonConvert.DeserializeObject<JObject> (File.ReadAllText (JsonHeightWaistlineDataPath)) ["HW"] as JArray;
			for(int i=0 ;i < ja.Count ; i++){
				DateTime dt = (DateTime)ja[i]["Date"];
				if(day > tmp && dt.Date <= day.Date){ //
					tmp = dt; 
					if(tmp.Date <= day.Date){
						lastHeight = float.Parse(ja[i]["Height"].ToString()) ;
					}
				}
			}
		}
		return lastHeight;
	}

	public float GetDayWaistline(DateTime day){
		string JsonHeightWaistlineDataPath = Application.persistentDataPath + "/HeightWaistline.txt"; //路徑
		float Waistline = 0;
		if (File.Exists (JsonHeightWaistlineDataPath)) {
			JArray ja = JsonConvert.DeserializeObject<JObject> (File.ReadAllText (JsonHeightWaistlineDataPath)) ["HW"] as JArray;
			for(int i=0;i < ja.Count ; i++){
				DateTime dt = (DateTime) ja[i]["Date"] ;
				if(dt.Date == day.Date ){
					Waistline = float.Parse(ja[i]["Waistline"].ToString());
					break;
				}
			}
		}
		return Waistline;
	}

	public float GetLastDayWaistline(DateTime day){
		string JsonHeightWaistlineDataPath = Application.persistentDataPath + "/HeightWaistline.txt"; //路徑
		float lastWaistline = 0;
		DateTime tmp = new DateTime(1900,1,1); 
		
		if (File.Exists (JsonHeightWaistlineDataPath)) {
			JArray ja = JsonConvert.DeserializeObject<JObject> (File.ReadAllText (JsonHeightWaistlineDataPath)) ["HW"] as JArray;
			for(int i=0 ;i < ja.Count ; i++){
				DateTime dt = (DateTime)ja[i]["Date"];
				if(day > tmp && day.Date <= day.Date){
					tmp = dt; 
					if(tmp.Date <= day.Date){
						lastWaistline = float.Parse(ja[i]["Waistline"].ToString()) ;
					}
				}
			}
		}
		return lastWaistline;
	}

}