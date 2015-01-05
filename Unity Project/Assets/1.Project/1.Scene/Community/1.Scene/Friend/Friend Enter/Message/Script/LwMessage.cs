using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections.Generic;

public class LwMessage : MonoBehaviour {

	public UIScrollView sv;

	public GameObject buttonBack;
	public GameObject buttonExit;

	public GameObject Msg_My;
	public GameObject Msg_Other;

	public Transform buttonMsgsRoot;
	public Transform buttonMainMsgsRoot;
	public GameObject buttonMsgs;

	public GameObject [] buttonMsg;

	public UILabel title;

	public static string FID = "";
	public static string FNAME = "";

	string myName = "";
	Texture2D myHead;
	Texture2D friendHead;

	string[] message = {"該減肥囉!","我想想!","我還要吃!","好吃!","瘦了多少?","快喝個水!","有效嗎?","OTZ","政還要吃!","別亂!","瘦了多少?","快喝個水!"};


	TcpClient tcp;
	NetworkStream stream;
	StreamReader sr;
	StreamWriter sw;
	string id = "";

	public Texture2D defaultUserTexture;

	
	
	// Use this for initialization
	void Awake () {

		UIEventListener.Get(buttonBack).onClick = ButtonBack;
		UIEventListener.Get(buttonExit).onClick = ButtonExit;
		UIEventListener.Get(buttonMsgs).onClick = ButtonMsgs;
		for(int i = 0; i<buttonMsg.Length; i++){
			UIEventListener.Get(buttonMsg[i]).onClick = ButtonMsg;
		}
		title.text = FNAME;
		myName = PlayerPrefs.GetString ("name");
		id = PlayerPrefs.GetString ("ID");

		//-----------------------

		tcp = new TcpClient(LwInit.ServerIP, LwInit.TalkServerPort);
		stream = new NetworkStream(tcp.Client);
		sr = new StreamReader (stream);
		sw = new StreamWriter(stream);
		sw.AutoFlush = true;
		sw.WriteLine(id + ","+ FID+ ","); // Login

		new Thread (FriendMessage).Start ();

		InvokeRepeating ("Run", 0, 0.001f);
	}

	void Run(){
		if(friendMessage != ""){
			FriendMessage2();
		}

		if(myMsg != ""){
			MyHistoryMsg();
		}
	}

	bool isQuit = false;

	string friendMessage = "";

	string hId, fhId;


	void FriendMessage(){
		while(!isQuit){


			string msg = sr.ReadLine ();
			print ("Msg : " + msg);

			// MyID_FriendID_Msg
			string [] msg2 = msg.Split('_');

			for(int i = 0; i<msg2.Length-1; i++){
				print ("ID : " + msg2[i]);
			}

			print ("Msg 2 : " + msg2[msg2.Length-1]);

			if(msg2[0]==id){
				hId = msg2[0];
				fhId = msg2[1];
				myMsg = msg2[msg2.Length-1];
			}else{
				hId = msg2[1];
				fhId = msg2[0];
				friendMessage = msg2[msg2.Length-1];
			}
		}
		print ("QQ"); // ??????????????????????????????????????????????????????????????????
	}

	string myMsg = "";

	void MyHistoryMsg(){
		GameObject m = Instantiate(Msg_My) as GameObject;
		m.transform.parent = buttonMainMsgsRoot;
		m.transform.localPosition = new Vector3(0, y);
		m.transform.localScale = Vector3.one;
		LwMessage_Unit mu = m.GetComponent<LwMessage_Unit>();
		mu.name.text = myName;
		mu.message.text = message[int.Parse(myMsg)];
		mu.userImg.mainTexture = myHead;
		
		y -= 120;

		myMsg = "";
	}
	
	int offset = 0, offset2 = 0;

	ArrayList fo = new ArrayList ();

	int count = 0;

	int y2= 180;

