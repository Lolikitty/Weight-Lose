using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LwBirthday : MonoBehaviour {

	public GameObject buttonOk;
	public GameObject buttonBoy;
	public GameObject buttonGirl;

	public ChooseNumber Y1, Y2, Y3, Y4, M1, M2, D1, D2;

	int yyyy;
	int MM;
	int dd;

	string sex = "Boy";

	// Unity Override Methods ==============================================================================================================================

	void Awake () {
		UIEventListener.Get(buttonOk).onClick = ButtonOk;
		UIEventListener.Get(buttonBoy).onClick = ButtonBoy;
		UIEventListener.Get(buttonGirl).onClick = ButtonGirl;
	}

	// Custom Methods ======================================================================================================================================

	void ButtonBoy(GameObject button){
		buttonBoy.GetComponent<UIButton> ().defaultColor = Color.white;
		buttonGirl.GetComponent<UIButton> ().defaultColor = Color.gray;
		sex = "Boy";
	}

	void ButtonGirl(GameObject button){
		buttonBoy.GetComponent<UIButton> ().defaultColor = Color.gray;
		buttonGirl.GetComponent<UIButton> ().defaultColor = Color.white;
		sex = "Girl";
	}

	void ButtonOk(GameObject button){

		yyyy = int.Parse("" + Y1.chooseNumber + Y2.chooseNumber + Y3.chooseNumber + Y4.chooseNumber);
		MM = int.Parse("" + M1.chooseNumber + M2.chooseNumber);
		dd = int.Parse("" + D1.chooseNumber + D2.chooseNumber);

		// 之後這裡要做檢查

//		PlayerPrefs.SetString ("Sex", "" + sex);
//		PlayerPrefs.SetString ("BirthdayYear", "" + yyyy);
//		PlayerPrefs.SetString ("BirthdayMonth", "" + MM);
//		PlayerPrefs.SetString ("BirthdayDay", "" + dd);
//		PlayerPrefs.Save ();

		LwMainMenu.UploadBirthdayAndWeight = true;

		//---------- Json

		string JsonUserDataPath = Application.persistentDataPath + "/User.txt";
		object data = null;
		if(File.Exists(JsonUserDataPath)){
			JObject obj = JsonConvert.DeserializeObject<JObject> (File.ReadAllText(JsonUserDataPath));
			obj["Birthday"] = DateTime.Parse(yyyy+"/"+MM+"/"+dd);
			obj["Sex"] = sex;
			data = obj;
		}else{
			data = new {
				Birthday = DateTime.Parse(yyyy+"/"+MM+"/"+dd),
				Sex = sex
			};
		}
		File.WriteAllText(JsonUserDataPath, JsonConvert.SerializeObject(data,Formatting.Indented));

		//---------- Json

		Application.LoadLevel ("SetWeight");
	}
	
}
