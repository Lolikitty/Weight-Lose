using UnityEngine;
using System.Collections;

public class MyID : MonoBehaviour {

	string msg = "";

	// Unity Override Methods ==============================================================================================================================

	IEnumerator Start () {
		var form = new WWWForm();
		form.AddField( "id", "2" );
		var download = new WWW( "http://54.191.103.38:1234/user/info", form );
		yield return download;
		if(download.error!=null) {
			print( "Error downloading: " + download.error );
		} else {
			msg = download.text;
		}
	}

	void OnGUI () {
		GUILayout.Label ("MSG :" + msg);
	}

	// Custom Methods ======================================================================================================================================

}
