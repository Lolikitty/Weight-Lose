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

	void Start(){
		int h1 = 0;
		int h2 = 0;
		int h3 = 0;
		int h4 = 0;
		int w1 = 0;
		int w2 = 0;
		int w3 = 0;
		int w4 = 0;
		SaveHeightWaistline hw = new SaveHeightWaistline ();
		float h = hw.GetLastDayHeight (DateTime.Now);
		float w = hw.GetLastDayWaistline (DateTime.Now);
//		Debug.Log ("h = " + h);
//		Debug.Log ("w = " + w);

		h1 = (int)h / 100;
		h2 = (int)h / 10 - h1 * 10;
		h3 = (int)h % 10;
		int tmp = (int)h;
		float h4tmp = (h - (float)tmp)*10;
//		Debug.Log ("h4tmp = " + h4tmp);
		h4 = (int)(h4tmp);

		w1 = (int)w / 100;
		w2 = (int)w / 10 - w1 * 10;
		w3 = (int)w % 10;
		int tmpW = (int)w;
		float w4tmp = (w - (float)tmpW)*10;
		w4 = (int)(w4tmp);

		HW1.Set_number (h1);
		HW2.Set_number (h2);
		HW3.Set_number (h3);
		HW4.Set_number (h4);
		WW1.Set_number (w1);
		WW2.Set_number (w2);
		WW3.Set_number (w3);
		WW4.Set_number (w4);

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
		
		Application.LoadLevel ("MainMenu");	
		
	}
	
}
