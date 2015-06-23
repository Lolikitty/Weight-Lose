using UnityEngine;
using System.Collections;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AndroidMediaBrowser;

public class UserSDLoading : MonoBehaviour {

	public Texture2D mark;

	byte [] byteImg;

	void Awake () {
		#if UNITY_ANDROID
		ImageBrowser.OnPicked += (image) => {
			StartCoroutine (imageLoaded2 (image.Path));
		};

		ImageBrowser.OnPickCanceled += () => {
			Application.LoadLevel ("User3");
		};

		ImageBrowser.Pick();

		#endif
	}
	
	IEnumerator imageLoaded2(string imagePath){
		
		WWW w = new WWW ("file://"+imagePath);
		yield return w;
		
		Texture2D texture = w.texture;
		
		
		// To Square ------------------------------------------
		
		int wh = Mathf.Min (texture.width, texture.height);
		
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
		
		// To Alpha -----------------------------------------
		
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
		
		// To Rotation -----------------------------------------
		
		Texture2D newTexture3 = RotateSquare (newTexture2, 90);
		
		
		// Delete Temp Texture -------------------------------

		Destroy (texture);
		Destroy (newTexture);
		Destroy (newTexture2);
		
		// To Save ------------------------------------------
		
		string imgPath = Application.persistentDataPath + "/User.png";
		byteImg = newTexture3.EncodeToPNG();
		System.IO.File.WriteAllBytes(imgPath, byteImg);
		
		StartCoroutine(UploadImage());
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
		
		Texture2D newImg = new Texture2D (W, H);
		newImg.SetPixels32 (arr2);
		newImg.Apply();
		
		return newImg;
	}

	IEnumerator UploadImage () {
		WWWForm wwwF = new WWWForm ();
		wwwF.AddField("id", JsonConvert.DeserializeObject<JObject> (File.ReadAllText(Application.persistentDataPath + "/User.txt"))["ID"].ToString());
		wwwF.AddBinaryData ("file", byteImg, "User.png");
		WWW www = new WWW (LwInit.HttpServerPath+"/UserImage", wwwF);
		yield return www;

		LwUserCamera.toWeight = true;
		Application.LoadLevel ("User3");
	}

}
