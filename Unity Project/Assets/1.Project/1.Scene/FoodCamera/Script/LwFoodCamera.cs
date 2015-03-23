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

	public static bool isSDImage = false;
	
	WebCamTexture c;

	void Awake () {

		UIEventListener.Get(buttonChooseFood).onClick = ButtonChooseFood;
		UIEventListener.Get(buttonShutter).onClick = ButtonShutter;
		UIEventListener.Get(photo).onClick = Photo;
		UIEventListener.Get(cancel).onClick = Cancel;
		#if UNITY_ANDROID
		EtceteraAndroid.initTTS();
		#endif

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

		isSDImage = false;


		Application.LoadLevel ("FoodCamera2");
	}

	void OnEnable(){
		#if UNITY_ANDROID
		// Listen to the texture loaded methods so we can load up the image on our plane
		EtceteraAndroidManager.albumChooserSucceededEvent += imageLoaded;
		EtceteraAndroidManager.photoChooserSucceededEvent += imageLoaded;
		#endif
	}
	
	
	void OnDisable(){
		#if UNITY_ANDROID
		EtceteraAndroidManager.albumChooserSucceededEvent -= imageLoaded;
		EtceteraAndroidManager.photoChooserSucceededEvent -= imageLoaded;
		#endif
		if(c != null){
			c.Stop();
		}
	}

	public void imageLoaded(string imagePath){
		#if UNITY_ANDROID
		// 後面的 1f 代表解析度的意思，1 為最大
		EtceteraAndroid.scaleImageAtPath( imagePath, 1f );
		texture = EtceteraAndroid.textureFromFileAtPath( imagePath );
		#endif
		isSDImage = true;
		Application.LoadLevel ("FoodCamera2");
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
















