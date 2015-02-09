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

	public GameObject buttonFind;

	public GameObject buttonDIY_Add;

	public GameObject buttonFoodDIY;
	public GameObject buttonFood1;
	public GameObject buttonFood2;
	public GameObject buttonFood3;
	public GameObject buttonFood4;
	public GameObject buttonFood5;
	public GameObject buttonFood6;
	public GameObject buttonFood7;
	public GameObject buttonFood8;

	public UIScrollView fsv, diysv;

	public GameObject chooseFood;

	GameObject bgDIY;
	GameObject bg1;
	GameObject bg2;
	GameObject bg3;
	GameObject bg4;
	GameObject bg5;
	GameObject bg6;
	GameObject bg7;
	GameObject bg8;

	string imgDir;

	void Awake () {

		try{

			string imgPath = "/Image/FoodMenu/" ;

			imgDir = Application.persistentDataPath + imgPath;

			if(!Directory.Exists(imgDir)) Directory.CreateDirectory(imgDir);



			UIEventListener.Get(buttonFoodCamera).onClick = ButtonFoodCamera;
			UIEventListener.Get(buttonOK).onClick = ButtonOK;
			UIEventListener.Get(buttonCancel).onClick = ButtonCancel;

			UIEventListener.Get(buttonFind).onClick = ButtonFind;

			UIEventListener.Get(buttonDIY_Add).onClick = ButtonDIY_Add;


			UIEventListener.Get(buttonFoodDIY).onClick = ButtonFoodDIY;
			UIEventListener.Get(buttonFood1).onClick = ButtonFood1;
			UIEventListener.Get(buttonFood2).onClick = ButtonFood2;
			UIEventListener.Get(buttonFood3).onClick = ButtonFood3;
			UIEventListener.Get(buttonFood4).onClick = ButtonFood4;
			UIEventListener.Get(buttonFood5).onClick = ButtonFood5;
			UIEventListener.Get(buttonFood6).onClick = ButtonFood6;
			UIEventListener.Get(buttonFood7).onClick = ButtonFood7;
			UIEventListener.Get(buttonFood8).onClick = ButtonFood8;

			bgDIY = buttonFoodDIY.transform.GetChild(0).gameObject;
			bg1 = buttonFood1.transform.GetChild(0).gameObject;
			bg2 = buttonFood2.transform.GetChild(0).gameObject;
			bg3 = buttonFood3.transform.GetChild(0).gameObject;
			bg4 = buttonFood4.transform.GetChild(0).gameObject;
			bg5 = buttonFood5.transform.GetChild(0).gameObject;
			bg6 = buttonFood6.transform.GetChild(0).gameObject;
			bg7 = buttonFood7.transform.GetChild(0).gameObject;
			bg8 = buttonFood8.transform.GetChild(0).gameObject;

			bgDIY.SetActive (false);
			bg1.SetActive (false);
			bg2.SetActive (false);
			bg3.SetActive (false);
			bg4.SetActive (false);
			bg5.SetActive (false);
			bg6.SetActive (false);
			bg7.SetActive (false);
			bg8.SetActive (false);

			buttonDIY_Add.SetActive (true);
			fsv.gameObject.SetActive (false);

			//JsonFoodInit ();

			EtceteraAndroid.initTTS();
		}catch(Exception e){
			LwError.Show(e.ToString());
		}
	}

	public static string FoodName;

	bool buttonLuck = false;

	public GameObject root;

	List<string> find = new List<string>();

	public UIScrollView findSV;

	string findTemp = "";

	void Update () {
		#if !UNITY_EDITOR
		findText.text = tsk.text;
		#endif	

		if(findText.text == ""){
			findTemp = "";
			root.SetActive (true);
			findSV.gameObject.SetActive(false);
		}else{
			if(findTemp == findText.text){ // 蛋 包 麵
				return;
			}

			for(int i = 0;i<findSV.transform.childCount;i++){
				GameObject go = findSV.transform.GetChild(i).gameObject;
				Destroy(go);
			}

			findTemp = findText.text;

			findSV.gameObject.SetActive(true);





			find.Clear();

			root.SetActive (false);



			foreach(string s in foodName1){
				if(s.IndexOf(findText.text) != -1){
					find.Add(s);
				}
			}

			foreach(string s in foodName2){
				if(s.IndexOf(findText.text) != -1){
					find.Add(s);
				}
			}

			foreach(string s in foodName3){
				if(s.IndexOf(findText.text) != -1){
					find.Add(s);
				}
			}

			foreach(string s in foodName4){
				if(s.IndexOf(findText.text) != -1){
					find.Add(s);
				}
			}

			foreach(string s in foodName5){
				if(s.IndexOf(findText.text) != -1){
					find.Add(s);
				}
			}

			foreach(string s in foodName6){
				if(s.IndexOf(findText.text) != -1){
					find.Add(s);
				}
			}

			foreach(string s in foodName7){
				if(s.IndexOf(findText.text) != -1){
					find.Add(s);
				}
			}

			foreach(string s in foodName8){
				if(s.IndexOf(findText.text) != -1){
					find.Add(s);
				}
			}

			for(int i = 0; i < find.Count; i++){
				GameObject food = Instantiate(chooseFood) as GameObject;
				food.transform.parent = findSV.transform;
				food.transform.localScale = Vector3.one;
				food.transform.localPosition = new Vector3 (21, i * 70);
				food.GetComponent<LwChooseFood>().buttonName = find[i];
				food.GetComponent<UILabel>().text = find[i];
			}

			findSV.OnScrollBar ();
		}
	}



	public UILabel findText;
	string temp = "";
	TouchScreenKeyboard tsk;

	void ButtonFind(GameObject button){
		tsk = TouchScreenKeyboard.Open (temp);
	}

	void ButtonDIY_Add(GameObject button){
		LwMainMenu.IsChooseAddFood = true;
		LwMainMenu.IsAddFood = true;
		Application.LoadLevel ("FoodCamera");
	}


	void Button_Search(GameObject button){

		EtceteraAndroid.promptForPictureFromAlbum( "a" );

	}

	bool isDIY = false;

	void ButtonFoodDIY(GameObject button){
		isDIY = true;
		buttonDIY_Add.SetActive (true);
		fsv.gameObject.SetActive (false);
		DeleteDIYSV ();
		bgDIY.SetActive (true);
		bg1.SetActive (false);
		bg2.SetActive (false);
		bg3.SetActive (false);
		bg4.SetActive (false);
		bg5.SetActive (false);
		bg6.SetActive (false);
		bg7.SetActive (false);
		bg8.SetActive (false);

		string [] files = Directory.GetFiles(Application.persistentDataPath+"/Food","*.info");
		string [] filesJPG = Directory.GetFiles(Application.persistentDataPath+"/Food","*.jpg");
		string [] filesPNG = Directory.GetFiles(Application.persistentDataPath+"/Food","*.png");

		ArrayList foods = new ArrayList ();
		ArrayList foodsKal = new ArrayList ();

		for(int i=0; i < files.Length; i++){
			string saveFoodName = files[i].Split('_')[2];
			string saveFoodKal = files[i].Split('_')[1];
			foods.Add(saveFoodName);
			foodsKal.Add(saveFoodKal);
		}


//		string [] foodName = {"可樂","烤香腸","可麗餅","布丁","珍珠奶茶","豆花","蝦捲","太陽餅"};

		for(int i = 0, y = 150; i < foods.Count; i++ , y -= 70){
			GameObject food = Instantiate(chooseFood) as GameObject;
			food.transform.parent = diysv.transform;
			food.transform.localScale = Vector3.one;
			food.transform.localPosition = new Vector3 (21, y);
			food.GetComponent<LwChooseFood>().buttonName = foods[i].ToString();
			food.GetComponent<LwChooseFood>().buttonKal = foodsKal[i].ToString();
			food.GetComponent<LwChooseFood>().buttonPath = files[i];
			food.GetComponent<LwChooseFood>().buttonPathJPG = filesJPG[i];
			food.GetComponent<LwChooseFood>().buttonPathPNG = filesPNG[i];
			food.GetComponent<UILabel>().text = foods[i].ToString();
		}
		diysv.OnScrollBar ();

	}

	void DeleteDIYSV(){
		for(int i = 0;i<diysv.transform.childCount;i++){
			GameObject go = diysv.transform.GetChild(i).gameObject;
			Destroy(go);
		}
	}

	void DeleteFSV(){
		for(int i = 0;i<fsv.transform.childCount;i++){
			GameObject go = fsv.transform.GetChild(i).gameObject;
			Destroy(go);
		}
	}

	void AddFood(string foodName){

		List<string> foods = new List<string> ();

		int kal_now = FoodStatusCircle.kalNow;

		string JsonFoodDataPath = imgDir + "FoodMenu.txt";

		JArray ja = JsonConvert.DeserializeObject<JObject> (File.ReadAllText (JsonFoodDataPath)) ["Food"] as JArray;

		for(int i = 0; i < ja.Count; i++){

			if(foodName == (string)ja[i]["name"]){
			
				JArray Foods = (JArray)ja[i]["foods"];
				for(int j = 0; j < Foods.Count; j++){

					string thisKal = (string)Foods[j]["kal"];


					if(Convert.ToInt32(thisKal) <= kal_now){

						foods.Add((string)Foods[j]["name"]);

					}
				}
			}
		}


		for(int i = 0, y = 150; i < foods.Count; i++ , y -= 70){
			GameObject food = Instantiate(chooseFood) as GameObject;
			food.transform.parent = fsv.transform;
			food.transform.localScale = Vector3.one;
			food.transform.localPosition = new Vector3 (21, y);
			food.GetComponent<LwChooseFood>().buttonName = foods[i];
			food.GetComponent<UILabel>().text = foods[i];
		}
		fsv.OnScrollBar ();
	}

	string [] foodName1 = {"蔥油餅","油條","豆漿","蛋餅","飯糰","荷包蛋","包子","刈包","饅頭","燒餅","小籠包","燒賣"};
	string [] foodKal1 = {"500","400","350","450","550","300","350","450","300","350","600","600"};

	void ButtonFood1(GameObject button){
		try{

			isDIY = false;
			buttonDIY_Add.SetActive (false);
			fsv.gameObject.SetActive (true);
			DeleteFSV ();

			bgDIY.SetActive (false);
			bg1.SetActive (true);
			bg2.SetActive (false);
			bg3.SetActive (false);
			bg4.SetActive (false);
			bg5.SetActive (false);
			bg6.SetActive (false);
			bg7.SetActive (false);
			bg8.SetActive (false);


			AddFood (foodKind[0]);
		}catch(Exception e){

			LwError.Show(e.ToString());

		}
	}

	string [] foodName2 = {"鬆餅","牛奶","麥片","吐司"};
	string [] foodKal2 = {"500","400","350","450"};

	void ButtonFood2(GameObject button){
		isDIY = false;
		buttonDIY_Add.SetActive (false);
		fsv.gameObject.SetActive (true);
		DeleteFSV ();
		bgDIY.SetActive (false);
		bg1.SetActive (false);
		bg2.SetActive (true);
		bg3.SetActive (false);
		bg4.SetActive (false);
		bg5.SetActive (false);
		bg6.SetActive (false);
		bg7.SetActive (false);
		bg8.SetActive (false);


		AddFood (foodKind[1]);
	}

	string [] foodName3 = {"牛肉麵"};
	string [] foodKal3 = {"900"};

	void ButtonFood3(GameObject button){
		isDIY = false;
		buttonDIY_Add.SetActive (false);
		fsv.gameObject.SetActive (true);
		DeleteFSV ();
		bgDIY.SetActive (false);
		bg1.SetActive (false);
		bg2.SetActive (false);
		bg3.SetActive (true);
		bg4.SetActive (false);
		bg5.SetActive (false);
		bg6.SetActive (false);
		bg7.SetActive (false);
		bg8.SetActive (false);


		AddFood (foodKind[2]);
	}

	string [] foodName4 = {"麥當勞"};
	string [] foodKal4 = {"900"};

	void ButtonFood4(GameObject button){
		isDIY = false;
		buttonDIY_Add.SetActive (false);
		fsv.gameObject.SetActive (true);
		DeleteFSV ();
		bgDIY.SetActive (false);
		bg1.SetActive (false);
		bg2.SetActive (false);
		bg3.SetActive (false);
		bg4.SetActive (true);
		bg5.SetActive (false);
		bg6.SetActive (false);
		bg7.SetActive (false);
		bg8.SetActive (false);


		AddFood (foodKind[3]);
	}

	string [] foodName5 = {"港式飲茶"};
	string [] foodKal5 = {"700"};

	void ButtonFood5(GameObject button){
		isDIY = false;
		buttonDIY_Add.SetActive (false);
		fsv.gameObject.SetActive (true);
		DeleteFSV ();
		bgDIY.SetActive (false);
		bg1.SetActive (false);
		bg2.SetActive (false);
		bg3.SetActive (false);
		bg4.SetActive (false);
		bg5.SetActive (true);
		bg6.SetActive (false);
		bg7.SetActive (false);
		bg8.SetActive (false);


		AddFood (foodKind[4]);
	}

	string [] foodName6 = {"義大利麵"};
	string [] foodKal6 = {"800"};

	void ButtonFood6(GameObject button){
		isDIY = false;
		buttonDIY_Add.SetActive (false);
		fsv.gameObject.SetActive (true);
		DeleteFSV ();
		bgDIY.SetActive (false);
		bg1.SetActive (false);
		bg2.SetActive (false);
		bg3.SetActive (false);
		bg4.SetActive (false);
		bg5.SetActive (false);
		bg6.SetActive (true);
		bg7.SetActive (false);
		bg8.SetActive (false);


		AddFood (foodKind[5]);
	}

	string [] foodName7 = {"麥芽糖"};
	string [] foodKal7 = {"300"};

	void ButtonFood7(GameObject button){
		isDIY = false;
		buttonDIY_Add.SetActive (false);
		fsv.gameObject.SetActive (true);
		DeleteFSV ();
		bgDIY.SetActive (false);
		bg1.SetActive (false);
		bg2.SetActive (false);
		bg3.SetActive (false);
		bg4.SetActive (false);
		bg5.SetActive (false);
		bg6.SetActive (false);
		bg7.SetActive (true);
		bg8.SetActive (false);


		AddFood (foodKind[6]);
	}

	string [] foodName8 = {"巧克力","紅茶","咖啡","藍山","曼特寧","巴西","卡布奇諾","拿鐵"};
	string [] foodKal8 = {"600","300","350","300","400","450","350","500"};

	void ButtonFood8(GameObject button){
		isDIY = false;
		buttonDIY_Add.SetActive (false);
		fsv.gameObject.SetActive (true);
		DeleteFSV ();
		bgDIY.SetActive (false);
		bg1.SetActive (false);
		bg2.SetActive (false);
		bg3.SetActive (false);
		bg4.SetActive (false);
		bg5.SetActive (false);
		bg6.SetActive (false);
		bg7.SetActive (false);
		bg8.SetActive (true);


		AddFood (foodKind[7]);
	}

	void ButtonFoodCamera(GameObject button){
		Application.LoadLevel ("FoodCamera");
	}

	void ButtonOK(GameObject button){
		if(isDIY){
			string nowDate = DateTime.Now.ToString ("yyyy-MM-dd");
			string nowPath = Application.persistentDataPath + "/" + nowDate;
			
			if(!Directory.Exists(nowPath)){
				Directory.CreateDirectory(nowPath);
			}
			
			int i = 1;
			
			while(File.Exists(nowPath + "/" + i + ".png")){
				i++;
			}

			File.Copy(LwChooseFood.choosePathJPG,nowPath + "/" + i + ".jpg");
			File.Copy(LwChooseFood.choosePathPNG,nowPath + "/" + i + ".png");
			File.Create(nowPath+"/" + i + "._" + LwChooseFood.chooseKal + "_" +LwChooseFood.chooseName+ "_.info");

			print (LwChooseFood.choosePathJPG);
			print (LwChooseFood.choosePathPNG);



//			print (LwChooseFood.choosePath.s);
		}else{

		}

		if(isDIY){
			LwMainMenu.IsChooseFoodFinish_DIY = true;
		}else{
			LwMainMenu.IsChooseFoodFinish_Default = true;
		}
		Application.LoadLevel ("MainMenu");
	}

	void ButtonCancel(GameObject button){
		Application.LoadLevel ("MainMenu");
	}

	void FoodGet(){

		string JsonFoodDataPath = Application.persistentDataPath + "/FoodMenu.txt";
		
		if(!File.Exists(JsonFoodDataPath)){
			return;
		}
		
		//path = JsonFoodDataPath;
		
		int AllKal = 0;
		
		JArray ja = JsonConvert.DeserializeObject<JObject> (File.ReadAllText (JsonFoodDataPath)) ["Food"] as JArray;
		
		for(int i = 0; i < ja.Count; i++){
			DateTime dt = (DateTime) ja[i]["Name"];
			if(dt.Date == DateTime.Today){
				AllKal+=int.Parse(ja[i]["Kal"].ToString());

			}
		}


	}

	string [] foodKind = {"中式早餐","西式早餐","中式午餐","西式午餐","中式晚餐","西式晚餐","中式點心","西式點心"};
	List<string[]> food_list = new List<string[]>();
	List<string[]> kal_list = new List<string[]>();

