using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LwFriend_User : MonoBehaviour {

	public UITexture head;
	public UILabel name;
	public string friendID;
	public UITexture rankUITexture;
	public UILabel rankDay;
	public GameObject buttonChoose;
	public UITexture chooseTexture;
	public Texture2D chooseV, chooseX;
	
	// Update is called once per frame
	void Awake () {
		UIEventListener.Get(buttonChoose).onClick = ButtonChoose;
	}

	bool isChoose = false;

	public static HashSet<string> ChooseIDs = new HashSet<string>();

	void ButtonChoose(GameObject button){
		isChoose = !isChoose;

		if(isChoose){
			chooseTexture.mainTexture = chooseV;
			ChooseIDs.Add(friendID);
		}else{
			chooseTexture.mainTexture = chooseX;
			ChooseIDs.Remove(friendID);
		}

//		LwMessage.FID = friendID;
//		LwMessage.FNAME = name.text;
//		Application.LoadLevel ("Message");
	}

	void Update(){
		if(LwTalk.isDoNotViewAddBG){
			isChoose = false;
			chooseTexture.mainTexture = chooseX;
		}
	}

}



