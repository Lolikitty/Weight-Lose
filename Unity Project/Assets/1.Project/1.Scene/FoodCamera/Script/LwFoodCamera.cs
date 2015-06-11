using UnityEngine;
using System.Collections;


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
	
	void OnDisable(){
		c.Stop ();
	}


	void Photo(GameObject obj){
		Application.LoadLevel ("FoodSDLoading");
	}

	void Cancel(GameObject obj){
		Application.LoadLevel ("MainMenu");

	}




}
















