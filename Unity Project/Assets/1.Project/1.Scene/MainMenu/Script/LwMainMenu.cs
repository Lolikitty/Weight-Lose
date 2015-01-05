using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class LwMainMenu : MonoBehaviour {

	public GameObject buttonSetting;
	public GameObject buttonUser;
	public GameObject buttonFriend;
	public GameObject buttonAddFood;
	public GameObject buttonTask;
	public GameObject buttonAchievements;
	public GameObject AddFoodFinish;
	public GameObject buttonAddFoodFinishExit;
	public GameObject root;

	public UILabel AddFoodFinish_Title;
	public UILabel AddFoodFinish_Kal;
	public UILabel WeightLoss;
	public UILabel kal_num = null;

	public UITexture AddFoodFinish_Food;
	public UITexture [] love;

	public Texture2D blackLove;
	public Texture2D defaultFood;

	public Transform pin;

	public static bool UploadBirthdayAndWeight = false;
	public static bool IsAddFood = false;
	public static bool IsChooseAddFood = false;
	public static bool IsChooseFoodFinish_DIY = false;
	public static bool IsChooseFoodFinish_Default = false;

	// Unity Override Methods ==============================================================================================================================

	void Awake () {
		UIEventListener.Get(buttonUser).onClick = ButtonUser;
		UIEventListener.Get(buttonFriend).onClick = ButtonFriend;
		UIEventListener.Get(buttonAddFood).onClick = ButtonAddFood;
		UIEventListener.Get(buttonTask).onClick = ButtonTask;
		UIEventListener.Get(buttonAddFoodFinishExit).onClick = ButtonAddFoodFinishExit;
		UIEventListener.Get(buttonAchievements).onClick = ButtonAchievements;
		UIEventListener.Get(buttonSetting).onClick = ButtonSetting;

		WeightLoss.text = (float.Parse (PlayerPrefs.GetString ("WeightFirst")) - float.Parse (PlayerPrefs.GetString ("WeightTarget"))).ToString ("0.0");

		string nowDate = DateTime.Now.ToString ("yyyy-MM-dd");

		string path;

		path = Application.persistentDataPath + "/" + nowDate;

		if(!Directory.Exists(path)){
			return;
		}

		string [] filesInfo = Directory.GetFiles (path, "*.info");

		int AllKal = 0;

		for(int i = 0; i<filesInfo.Length; i++){
			int kal = int.Parse(filesInfo[i].Split('_')[1]);
			AllKal += kal;
		}

		float az = 0;

		for(int i = 0; i<filesInfo.Length; i++){
			int kal = int.Parse(filesInfo[i].Split('_')[1]);
			Transform t = Instantiate(pin) as Transform;
			t.transform.parent = root.transform;
			t.localScale = Vector3.one;
			t.localPosition = Vector3.zero;
			float z = 360-(((float) kal / (float)AllKal) * 360);
			az +=z;
			t.localEulerAngles = new Vector3(0,0,az);
		}

		float kalInit = 2000;

		kal_num.text = "" + (kalInit-AllKal);

		float loveN = 200;

		for(int i=0; i<love.Length; i++){
			if((kalInit-AllKal) < loveN * -i){
				love[i].mainTexture = blackLove;
			}
		}
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
		
	void ButtonSetting(GameObject obj){
		Application.LoadLevel ("Setting");
	}

	void ButtonUser(GameObject button){
		Application.LoadLevel ("User");
	}

	void ButtonFriend(GameObject button){
		Application.LoadLevel ("Friend");
	}

	void ButtonAddFood(GameObject button){
		Application.LoadLevel ("FoodCamera");
	}

	void ButtonTask(GameObject button){
		Application.LoadLevel ("Waiter");
	}

	void ButtonAchievements(GameObject button){
		Application.LoadLevel ("Achievements");
	}

	void ButtonAddFoodFinishExit(GameObject button){
		AddFoodFinish.SetActive(false);
		if(IsChooseAddFood){
			IsChooseAddFood = false;
			Application.LoadLevel("FoodChoose");
		}

	}

}
