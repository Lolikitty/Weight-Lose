using UnityEngine;
using System.Collections;

public class LwUpdateImg : MonoBehaviour {
	public Texture2D [] Img; //圖片
	public UITexture [] Target; // mark　項目
	int [,]TargetState = new int [5,7];//[項目][星期]的狀態值，0=時間未到，1=達成，2=失敗
	int Week = 7;
	int[] TargetGrade;
	// Use this for initialization
	void Start () {
		//GetComponent<UITexture>().mainTexture = img[0];
	}

	// Update is called once per frame
	void Update () {

	
		for (int i=0; i<Target.Length; i++) { //重置 TargetGrade
			TargetGrade[i] = 0;
		}

		for (int i=0; i<Target.Length; i++) { //計算每個項目的分數
			for (int j=0; j<Week; j++) {
					switch (TargetState [i,j]) { //判斷mark的狀態值
						case 1:
							TargetGrade [i] += 1; //累加
							
							break;
					}
			}
		}

		for (int i=0; i<Target.Length; i++) { //更新圖片
			Target[i].mainTexture = Img[TargetGrade [i]];
	    }
		/*
		//更新
		for (int i=0; i<BlueMark.Length; i++) {
			BlueMark[i].SetActive((State==1)?true:false);				
		}*/
	}
}
