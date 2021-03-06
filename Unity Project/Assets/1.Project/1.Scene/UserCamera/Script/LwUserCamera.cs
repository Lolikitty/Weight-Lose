﻿using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class LwUserCamera : MonoBehaviour {

	public static bool toWeight = false;

	public UITexture CameraTextureObj;
	public GameObject buttonOk;

	public Texture2D mark;

	public GameObject sdImage_Btn;

	WebCamTexture c;

	byte [] byteImg;


	// Unity Override Methods ==============================================================================================================================

	void Awake () {

		UIEventListener.Get(buttonOk).onClick = ButtonOk;
		UIEventListener.Get(sdImage_Btn).onClick = SdImage_Btn;


		if (WebCamTexture.devices.Length == 0){
			return;
		}
		
		c = new WebCamTexture (WebCamTexture.devices[0].name);
		CameraTextureObj.mainTexture = c;
		c.Play ();
		
		CameraTextureObj.transform.localScale =
			new Vector3 (CameraTextureObj.transform.localScale.x * ((float)c.width/c.height),
			             CameraTextureObj.transform.localScale.y,
			             CameraTextureObj.transform.localScale.z);
	}

	// Custom Methods ======================================================================================================================================
	
	void ButtonOk(GameObject button){

		if (WebCamTexture.devices.Length != 0){
			Texture2D texture = new Texture2D(c.width, c.height);
			texture.SetPixels(c.GetPixels());
			texture.Apply();
			c.Stop ();

			int wh = Mathf.Min (c.width, c.height);

			Texture2D newTexture = new Texture2D (wh, wh);
			
			int start = (texture.width - wh) / 2;
			int end = texture.width - ((texture.width - wh) / 2);
			
			for(int x = start ; x < end ; x++){
				for(int y = start ; y < end ; y++){
					if(texture.width > texture.height){
						newTexture.SetPixel(x-start, y, texture.GetPixel(x, y));
					}else{
						newTexture.SetPixel(x, y-start, texture.GetPixel(x, y));
					}
				}
			}
			newTexture.Apply ();

			TextureScale.Bilinear (mark, wh, wh);

			Texture2D newTexture2 = new Texture2D (wh, wh);
			
			for(int x = 0; x < wh; x++){
				for(int y =0; y < wh; y++){
					float alpha = mark.GetPixel(x, y).a;
					if(alpha != 0){
						Color col = newTexture.GetPixel(x,y);
						newTexture2.SetPixel(x, y, new Color(col.r, col.g, col.b, alpha));
					}else{
						newTexture2.SetPixel(x, y, new Color(1, 1, 1, 0));
					}
				}
			}
			
			newTexture2.Apply ();

			string imgPath = Application.persistentDataPath + "/User.png";
			byteImg = newTexture2.EncodeToPNG();
			System.IO.File.WriteAllBytes(imgPath, byteImg);

			StartCoroutine(UploadImage());
		}
	}

	void SdImage_Btn(GameObject obj){
		Application.LoadLevel ("UserSDLoading");
	}
	
	void OnDisable(){
		c.Stop ();
	}

	IEnumerator UploadImage () {
		WWWForm wwwF = new WWWForm ();
		wwwF.AddField("id", JsonConvert.DeserializeObject<JObject> (File.ReadAllText(Application.persistentDataPath + "/User.txt"))["ID"].ToString());
		wwwF.AddBinaryData ("file", byteImg, "User.png");
		WWW www = new WWW (LwInit.HttpServerPath+"/UserImage", wwwF);
		yield return www;

		toWeight = true;
		Application.LoadLevel ("User3");
	}

}
