using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Group : MonoBehaviour {

	public string roomID;

	public Hashtable user = new Hashtable();
	public HashSet <string> ids = new HashSet<string>();

	public static Hashtable USER;
	public static HashSet <string> IDs;

	void OnClick () {
		USER = user;
		IDs = ids;
		GroupMessage.RoomID = roomID;
		Application.LoadLevel ("GroupMessage");
	}
}
