using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LwUser3 : MonoBehaviour {

	public GameObject buttonChangeImage;
	public GameObject buttonChangeName;
	public GameObject buttonOK;

	public UITexture head;

	public UILabel uiName;

	string temp = "";
	TouchScreenKeyboard tsk;

	// Unity Override Methods ==============================================================================================================================

	void Awake () {

		uiName.text = "";

		//try{
		string JsonUserDataPath = Application.persistentDataPath + "/User.txt";
		object data = null;
		if(File.Exists(JsonUserDataPath)){
			JObject obj = JsonConvert.DeserializeObject<JObject> (File.ReadAllText(JsonUserDataPath));
			try{
					uiName.text = obj["Name"].ToString();
			}catch{
			}

		}
	

		UIEventListener.Get(buttonChangeImage).onClick = ButtonChangeImage;
		UIEventListener.Get(buttonChangeName).onClick = ButtonChangeName;
		UIEventListener.Get(buttonOK).onClick = ButtonOK;
		//}catch(Exception e){
		//	LwError.Show("error:" + e.ToString());
		//}
	}

	IEnumerator Start(){
		WWW www = new WWW ("file://" + Application.persistentDataPath + "/User.png");
		yield return www;
		head.mainTexture = www.texture;
	}

	void Update(){
		if(tsk != null){
			uiName.text = tsk.text;
		}
	}

	// Custom Methods ======================================================================================================================================

	void ButtonChangeImage(GameObject button){
		Application.LoadLevel ("UserCamera");
	}

	void ButtonChangeName(GameObject button){
		#if !UNITY_EDITOR
		tsk = TouchScreenKeyboard.Open (temp);
		#endif
	}

	IEnumerator UploadData(){

		WWWForm wwwF = new WWWForm();
		wwwF.AddField("id", PlayerPrefs.GetString ("ID"));
		wwwF.AddField("name", uiName.text);
				
		WWW www = new WWW(LwInit.HttpServerPath+"/UserName", wwwF);
		yield return www;



	}

	void ButtonOK(GameObject button){
		if(uiName.text.IndexOf("+") != -1) return;
		if(uiName.text.IndexOf("-") != -1) return;
		if(uiName.text.IndexOf("*") != -1) return;
		if(uiName.text.IndexOf("/") != -1) return;
		if(uiName.text.IndexOf("\\") != -1) return;
		if(uiName.text.IndexOf("_") != -1) return;
		if(uiName.text.IndexOf(":") != -1) return;
		if(uiName.text.IndexOf("?") != -1) return;
		if(uiName.text.IndexOf("<") != -1) return;
		if(uiName.text.IndexOf(">") != -1) return;
		if(uiName.text.IndexOf("\"") != -1) return;
		if(uiName.text.IndexOf("'") != -1) return;
		if(uiName.text.IndexOf("|") != -1) return;

		PlayerPrefs.SetString ("name", uiName.text);
		PlayerPrefs.Save ();

		string JsonUserDataPath = Application.persistentDataPath + "/User.txt";
		object data = null;
		if(File.Exists(JsonUserDataPath)){
			JObject obj = JsonConvert.DeserializeObject<JObject> (File.ReadAllText(JsonUserDataPath));
			obj["Name"] = uiName.text;
			data = obj;
		}else{
			data = new {
				Name = uiName.text
			};
		}
		File.WriteAllText(JsonUserDataPath, JsonConvert.SerializeObject(data,Formatting.Indented));



		StartCoroutine (UploadData());

		//Application.LoadLevel ("SetBirthday");
	}

}



