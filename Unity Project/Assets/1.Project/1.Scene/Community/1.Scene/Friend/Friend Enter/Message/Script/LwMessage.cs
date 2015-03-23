using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

	Texture2D myHead;
	Texture2D friendHead;

	string[] message = {"該減肥囉!","我想想!","我還要吃!","好吃!","瘦了多少?","快喝個水!","有效嗎?","OTZ","政還要吃!","別亂!","瘦了多少?","快喝個水!"};


	public Texture2D defaultUserTexture;
	
	ArrayList fo = new ArrayList ();

	TcpClient tcp;
	NetworkStream stream;
	StreamReader sr;
	StreamWriter sw;

	string myName = "";
	string id = "";
	string friendMessage = "";	
	string hId, fhId;
	string myMsg = "";
	string ERROR = "No";

	int y = 180;
	int offset = 0, offset2 = 0;
//	int count = 0;	
	int y2= 180;
	int up = -280;
	int down = -775;

	bool isUp = false;
	bool isQuit = false;
	
	// Use this for initialization
	void Awake () {
		try{
			UIEventListener.Get(buttonBack).onClick = ButtonBack;
			UIEventListener.Get(buttonExit).onClick = ButtonExit;
			UIEventListener.Get(buttonMsgs).onClick = ButtonMsgs;
			for(int i = 0; i<buttonMsg.Length; i++){
				UIEventListener.Get(buttonMsg[i]).onClick = ButtonMsg;
			}

			id = JsonConvert.DeserializeObject<JObject> (File.ReadAllText(Application.persistentDataPath + "/User.txt"))["ID"].ToString();

			title.text = FNAME;

			JObject jobj = JsonConvert.DeserializeObject<JObject> (File.ReadAllText(Application.persistentDataPath + "/User.txt"));

			if(jobj["Name"] != null){
				myName = jobj["Name"].ToString();
			}else{
				myName = id;
			}


			
			//-----------------------
			
			tcp = new TcpClient(LwInit.ServerIP, LwInit.TalkServerPort);
			stream = new NetworkStream(tcp.Client);
			sr = new StreamReader (stream);
			sw = new StreamWriter(stream);
			sw.AutoFlush = true;
			sw.WriteLine(id + ","+ FID); // Login
			
			new Thread (FriendMessage).Start ();

		}catch(Exception e){
			LwError.Show("LwMessage.Awake() : " + e);
		}
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
		
	}
	
	void ButtonMsg(GameObject button){
		try{
			for(int i = 0; i<buttonMsg.Length; i++){
				if("ButtonMsg"+i == button.name){
					GameObject m = Instantiate(Msg_My) as GameObject;
					m.transform.parent = buttonMainMsgsRoot;
					m.transform.localPosition = new Vector3(0, y);
					m.transform.localScale = Vector3.one;
					LwMessage_Unit mu = m.GetComponent<LwMessage_Unit>();
					mu.NAME = myName;
					mu.message.text = message[i];
					mu.userImg.mainTexture = myHead;
					
					sw.WriteLine(i);
					
					y -= 120;
				}
			}
			isUp = false;
		}catch(Exception e){
			LwError.Show("LwMessage.Update() : " + e);
		}
	}

	void MyHistoryMsg(){
		try{
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
		}catch(Exception e){
			LwError.Show("LwMessage.MyHistoryMsg() : " + e);
		}
	}	
	
	void FriendMessage(){
		while(!isQuit){
			friendMessage = sr.ReadLine ();
		}
	}

	void FriendMessage2(string msg){
		try{
			GameObject f = Instantiate(Msg_Other) as GameObject;
			fo.Add(f);
			f.transform.parent = buttonMainMsgsRoot;
			f.transform.localPosition = new Vector3(0, y);
			f.transform.localScale = Vector3.one;
			LwMessage_Unit fmu = f.GetComponent<LwMessage_Unit>();
			fmu.userImg.mainTexture = friendHead;
			fmu.NAME = FNAME;
			fmu.message.text = message[int.Parse(msg)];
			y -= 120;
		}catch(Exception e){
			ERROR = e.Message;
		}
	}

	void Update () {
		try{
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
		}catch(Exception e){
			LwError.Show("LwMessage.Update() : " + e);
		}

		if(friendMessage != ""){
			FriendMessage2(friendMessage);
			friendMessage = "";
		}

	}

	void OnGUI(){
		GUILayout.Label ("myName : " + myName);
//		GUILayout.Label ("FNAME : " + FNAME);
//		GUILayout.Label ("ERROR : " + ERROR);
	}

	void ButtonMsgs(GameObject button){
		isUp = true;
	}

	void ButtonBack(GameObject button){
		Application.LoadLevel ("Friend");
	}

	void ButtonExit(GameObject button){
		Application.LoadLevel ("MainMenu");
	}

	void OnDisable() {
		Close ();
	}

	void Close(){
		isQuit = true;
		if(sw != null){
			sw.Dispose();
			sw.Close ();
			sw=null;
		}
		if(sr != null){
			sr.Dispose();
			sr.Close ();
			sr=null;
		}
		if(stream != null){
			stream.Dispose();
			stream.Close ();
			stream=null;
		}
		if(tcp != null){
			tcp.Close ();
			tcp=null;
		}
	}
}
