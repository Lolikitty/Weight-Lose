using UnityEngine;
using System.Collections;
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class User : MonoBehaviour {

	public GameObject buttonExit;
	public GameObject buttonHead;
	public GameObject buttonEdit;

	public UILabel id;
	public UILabel birthday;
	public UILabel weightFirst;
	public UILabel weightTarget;
	public UILabel weightToday;

	public GameObject FoodFather;
	public GameObject Food;

	public UILabel name;

	public UITexture head;

	public UICamera cam;

	public NewBehaviourScript nbs;

	GameObject vc;

	float y,y2;
	
	bool isDown = false;

	// Unity Override Methods ==============================================================================================================================

	void Awake () {
		UIEventListener.Get(buttonExit).onClick = ButtonExit;
		UIEventListener.Get(buttonHead).onClick = ButtonHead;
		UIEventListener.Get(buttonEdit).onClick = ButtonEdit;
		name.text = PlayerPrefs.GetString ("name");
		if(name.text == ""){
			name.text = "請至編輯區輸入姓名";
		}
		id.text = PlayerPrefs.GetString ("ID");

		#if UNITY_ANDROID
		EtceteraAndroid.initTTS();
		#endif

		#if !UNITY_ANDROID
		#endif
	}

	IEnumerator Start(){
		WWW wwww = new WWW ("file://" + Application.persistentDataPath + "/User.png");
		yield return wwww;
		head.mainTexture = wwww.texture;

//		string nowDate = DateTime.Now.ToString ("yyyy-MM-dd");
//		string nowPath = Application.persistentDataPath + "/" + nowDate + "/";

		string JsonFoodDataPath = Application.persistentDataPath + "/Food.txt";
		
		if(File.Exists(JsonFoodDataPath)){
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
			
			nbs.isUserPage = true;
			vc = GameObject.Find ("VectorCam");

		}

		string JsonUserDataPath = Application.persistentDataPath + "/User.txt";

		if(File.Exists(JsonUserDataPath)){
			JArray ja = JsonConvert.DeserializeObject<JObject> (File.ReadAllText (JsonUserDataPath)) ["User"] as JArray;



		}
	}



	void Update () {

		if(Input.GetMouseButtonDown(0)){
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

		if(vc != null){
			if(isDown){
				cam.transform.localPosition = new Vector3(0,Mathf.Lerp(cam.transform.localPosition.y,-800,Time.deltaTime * 5));
			}else{
				cam.transform.localPosition = new Vector3(0,Mathf.Lerp(cam.transform.localPosition.y, 0, Time.deltaTime * 5));
			}

			if(cam.transform.localPosition.y < -750){
				vc.GetComponent<Camera>().enabled = true;
				nbs.enabled = true;
			}else if(cam.transform.localPosition.y > -1 || cam.transform.localPosition.y > -750){
				vc.GetComponent<Camera>().enabled = false;
				nbs.enabled = false;
			}
		}
	}


	// 載入圖片
	public void imageLoaded(string imagePath){
		// 後面的 1f 代表解析度的意思，1 為最大
		EtceteraAndroid.scaleImageAtPath( imagePath, 1f );
		//testPlane.renderer.material.mainTexture = EtceteraAndroid.textureFromFileAtPath( imagePath );
	}

	// Custom Methods ======================================================================================================================================

	void ButtonExit(GameObject button){
		Application.LoadLevel ("MainMenu");
	}

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
