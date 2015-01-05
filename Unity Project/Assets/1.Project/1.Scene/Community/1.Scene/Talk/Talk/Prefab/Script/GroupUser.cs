using UnityEngine;
using System.Collections;

public class GroupUser : MonoBehaviour {
	public UITexture img;
	public UILabel userName;

	public void DownloadImg(string id){
		StartCoroutine (DownloadImg2(id));
	}

	IEnumerator DownloadImg2(string id){
		WWW w = new WWW (LwInit.HttpServerPath+"/ServerData/"+id+"/User.png");
		yield return w;
		if(w.error== null){
			img.mainTexture = w.texture;
		}
	}
}
