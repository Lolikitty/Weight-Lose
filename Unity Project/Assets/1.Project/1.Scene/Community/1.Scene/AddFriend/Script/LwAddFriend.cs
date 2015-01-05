using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Security.Cryptography;

public class LwAddFriend : MonoBehaviour {

	public GameObject friend;
	public UIScrollView scrollView;

	public Texture2D defaultImage;
	
	IEnumerator Start () {

		WWWForm wwwF = new WWWForm();
		wwwF.AddField("id", PlayerPrefs.GetString ("ID"));
		
		WWW www = new WWW(LwInit.HttpServerPath+"/GetWaitFriend", wwwF);
		yield return www;
		string msg = www.text;
		if(msg == ""){
			print ("No Wait Friend.");
		}else{
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



//		string msg = "";
//		TcpClient tcp = null;
//		NetworkStream stream = null;
//		StreamReader sr = null;
//		StreamWriter sw = null;
//		//		try{
//		tcp = new TcpClient(LwInit.ServerIP, LwInit.ServerPort);
//		stream = new NetworkStream(tcp.Client);
//		sr = new StreamReader (stream);
//		sw = new StreamWriter(stream);
//		sw.AutoFlush = true;
//
//		tcp.Client.ReceiveBufferSize = 1000000;
//		
//		string id = PlayerPrefs.GetString ("ID");
//		sw.WriteLine ("GetWaitFriend/" + id + "/ ");
//			
//		
//		//			if (msg == "n")	return;
//		//			string [] msg2 = msg.Split ('/');
//		
//		int i = 0;
//		
//		while(true){
//			msg = sr.ReadLine ();
//			
//			if(msg=="n"){
//				continue;
//			}else if(msg == "ok"){
//				break;
//			}
//
//
//			string nameFriend = msg.Split('_')[1];
//			string idFriend = msg.Split('_')[0];
//			
//			GameObject fri = Instantiate (friend) as GameObject;
//			fri.transform.parent = scrollView.transform;
//			fri.transform.localScale = Vector3.one;
//			fri.transform.localPosition = new Vector3(0, i * -127, 0);
//			fri.GetComponent<LwAddFriendInformation>().name.text = nameFriend; // Name
//			fri.GetComponent<LwAddFriendInformation>().friendID = idFriend; // ID
//			i++;
//			
//			msg = sr.ReadLine ();
//			
//			if(msg != "HaveImg"){
//				continue;
//			}
//			
//			int fileSize = int.Parse(sr.ReadLine ());
//			
//			print ("Size : " + fileSize);
//
//			string savePath = Application.persistentDataPath + "/Friend/"+idFriend+".png";
//			
//			byte [] b = new byte[fileSize];
//
//			if(File.Exists(savePath)){
//				FileStream f = new FileStream(savePath, FileMode.Open);
//				if((int)f.Length == fileSize){
//					sw.WriteLine("ContinueImage");
//					f.Close();
//					WWW www = new WWW("file://"+savePath);
//					yield return www;			
//					fri.GetComponent<LwAddFriendInformation>().head.mainTexture = www.texture;
//					continue;
//				}else{
//					f.Close();
//					File.Delete(savePath);
//					sw.WriteLine("GetImage");
//				}
//			}else{
//				sw.WriteLine("GetImage");
//			}
//
//
//
//
//
////			stream.Read(b,0,fileSize);
////			tcp.Client.Receive(b);
//
//
//			for(int k=0; k<fileSize; k++){
//				b[k] = (byte) stream.ReadByte();
//			}
//
//			
//
//			File.WriteAllBytes(savePath, b);
//			
//
//			WWW www2 = new WWW("file://"+savePath);
//			yield return www2;			
//			fri.GetComponent<LwAddFriendInformation>().head.mainTexture = www2.texture;
//
//
//		}
//		
//		scrollView.OnScrollBar ();
//		
//		//		}catch(Exception e){
//		//			LwError.Show (msg + "\n" + e.ToString());
//		//		}
//		
//		if(sw != null){
//			sw.Close ();
//			sw = null;
//		}
//		
//		if(stream != null){
//			stream.Close ();
//			stream = null;
//		}
//		
//		if(tcp != null){
//			tcp.Close ();
//			tcp = null;
//		}









	}


}
