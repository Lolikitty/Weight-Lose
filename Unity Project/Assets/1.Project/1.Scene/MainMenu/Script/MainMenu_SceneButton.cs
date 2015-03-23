using UnityEngine;
using System.Collections;

public class MainMenu_SceneButton : MonoBehaviour {

	public GameObject buttonSetting;
	public GameObject buttonUser;
	public GameObject buttonFriend;
	public GameObject buttonAddFood;
	public GameObject buttonTask;
	public GameObject buttonAchievements;

	// Unity Override Methods ==============================================================================================================================

	void Awake () {
		UIEventListener.Get(buttonUser).onClick = ButtonUser;
		UIEventListener.Get(buttonFriend).onClick = ButtonFriend;
		UIEventListener.Get(buttonAddFood).onClick = ButtonAddFood;
		UIEventListener.Get(buttonTask).onClick = ButtonTask;
		UIEventListener.Get(buttonAchievements).onClick = ButtonAchievements;
		UIEventListener.Get(buttonSetting).onClick = ButtonSetting;
	}

	void Update () {	
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
		int fw = PlayerPrefs.GetInt ("FirstWater");
		if(fw == 1){
			Application.LoadLevel ("Water2");
		}else{
			Application.LoadLevel ("Water");
		}
	}
	
	void ButtonAchievements(GameObject button){
		Application.LoadLevel ("Achievements");
	}
}
