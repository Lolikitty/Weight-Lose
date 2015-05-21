using UnityEngine;
using System.Collections;
using System.Threading;

public class ConnectedCheck : MonoBehaviour {

	public UILabel con;
	bool connected = false;

	WWW w;
	// Use this for initialization
	void Start () {
//		w = new WWW (LwInit.HttpServerPath+"/connectedCheck.jsp");
//		Thread t = new Thread(new ThreadStart(ThreadProc));
//		t.Start ();
//		StartCoroutine (check());
//		Debug.Log ("connected = " +  connected);
//		StartCoroutine (check());
		InvokeRepeating ("Check", 0, 1);
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Check () {
//		Debug.Log ("connected = " +  connected);
//		print ("ok");
		StartCoroutine (check());
	}

//	public void ThreadProc() {
//		Debug.Log("A");
////		WWW w;
//		while(true) {
////			StartCoroutine (check());
////
////			w = new WWW (LwInit.HttpServerPath+"/connectedCheck.jsp");
////				yield return w;
//
//			if(string.IsNullOrEmpty(w.error)){
//				if(w.text == "1"){
//					connected = true;
//				}else{
//					connected = false;
//				}
//			}else{
//				connected = false;
//			}
//
//			Debug.Log("B");
//			Thread.Sleep(5000);
//		}
//	}

	IEnumerator check(){
//		Debug.Log ("connected = " +  connected);

		using( WWW w = new WWW (LwInit.HttpServerPath+"/connectedCheck.html")){
							
			yield return w;
			if(string.IsNullOrEmpty(w.error)){
				if(w.text == "1"){
					connected = true;
				}else{
					connected = false;
				}
			}else{
				connected = false;
			}
			if(connected){
				con.text = "Connected";
			}else{
				con.text = "Unconnected";
			}
//			Debug.Log ("connected = " +  connected);
	//			Thread.Sleep(5000);
		}
	}


//	IEnumerator check(){
//		while(true){
//			using( WWW w = new WWW (LwInit.HttpServerPath+"/connectedCheck.jsp")){
//				
//				yield return w;
//				
//				if(string.IsNullOrEmpty(w.error)){
//					if(w.text == "1"){
//						connected = true;
//					}else{
//						connected = false;
//					}
//				}else{
//					connected = false;
//				}
//			}
//			Thread.Sleep(5000);
//		}
//	}
}
