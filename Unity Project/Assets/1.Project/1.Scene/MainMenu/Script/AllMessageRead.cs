using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class AllMessageRead : MonoBehaviour {


	public GameObject gobj_show;
	public UILabel label_count;

	void Awake(){
		gobj_show.SetActive(false);
	}

	IEnumerator Start () {

		string JsonUserDataPath = Application.persistentDataPath + "/User.txt";
		JObject obj = JsonConvert.DeserializeObject<JObject> (File.ReadAllText(JsonUserDataPath));

		string [] msgs;

		WWWForm f = new WWWForm ();
		f.AddField ("id", obj["ID"].ToString());

		using (WWW w = new WWW (LwInit.HttpServerPath+"/GetFriend", f)) {
			yield return w;
			msgs = w.text.Split(';');
		}

		List <bool> reads = new List<bool> ();

		foreach(string data in msgs){
			if(data == ""){
				break;
			}
			string friend_id = data.Split(',')[0];

			WWWForm f2 = new WWWForm ();
			f2.AddField ("id", obj["ID"].ToString());
			f2.AddField ("friend_id", friend_id);

			using (WWW w = new WWW (LwInit.HttpServerPath+"/GetReadMessage", f2)) {
				yield return w;
				reads.Add(bool.Parse(w.text));
			}
		}

		int count = 0;

		foreach(bool read in reads){
			if(!read){
				count++;
			}
		}

		if(count > 0){
			gobj_show.SetActive(true);
			label_count.text = count.ToString();
		}

	}

}
