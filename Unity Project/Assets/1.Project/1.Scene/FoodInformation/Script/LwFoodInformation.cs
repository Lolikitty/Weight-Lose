using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LwFoodInformation : MonoBehaviour {

	public static DateTime pathDate;
	public static string pathJPG = null;
	public static string pathPNG = null;

	public GameObject buttonExit;
	public GameObject buttonDelete;
	public GameObject buttonDelete_Ok;
	public GameObject buttonDelete_Cancel;

	public UILabel FoodName;
	public UILabel FoodKalNumber;

	public UILabel DeleteScreen_FoodName;
	public UITexture DeleteScreen_Food;

	public GameObject DeleteScreen;


	void Awake () {
		DeleteScreen.SetActive (false);

		UIEventListener.Get(buttonExit).onClick = ButtonExit;
		UIEventListener.Get(buttonDelete).onClick = ButtonDelete;
		UIEventListener.Get(buttonDelete_Ok).onClick = ButtonDelete_Ok;
		UIEventListener.Get(buttonDelete_Cancel).onClick = ButtonDelete_Cancel;
	}

	void ButtonExit(GameObject button){		
		Application.LoadLevel ("FoodHistory");
	}

	void ButtonDelete(GameObject button){
		DeleteScreen_FoodName.text = FoodName.text;
		DeleteScreen.SetActive (true);
	}

	void ButtonDelete_Ok(GameObject button){
		try{
			File.Delete (pathJPG);
			File.Delete (pathPNG);
		}catch{
		}
		try{
			string JsonFoodDataPath = Application.persistentDataPath + "/Food.txt";



			JArray Food = JsonConvert.DeserializeObject<JObject> (File.ReadAllText (JsonFoodDataPath))["Food"] as JArray;

			for(int i = 0; i < Food.Count; i++){
				DateTime date = (DateTime) Food[i]["Date"];
				if(date == pathDate){
					Food.RemoveAt(i);
					File.WriteAllText(JsonFoodDataPath, JsonConvert.SerializeObject(new{Food},Formatting.Indented));
					break;
				}
			}

		}catch (Exception e){
		}
		Application.LoadLevel ("FoodHistory");
	}

	void ButtonDelete_Cancel(GameObject button){		
		DeleteScreen.SetActive (false);
	}

	public UITexture t;

	IEnumerator Start () {
		if(pathJPG != null){
			WWW www = new WWW ("file://" + pathJPG);
			yield return www;
			t.mainTexture = www.texture;
			t.transform.localScale = new Vector3((float)www.texture.width/www.texture.height, 1);
		}
		if(pathPNG != null){
			WWW www = new WWW ("file://" + pathPNG);
			yield return www;
			DeleteScreen_Food.mainTexture = www.texture;
		}
	}

}
