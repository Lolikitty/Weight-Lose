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

public class GroupMessage : MonoBehaviour {

	public UIScrollView sv;	
	public Transform buttonMsgsRoot;
	public Transform buttonMainMsgsRoot;
	public UILabel title;
	public Texture2D defaultUserTexture;
	public GameObject buttonBG;
	public GameObject buttonBack;
	public GameObject buttonExit;
	public GameObject Msg_My;
	public GameObject Msg_Other;
	public GameObject buttonMsgs;	
	public GameObject [] buttonMsg;
	
	public static string RoomID = "";
	public static string FNAME = "";

	Hashtable friendHeads = new Hashtable();
	TcpClient tcp;
	NetworkStream stream;
	StreamReader sr;
	StreamWriter sw;
	Texture2D myHead;
	Texture2D friendHead;

	string id = "";
	string friendMessage = "";
	string friendID = "";
	bool isQuit = false;
	bool isUp = false;
	int up = -280;
	int down = -775;
	int y = 180;

	string[] message = {"該減肥囉!","我想想!","我還要吃!","好吃!","瘦了多少?","快喝個水!","有效嗎?","OTZ","朕還要吃!","別亂!","瘦了多少?","快喝個水!"};

	// Unity Override Methods ==============================================================================================================================

	void Awake () {		
		ButtonInit ();
		VarInit ();
		TcpInit ();
		new Thread (FriendMessage).Start ();
		StartCoroutine (DownloadFriendHeadImage());
	}
			
	void Update () {
		KeyboardControl ();
		MessageEventListener ();
		sv.OnScrollBar();
	}

	void OnDisable(){
		Close ();
	}

	// Custom Methods ======================================================================================================================================

	IEnumerator DownloadFriendHeadImage(){
		foreach(string fid in Group.IDs){			
			WWW w = new WWW (LwInit.HttpServerPath+"/ServerData/"+fid+"/User.png");
			yield return w;
			if(w.error == null){
				print (LwInit.HttpServerPath+"/ServerData/"+fid+"/User.png");
				friendHeads.Add(fid, w.texture);
			}else{
				friendHeads.Add(fid, defaultUserTexture);
			}
			w.Dispose();
		}
		sw.WriteLine ("<InitFinish>");
	}

	void VarInit(){
		title.text = "Group";
		id = JsonConvert.DeserializeObject<JObject> (File.ReadAllText(Application.persistentDataPath + "/User.txt"))["ID"].ToString();
	}

	void TcpInit(){
		tcp = new TcpClient(LwInit.ServerIP, LwInit.TalkGroupServerPort);
		stream = new NetworkStream(tcp.Client);
		sr = new StreamReader (stream);
		sw = new StreamWriter(stream);
		sw.AutoFlush = true;
		#if UNITY_EDITOR
		sw.WriteLine("9,"+ RoomID); // Login
		id = "9";
		#else
		sw.WriteLine(id + ","+ RoomID); // Login
		#endif
	}

	void MessageEventListener(){
		if(friendMessage != ""){
			if(friendMessage == id){
				MyHistoryMessage(friendID);
			}else{
				FriendMessage2(friendMessage, friendID);
			}
			friendMessage = "";
		}
	}

	void FriendMessage(){
		while(!isQuit){			
			string msg = sr.ReadLine ();
			string [] msg2 = msg.Split('_');
			friendMessage = msg2[0];
			friendID = msg2[1];
		}
	}

	void FriendMessage2(string id, string msg){
		try{
			GameObject f = Instantiate(Msg_Other) as GameObject;
			f.transform.parent = buttonMainMsgsRoot;
			f.transform.localPosition = new Vector3(0, y);
			f.transform.localScale = Vector3.one;
			LwMessage_Unit fmu = f.GetComponent<LwMessage_Unit>();
			fmu.userImg.mainTexture = (Texture) friendHeads[id];
			fmu.name.text = "" + Group.USER[id];						
			fmu.message.text = message[int.Parse(msg)];
			y -= 120;
		}catch(Exception e){
			print (e.ToString());
		}

	}

	void MyHistoryMessage(string msg){
		GameObject m = Instantiate(Msg_My) as GameObject;
		m.transform.parent = buttonMainMsgsRoot;
		m.transform.localPosition = new Vector3(0, y);
		m.transform.localScale = Vector3.one;
		LwMessage_Unit mu = m.GetComponent<LwMessage_Unit>();
		mu.name.text =  "" + Group.USER[id];
		mu.userImg.mainTexture = (Texture) friendHeads[id];
		mu.message.text = message[int.Parse(msg)];				
		y -= 120;
	}

	void ButtonMsg(GameObject button){
		for(int i = 0; i<buttonMsg.Length; i++){
			if("ButtonMsg"+i == button.name){
				GameObject m = Instantiate(Msg_My) as GameObject;
				m.transform.parent = buttonMainMsgsRoot;
				m.transform.localPosition = new Vector3(0, y);
				m.transform.localScale = Vector3.one;
				LwMessage_Unit mu = m.GetComponent<LwMessage_Unit>();
				mu.name.text =  "" + Group.USER[id];
				mu.userImg.mainTexture = (Texture) friendHeads[id];
				mu.message.text = message[i];				
				sw.WriteLine(i);
				y -= 120;
			}
		}
		isUp = false;
	}

	void ButtonInit(){
		UIEventListener.Get(buttonBack).onClick = ButtonBack;
		UIEventListener.Get(buttonExit).onClick = ButtonExit;
		UIEventListener.Get(buttonMsgs).onClick = ButtonMsgs;
		UIEventListener.Get(buttonBG).onClick = ButtonBG;
		for(int i = 0; i<buttonMsg.Length; i++){
			UIEventListener.Get(buttonMsg[i]).onClick = ButtonMsg;
		}
	}

	void KeyboardControl(){
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

	void ButtonMsgs(GameObject button){
		isUp = true;
	}

	void ButtonBG(GameObject button){
		isUp = false;
	}
	
	void ButtonBack(GameObject button){
		Application.LoadLevel ("Talk");
	}
	
	void ButtonExit(GameObject button){
		Application.LoadLevel ("MainMenu");
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
		print ("Quit");
	}
}
