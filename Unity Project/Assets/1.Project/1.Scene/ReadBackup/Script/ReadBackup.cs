using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;

public class ReadBackup : MonoBehaviour {


	public GameObject buttonExit;
	public UITexture imgObj;
	public UILabel userName;	
	public UILabel status;

	string [] data = null;

	// Unity Override Methods ==============================================================================================================================
#if !UNITY_EDITOR
	void Awake () {
		UIEventListener.Get (buttonExit).onClick = ButtonExit;
		imgObj.mainTexture = FBInit.FB_USER_IMAGE;
		userName.text = FBInit.FB_USER_NAME;
	}

	IEnumerator Start(){

		status.text = "讀取備份中...";

		WWWForm wf = new WWWForm ();

		#if UNITY_EDITOR
		wf.AddField ("id", "1039311976095070");
		#else 
		wf.AddField ("id", FBInit.FB_USER_ID);
		#endif

		WWW w = new WWW (LwInit.HttpServerPath+"/ReadBackup", wf);
		yield return w;
		data = w.text.Split (',');

		float count = 0;

		foreach(string url in data){
			string URL = LwInit.HttpServerPath + url;
			w = new WWW(URL);
			yield return w;

			string path = Regex.Split(url, "1039311976095070", RegexOptions.IgnoreCase)[1];
			string dir = Path.GetDirectoryName("." + path);
			string dir2 = Path.GetDirectoryName(path);

			if(!Directory.Exists(Application.persistentDataPath + dir2)){
				Directory.CreateDirectory(Application.persistentDataPath + dir2);
			}
			File.WriteAllBytes(Application.persistentDataPath + path, w.bytes);

			w.Dispose();
			count ++;
			status.text = "讀取備份中..." + ((count/data.Length)*100).ToString("0.0");
		}

		if(data.Length == (int)count){
			status.text = "讀取完成！";
		}

		w.Dispose ();

	}		

	void OnGUI () {
		if(data != null){
			foreach(string url in data){
				GUILayout.Label (LwInit.HttpServerPath + url);
			}
		}
	}

	// Custom Methods ======================================================================================================================================

	void ButtonExit(GameObject obj){
		Application.LoadLevel ("MainMenu");
	}
#endif
}
