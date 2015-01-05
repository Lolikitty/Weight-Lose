using UnityEngine;
using System.Collections;

public class TopButton : MonoBehaviour {

	public GameObject exit;
	public GameObject friend;
	public GameObject talk;
	public GameObject addFriend;

	// Unity Override Methods ==============================================================================================================================

	void Awake () {
		UIEventListener.Get(exit).onClick = Exit;
		UIEventListener.Get(friend).onClick = Friend;
		UIEventListener.Get(talk).onClick = Talk;
		UIEventListener.Get(addFriend).onClick = AddFriend;
	}

	// Custom Methods ======================================================================================================================================

	void Exit(GameObject button){
		Application.LoadLevel ("MainMenu");
	}

	void Friend(GameObject button){
		Application.LoadLevel ("Friend");
	}

	void Talk(GameObject button){
		Application.LoadLevel ("Talk");
	}

	void AddFriend(GameObject button){
		Application.LoadLevel ("AddFriend");
	}
}
