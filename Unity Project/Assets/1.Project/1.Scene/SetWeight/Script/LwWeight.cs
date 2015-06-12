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

		string JsonUserDataPath = Application.persistentDataPath + "/User.txt";

		if(File.Exists(JsonUserDataPath)){
			JObject obj = JsonConvert.DeserializeObject<JObject> (File.ReadAllText(JsonUserDataPath));

			// Set WeightInit ----------------------------------------------------------------------------

			string WeightInit =  float.Parse(obj["WeightInit"].ToString()).ToString("000.0");

			char [] WeightInitChar = WeightInit.ToCharArray();

			int WeightInit0 = int.Parse(WeightInitChar[0].ToString());
			int WeightInit1 = int.Parse(WeightInitChar[1].ToString());
			int WeightInit2 = int.Parse(WeightInitChar[2].ToString());
			int WeightInit3 = int.Parse(WeightInitChar[4].ToString());

			NW1.SetNumber (WeightInit0);
			NW2.SetNumber (WeightInit1);
			NW3.SetNumber (WeightInit2);
			NW4.SetNumber (WeightInit3);

			// Set WeightTarget ----------------------------------------------------------------------------

			string WeightTarget =  float.Parse(obj["WeightTarget"].ToString()).ToString("000.0");
			
			char [] WeightTargetChar = WeightTarget.ToCharArray();
			
			int WeightTarget0 = int.Parse(WeightTargetChar[0].ToString());
			int WeightTarget1 = int.Parse(WeightTargetChar[1].ToString());
			int WeightTarget2 = int.Parse(WeightTargetChar[2].ToString());
			int WeightTarget3 = int.Parse(WeightTargetChar[4].ToString());
			
			WW1.SetNumber (WeightTarget0);
			WW2.SetNumber (WeightTarget1);
			WW3.SetNumber (WeightTarget2);
			WW4.SetNumber (WeightTarget3);

			// Set WeightTargetMonth ----------------------------------------------------------------------------

			string WeightTargetMonth =  float.Parse(obj["WeightTargetMonth"].ToString()).ToString("00");
			
			char [] WeightTargetMonthChar = WeightTargetMonth.ToCharArray();
			
			int WeightTargetMonth0 = int.Parse(WeightTargetMonthChar[0].ToString());
			int WeightTargetMonth1 = int.Parse(WeightTargetMonthChar[1].ToString());
			
			M1.SetNumber (WeightTargetMonth0);
			M2.SetNumber (WeightTargetMonth1);
		}
		
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
