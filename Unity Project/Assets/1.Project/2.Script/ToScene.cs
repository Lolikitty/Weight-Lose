using UnityEngine;
using System.Collections;

public class ToScene : MonoBehaviour {

	public enum CommunityScene{
		Null,
		AddFriend,
		Message,
		Friend,
		Talk,
		GroupMessage
	}

	public enum PasswordScene{
		Null,
		InputPassword,
		SetPassword,
		SetPassword2
	}

	public enum OtherScene{
		Null,
		Achievements,
		Backup,
		Error,
		FoodCamera,
		FoodCamera2,
		FoodChoose,
		FoodHistory,
		FoodInformation,
		Init,
		MainMenu,
		ReadBackup,
		SetBirthday,
		Setting,
		SetWeight,
		Sports,
		Sports2,
		Sports3,
		User,
		User2,
		User3,
		UserCamera,
		Water,
		Water2,
		WeightMeasurements,
		WeightMeasurements2,
		Teacher01,
		Teacher02,
		Teacher03,
		Teacher04,
		Water3,
		Tip
	};

	public CommunityScene communityScene;
	public PasswordScene passwordScene;
	public OtherScene otherScene;
	
	public bool useClick = true;
	public bool useBack;

	public Transform animationBG;

	bool isClick = false;

	// Unity Override Methods ==============================================================================================================================

	void Awake(){
		animationBG.GetComponent<UITexture> ().alpha = 0.7f;
		animationBG.GetComponent<UITexture> ().depth = 1000;
	}

	void OnClick () {

//		Debug.Log ("test");
		if(useClick){
			StatusControl();
		}
	}
	
	void Update () {
		if(useBack){
			if(Input.GetKeyDown(KeyCode.Escape)){
				StatusControl();
			}
		}

		if(isClick){
			if(animationBG != null){
				if(animationBG.localPosition.y < 0){
					animationBG.Translate (0,10 * Time.deltaTime, 0);
				}else{
					GoToScene();
				}
			}
		}
	}

	// Custom Methods ======================================================================================================================================

	void StatusControl(){
		if(animationBG == null){
			GoToScene();
		}else{
			isClick = true;
		}
	}
	
	void GoToScene(){
		string community = communityScene.ToString();
		string password = passwordScene.ToString();
		string other = otherScene.ToString();
		string nul = "Null";

		string nowSence = Application.loadedLevelName; 

	
//		Debug.Log ("PlayerPrefs.GetInt ('FirstSports') = " + PlayerPrefs.GetInt ("FirstSports"));

//		if (other == "Teacher03") {
//
//		}


		if(other == "Water"){
			int fw = PlayerPrefs.GetInt ("FirstWater");
			if(fw == 1)
				other = "Water2";
			else{

			}

			if(name == "ButtonBackToWater"){ //判斷是否為特殊進入
				other = "Water";
			}
		}
		if(other == "Sports"){
			int fs = PlayerPrefs.GetInt ("FirstSports");
			if(fs == 1)
				other = "Sports2";
			else{

			}

			if(name == "SportsItemAdd"){ //判斷是否為特殊進入
				other = "Sports";
			}
		}
		

		if(community != nul){
			Application.LoadLevel(community);
		}else if(password != nul){
			Application.LoadLevel(password);
		}else if(other != nul){
			Application.LoadLevel(other);
		}

	}

	public void SetClick(){
		isClick = true;
	}
	
}