//	void JsonFoodInit(){
//		string JsonFoodDataPath = imgDir + "/FoodMenu.txt";
//		List<object> Food = null;
//
//		if (!File.Exists (JsonFoodDataPath)) {
//			Food = new List<object> ();
//			File.WriteAllText(JsonFoodDataPath, JsonConvert.SerializeObject(new{Food},Formatting.Indented));
//
//			food_list.Add(foodName1);
//			food_list.Add(foodName2);
//			food_list.Add(foodName3);
//			food_list.Add(foodName4);
//			food_list.Add(foodName5);
//			food_list.Add(foodName6);
//			food_list.Add(foodName7);
//			food_list.Add(foodName8);
//
//			kal_list.Add(foodKal1);
//			kal_list.Add(foodKal2);
//			kal_list.Add(foodKal3);
//			kal_list.Add(foodKal4);
//			kal_list.Add(foodKal5);
//			kal_list.Add(foodKal6);
//			kal_list.Add(foodKal7);
//			kal_list.Add(foodKal8);
//
//			Food = (JsonConvert.DeserializeObject<JObject> (File.ReadAllText(JsonFoodDataPath))["Food"] as JArray).ToObject<List<object>>();
//
//			for(int i = 0 ; i < 8 ; i++){
//				var food = new {
//					Name = foodKind[i],
//					Food2 = food_list[i],
//					Kal = kal_list[i],
//					JPGPath = "/" ,
//					PNGPath = "/" ,
//				};
//
//				Food.Add(food);
//			}
//
//			File.WriteAllText(JsonFoodDataPath, JsonConvert.SerializeObject(new{Food},Formatting.Indented));
//
//		}



	}

