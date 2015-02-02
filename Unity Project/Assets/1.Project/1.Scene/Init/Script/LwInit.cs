using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;


public class LwInit : MonoBehaviour {

	//聲音
	public AudioSource audio_s;
	public AudioClip audio_c;

	public bool go = false ;

	public Texture2D head;

//	public static string ServerIP = "54.69.109.145";
	public static string ServerIP = "192.168.1.103";
	public static int ServerPort = 4040;
	public static int TalkServerPort = 4041;
	public static string HttpServerPath = "http://" + ServerIP + ":" + ServerPort;

	// Unity Override Methods ==============================================================================================================================

	IFormatProvider culture;

	DateTime st;
	DateTime et;
	DateTime clock;
	DateTime now;

	string ct;

	public string next_time;



	void Awake(){
		StartCoroutine (InitMix ());

		DontDestroyOnLoad (this.gameObject);
	}

	// Custom Methods ======================================================================================================================================

	void Start(){

		audio_s = this.gameObject.GetComponent<AudioSource>();
		audio_s.Play ();


	}



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

	void Update () {
		
		
		if (go == true) {
			
			now = DateTime.Now;
			
			//Debug.Log(clock.Subtract(now).TotalMinutes).ToString());
			
			if(DateTime.Compare(now , clock) >= 0 ){
				
				Debug.Log("time_out");
				
				Call() ;
				
			}
			
		}
		
	}




	void Call(){
		
		audio_s.Play ();
		//audio_s.SetScheduledEndTime (30);
		
		clock = clock.AddMinutes (Convert.ToDouble(ct));
		
		Debug.Log (clock.ToString ());
		
		if(DateTime.Compare(clock , et) >= 0 ){
			
			go = false;

		}
		
	}

	public void clock_start(DateTime c_st , DateTime c_et  ,DateTime c_clock,string c_ct){

		st = c_st;
		et = c_et;
		clock = c_clock;
		ct = c_ct;

		go = true;
	}
}































