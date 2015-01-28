using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;


public class LwInit : MonoBehaviour {

	public Texture2D head;

//	public static string ServerIP = "54.69.109.145";
	public static string ServerIP = "192.168.2.207";//"127.0.0.1";//"192.168.1.103";
	public static int ServerPort = 4040;
	public static int TalkServerPort = 4041;
	public static string HttpServerPath = "http://" + ServerIP + ":" + ServerPort;

	// Unity Override Methods ==============================================================================================================================

	void Awake(){
		StartCoroutine (InitMix ());
	}

	// Custom Methods ======================================================================================================================================

	IEnumerator InitMix () {
		print (Application.persistentDataPath);

//		PlayerPrefs.SetString ("ID", "4");
//		PlayerPrefs.Save ();

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
			WWW www = new WWW(HttpServerPath+"/SignID");
			yield return www;
			try{
				string id = www.text;
				print ("My ID : " + id);
				PlayerPrefs.SetString ("ID", id);
				PlayerPrefs.Save ();
				Directory.CreateDirectory (Application.persistentDataPath + "/Friend");
				Application.LoadLevel ("SetBirthday");
			}catch(Exception ex){
				LwError.Show("LwInit.Start() : " + ex);
			}

		}
		if(!Directory.Exists(Application.persistentDataPath+"/Food")){
			Directory.CreateDirectory(Application.persistentDataPath+"/Food");
		}
	}

}































