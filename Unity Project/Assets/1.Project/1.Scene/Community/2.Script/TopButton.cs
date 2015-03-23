using UnityEngine;
using System.Collections;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class TopButton : MonoBehaviour {

	public GameObject countFriendTalk;
	public UILabel countFriendTalkNumber;

	public GameObject countGroupTalk;
	public UILabel countGroupTalkNumber;

	public GameObject countAddFriend;
	public UILabel countAddFriendNumber;

	// Unity Override Methods ==============================================================================================================================

	void Awake () {
		countFriendTalk.SetActive (false);
		countGroupTalk.SetActive (false);
		countAddFriend.SetActive (false);
	}

	IEnumerator Start(){
		WWWForm wwwF = new WWWForm();
		wwwF.AddField("id", JsonConvert.DeserializeObject<JObject> (File.ReadAllText(Application.persistentDataPath + "/User.txt"))["ID"].ToString());
		WWW www = new WWW(LwInit.HttpServerPath+"/GetWaitFriend", wwwF);
		yield return www;
		if(www.text != ""){
			countAddFriend.SetActive (true);
			string [] unit = www.text.Split(';');
			countAddFriendNumber.text = (unit.Length-1).ToString();			
		}
		www.Dispose ();
		//--------------------------------------------

	}

	// Custom Methods ======================================================================================================================================


}
