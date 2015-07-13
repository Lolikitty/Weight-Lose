using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LwFriend_User : MonoBehaviour {

	public LwFriend friend;
	public UITexture head;
	public UILabel name;
	public string friendID;
	public UITexture rankUITexture;
	public UILabel rankDay;
	public GameObject buttonChoose;
	public GameObject buttonInfo;
	public GameObject dragObj;
	public GameObject buttonDelete;
	public GameObject food;
	public Transform foodRoot;

	public GameObject userInfo;
	public Transform userInfoFoodRoot;
	public GameObject userInfoFood;
	public GameObject readObj;

	// Update is called once per frame
	void Awake () {
		UIEventListener.Get(buttonChoose).onClick = ButtonChoose;
		UIEventListener.Get(buttonInfo).onClick = ButtonInfo;
		UIEventListener.Get(dragObj).onDrag = DragObj;
		UIEventListener.Get(buttonDelete).onClick = ButtonDelete;

		buttonDelete.SetActive (false);
		readObj.SetActive(false);
	}

	bool isChoose = false;

	public static HashSet<string> ChooseIDs = new HashSet<string>();

	void ButtonChoose(GameObject button){

		try{
			isChoose = !isChoose;
			LwMessage.FID = friendID;
			LwMessage.FNAME = name.text;
			Application.LoadLevel ("Message");
		}catch(Exception e){
			LwError.Show("LwFriend_User.ButtonChoose() : " + e);
		}
	}

	void ButtonDelete(GameObject button){
		StartCoroutine (DeleteFriend());
	}

	void ButtonInfo(GameObject button){

		userInfo.GetComponent<UserInfo> ().id.text = friendID;
		userInfo.GetComponent<UserInfo> ().name.text = name.text;
		userInfo.GetComponent<UserInfo> ().head.mainTexture = head.mainTexture;

		userInfo.SetActive (true);

		int x = -110;

		foreach(Texture2D t in foods){
			GameObject g = Instantiate(userInfoFood) as GameObject;
			g.GetComponent<UITexture>().mainTexture = t;
			g.transform.parent = userInfoFoodRoot;
			g.transform.localScale = Vector3.one;
			g.transform.localPosition = new Vector3(x, 50);
			x += 110;
		}
		userInfoFoodRoot.GetComponent<UIScrollView> ().OnScrollBar ();
	}

	void DragObj(GameObject obj, Vector2 delta){
		if(delta.x < -30){
			buttonDelete.SetActive (true);
			transform.localPosition = new Vector3 (-100, transform.localPosition.y);
		}else if(delta.x > 30){
			buttonDelete.SetActive (false);
			transform.localPosition = new Vector3 (0, transform.localPosition.y);
		}
	}

	IEnumerator DeleteFriend(){
		WWWForm f = new WWWForm ();
		f.AddField("id", JsonConvert.DeserializeObject<JObject> (File.ReadAllText(Application.persistentDataPath + "/User.txt"))["ID"].ToString());
		f.AddField("friend_id", friendID);
		WWW w = new WWW (LwInit.HttpServerPath + "/DeleteFriend", f);
		yield return w;
		Application.LoadLevel ("Friend");
	}

	List<Texture2D> foods = new List<Texture2D>();

	string json;

	IEnumerator Start(){
		WWW w = new WWW (LwInit.HttpServerPath + "/ServerData/"+friendID+"/Food.jsp");
		yield return w;
		if(string.IsNullOrEmpty(w.error)){
			json = w.text;
			JArray ja = JsonConvert.DeserializeObject<JObject> (json) ["Food"] as JArray;
			w.Dispose ();
			for(int i = 0, x = -128; i < ja.Count; i++){
				DateTime dt = (DateTime) ja[i]["Date"];
				if(dt.Date == DateTime.Today){
					string Name = ja[i]["Name"].ToString();
					string Kal = ja[i]["Kal"].ToString();
					string JPGPath = ja[i]["JPGPath"].ToString();
					string PNGPath = ja[i]["PNGPath"].ToString();

					string JPGName = JPGPath.Split('/')[3];
					string PNGName = PNGPath.Split('/')[3];

					w = new WWW (LwInit.HttpServerPath + "/ServerData/"+friendID+"/Food/"+PNGName);
					yield return w;

					if(string.IsNullOrEmpty(w.error)){
						GameObject g = Instantiate (food) as GameObject;
						g.transform.parent = foodRoot;
						g.transform.localScale = Vector3.one;
						g.transform.localPosition = new Vector3 (x, 0);
						g.GetComponent<UITexture>().mainTexture = w.texture;
						foods.Add(w.texture);
						w.Dispose ();
						
						x += 40;
					}
				}
			}
			foodRoot.GetComponent<UIScrollView> ().OnScrollBar ();
		}
		w.Dispose ();


		WWWForm f2 = new WWWForm ();
		f2.AddField ("id", LwFriend.ID);
		f2.AddField ("friend_id", friendID);
		
		using (WWW ww = new WWW (LwInit.HttpServerPath+"/GetReadMessage", f2)) {
			yield return ww;
			if(!bool.Parse(ww.text)){
				readObj.SetActive(true);
			}
		}
	}

	void Update(){
		if(LwTalk.isDoNotViewAddBG){
			isChoose = false;
		}
	}

}



