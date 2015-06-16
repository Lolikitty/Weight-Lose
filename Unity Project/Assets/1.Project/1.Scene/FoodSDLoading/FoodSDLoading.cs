using UnityEngine;
using System.Collections;
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prime31;

public class FoodSDLoading : MonoBehaviour {
	
	void Awake () {
		#if UNITY_ANDROID
		EtceteraAndroid.initTTS();
		EtceteraAndroid.promptForPictureFromAlbum( "a" );
		#endif
	}
	
	void OnEnable(){
		#if UNITY_ANDROID
		EtceteraAndroidManager.albumChooserSucceededEvent += imageLoaded;
		EtceteraAndroidManager.photoChooserSucceededEvent += imageLoaded;
		#endif
	}
	
	
	void OnDisable(){
		#if UNITY_ANDROID
		EtceteraAndroidManager.albumChooserSucceededEvent -= imageLoaded;
		EtceteraAndroidManager.photoChooserSucceededEvent -= imageLoaded;
		#endif
	}
	
	
	public void imageLoaded(string imagePath){
		StartCoroutine (imageLoaded2 (imagePath));
	}
	
	IEnumerator imageLoaded2(string imagePath){
		
		WWW w = new WWW ("file://"+imagePath);
		yield return w;


		// To Square ------------------------------------------

//		int wh = Mathf.Min (w.texture.width, w.texture.height);
//		
//		Texture2D newTexture = new Texture2D (wh, wh);
//		
//
//		for(int y = 0 ; y < 10 ; y++){
//			for(int x = 0 ; x < 10 ; x++){
//				newTexture.SetPixel(x, y, w.texture.GetPixel(x, y));
//			}
//		}
//		
//		
//		newTexture.Apply ();

//		LwFoodCamera.texture = newTexture;

//		LwFoodCamera.texture = RotateSquare(newTexture, 90);

		LwFoodCamera.texture = RotateSquare(w.texture, 90);
		
		
		Application.LoadLevel ("FoodCamera2");
	}


	Texture2D RotateSquare(Texture2D texture, float eulerAngles){
		int x;
		int y;
		int i;
		int j;
		float phi = eulerAngles/(180/Mathf.PI);
		float sn = Mathf.Sin(phi);
		float cs = Mathf.Cos(phi);
		Color32[] arr = texture.GetPixels32();
		Color32[] arr2 = new Color32[arr.Length];
		int W = texture.width;
		int H = texture.height;
		int xc = W/2;
		int yc = H/2;
		
		for (j=0; j<H; j++){
			for (i=0; i<W; i++){
				arr2[j*W+i] = new Color32(0,0,0,0);
				
				x = (int)(cs*(i-xc)+sn*(j-yc)+xc);
				y = (int)(-sn*(i-xc)+cs*(j-yc)+yc);
				
				if ((x>-1) && (x<W) &&(y>-1) && (y<H)){ 
					arr2[j*W+i]=arr[y*W+x];
				}
			}
		}

		int max = Mathf.Max (W, H);

		Texture2D newImg = new Texture2D (W, H);
		newImg.SetPixels32 (arr2);
		newImg.Apply();
		
		return newImg;
	}

}
