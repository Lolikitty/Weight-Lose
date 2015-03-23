using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;


public class t1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string st1="12:13"; 
		string st2="14:14"; 
		DateTime dt1 = new DateTime (2015, 2, 3);//Convert.ToDateTime(st1); 
		DateTime dt2 = new DateTime (2016,2,2);//Convert.ToDateTime(st2); 
		DateTime dt3 = DateTime.Now; 
		TimeSpan t = dt3 - dt1;

//		print (dt1.Subtract(dt2).Days);
//		Debug.Log (dt4.Year);
		/*
		if (DateTime.Compare (dt1, dt2) > 0) 
			print (dt1 + "  >  " + dt2);
		else 
			print (dt2 + "  <  " + dt2);
			*/

//		string JsonHeightWaistlineDataPath = Application.persistentDataPath + "/User.txt"; //路徑
//		
//		if (File.Exists (JsonHeightWaistlineDataPath)) {
//			JObject j = JsonConvert.DeserializeObject<JObject> (File.ReadAllText (JsonHeightWaistlineDataPath));
//			DateTime d = (DateTime)(j ["Birthday"]);
//
//		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
