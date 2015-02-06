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


public class LwInit : MonoBehaviour {

	//聲音
	public AudioSource audio_s;
	public AudioClip audio_c;

	public bool go_clock = false ;
	public bool go_food = false;
	public bool go_password = false;

	public bool go = false;

	public Texture2D head;

//	public static string ServerIP = "54.69.109.145";
	public static string ServerIP = "192.168.1.103";
	public static int ServerPort = 4040;
	public static int TalkServerPort = 4041;
	public static string HttpServerPath = "http://" + ServerIP + ":" + ServerPort;

	// Unity Override Methods ==============================================================================================================================

	IFormatProvider culture;

	public DateTime st = new DateTime();
	public DateTime et = new DateTime();
	public DateTime clock = new DateTime();
	public DateTime now = new DateTime();

	string wc_from;
	string wc_end;

	string ct;

	public string next_time;

	string imgDir ;




	void Awake(){

		try{

		string p_clock = PlayerPrefs.GetString ("go_clock");
		string p_food = PlayerPrefs.GetString ("go_food");
		string p_password = PlayerPrefs.GetString ("go_password");
		
		if (p_clock == "true") {
			
			go_clock = true;
		}
		
		if (p_food == "true") {
			
			go_food = true;
		}
		
		if (p_password == "true") {
			
			go_password = true;
		}
		
		audio_s = this.gameObject.GetComponent<AudioSource>();

		DontDestroyOnLoad (this.transform.gameObject);

//		string imgPath = "/Image/FoodMenu/" ;
//
//		imgDir =  Application.persistentDataPath + imgPath;
//
//		JsonFoodInit ();
		

		StartCoroutine (InitMix ());
		}catch(Exception e){
			LwError.Show(e.ToString());
		}


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
	}

	void Update () {
		
		
		if (go_clock == true && go == true) {
			
			now = DateTime.Now;

			next_time = Convert.ToInt32(Math.Ceiling( clock.Subtract(now).TotalMinutes)).ToString();
			
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




	string [] foodName1 = {"蔥油餅","油條","豆漿","蛋餅","飯糰","荷包蛋","包子","刈包","饅頭","燒餅","小籠包","燒賣"};
	string [] foodKal1 = {"500","400","350","450","550","300","350","450","300","350","600","600"};
	string [] foodName2 = {"鬆餅","牛奶","麥片","吐司"};
	string [] foodKal2 = {"500","400","350","450"};
	string [] foodName3 = {"牛肉麵"};
	string [] foodKal3 = {"900"};
	string [] foodName4 = {"麥當勞"};
	string [] foodKal4 = {"900"};
	string [] foodName5 = {"港式飲茶"};
	string [] foodKal5 = {"700"};
	string [] foodName6 = {"義大利麵"};
	string[] foodKal6 = {"800"};
	string [] foodName7 = {"麥芽糖"};
	string [] foodKal7 = {"300"};


	string [] foodName8 = {"巧克力","紅茶","咖啡","藍山","曼特寧","巴西","卡布奇諾","拿鐵"};
	string [] foodKal8 = {"600","300","350","300","400","450","350","500"};

	string [] foodKind = {"中式早餐","西式早餐","中式午餐","西式午餐","中式晚餐","西式晚餐","中式點心","西式點心"};
	List<string[]> food_list = new List<string[]>();
	List<string[]> kal_list = new List<string[]>();

	void JsonFoodInit(){
		string JsonFoodDataPath = imgDir + "/FoodMenu.txt";
		List<object> Food = null;
		
		if (!File.Exists (JsonFoodDataPath)) {
			Food = new List<object> ();
			File.WriteAllText(JsonFoodDataPath, JsonConvert.SerializeObject(new{Food},Formatting.Indented));
			
			food_list.Add(foodName1);
			food_list.Add(foodName2);
			food_list.Add(foodName3);
			food_list.Add(foodName4);
			food_list.Add(foodName5);
			food_list.Add(foodName6);
			food_list.Add(foodName7);
			food_list.Add(foodName8);
			
			kal_list.Add(foodKal1);
			kal_list.Add(foodKal2);
			kal_list.Add(foodKal3);
			kal_list.Add(foodKal4);
			kal_list.Add(foodKal5);
			kal_list.Add(foodKal6);
			kal_list.Add(foodKal7);
			kal_list.Add(foodKal8);
			
			Food = (JsonConvert.DeserializeObject<JObject> (File.ReadAllText(JsonFoodDataPath))["Food"] as JArray).ToObject<List<object>>();
			
			for(int i = 0 ; i < 8 ; i++){
				var food = new {
					Name = foodKind[i],
					Food2 = food_list[i],
					Kal = kal_list[i],
					JPGPath = "/" ,
					PNGPath = "/" ,
				};
				
				Food.Add(food);
			}
			
			File.WriteAllText(JsonFoodDataPath, JsonConvert.SerializeObject(new{Food},Formatting.Indented));
			
		}
		
		
		
	}
}































