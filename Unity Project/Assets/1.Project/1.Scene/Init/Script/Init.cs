using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class Init : MonoBehaviour {

	public Texture2D head;
	
	void Awake () {
		String[] FileCollection;
		
		string FilePath = Application.persistentDataPath;
		
		FileInfo theFileInfo;
		
		
		FileCollection = Directory.GetFiles(FilePath, "*.png");
		
		for(int i = 0 ; i < FileCollection.Length ; i++)
			
		{
			
			theFileInfo = new FileInfo(FileCollection[i]);

			
		}

		StartCoroutine (InitMix ());

	}

	IEnumerator InitMix () {

		using (WWW w = new WWW (LwInit.HttpServerPath + "/FoodMenu.txt")) {
			yield return w;
			string JsonFoodDataPath = Application.persistentDataPath + "/FoodMenu.txt";
			File.WriteAllBytes(JsonFoodDataPath, w.bytes);
		}

		string headPath = Application.persistentDataPath + "/User.png";

		if (File.Exists (headPath)) {
			string [] files = Directory.GetFiles (Application.persistentDataPath, "*.pw");
			if(files.Length == 0){
				Application.LoadLevel ("MainMenu");
			}else{
				Application.LoadLevel ("InputPassword");
			}

			
		} else {
			System.IO.File.WriteAllBytes(headPath, head.EncodeToPNG());
			WWW www = new WWW(LwInit.HttpServerPath+"/SignID");
			yield return www;
			try{
				string id = www.text;
				www.Dispose();
				//msg = id + " : " + www.error;
				
				//				print ("My ID : " + id);
				//				PlayerPrefs.SetString ("ID", id);
				//				PlayerPrefs.Save ();
				
				//---------- Json
				
				string JsonUserDataPath = Application.persistentDataPath + "/User.txt";
				object data = null;
				if(File.Exists(JsonUserDataPath)){
					JObject obj = JsonConvert.DeserializeObject<JObject> (File.ReadAllText(JsonUserDataPath));
					obj["ID"] = id;
					obj["Name"] = id;
					data = obj;
				}else{
					data = new {
						ID = id,
						Name = id
					};
				}
				
				string jsonText = JsonConvert.SerializeObject(data,Formatting.Indented);
				
				File.WriteAllText(JsonUserDataPath, jsonText);

				//---------- Json

				
				Directory.CreateDirectory (Application.persistentDataPath + "/Friend");
				
				Application.LoadLevel ("SetBirthday");
				
			}catch(Exception ex){
				LwError.Show("LwInit.Start() : " + ex);
			}
			
		}
	}

}
