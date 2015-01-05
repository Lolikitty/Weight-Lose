using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class LwFoodCamera2 : MonoBehaviour {

	public GameObject ImageOgject;
	public GameObject buttonDone;
	public GameObject buttonCancel;

	public ChooseNumber number1;
	public ChooseNumber number2;
	public ChooseNumber number3;
	public ChooseNumber number4;

	void Awake () {
		ImageOgject.renderer.material.mainTexture = LwFoodCamera.texture;
		UIEventListener.Get(buttonDone).onClick = ButtonDone;
		UIEventListener.Get(buttonCancel).onClick = ButtonCancel;
	}

	public TextMesh name;
	string temp = "";
	TouchScreenKeyboard tsk;
	public static string FoodName;
	public static string FoodKal;

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

	void ButtonDone(GameObject button){

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


		LwMainMenu.IsAddFood = true;

		Texture2D texture = LwFoodCamera.texture;

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
		
		string nowDate = DateTime.Now.ToString ("yyyy-MM-dd");
		string nowPath = Application.persistentDataPath + "/" + nowDate;
		
		if(!Directory.Exists(nowPath)){
			Directory.CreateDirectory(nowPath);
		}
		
		int i = 1;
		
		while(File.Exists(nowPath + "/" + i + ".png")){
			i++;
		}
		
		TextureScale.Bilinear (newTexture2, 128, 128);

		string kal = "" + number1.chooseNumber + number2.chooseNumber + number3.chooseNumber + number4.chooseNumber;
		kal = int.Parse (kal).ToString ();

		print (kal);




		FoodName = name.text;
		FoodKal = kal;
		
		#if UNITY_EDITOR
		System.IO.File.WriteAllBytes("image1.png", texture.EncodeToPNG());
		System.IO.File.WriteAllBytes("image2.png", newTexture.EncodeToPNG());
		System.IO.File.WriteAllBytes("image3.png", newTexture2.EncodeToPNG());
		System.IO.File.WriteAllBytes("image4.png", MarkTexture.EncodeToPNG());
		#endif

		if(LwMainMenu.IsChooseAddFood){
			i = 1;

			while(File.Exists(Application.persistentDataPath+"/Food" + "/" + i + ".png")){
				i++;
			}

			System.IO.File.WriteAllBytes(Application.persistentDataPath+"/Food" + "/" + i + ".png", newTexture2.EncodeToPNG());
			System.IO.File.WriteAllBytes(Application.persistentDataPath+"/Food" + "/" + i + ".jpg", texture.EncodeToJPG());
			System.IO.File.Create (Application.persistentDataPath+"/Food" + "/" + i + "._" + kal + "_" + name.text + "_.info");

		}else{
			System.IO.File.WriteAllBytes(nowPath + "/" + i + ".png", newTexture2.EncodeToPNG());
			System.IO.File.WriteAllBytes(nowPath + "/" + i + ".jpg", texture.EncodeToJPG());
			
			System.IO.File.Create (nowPath + "/" + i + "._" + kal + "_" + name.text + "_.info");
		}






		Application.LoadLevel ("MainMenu");
	}

	void ButtonCancel(GameObject button){
		Application.LoadLevel ("FoodCamera");
	}

}


