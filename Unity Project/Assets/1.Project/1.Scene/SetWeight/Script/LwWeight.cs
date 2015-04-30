using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LwWeight : MonoBehaviour {

	public GameObject buttonOk;

	public ChooseNumber NW1, NW2, NW3, NW4, WW1, WW2, WW3, WW4, M1, M2;

	// Unity Override Methods ==============================================================================================================================

	void Awake () {
		UIEventListener.Get(buttonOk).onClick = ButtonOk;
	}

	// Custom Methods ======================================================================================================================================
	
	string weightInit;
	string weightTarget;
	string weightTargetMonth;

	void ButtonOk(GameObject button){

		LwMainMenu.UploadBirthdayAndWeight = true;

		weightInit = "" + float.Parse ("" + NW1.chooseNumber + NW2.chooseNumber + NW3.chooseNumber + "." + NW4.chooseNumber);
		weightTarget = "" + float.Parse ("" + WW1.chooseNumber + WW2.chooseNumber + WW3.chooseNumber + "." + WW4.chooseNumber);
		weightTargetMonth = "" + int.Parse("" + M1.chooseNumber + M2.chooseNumber);

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
			obj["WeightInit"] = weightInit;
			obj["WeightTarget"] = weightTarget;
			obj["WeightTargetMonth"] = weightTargetMonth;
			data = obj;
		}else{
			data = new {
				WeightInit = weightInit,
				WeightTarget = weightTarget,
				WeightTargetMonth = weightTargetMonth
			};
		}
		File.WriteAllText(JsonUserDataPath, JsonConvert.SerializeObject(data,Formatting.Indented));

		//---------- Json

		//storage to weight.txt
		SaveWeight ss = new SaveWeight ();
		float w = float.Parse(weightInit);
		ss.f (DateTime.Now,w);

//		if(LwUserCamera.toWeight){
//			Application.LoadLevel ("User");	
//		}else{
//				
//		}

		Application.LoadLevel ("SetHeight");

	}

}
