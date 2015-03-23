using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LwTalk : MonoBehaviour {
		
	public GameObject button_AddFriend;
	public Transform AddBG;
	public GameObject addBGExit;

	public UIScrollView sv;
	public GameObject friend;
		
	public Texture2D defaultUserTextture;

	public GameObject buttonAddBgOk;
	public GameObject buttonAddBgCancel;
	
	void Awake () {
		UIEventListener.Get (button_AddFriend).onClick = Button_AddFriend;
		UIEventListener.Get (addBGExit).onClick = AddBGExit;

		UIEventListener.Get (buttonAddBgOk).onClick = ButtonAddBgOk;
		UIEventListener.Get (buttonAddBgCancel).onClick = AddBGExit;

		StartCoroutine (GetGroup());
	}

	IEnumerator GetGroup(){
		WWWForm f = new WWWForm ();
		f.AddField ("id", JsonConvert.DeserializeObject<JObject> (File.ReadAllText(Application.persistentDataPath + "/User.txt"))["ID"].ToString());
		WWW w = new WWW (LwInit.HttpServerPath + "/GetGroup", f);
		yield return w;

		if(w.text != ""){
			msg = w.text;
			int y = 100;
			foreach(string s in msg.Split(';')){
				string room = s.Split(':')[0];
				
				GameObject g = Instantiate(group) as GameObject;
				g.transform.parent = gsv.transform;
				g.transform.localScale = Vector3.one;
				g.transform.localPosition = new Vector3(0, y);

				g.GetComponent<Group>().roomID = room;

				int x = -150;

				foreach(string user in s.Split(':')[1].Split(',')){
					string id = user.Split('_')[0];
					string name = user.Split('_')[1];

					g.GetComponent<Group>().user.Add(id, name);
					g.GetComponent<Group>().ids.Add(id);
					
					GameObject g2 = Instantiate(grpupUser) as GameObject;
					g2.transform.parent = g.transform;
					g2.transform.localScale = Vector3.one;
					g2.transform.localPosition = new Vector3(x, 50);
					g2.GetComponent<GroupUser>().userName.text  = name;
					g2.GetComponent<GroupUser>().DownloadImg(id);
					x+= 100;
				}
				y -= 100;
			}
		}
	}

	public UIScrollView gsv;
	public GameObject group;
	public GameObject grpupUser;


	public static bool isViewAddBG = false;
	public static bool isDoNotViewAddBG = false;

	void Button_AddFriend(GameObject obj){
		isViewAddBG = true;
		isDoNotViewAddBG = false;
		AddBG.transform.localPosition = new Vector3 (0, -800);
	}

	void AddBGExit(GameObject obj){
		isDoNotViewAddBG = true;
		LwTalk_User.ChooseIDs.Clear ();
	}

	void ButtonAddBgOk(GameObject obj){
		StartCoroutine (CreateGroup());
	}

	IEnumerator CreateGroup(){
		string msg = "";
		foreach(string id in LwTalk_User.ChooseIDs){
			msg += id + ",";
		}


		WWWForm f = new WWWForm ();
		f.AddField ("id", JsonConvert.DeserializeObject<JObject> (File.ReadAllText(Application.persistentDataPath + "/User.txt"))["ID"].ToString());
		f.AddField ("ids", msg);
		WWW w = new WWW (LwInit.HttpServerPath+"/CreateGroup", f);
		yield return w;

		isDoNotViewAddBG = true;
		LwTalk_User.ChooseIDs.Clear ();
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
			f.transform.localPosition = new Vector3(0, 170 - i * 100, 0);
			LwTalk_User fu = f.GetComponent<LwTalk_User>();
			fu.friendID = id;
			fu.name.text = name;
			
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

	void Update(){
		if(isViewAddBG){
			if(AddBG.localPosition.y < -20){
				AddBG.Translate(0, 5 * Time.deltaTime, 0);
			}else{
				AddBG.localPosition = Vector3.zero;
				isViewAddBG = false;
			}
		}

		if(isDoNotViewAddBG){
			if(AddBG.localPosition.y > -800){
				AddBG.Translate(0, -5 * Time.deltaTime, 0);
			}else{
				AddBG.localPosition = new Vector3(0, -800);
				isDoNotViewAddBG = false;
				Application.LoadLevel("Talk");
			}
		}
	}

	string msg = "No";

//	void OnGUI(){
//		foreach(string s in msg.Split(';')){
//			string room = s.Split(':')[0];
//			string msg2 = "# Room : " + room + " ||| ";
//
//			foreach(string user in s.Split(':')[1].Split(',')){
//				string id = user.Split('_')[0];
//				string name = user.Split('_')[1];
//				msg2 += " Name:"+name + " ID:"+id+",    ";
//			}
//			GUILayout.Label(msg2);
//		}
//
//
//	}
	
}
