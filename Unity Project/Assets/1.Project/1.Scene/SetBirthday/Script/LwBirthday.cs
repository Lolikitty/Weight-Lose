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

	void Start(){
		string JsonUserDataPath = Application.persistentDataPath + "/User.txt"; //
		int y1 = 0;
		int y2 = 0;
		int y3 = 0;
		int y4 = 0;
		int m1 = 0;
		int m2 = 0;
		int d1 = 0;
		int d2 = 0;

		if (File.Exists (JsonUserDataPath)) {
			JObject obj = JsonConvert.DeserializeObject<JObject> (File.ReadAllText (JsonUserDataPath));
			DateTime bir = (DateTime) obj["Birthday"];
			int year = bir.Year;
			int month = bir.Month;
			int day = bir.Day;
			y1 = year / 1000;
			y2 = year / 100 - y1 * 10;
			y3 = year / 10 - y1 * 100 - y2 * 10 ;
			y4 = year % 10;
			m1 = month / 10;
			m2 = month - m1 * 10;
			d1 = day / 10;
			d2 = day - d1 *10;
		}
		Y1.SetNumber (y1);
		Y2.SetNumber (y2);
		Y3.SetNumber (y3);
		Y4.SetNumber (y4);
		M1.SetNumber (m1);
		M2.SetNumber (m2);
		D1.SetNumber (d1);
		D2.SetNumber (d2);
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
		DateTime d = new DateTime(yyyy,MM,dd);
		if(File.Exists(JsonUserDataPath)){
			JObject obj = JsonConvert.DeserializeObject<JObject> (File.ReadAllText(JsonUserDataPath));
//			DateTime d = new DateTime(yyyy,MM,dd);
			obj["Birthday"] = d;
			obj["Sex"] = sex;
			data = obj;
		}else{
			data = new {
				Birthday = d,
				Sex = sex
			};
		}
		File.WriteAllText(JsonUserDataPath, JsonConvert.SerializeObject(data,Formatting.Indented));

		//---------- Json

		Application.LoadLevel ("SetWeight");
	}
	
}
