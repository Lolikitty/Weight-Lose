using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LwTalk_User : MonoBehaviour {

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

		try{
			isChoose = !isChoose;

			if(isChoose){
				chooseTexture.mainTexture = chooseV;
				ChooseIDs.Add(friendID);
			}else{
				chooseTexture.mainTexture = chooseX;
				ChooseIDs.Remove(friendID);
			}
		}catch(Exception e){
			LwError.Show("LwTalk_User.ButtonChoose() : " + e);
		}
	}

	void Update(){
		if(LwTalk.isDoNotViewAddBG){
			isChoose = false;
			chooseTexture.mainTexture = chooseX;
		}
	}

}



