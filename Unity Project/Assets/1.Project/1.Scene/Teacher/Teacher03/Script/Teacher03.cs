using UnityEngine;
using System.Collections;
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Teacher03 : MonoBehaviour {

	public GameObject cancel;
	public GameObject agree;

	public GameObject err;
	public GameObject errBtn;


	public UILabel phone_l;
	public UILabel line_l;
	public UILabel skype_l;
	public UILabel smallsin_l;
	
	string phone_number;
	string line_id;
	string skype_id;
	string smallsin_id;



	
	// Use this for initialization
	void Start () {
		
		UIEventListener.Get (cancel).onClick = Cancel;
		UIEventListener.Get (agree).onClick = Agree;
		
		UIEventListener.Get (errBtn).onClick = ErrBtn;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void Cancel(GameObject obj){
		
	//	Application.LoadLevel ("User");
		
	}

	void ErrBtn(GameObject obj){

		err.SetActive(false);
	}

	//須確認資料
	void Agree(GameObject obj){

		StartCoroutine (UploadData());
	}

	IEnumerator UploadData(){

		string JsonUserDataPath = Application.persistentDataPath + "/User.txt"; //
		string id = "";

		if (File.Exists (JsonUserDataPath)) {
						JObject obj = JsonConvert.DeserializeObject<JObject> (File.ReadAllText (JsonUserDataPath));
						id = obj ["ID"].ToString ();
		}

		WWWForm f = new WWWForm ();
		f.AddField ("id", id);
		f.AddField ("phone_l", phone_l.text);
		f.AddField ("line_l", line_l.text);
		f.AddField ("skype_l", skype_l.text);
		f.AddField ("smallsin_l", smallsin_l.text);

		using( WWW w = new WWW (LwInit.HttpServerPath+"/ApplyCoach", f)){

			yield return w;

			if(string.IsNullOrEmpty(w.error)){
				//

				int coachCost = 0;
				SaveMissionLv sml = new SaveMissionLv ();
				int lv = sml.GetMissionNowLv (10);
				sml.Save(DateTime.Now,10,lv+1);
				int standardBaseCost = 10000;
				coachCost = lv * standardBaseCost;

				SaveCost sc = new SaveCost();
				sc.Save(DateTime.Now,coachCost);

				print (w.text);
				Application.LoadLevel ("Teacher04");
			}else{
				print ("Error: " + w.error);
				err.SetActive(true);
			}

		}

//		Application.LoadLevel ("Teacher04");
	}





}
