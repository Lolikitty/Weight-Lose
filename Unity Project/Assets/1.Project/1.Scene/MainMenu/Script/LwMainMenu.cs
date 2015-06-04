using UnityEngine;
using System.Collections;
using System.IO;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LwMainMenu : MonoBehaviour {

	public GameObject AddFoodFinish;
	public GameObject buttonAddFoodFinishExit;

	public UILabel AddFoodFinish_Title;
	public UILabel AddFoodFinish_Kal;
	public UILabel WeightLoss;
	public UISlider WeightLossSlider;

	public UITexture AddFoodFinish_Food;
	public static Texture2D defaultFood;

	public static bool UploadBirthdayAndWeight = false;
	public static bool IsAddFood = false;
	public static bool IsChooseAddFood = false;
	public static bool IsChooseFoodFinish_DIY = false;
	public static bool IsChooseFoodFinish_Default = false;

	// Unity Override Methods ==============================================================================================================================

	void Awake () {

		UIEventListener.Get(buttonAddFoodFinishExit).onClick = ButtonAddFoodFinishExit;

		string JsonUserDataPath = Application.persistentDataPath + "/User.txt";
		if(File.Exists(JsonUserDataPath)){
			JObject obj = JsonConvert.DeserializeObject<JObject> (File.ReadAllText(JsonUserDataPath));
			float WeightInit = float.Parse(obj["WeightInit"].ToString());
			float WeightTarget = float.Parse(obj["WeightTarget"].ToString());
			float nowKG = PlayerPrefs.GetFloat("Weight9");

//			WeightLoss.text = (nowKG-WeightTarget).ToString ("0.0");
			WeightLoss.text = (WeightInit-WeightTarget).ToString ("0.0");
			WeightLossSlider.value = 1 - ((nowKG - WeightTarget) / (WeightInit - WeightTarget));
		}


//		float nowKG = PlayerPrefs.GetFloat("Weight9");
//		float WeightFirst = float.Parse (PlayerPrefs.GetString ("WeightFirst"));
//		float WeightTarget = float.Parse (PlayerPrefs.GetString ("WeightTarget"));
//
//
//		WeightLoss.text = (nowKG-WeightTarget).ToString ("0.0");
//
//		WeightLossSlider.value = 1 - ((nowKG - WeightTarget) / (WeightFirst - WeightTarget));
	}

	string msg = "No Data";

	IEnumerator Start () {

		WWW w = new WWW(LwInit.HttpServerPath);
		yield return w;
		
		msg = w.text;

		if(UploadBirthdayAndWeight){
			UploadBirthdayAndWeight = false;
			WWWForm wwwF = new WWWForm();
			wwwF.AddField("id", PlayerPrefs.GetString ("ID"));
			wwwF.AddField("sex", PlayerPrefs.GetString ("Sex"));
			wwwF.AddField("year", PlayerPrefs.GetString ("BirthdayYear"));
			wwwF.AddField("month", PlayerPrefs.GetString ("BirthdayMonth"));
			wwwF.AddField("day", PlayerPrefs.GetString ("BirthdayDay"));

			wwwF.AddField("weight_first", PlayerPrefs.GetString ("WeightFirst"));
			wwwF.AddField("weight_target", PlayerPrefs.GetString ("WeightTarget"));
			wwwF.AddField("weight_target_month", PlayerPrefs.GetString ("WeightTargetMonth"));

			WWW www = new WWW(LwInit.HttpServerPath+"/SetBirthdayAndWeight", wwwF);
			yield return www;
		}


		
		if(IsAddFood){
			AddFoodFinish_Food.mainTexture = LwFoodCamera.texture;
			AddFoodFinish_Title.text = LwFoodCamera2.FoodName;
			print ("--------------------- " + LwFoodCamera2.FoodName);
			AddFoodFinish_Kal.text = LwFoodCamera2.FoodKal;
			AddFoodFinish.SetActive(true);
		}else if(IsChooseFoodFinish_DIY){

			WWW www2 = new WWW("file://" + Application.persistentDataPath + ChooseFood2.JPG_PATH);
			yield return www2;

			AddFoodFinish_Food.mainTexture = www2.texture;
			AddFoodFinish_Title.text = LwFoodCamera2.FoodName;
			AddFoodFinish_Kal.text = LwFoodCamera2.FoodKal;
			AddFoodFinish.SetActive(true);
		}else if(IsChooseFoodFinish_Default){
			print ("IsChooseFoodFinish_Default");
			AddFoodFinish_Food.mainTexture = defaultFood;
			AddFoodFinish_Title.text = LwFoodCamera2.FoodName;
			AddFoodFinish_Kal.text = LwFoodCamera2.FoodKal;
			AddFoodFinish.SetActive(true);
		}
	}

	// Custom Methods ======================================================================================================================================

	void ButtonAddFoodFinishExit(GameObject button){
//		AddFoodFinish.SetActive(false);
//		if(IsChooseAddFood){
			IsChooseAddFood = false;
			IsChooseFoodFinish_DIY = false;
			IsChooseFoodFinish_Default = false;
			IsAddFood = false;
			Application.LoadLevel("FoodChoose");
//		}

	}


	void OnGUI(){
		GUILayout.Space (20);
		GUILayout.Label (msg);
	}


}
