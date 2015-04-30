using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System;

public class LwBackup : MonoBehaviour {


	public GameObject buttonExit;

	public UITexture imgObj;

	public UILabel userName;
	public UILabel status;

	int count = 0;
	string err = "No";

	// Unity Override Methods ==============================================================================================================================


	void Awake () {
		UIEventListener.Get (buttonExit).onClick = ButtonExit;

		imgObj.mainTexture = FBInit.FB_USER_IMAGE;
		userName.text = FBInit.FB_USER_NAME;


		foreach(string filePath in Directory.GetFiles(Application.persistentDataPath)) {
			hs.Add(filePath);
		}

		GetFile (Application.persistentDataPath);
		StartCoroutine (UploadFile ());
	}

	void OnGUI(){
		GUILayout.Label ("Count : "+count);
		GUILayout.Label ("Error : "+err);
		GUILayout.Space (50);		
		foreach(string path in hs){
			GUILayout.Label (path);
		}
	}

	// Custom Methods ======================================================================================================================================

	void ButtonExit(GameObject obj){
		Application.LoadLevel ("MainMenu");
	}

	IEnumerator UploadFile(){
		status.text = "備份中...";
		byte [] b = null;
		FileStream fs = null;
		foreach(string path in hs){
			try{
				fs = new FileStream(path, FileMode.Open);
				b = new byte[fs.Length];
				fs.Read(b, 0, b.Length);
			}catch(Exception e){
				err = " A : " + e.ToString() +"    "+ string.IsNullOrEmpty(path);
			}
			fs.Close();
			WWWForm wf = new WWWForm();
			wf.AddField("id", FBInit.FB_USER_ID);
			#if UNITY_ANDROID
				wf.AddField("device", "Android");
			#endif
			wf.AddBinaryData("file", b, WWW.EscapeURL(path));
			WWW w = new WWW(LwInit.HttpServerPath+"/Backup", wf);
			yield return w;
			err = " B : " + w.error;
			w.Dispose();
			count++;
			status.text = "備份中..." + (((float)count / (float)hs.Count)*100).ToString("0.0") + "%" ;
		}
		if(count == hs.Count){
			status.text = "備份完成！";
		}
	}

	HashSet<string> hs = new HashSet<string>();

	void GetFile(string path){
		try{
			foreach(string dirPath in Directory.GetDirectories(path)) {		
				foreach(string filePath in Directory.GetFiles(dirPath)) {
					hs.Add(filePath);
				}
				GetFile(dirPath);
			}
		}catch(Exception e){
			LwError.Show("LwBackup.GetFile() : " + e);
		}
	}

}
