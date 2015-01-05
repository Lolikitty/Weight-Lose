using UnityEngine;
using System.Collections;
using Facebook.MiniJSON;

public class FBInit : MonoBehaviour {

	public static string FB_JSON_DATA = "No Data";
	public static string FB_USER_NAME = "No Name";
	public static string FB_USER_ID = "No Data";
	public static Texture2D FB_USER_IMAGE;
	public static bool isInit = false;
	public GameObject buttonBackup, buttonReadBackup;

	// Unity Override Methods ==============================================================================================================================

	void Awake(){
		UIEventListener.Get (buttonBackup).onClick = FBLogInButton;
		UIEventListener.Get (buttonReadBackup).onClick = FBLogInButton;

		// 可參考： https://developers.facebook.com/docs/unity/reference/current
		FB.Init (InitComplete); // 初始化
	}

	// Custom Methods ======================================================================================================================================

	// 初始化完成這個函示會被執行
	void InitComplete(){
		isInit = true;
	}
	
	void Login(FBResult result){ // 登入成功後會執行這個函式('方法)
		FB_JSON_DATA = result.Text;
		FB_USER_ID = FB.UserId;
		FB.API("me?fields=name", Facebook.HttpMethod.GET, UserCallBack);
	}
	
	void UserCallBack(FBResult result) {
		IDictionary dict = Json.Deserialize(result.Text) as IDictionary;
		FB_USER_NAME =dict ["name"].ToString(); // 取得用戶名稱
		StartCoroutine(GetImage());
	}
	
	IEnumerator GetImage () { // 取得用戶頭像
		WWW www = new WWW("https://graph.facebook.com/" + FB.UserId + "/picture?type=large");
		yield return www;
		FB_USER_IMAGE = www.texture;
		Finish ();
	}

	string status;

	void Finish(){
		if(status == "Backup"){
			Application.LoadLevel ("Backup");
		}else if(status == "ReadBackup"){
			Application.LoadLevel ("ReadBackup");
		}
	}


	void FBLogInButton(GameObject obj){
		if(isInit){
			status = obj.name;
			FB.Login ("public_profile,user_birthday,email,user_friends",Login); // 登入，並設定要取得的資料
		}
	}


}
