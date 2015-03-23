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

public class LwAddFriendInformation : MonoBehaviour {

	public UITexture head;
	public UILabel name;
	public GameObject buttonOK;
	public GameObject buttonDelete;
	public string friendID;

	void Awake () {
		UIEventListener.Get(buttonOK).onClick = ButtonOK;
		UIEventListener.Get(buttonDelete).onClick = ButtonDelete;
	}
	
	void ButtonOK(GameObject button){
		print (friendID + " ok");
//		Destroy (gameObject);
//		SaveServer (true);

		StartCoroutine (UploadData("y"));


	}

	void ButtonDelete(GameObject button){
		print (friendID + " delete");
//		Destroy (gameObject);
//		SaveServer (false);

		StartCoroutine (UploadData("n"));
	}

	IEnumerator UploadData(string agree){
		WWWForm wwwF = new WWWForm();
		wwwF.AddField("id", JsonConvert.DeserializeObject<JObject> (File.ReadAllText(Application.persistentDataPath + "/User.txt"))["ID"].ToString());
		wwwF.AddField("friend_id", friendID);
		wwwF.AddField("agree", agree);

		WWW www = new WWW(LwInit.HttpServerPath+"/AgreeFriend", wwwF);
		yield return www;

		Application.LoadLevel ("AddFriend");
	}


//	void SaveServer(bool agree){
//		TcpClient tcp = new TcpClient(LwInit.ServerIP, LwInit.ServerPort);
//		NetworkStream stream = new NetworkStream(tcp.Client);
//		StreamReader sr = new StreamReader (stream);
//		StreamWriter sw = new StreamWriter(stream);
//		sw.AutoFlush = true;
//		string id = PlayerPrefs.GetString ("ID");
//		sw.WriteLine ("AgreeFriend/" + id + "/" + friendID + "/" + (agree ? "y" : "n") + "/ ");
//	}

}
