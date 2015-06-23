using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LwFoodCamera2 : MonoBehaviour {

	public UITexture imageOgject;

	public GameObject buttonDone;
	public GameObject buttonCancel;

	public ChooseNumber number1;
	public ChooseNumber number2;
	public ChooseNumber number3;
	public ChooseNumber number4;

	void Awake () {

		imageOgject.mainTexture = LwFoodCamera.texture;

		UIEventListener.Get(buttonDone).onClick = ButtonDone;
		UIEventListener.Get(buttonCancel).onClick = ButtonCancel;

		if(LwMainMenu.IsChooseFoodFinish_DIY || LwMainMenu.IsChooseFoodFinish_Default){
			name.text = FoodName;
			imageOgject.mainTexture = FOOD_IMAGE;
		}

		float w = (float)imageOgject.mainTexture.width / imageOgject.mainTexture.height;

		imageOgject.width = (int)(imageOgject.width * w);
	}

	public UILabel name;
	string temp = "";
	TouchScreenKeyboard tsk;
	public static string FoodName;
	public static string FoodKal;

	public static Texture2D FOOD_IMAGE;

//	public static bool IS_FAST_CHOOSE_FOOD = false;

	void Update(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if(Physics.Raycast(ray,out hit)){ // 如果指到 Collider
			if(Input.GetMouseButtonDown(0)){ // 如果按下滑鼠左鍵
				if(hit.collider.name == "TouchName"){ // 如果該物件
					tsk = TouchScreenKeyboard.Open (temp);
				}
			}
		}

		#if !UNITY_EDITOR
		name.text = tsk.text;
		FoodName = tsk.text;
		#endif

	}

	public Texture2D MarkTexture;
	public GameObject warningObj;

	void ButtonDone(GameObject button){

		if (name.text == "請輸入食物名稱") {
			warningObj.SetActive (true);
			return;
		}

		if(name.text.IndexOf("_") != -1)return;
		if(name.text.IndexOf("/") != -1)return;
		if(name.text.IndexOf("\\") != -1)return;
		if(name.text.IndexOf(":") != -1)return;
		if(name.text.IndexOf("*") != -1)return;
		if(name.text.IndexOf("?") != -1)return;
		if(name.text.IndexOf("<") != -1)return;
		if(name.text.IndexOf(">") != -1)return;
		if(name.text.IndexOf("|") != -1)return;
		if(name.text.IndexOf("\"") != -1)return;
		if(name.text.IndexOf("'") != -1)return;


		string kal = "" + number1.chooseNumber + number2.chooseNumber + number3.chooseNumber + number4.chooseNumber;
		kal = int.Parse (kal).ToString ();
		FoodKal = kal;

		if(LwMainMenu.IsAddFood){
			SaveImage(LwFoodCamera.texture, "/Image/MyFoodMenu/");
			JsonMyFoodMenuSave(name.text, FoodKal, jpgPath, pngPath);
		}else if(LwMainMenu.IsChooseFoodFinish_DIY || LwMainMenu.IsChooseFoodFinish_Default){
//			LwMainMenu.IsAddFood = true;
			SaveImage(LwMainMenu.defaultFood, "/Image/Food/");
			JsonFoodSave (DateTime.Now, name.text, FoodKal, jpgPath, pngPath);
		}else{
			SaveImage(LwFoodCamera.texture, "/Image/Food/");
			JsonFoodSave (DateTime.Now, name.text, FoodKal, jpgPath, pngPath);
//			StartCoroutine (ButtonDone2()); // Send To Web
		}
		Application.LoadLevel("MainMenu");
	}

	byte[] bJPG;
	byte[] bPNG;
	byte[] bFood;
	string imgName;
	string jpgPath;
	string pngPath;

	void SaveImage(Texture2D texture, string imgPath){
		
		int wh = Mathf.Min (texture.width, texture.height);
		
		TextureScale.Bilinear (MarkTexture, wh, wh);
		
		Texture2D newTexture = new Texture2D (wh, wh);
		
		int start = (texture.width - wh) / 2;
		int end = texture.width - ((texture.width - wh) / 2);
		
		for(int x = start ; x < end ; x++){
			for(int y = start ; y < end ; y++){
				if(texture.width > texture.height){
					newTexture.SetPixel(x-start, y, texture.GetPixel(x, y));
				}else{
					newTexture.SetPixel(x, y-start, texture.GetPixel(x, y));
				}
			}
		}
		newTexture.Apply ();
		
		Texture2D newTexture2 = new Texture2D (wh, wh);
		
		for(int x = 0; x < wh; x++){
			for(int y =0; y < wh; y++){
				float alpha = MarkTexture.GetPixel(x, y).a;
				if(alpha != 0){
					Color c = newTexture.GetPixel(x,y);
					newTexture2.SetPixel(x, y, new Color(c.r, c.g, c.b, alpha));
				}else{
					newTexture2.SetPixel(x, y, new Color(1, 1, 1, 0));
				}
			}
		}
		
		newTexture2.Apply ();
		
		TextureScale.Bilinear (newTexture2, 128, 128);
		
		//		string kal = "" + number1.chooseNumber + number2.chooseNumber + number3.chooseNumber + number4.chooseNumber;
		//		kal = int.Parse (kal).ToString ();
		//		
		//		print (kal);	
		
		FoodName = name.text;
		//		FoodKal = kal;

		imgName = DateTime.Now.ToFileTimeUtc() + "";
		
		string imgDir = Application.persistentDataPath + imgPath;
		
		if(!Directory.Exists(imgDir)) Directory.CreateDirectory(imgDir);
		
		jpgPath = imgPath + imgName + ".jpg";
		pngPath = imgPath + imgName + ".png";
		
		bJPG = texture.EncodeToJPG ();
		bPNG = newTexture2.EncodeToPNG();
		
		System.IO.File.WriteAllBytes(Application.persistentDataPath + jpgPath, bJPG);
		System.IO.File.WriteAllBytes(Application.persistentDataPath + pngPath, bPNG);
		

		

	}

	IEnumerator ButtonDone2(){

		FileStream fs = new FileStream (Application.persistentDataPath + "/Food.txt", FileMode.Open);
		bFood = new byte[fs.Length];
		fs.Read (bFood, 0, bFood.Length);

		WWWForm f = new WWWForm ();
		f.AddField ("id", JsonConvert.DeserializeObject<JObject> (File.ReadAllText(Application.persistentDataPath + "/User.txt"))["ID"].ToString());
		f.AddBinaryData ("jpg", bJPG, imgName + ".jpg");
		f.AddBinaryData ("png", bPNG, imgName + ".png");
		f.AddBinaryData ("txt", bFood, "Food.txt");
		WWW w = new WWW (LwInit.HttpServerPath + "/UploadFood", f);
		yield return w;

		Application.LoadLevel("MainMenu");
	}

	void ButtonCancel(GameObject button){
		Application.LoadLevel ("FoodCamera");
	}

	void JsonFoodSave(DateTime date, string name, string kal, string jpgPath, string pngPath){
		string JsonFoodDataPath = Application.persistentDataPath + "/Food.txt";
				
		var food = new {
			Date = date,
			Name = name,
			Kal = kal,
			JPGPath = jpgPath,
			PNGPath = pngPath
		};

		List<object> Food = null;
		
		if(File.Exists(JsonFoodDataPath)){
			Food = (JsonConvert.DeserializeObject<JObject> (File.ReadAllText(JsonFoodDataPath))["Food"] as JArray).ToObject<List<object>>();
			Food.Add(food);
		}else{
			Food = new List<object> (){food};
		}
		File.WriteAllText(JsonFoodDataPath, JsonConvert.SerializeObject(new{Food},Formatting.Indented));
	}

	void JsonMyFoodMenuSave(string name, string kal, string jpgPath, string pngPath){
		string JsonFoodDataPath = Application.persistentDataPath + "/MyFoodMenu.txt";
		
		var food = new {
			Name = name,
			Kal = kal,
			JPGPath = jpgPath,
			PNGPath = pngPath
		};
		
		List<object> Food = null;
		
		if(File.Exists(JsonFoodDataPath)){
			Food = (JsonConvert.DeserializeObject<JObject> (File.ReadAllText(JsonFoodDataPath))["Food"] as JArray).ToObject<List<object>>();
			Food.Add(food);
		}else{
			Food = new List<object> (){food};
		}
		File.WriteAllText(JsonFoodDataPath, JsonConvert.SerializeObject(new{Food},Formatting.Indented));
	}



	void OnGUI () {
//		GUILayout.Label ("IsChooseFoodFinish_DIY : " + LwMainMenu.IsChooseFoodFinish_DIY);
//		GUILayout.Label ("IsChooseFoodFinish_Default : " + LwMainMenu.IsChooseFoodFinish_Default);
//		GUILayout.Label ("Width : "+imageOgject.width);
//		GUILayout.Label ("Height : "+imageOgject.height);
//		GUILayout.Label ("Scale : "+imageOgject.transform.localScale);

//		GUI.DrawTexture (new Rect (0, 0, imageOgject.width, imageOgject.height), imageOgject.mainTexture);
	}


}


