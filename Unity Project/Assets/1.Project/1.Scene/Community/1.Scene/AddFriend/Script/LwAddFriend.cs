using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LwAddFriend : MonoBehaviour {

	public GameObject friend;
	public UIScrollView scrollView;

	public Texture2D defaultImage;

	string msg;

	public GameObject noFriend;

	IEnumerator Start () {

		WWWForm wwwF = new WWWForm();
		wwwF.AddField("id", JsonConvert.DeserializeObject<JObject> (File.ReadAllText(Application.persistentDataPath + "/User.txt"))["ID"].ToString());
		
		WWW www = new WWW(LwInit.HttpServerPath+"/GetWaitFriend", wwwF);
		yield return www;
		msg = www.text;
		www.Dispose ();
		if(msg == ""){
			noFriend.SetActive(true);
		}else{
			noFriend.SetActive(false);
			string [] idName = msg.Split(';');
			for(int i = 0 ; i < idName.Length-1 ; i++){
				GameObject fri = Instantiate (friend) as GameObject;
				fri.transform.parent = scrollView.transform;
				fri.transform.localScale = Vector3.one;
				fri.transform.localPosition = new Vector3(0, i * -127, 0);
				fri.GetComponent<LwAddFriendInformation>().friendID = idName[i].Split(',')[0]; // ID
				fri.GetComponent<LwAddFriendInformation>().name.text = idName[i].Split(',')[1]; // Name

				WWW www2 = new WWW(LwInit.HttpServerPath+"/ServerData/" + idName[i].Split(',')[0] + "/User.png");
				yield return www2;
				fri.GetComponent<LwAddFriendInformation>().head.mainTexture = www2.error != null ? defaultImage : www2.texture;
			}
		}

		scrollView.OnScrollBar ();
	}

	void OnGUI(){
		GUILayout.Label ("Msg : " + msg);
	}
}
