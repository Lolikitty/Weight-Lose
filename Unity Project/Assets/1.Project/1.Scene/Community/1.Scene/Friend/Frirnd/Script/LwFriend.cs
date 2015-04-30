using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LwFriend : MonoBehaviour {

	public GameObject AddFriend;
	public GameObject buttonAddFriend;
	public GameObject buttonExitAddFriend;
	public GameObject buttonAddFriendOK;
	public GameObject id_Number;

	public GameObject textureCanNotFindID;

	public UIScrollView sv;
	public GameObject friend;

	public Texture2D [] rankTexture;

	public Texture2D defaultUserTextture;

	public GameObject userInfo;
	public GameObject userInfoExit;
	public Transform userInfoFoodRoot;

	void Awake () {
		AddFriend.SetActive (false);
		textureCanNotFindID.SetActive (false);

		UIEventListener.Get(buttonAddFriend).onClick = ButtonAddFriend;
		UIEventListener.Get(buttonExitAddFriend).onClick = ButtonExitAddFriend;
		UIEventListener.Get(buttonAddFriendOK).onClick = ButtonAddFriendOK;
		UIEventListener.Get(id_Number).onClick = ID_Number;
		UIEventListener.Get(userInfoExit).onClick = UserInfoExit;
	}

	IEnumerator Start (){

		WWWForm wwwF = new WWWForm();
		wwwF.AddField("id", JsonConvert.DeserializeObject<JObject> (File.ReadAllText(Application.persistentDataPath + "/User.txt"))["ID"].ToString());
		
		WWW www = new WWW(LwInit.HttpServerPath+"/GetFriend", wwwF);
		yield return www;
		string [] idName = www.text.Split (';');
		for(int i = 0 ; i < idName.Length-1 ; i++){
			string id = idName[i].Split(',')[0];
			string name = idName[i].Split(',')[1];

			GameObject f = Instantiate(friend) as GameObject;
			f.transform.parent = sv.transform;
			f.transform.localScale = Vector3.one;
			f.transform.localPosition = new Vector3(0, 170 - i * 110, 0);
			LwFriend_User fu = f.GetComponent<LwFriend_User>();
			fu.userInfo = userInfo;
			fu.userInfoFoodRoot = userInfoFoodRoot;
			fu.friendID = id;
			fu.name.text = name;
			fu.rankDay.text = 10-i + " Day";
			if(i<10) fu.rankUITexture.mainTexture = rankTexture[i];

			WWW www2 = new WWW(LwInit.HttpServerPath+"/ServerData/" + idName[i].Split(',')[0] + "/User.png");
			yield return www2;
			if( www2.error != null ){
				fu.head.mainTexture = defaultUserTextture;
			}else{
				fu.head.mainTexture = www2.texture;
				File.WriteAllBytes(Application.persistentDataPath+"/Friend/"+id+".png", www2.texture.EncodeToPNG());
			}

		}

		sv.OnScrollBar ();
	}

	void UserInfoExit(GameObject obj){
		for(int i = 0; i < userInfoFoodRoot.childCount; i++){
			Destroy(userInfoFoodRoot.GetChild(i).gameObject);
		}
		userInfo.SetActive (false);
	}

//	IEnumerator
////	void 
//	Start () {
//
//		TcpClient tcp = new TcpClient(LwInit.ServerIP, LwInit.ServerPort);
//		NetworkStream stream = new NetworkStream(tcp.Client);
//		StreamReader sr = new StreamReader (stream);
//		StreamWriter sw = new StreamWriter(stream);
//		sw.AutoFlush = true;
//		string id = PlayerPrefs.GetString ("ID");
//		sw.WriteLine ("GetFriend/" + id + "/ ");
//
//		int y = 0;
//
//		while(true){
//			string msg = sr.ReadLine (); // Name_id
//			if(msg == "end"){
//				break;
//			}
//			if (msg != "") {
//				string nameFriend = msg.Split('_')[1];
//				string idFriend = msg.Split('_')[0];
//				print (nameFriend + "    " + idFriend);
//
//
//
//				GameObject f = Instantiate(friend) as GameObject;
//				f.transform.parent = sv.transform;
//				f.transform.localScale = Vector3.one;
//				f.transform.localPosition = new Vector3(0, 170 - y * 110, 0);
//
//
//				LwFriend_User fu = f.GetComponent<LwFriend_User>();
//
//				string imgPath = Application.persistentDataPath+"/Friend/"+idFriend+".png";
//
//				if(File.Exists(imgPath)){
//					WWW www = new WWW ("file://"+imgPath);
//					yield return www;
//					fu.head.mainTexture = www.texture;
//				}else{
//					fu.head.mainTexture = defaultUserTextture;
//				}
//
//				fu.name.text = nameFriend;
//				fu.friendID = idFriend;
//				fu.rankDay.text = 10-y + " Day";
//				if(y<10) fu.rankUITexture.mainTexture = rankTexture[y];
//
//				y++;
//			}else{
//				print ("No Friend...");
//			}
//		}
//
//
//
//		sw.Close ();
//		stream.Close ();
//		tcp.Close ();
//
//
//	}
	
	void ButtonAddFriend(GameObject button){
		AddFriend.SetActive (true);
	}

	void ButtonExitAddFriend(GameObject button){
		AddFriend.SetActive (false);
	}

	void ButtonAddFriendOK(GameObject button){


		try{
			int id = int.Parse(tsk.text);
			if(id < 1){
				textureCanNotFindID.SetActive (true);
				return;
			}
		}catch(Exception ex){
			textureCanNotFindID.SetActive (true);
			return;
		}


		StartCoroutine (UploadData ());				
	}

	string temp = "";
	TouchScreenKeyboard tsk;

	void ID_Number(GameObject button){
		#if !UNITY_EDITOR
		tsk = TouchScreenKeyboard.Open (temp, TouchScreenKeyboardType.NumberPad);
		#endif
	}

	void Update(){
		if(tsk != null){
			id_Number.GetComponent<UILabel>().text = tsk.text;
		}
	}


	IEnumerator UploadData(){		
		WWWForm wwwF = new WWWForm();
		wwwF.AddField("id", JsonConvert.DeserializeObject<JObject> (File.ReadAllText(Application.persistentDataPath + "/User.txt"))["ID"].ToString());
		wwwF.AddField("friend_id", tsk.text); // 
		
		WWW www = new WWW(LwInit.HttpServerPath+"/AddFriend", wwwF);
		yield return www;

		if(www.text == "NoID"){
			textureCanNotFindID.SetActive (true);
		}else{
			textureCanNotFindID.SetActive (false);
			AddFriend.SetActive (false);
		}
		tsk.text = "";
	}

//	bool SaveToServer_AddFriend(){
//		TcpClient tcp = new TcpClient(LwInit.ServerIP, LwInit.ServerPort);
//		NetworkStream stream = new NetworkStream(tcp.Client);
//		StreamReader sr = new StreamReader (stream);
//		StreamWriter sw = new StreamWriter(stream);
//		sw.AutoFlush = true;
//		string id = PlayerPrefs.GetString ("ID");
//		sw.WriteLine ("AddFriend/" + id + "/" + tsk.text +"/ ");
//		string ssr = sr.ReadLine ();
//		sw.Close ();
//		stream.Close ();
//		tcp.Close ();
//		tsk.text = "";
//		return ssr == "y" ? true : false;
//	}

}
