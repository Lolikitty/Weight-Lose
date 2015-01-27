using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class LwMainMenu : MonoBehaviour {

	public GameObject AddFoodFinish;
	public GameObject buttonAddFoodFinishExit;

	public UILabel AddFoodFinish_Title;
	public UILabel AddFoodFinish_Kal;
	public UILabel WeightLoss;
	public UISlider WeightLossSlider;

	public UITexture AddFoodFinish_Food;
	public Texture2D defaultFood;

	public static bool UploadBirthdayAndWeight = false;
	public static bool IsAddFood = false;
	public static bool IsChooseAddFood = false;
	public static bool IsChooseFoodFinish_DIY = false;
	public static bool IsChooseFoodFinish_Default = false;

	// Unity Override Methods ==============================================================================================================================

	void Awake () {

		UIEventListener.Get(buttonAddFoodFinishExit).onClick = ButtonAddFoodFinishExit;


		float WeightFirst = float.Parse (PlayerPrefs.GetString ("WeightFirst"));
		float WeightTarget = float.Parse (PlayerPrefs.GetString ("WeightTarget"));
		float nowKG = 50;

		WeightLoss.text = (WeightFirst-WeightTarget).ToString ("0.0");

		WeightLossSlider.value = 1 - ((nowKG - WeightTarget) / (WeightFirst - WeightTarget));
	}

	IEnumerator Start () {
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
			IsAddFood = false;
			AddFoodFinish_Food.mainTexture = LwFoodCamera.texture;
			AddFoodFinish_Title.text = LwFoodCamera2.FoodName;
			AddFoodFinish_Kal.text = LwFoodCamera2.FoodKal;
			AddFoodFinish.SetActive(true);
		}else if(IsChooseFoodFinish_DIY){
			IsChooseFoodFinish_DIY = false;

			WWW www2 = new WWW("file://"+LwChooseFood.choosePathJPG);
			yield return www2;

			AddFoodFinish_Food.mainTexture = www2.texture;
			AddFoodFinish_Title.text = LwChooseFood.chooseName;
			AddFoodFinish_Kal.text = LwChooseFood.chooseKal;
			AddFoodFinish.SetActive(true);
		}else if(IsChooseFoodFinish_Default){
			IsChooseFoodFinish_Default = false;
			AddFoodFinish_Food.mainTexture = defaultFood;
			AddFoodFinish_Title.text = LwChooseFood.chooseName;
			AddFoodFinish_Kal.text = LwChooseFood.chooseKal;
			AddFoodFinish.SetActive(true);
		}
	}

	// Custom Methods ======================================================================================================================================

	void ButtonAddFoodFinishExit(GameObject button){
		AddFoodFinish.SetActive(false);
		if(IsChooseAddFood){
			IsChooseAddFood = false;
			Application.LoadLevel("FoodChoose");
		}

	}

}
