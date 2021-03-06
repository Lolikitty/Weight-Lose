﻿using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
//using System.Web;

public class LwInit : MonoBehaviour {

	/*
	void Start(){
		Application.LoadLevel ("MainMenu");
	}
	*/

	//聲音
	public AudioSource audio_s;
	public AudioClip audio_c;
	
	public bool go_clock = false ;
	public bool go_food = false;


	public bool go_password = false;
	
	public bool go = false;

//	public static string ServerIP = "54.69.109.145";
	public static string ServerIP = "52.69.77.17";
//	public static string ServerIP = "192.168.2.187";
	public static int ServerPort = 4040;
	public static int TalkGroupServerPort = 4041;
	public static int TalkServerPort = 4042;
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

		}catch(Exception e){
			LwError.Show(e.ToString());
		}

	}
	
	// Custom Methods ======================================================================================================================================

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
	


}

