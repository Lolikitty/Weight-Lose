using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LwHeight : MonoBehaviour {
	
	public GameObject buttonOk;
	
	public ChooseNumberNGUI HW1, HW2, HW3, HW4, WW1, WW2, WW3, WW4;
	
	// Unity Override Methods ==============================================================================================================================
	
	void Awake () {
		UIEventListener.Get(buttonOk).onClick = ButtonOk;
	}
	
	// Custom Methods ======================================================================================================================================
	
	string heightNow;
	string waistNow;
	
	void ButtonOk(GameObject button){
		
		LwMainMenu.UploadBirthdayAndWeight = true;
		
		heightNow = "" + float.Parse ("" + HW1.chooseNumber + HW2.chooseNumber + HW3.chooseNumber + "." + HW4.chooseNumber);
		waistNow = "" + float.Parse ("" + WW1.chooseNumber + WW2.chooseNumber + WW3.chooseNumber + "." + WW4.chooseNumber);
	
		
		// 之後這裡要做檢查
		
		//		PlayerPrefs.SetString ("WeightFirst", WeightFirst);
		//		PlayerPrefs.SetString ("WeightTarget", WeightTarget);
		//		PlayerPrefs.SetString ("WeightTargetMonth", WeightTargetMonth);
		//		PlayerPrefs.Save ();
		
		//---------- Json
		
		string JsonUserDataPath = Application.persistentDataPath + "/User.txt";
		object data = null;
		if(File.Exists(JsonUserDataPath)){
			JObject obj = JsonConvert.DeserializeObject<JObject> (File.ReadAllText(JsonUserDataPath));
			obj["HeightNow"] = heightNow;
			obj["WaistNow"] = waistNow;
			data = obj;
		}else{
			data = new {
				HeightNow = heightNow,
				WaistNow = waistNow
			};
		}
		File.WriteAllText(JsonUserDataPath, JsonConvert.SerializeObject(data,Formatting.Indented));
		
		//---------- Json
		
		if(LwUserCamera.toWeight){
			Application.LoadLevel ("User2");	
		}else{
			Application.LoadLevel ("MainMenu");	
		}
		
	}
	
}
