using UnityEngine;
using System.Collections;
using Prime31;


public class LwFoodCamera : MonoBehaviour {


	public GameObject DisplayTextureObject;

	public GameObject buttonChooseFood;
	public GameObject buttonShutter;

	public GameObject photo;
	public GameObject cancel;

	public static Texture2D texture;
	public UITexture search;
	
	WebCamTexture c;

	void Awake () {
		UIEventListener.Get(buttonChooseFood).onClick = ButtonChooseFood;
		UIEventListener.Get(buttonShutter).onClick = ButtonShutter;
		UIEventListener.Get(photo).onClick = Photo;
		UIEventListener.Get(cancel).onClick = Cancel;

		EtceteraAndroid.initTTS();


//		print (Application.persistentDataPath);
	}

	void Start () {

		if (WebCamTexture.devices.Length == 0){
			return;
		}

		c = new WebCamTexture (WebCamTexture.devices[0].name);
		DisplayTextureObject.renderer.material.mainTexture = c;
		c.Play ();

		DisplayTextureObject.transform.localScale =
			new Vector3 (DisplayTextureObject.transform.localScale.x,
			             DisplayTextureObject.transform.localScale.y * ((float)c.height/c.width),
			             DisplayTextureObject.transform.localScale.z);

		print ("Camera Resolution Is : " + c.width + " X " + c.height);
	}
		
	void ButtonChooseFood(GameObject button){

		if (WebCamTexture.devices.Length != 0){
			c.Stop ();
		}
		Application.LoadLevel ("FoodChoose");
	}

	void ButtonShutter(GameObject button){

		if (WebCamTexture.devices.Length != 0){
			texture = new Texture2D(c.width, c.height);
			texture.SetPixels(c.GetPixels());
			texture.Apply();
			c.Stop ();
		}




		Application.LoadLevel ("FoodCamera2");
	}

	void OnEnable(){
		// Listen to the texture loaded methods so we can load up the image on our plane
		EtceteraAndroidManager.albumChooserSucceededEvent += imageLoaded;
		EtceteraAndroidManager.photoChooserSucceededEvent += imageLoaded;
	}
	
	
	void OnDisable(){
		EtceteraAndroidManager.albumChooserSucceededEvent -= imageLoaded;
		EtceteraAndroidManager.photoChooserSucceededEvent -= imageLoaded;
	}

	public void imageLoaded(string imagePath){
		// 後面的 1f 代表解析度的意思，1 為最大
		EtceteraAndroid.scaleImageAtPath( imagePath, 1f );
		search.mainTexture = EtceteraAndroid.textureFromFileAtPath( imagePath );
	}

//	Texture2D TextureScale(Texture2D, int width, int height){
//		Texture2D nt = new Texture2D(width, height);
//
//		return nt;
//	} 

	void Photo(GameObject obj){
		#if UNITY_ANDROID
			EtceteraAndroid.promptForPictureFromAlbum( "a" );
		#endif
	}

	void Cancel(GameObject obj){

		Application.LoadLevel ("MainMenu");

	}




}
















