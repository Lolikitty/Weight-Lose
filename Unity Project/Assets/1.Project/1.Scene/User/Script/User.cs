using UnityEngine;
using System.Collections;
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class User : MonoBehaviour {
	
//	public GameObject buttonExit;
	public GameObject buttonHead;
	public GameObject buttonEdit;
	public GameObject coach;
	
	public UILabel id;
	public UILabel birthday;
	public UILabel weightFirst;
	public UILabel weightTarget;
	public UILabel weightToday;
	
	public GameObject FoodFather;
	public GameObject Food;
	
	public UILabel name;
	public UITexture head;
	public UITexture boy;
	public UITexture girl;

	public GameObject red; 
	public GameObject blue; 
	Boolean once = true; 

	
	public UICamera cam;
	
//	public NewBehaviourScript nbs;
	
//	GameObject vc;
	
	float y,y2;
	
	bool isDown = false;
	
	// Unity Override Methods ==============================================================================================================================

	void Awake () {

		try{
		msg += "A";
//		UIEventListener.Get(buttonExit).onClick = ButtonExit;
		UIEventListener.Get(buttonHead).onClick = ButtonHead;
		UIEventListener.Get(buttonEdit).onClick = ButtonEdit;
		//		UIEventListener.Get (coach).onClick = Coach;
//		name.text = PlayerPrefs.GetString ("name");
//		if(name.text == ""){
//			name.text = "?????????";
//		}

		msg += "B";

		string idStr = "xxx";
		string nameStr ="???";
		string y = "0";
		string m = "0";
		string d = "0";
		string firstW = "0";
		string goalW = "0";
		string nowW = "0";

		string sex = "";

		string JsonUserDataPath = Application.persistentDataPath + "/User.txt"; //
		msg += "C";
		if (File.Exists(JsonUserDataPath)) {

				msg += "File.Exists(JsonUserDataPath) = true\n";

			JObject obj = JsonConvert.DeserializeObject<JObject> (File.ReadAllText(JsonUserDataPath));
				msg += "D";
			idStr = obj["ID"].ToString();
				msg += "E";
			nameStr = obj["Name"].ToString();
				msg += "F";
			sex = obj["Sex"].ToString();
				msg += "G";
			DateTime bir = (DateTime)obj["Birthday"];
				msg += "H";
			int year = bir.Year;
			int month = bir.Month;
			int day = bir.Day;
			y = year.ToString();
			m = month.ToString();
			d = day.ToString();
			firstW = obj["WeightInit"].ToString();
				msg += "I";
			goalW = obj["WeightTarget"].ToString();
				msg += "J";
				msg += "K";
			SaveWeight ss = new SaveWeight();
			nowW = ss.GetLastDayWeight(DateTime.Now).ToString();
		}
		name.text = nameStr;
		id.text = idStr;
		birthday.text = y + "/" + m + "/" + d + " "; 
		weightFirst.text = firstW;
		weightTarget.text = goalW;
		weightToday.text = nowW;
		if (sex == "Boy") {
				msg += "L";
			boy.gameObject.SetActive(true);
			girl.gameObject.SetActive(false);
		} else if (sex == "Girl") {
				msg += "M";
			boy.gameObject.SetActive(false);
			girl.gameObject.SetActive(true);
		}
		//id.text = JsonConvert.DeserializeObject<JObject> (File.ReadAllText(Application.persistentDataPath + "/User.txt"))["ID"].ToString();
		
		}catch(Exception e){
			msg += "\n\n" + e.Message;
		}

//		#if UNITY_ANDROID
//		#endif
//		
//		#if !UNITY_ANDROID
//		#endif
		
	}

	string msg = "0";
	void OnGUI(){
		//GUILayout.Label (msg);
		//Application.LoadLevel ("MainMenu");
	}

	IEnumerator Start(){
		WWW wwww = new WWW ("file://" + Application.persistentDataPath + "/User.png");
		yield return wwww;
		head.mainTexture = wwww.texture;
		
		string nowDate = DateTime.Now.ToString ("yyyy-MM-dd");
		string nowPath = Application.persistentDataPath + "/" + nowDate + "/";
		
//		int i = 1;
//		int x = -100;
		
//		while(File.Exists(nowPath + i + ".png")){
//			GameObject newFood = Instantiate(Food) as GameObject;
//			newFood.transform.parent = FoodFather.transform;
//			newFood.transform.localPosition = new Vector3 (x, -37, 0);
//			newFood.transform.localScale = new Vector3 (1, 1, 1);
//			newFood.GetComponent <UIDragScrollView> ().scrollView = FoodFather.GetComponent <UIScrollView> ();
//			
//			WWW www = new WWW ("file://" + nowPath + i + ".png");
//			yield return www;
//			Texture2D t = www.texture;
//			newFood.GetComponent <UITexture> ().mainTexture = t;
//			
//			i++;
//			x += 110;
//		}

//		int x;

		string JsonFoodDataPath = Application.persistentDataPath + "/Food.txt";
		
		if(File.Exists(JsonFoodDataPath)){

			msg += "File.Exists(JsonFoodDataPath) = true\n";

			JArray ja = JsonConvert.DeserializeObject<JObject> (File.ReadAllText (JsonFoodDataPath)) ["Food"] as JArray;
			
			for(int i = 0, x = -100; i < ja.Count; i++){
				
				DateTime dt = (DateTime) ja[i]["Date"];
				
				if(dt.Date == DateTime.Today){
					GameObject newFood = Instantiate(Food) as GameObject;
					newFood.transform.parent = FoodFather.transform;
					newFood.transform.localPosition = new Vector3 (x, -37, 0);
					newFood.transform.localScale = new Vector3 (1, 1, 1);
					newFood.GetComponent <UIDragScrollView> ().scrollView = FoodFather.GetComponent <UIScrollView> ();
					
					WWW www = new WWW ("file://" + Application.persistentDataPath + ja[i]["PNGPath"].ToString());
					yield return www;
					Texture2D t = www.texture;
					newFood.GetComponent <UITexture> ().mainTexture = t;
					www.Dispose();
					x += 110;
				}
			}
		}
		
//		nbs.isUserPage = true;
//		vc = GameObject.Find ("VectorCam");
		
	}

	bool loc = false;
	bool isDownFirst = true;
	
	void Update () {
		
		if(Input.GetMouseButtonDown(0)){
//			if(!loc){
//				loc = true;
//				Invoke("Loc",0.5f);
//				y = Input.mousePosition.y;
//			}
			y = Input.mousePosition.y;
		}
		
		if(Input.GetMouseButton(0)){
			y2 = Input.mousePosition.y;
			
			if(y-y2 > 100){
				isDown = false;
			}else if(y-y2 < -100){
				isDown = true;
			}
		}
		if (cam.transform.localPosition.y > -780) {
			Stop ();
		} else {
			Run();
		}
//		if(vc != null){


//		if(cam.transform.localPosition.y < -780){
//			red.SetActive (true); 
//			blue.SetActive (true); 		
//		}else{
//			red.SetActive (false); 
//			blue.SetActive (false); 
//		}


//		if (!loc) {
		if(isDown){
			cam.transform.localPosition = new Vector3(0,Mathf.Lerp(cam.transform.localPosition.y,-800,Time.deltaTime * 5));	
				if(once){
					Invoke("Run",0.5f);
					once = false;
				}				
		}else{
			cam.transform.localPosition = new Vector3(0,Mathf.Lerp(cam.transform.localPosition.y, 0, Time.deltaTime * 5));
				if(!once){
					Invoke("Stop",0);
					once = true;
				}
//				isDownFirst = true;
		}
//		}

			
			/*
			if(cam.transform.localPosition.y < -700){
//				vc.GetComponent<Camera>().enabled = true;
	//				nbs.enabled = true;
//				if(once){
//					Invoke("Run",0.5f);
//					once = false;
//				}
				Invoke("Run",0.5f);
			}else{
			//}else if(cam.transform.localPosition.y > -1 || cam.transform.localPosition.y > -795){
//				vc.GetComponent<Camera>().enabled = false;
//				nbs.enabled = false;
			
				if(!once){
					Invoke("Stop",0);
					once = true;
				}
			
			}
			*/
//		}
	}

//	void Loc(){
//		loc = false;
//	}
//
	void Run(){
		red.SetActive (true); 
		blue.SetActive (true);  //***
	}

	void Stop(){
		red.SetActive (false); 
		blue.SetActive (false); 
	}

	
	// Custom Methods ======================================================================================================================================
	
	
	
//	void ButtonExit(GameObject button){
//		Application.LoadLevel ("MainMenu");
//	}
	
	void ButtonHead(GameObject button){
		//		Application.LoadLevel ("User2");
	}
	
	void ButtonEdit(GameObject button){
		Application.LoadLevel ("User3");
	}

	void OnApplicationQuit(){
		//		PlayerPrefs.DeleteKey ("ID");
	}
	
}