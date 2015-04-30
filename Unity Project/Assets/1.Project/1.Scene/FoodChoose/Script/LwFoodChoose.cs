using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using Prime31;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LwFoodChoose : MonoBehaviour {

	public GameObject buttonFoodCamera;
	public GameObject buttonOK;
	public GameObject buttonCancel;
	public GameObject buttonDIY_Add;
	public GameObject buttonFoodDIY;
	public GameObject buttonFood;
	public GameObject chooseFood;
	public GameObject food2;
	public GameObject root;	
	public UIScrollView fsv1, fsv2, diysv;
	public UIScrollView findSV;
	public UILabel findText;
	public UIInput input;

	List<string> find = new List<string>();	
	bool buttonLuck = false;	
	GameObject bgDIY;
	string findTemp = "";
	bool isDIY = false;
	JArray ja;

	void Awake () {
		UIEventListener.Get(buttonFoodCamera).onClick = ButtonFoodCamera;
		UIEventListener.Get(buttonOK).onClick = ButtonOK;
		UIEventListener.Get(buttonCancel).onClick = ButtonCancel;
		UIEventListener.Get(buttonDIY_Add).onClick = ButtonDIY_Add;
		UIEventListener.Get(buttonFoodDIY).onClick = ButtonFoodDIY;
		bgDIY = buttonFoodDIY.transform.GetChild(0).gameObject;
		bgDIY.SetActive (false);
		buttonDIY_Add.SetActive (true);
		#if UNITY_ANDROID
		EtceteraAndroid.initTTS();
		#endif
	}

	void Start(){
		ja = JsonConvert.DeserializeObject<JObject> (File.ReadAllText(Application.persistentDataPath + "/FoodMenu.txt"))["Food"] as JArray;

		for(int i = 0, y = 77; i< ja.Count; i++, y-=70){
			string name = ja[i]["Name"].ToString();
			GameObject g = Instantiate(buttonFood) as GameObject;
			g.transform.parent = fsv1.transform;
			g.transform.localScale = Vector3.one;
			g.transform.localPosition = new Vector3(-100, y);
			g.GetComponent<UILabel>().text = name;
			g.GetComponent<ChooseFood>().foods = ja[i]["Foods"] as JArray;
			g.GetComponent<ChooseFood>().fsv2 = fsv2;
			g.GetComponent<ChooseFood>().food2 = food2;
			g.GetComponent<ChooseFood>().diyAdd = buttonDIY_Add;
		}

		ChooseFood.bgs.Add (bgDIY);
	}

	void Update () {
		if(!string.IsNullOrEmpty(input.value)){
			if(findTemp != input.value){
				for(int i = 0;i<findSV.transform.childCount;i++){
					Destroy(findSV.transform.GetChild(i).gameObject);
				}
				root.SetActive(false);
				int y = 150;
				for(int i = 0; i < ja.Count; i++){
					JArray ja2 = ja[i]["Foods"] as JArray;
					for(int k = 0; k < ja2.Count; k++){
						string foodName = ja2[k]["Name"].ToString();
						if(foodName.IndexOf(input.value) != -1){
							GameObject g = Instantiate(food2) as GameObject;
							g.transform.parent = findSV.transform;
							g.transform.localScale = Vector3.one;
							g.transform.localPosition = new Vector3(22, y);
							g.GetComponent<UILabel>().text = foodName;
							y -= 70;
						}
					}
				}
				findSV.OnScrollBar();
			}
		}else{
			root.SetActive(true);
		}
		findTemp = input.value;
	}

	void ButtonDIY_Add(GameObject button){
		LwMainMenu.IsChooseAddFood = true;
		LwMainMenu.IsAddFood = true;
		Application.LoadLevel ("FoodCamera");
	}

	void Button_Search(GameObject button){
		#if UNITY_ANDROID
		EtceteraAndroid.promptForPictureFromAlbum("a");
		#endif
	}

	void ButtonFoodDIY(GameObject button){
		buttonDIY_Add.SetActive (true);
		fsv2.gameObject.SetActive (false);
		foreach(GameObject g in ChooseFood.bgs){
			g.SetActive(false);
		}
		bgDIY.SetActive (true);

//		string [] files = Directory.GetFiles(Application.persistentDataPath+"/Food","*.info");
//		string [] filesJPG = Directory.GetFiles(Application.persistentDataPath+"/Food","*.jpg");
//		string [] filesPNG = Directory.GetFiles(Application.persistentDataPath+"/Food","*.png");
//
//		ArrayList foods = new ArrayList ();
//		ArrayList foodsKal = new ArrayList ();
//
//		for(int i=0; i < files.Length; i++){
//			string saveFoodName = files[i].Split('_')[2];
//			string saveFoodKal = files[i].Split('_')[1];
//			foods.Add(saveFoodName);
//			foodsKal.Add(saveFoodKal);
//		}


//		string [] foodName = {"可樂","烤香腸","可麗餅","布丁","珍珠奶茶","豆花","蝦捲","太陽餅"};

//		for(int i = 0, y = 150; i < foods.Count; i++ , y -= 70){
//			GameObject food = Instantiate(chooseFood) as GameObject;
//			food.transform.parent = diysv.transform;
//			food.transform.localScale = Vector3.one;
//			food.transform.localPosition = new Vector3 (21, y);
//			food.GetComponent<LwChooseFood>().buttonName = foods[i].ToString();
//			food.GetComponent<LwChooseFood>().buttonKal = foodsKal[i].ToString();
//			food.GetComponent<LwChooseFood>().buttonPath = files[i];
//			food.GetComponent<LwChooseFood>().buttonPathJPG = filesJPG[i];
//			food.GetComponent<LwChooseFood>().buttonPathPNG = filesPNG[i];
//			food.GetComponent<UILabel>().text = foods[i].ToString();
//		}
		diysv.OnScrollBar ();
	}

	void ButtonOK(GameObject button){
//		if(isDIY){
//			string nowDate = DateTime.Now.ToString ("yyyy-MM-dd");
//			string nowPath = Application.persistentDataPath + "/" + nowDate;
//			
//			if(!Directory.Exists(nowPath)){
//				Directory.CreateDirectory(nowPath);
//			}
//			
//			int i = 1;
//			
//			while(File.Exists(nowPath + "/" + i + ".png")){
//				i++;
//			}
//
//			File.Copy(LwChooseFood.choosePathJPG,nowPath + "/" + i + ".jpg");
//			File.Copy(LwChooseFood.choosePathPNG,nowPath + "/" + i + ".png");
//			File.Create(nowPath+"/" + i + "._" + LwChooseFood.chooseKal + "_" +LwChooseFood.chooseName+ "_.info");
//
//			print (LwChooseFood.choosePathJPG);
//			print (LwChooseFood.choosePathPNG);
//
//
//
////			print (LwChooseFood.choosePath.s);
//		}else{
//
//		}
//
//		if(isDIY){
//			LwMainMenu.IsChooseFoodFinish_DIY = true;
//		}else{
//			LwMainMenu.IsChooseFoodFinish_Default = true;
//		}
//		Application.LoadLevel ("MainMenu");
	}

	void ButtonFoodCamera(GameObject button){
		Application.LoadLevel ("FoodCamera");
	}

	void ButtonCancel(GameObject button){
		Application.LoadLevel ("MainMenu");
	}


}
