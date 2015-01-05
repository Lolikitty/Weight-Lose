using UnityEngine;
using System.Collections;
using Vectrosity;
using System.Security.Policy;

public class NewBehaviourScript : MonoBehaviour {

	public VectorLine l1;
	public Texture t1, t2;
	public float lightSize = 3;
	public int imageSize = 30;
	public int offsetX, offsetY;	
	public bool isUserPage = false;

	float MoveX = Screen.width / 11.0f;
	float MoveY = Screen.height / 5.0f;
	float MoveW = Screen.width / 615.0f;
	float MoveH = Screen.height / 12600.0f;
	Vector2 [] lp1, lp2;

	// Unity Override Methods ==============================================================================================================================

	void Start () {

		float [] w = new float[10];

		for(int i = 0; i < w.Length; i++){
			w [i] = PlayerPrefs.GetFloat("Weight" + i);
		}


		lp1 = Drow (w[0], w[1], w[2], w[3], w[4], w[5], w[6], w[7], w[8],w[9]);
//		lp2 = Drow (30,30,30,30,30,30,30,30,30,30);

		GameObject.Find ("VectorCam").GetComponent<Camera>().enabled = true;
	}

	// Custom Methods ======================================================================================================================================
		
	public Vector2 [] Drow (float point1,float point2,float point3,float point4,float point5,float point6,float point7,float point8,float point9,float point10) {
		
		Vector2 [] lp = new Vector2 [18];


		point1 = (point1 - 30) * 57.5f;
		point2 = (point2 - 30) * 57.5f;
		point3 = (point3 - 30) * 57.5f;
		point4 = (point4 - 30) * 57.5f;
		point5 = (point5 - 30) * 57.5f;
		point6 = (point6 - 30) * 57.5f;
		point7 = (point7 - 30) * 57.5f;
		point8 = (point8 - 30) * 57.5f;
		point9 = (point9 - 30) * 57.5f;
		point10 = (point10 - 30) * 57.5f;

		
		lp[0] = new Vector2 (MoveX + 0, MoveY + (MoveH * point1));
		lp[1] = new  Vector2 (MoveX + (50 * MoveW), MoveY + (MoveH * point2));
		lp[3] = new  Vector2 (MoveX + (100 * MoveW), MoveY + (MoveH * point3));
		lp[5] = new  Vector2 (MoveX + (150 * MoveW), MoveY + (MoveH * point4));
		lp[7] = new  Vector2 (MoveX + (200 * MoveW), MoveY + (MoveH * point5));
		lp[9] = new  Vector2 (MoveX + (250 * MoveW), MoveY + (MoveH * point6));
		lp[11] = new  Vector2 (MoveX + (300 * MoveW), MoveY + (MoveH * point7));
		lp[13] = new  Vector2 (MoveX + (350 * MoveW), MoveY + (MoveH * point8));
		lp[15] = new  Vector2 (MoveX + (400 * MoveW), MoveY + (MoveH * point9));
		lp[17] = new  Vector2 (MoveX + (450 * MoveW), MoveY + (MoveH * point10));
		
		lp[2] = lp[1];
		lp[4] = lp[3];
		lp[6] = lp[5];
		lp[8] = lp[7];
		lp[10] = lp[9];
		lp[12] = lp[11];
		lp[14] = lp[13];
		lp[16] = lp[15];


		l1 = new VectorLine("Line", lp, null, lightSize);
		l1.Draw();
		
		l1.active = true;
		return lp;
	}

	void OnGUI(){
		if(lp1 == null || lp2== null){
			return;
		}
		for(int i = 0; i < lp1.Length; i+=2){
			GUI.Label (new Rect (lp1[i].x + offsetX, Screen.height - lp1[i].y + offsetY, imageSize,imageSize), t1);
			GUI.Label (new Rect (lp2[i].x + offsetX, Screen.height - lp2[i].y + offsetY, imageSize,imageSize), t2);
		}
		GUI.Label (new Rect (lp1[17].x + offsetX, Screen.height - lp1[17].y + offsetY, imageSize,imageSize), t1);
		GUI.Label (new Rect (lp2[17].x + offsetX, Screen.height - lp2[17].y + offsetY, imageSize,imageSize), t2);
	}

}