	void FriendMessage2(){
		try{
			GameObject f = Instantiate(Msg_Other) as GameObject;
			fo.Add(f);
			f.transform.parent = buttonMainMsgsRoot;
			f.transform.localPosition = new Vector3(0, y);
			f.transform.localScale = Vector3.one;
			LwMessage_Unit fmu = f.GetComponent<LwMessage_Unit>();
			fmu.userImg.mainTexture = friendHead;
			fmu.name.text = FNAME;
			// MyID_FriendID_Msg

			fmu.message.text = message[int.Parse(friendMessage)];
			y -= 120;


			if(count > 3){
				y2 = (120* (count-1));// offset
				for(int i = 0; i < fo.Count; i++, y2-=120){
//					((GameObject)fo[i]).transform.localPosition = new Vector3(0, y2);
				}
			}
			count++;
		}catch(Exception e){
			print (e.ToString());
		}
		friendMessage = "";
	}
	
	
	IEnumerator Start(){

		string myHeadPath = Application.persistentDataPath + "/User.png";
		string friendHeadPath = Application.persistentDataPath+"/Friend/"+FID+".png";

		if(File.Exists(myHeadPath)){
			WWW www = new WWW ("file://" + myHeadPath);
			yield return www;
			myHead = www.texture;
		}else{
			myHead = defaultUserTexture;
		}

		if(File.Exists(friendHeadPath)){
			WWW www2 = new WWW ("file://" + friendHeadPath);
			yield return www2;
			friendHead = www2.texture;
		}else{
			friendHead = defaultUserTexture;
		}


//
//
//

//		
//		while(true){
//			string msg = sr.ReadLine();
//			if(msg == "end"){
//				break;
//			}
//			string [] msg2 = msg.Split('_');
//			string _id =  msg2[0];
//			string _fid =  msg2[1];
//			string _msg =  msg2[2];
//			
//			
//			if(_id == id){ // if = my id
//				GameObject m = Instantiate(Msg_My) as GameObject;
//				m.transform.parent = buttonMainMsgsRoot;
//				m.transform.localPosition = new Vector3(0, y);
//				m.transform.localScale = Vector3.one;
//				LwMessage_Unit mu = m.GetComponent<LwMessage_Unit>();
//				mu.name.text = myName;
//				mu.message.text = message[int.Parse(_msg)];
//				mu.userImg.mainTexture = myHead;
//			}else{
//				GameObject f = Instantiate(Msg_Other) as GameObject;
//				f.transform.parent = buttonMainMsgsRoot;
//				f.transform.localPosition = new Vector3(0, y);
//				f.transform.localScale = Vector3.one;
//				LwMessage_Unit fmu = f.GetComponent<LwMessage_Unit>();
//				fmu.name.text = FNAME;
//				fmu.message.text = message[int.Parse(_msg)];
//				fmu.userImg.mainTexture = friendHead;
//			}
//			y -= 120;
//		}
//		
//		
//		
//		sr.Close();
//		sw.Close ();
//		stream.Close ();
//		tcp.Close ();
//		sr = null;
//		sw = null;
//		stream = null;
//		tcp = null;
//		
//		//-----------------------
//		
//		tcp = new TcpClient(LwInit.ServerIP, LwInit.ServerPort);
//		stream = new NetworkStream(tcp.Client);
//		sr = new StreamReader (stream);
//		sw = new StreamWriter(stream);
//		sw.AutoFlush = true;
//		
//		sw.WriteLine ("UploadMessage/" + id + "/" + FID + "/ ");
	}

	int up = -280;
	int down = -775;

	void Update () {
		if(isUp){
			if(buttonMsgsRoot.localPosition.y < up){
				buttonMsgsRoot.Translate(0, 5 * Time.deltaTime, 0);
				if(buttonMsgsRoot.localPosition.y > up){
					buttonMsgsRoot.localPosition = new Vector3(0, up);
				}
			}

		}else{
			if(buttonMsgsRoot.localPosition.y > down){
				buttonMsgsRoot.Translate(0, -5 * Time.deltaTime, 0);
				if(buttonMsgsRoot.localPosition.y < down){
					buttonMsgsRoot.localPosition = new Vector3(0, down);
				}
			}
		}


	}

	bool isUp = false;

	void ButtonMsgs(GameObject button){
		isUp = true;
	}

	void ButtonBack(GameObject button){
		Close ();
		Application.LoadLevel ("Friend");
	}

	void ButtonExit(GameObject button){
		Close ();
		Application.LoadLevel ("MainMenu");
	}

	void OnApplicationQuit(){
		print ("Quit");
		Close ();
	}

	void Close(){
		isQuit = true;
		sw.WriteLine ("exit");
		if(sw != null){
			sw.Close ();
			sw=null;
		}
		if(sr != null){
			sr.Close ();
			sr=null;
		}
		if(stream != null){
			stream.Close ();
			stream=null;
		}
		if(tcp != null){
			tcp.Close ();
			tcp=null;
		}
	}

	int y = 180;

	void ButtonMsg(GameObject button){
		for(int i = 0; i<buttonMsg.Length; i++){
			if("ButtonMsg"+i == button.name){
				GameObject m = Instantiate(Msg_My) as GameObject;
				m.transform.parent = buttonMainMsgsRoot;
				m.transform.localPosition = new Vector3(0, y);
				m.transform.localScale = Vector3.one;
				LwMessage_Unit mu = m.GetComponent<LwMessage_Unit>();
				mu.name.text = myName;
				mu.message.text = message[i];
				mu.userImg.mainTexture = myHead;

				sw.WriteLine(i + ",");

				y -= 120;

//				GameObject f = Instantiate(Msg_Other) as GameObject;
//				f.transform.parent = buttonMainMsgsRoot;
//				f.transform.localPosition = new Vector3(0, y);
//				f.transform.localScale = Vector3.one;
//				LwMessage_Unit fmu = f.GetComponent<LwMessage_Unit>();
//				fmu.name.text = FNAME;
//				fmu.message.text = "Hi";
//
//				y -= 120;
			}
		}
		isUp = false;
	}

}
