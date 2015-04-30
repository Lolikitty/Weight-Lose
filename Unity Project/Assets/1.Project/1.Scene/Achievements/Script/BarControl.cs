using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class BarControl : MonoBehaviour {

	public GameObject[] BlueMark;
	public GameObject[] RedMark;
	public GameObject[] GrayMark;
	public UITexture Feather; //獎盃
	public Texture2D [] Img; //圖片
	public UITexture Lv; //現在等級
	public Texture2D[] LvLabel; //等級標籤
	public UILabel TitleLabel;
	public UILabel TitleContentLabel;
	public UILabel ShowLvUp;

	public string TitleStr;
	public string TitleContentStr;
	//public UITexture [] Target; // mark　項目
	//int []TargetState = new int [7];//[項目][星期]的狀態值，0=時間未到，1=達成，2=失敗
	//int Week = 7;
	//int TargetGrade; //分數

//	public int[] Value;
	// Use this for initialization
	void Start () {
		//重置
		//UpdateBar (Value,TitleStr,TitleContentStr);
	}
	//LvUp↑　+300E
	public void UpdateBar(int []MarkState,string Title,string TilteContent,int NowLv,bool IsLvUpdate,int ExtraGrade){
		int Week = 7;
		int TargetGrade ;

		TitleLabel.text = Title;
		TitleContentLabel.text = TilteContent;
		//reset
	
		foreach(GameObject MarkTemp in BlueMark){
			MarkTemp.SetActive(false);
		}
		foreach(GameObject MarkTemp in RedMark){
			MarkTemp.SetActive(false);
		}
		foreach(GameObject MarkTemp in GrayMark){
			MarkTemp.SetActive(true);
		}

//		TargetGrade = 0; //重置計分
//		Feather.mainTexture = Img[NowLv-1];
//		Lv.mainTexture = LvLabel[NowLv-1];


		//process
		for (int i=0; i<Week; i++) { //更新一個禮拜的Mark，和計算對應的分數
			//Debug.Log ("i = " + i +"  MarkState = " + MarkState [i] );
			switch (MarkState [i]) {
			/*
			case 0:
				BlueMark[i].SetActive(false);					
				RedMark[i].SetActive(false);
				GrayMark[i].SetActive(true);
				break;
			*/
			case 1:
//				TargetGrade += 1; //累加
				BlueMark[i].SetActive(true);					
				//RedMark[i].SetActive(false);
				//GrayMark[i].SetActive(false);
				break;
			case 2:
				//BlueMark[i].SetActive(false);					
				RedMark[i].SetActive(true);
				//GrayMark[i].SetActive(false);
				break;
			}
		}

		if (IsLvUpdate) {
			ShowLvUp.gameObject.SetActive(true);
			string str = "LvUp↑　+" + ExtraGrade.ToString () + "E";	//LvUp↑　+300E 
			ShowLvUp.text = str;
		} else {
			ShowLvUp.gameObject.SetActive(false);
		}
		
		//更新獎盃圖片 //.GetComponent<UITexture>()
		Feather.mainTexture = Img[NowLv-1];
		Lv.mainTexture = LvLabel[NowLv-1];
	}

	// Update is called once per frame
	void Update () {
//		UpdateBar (Value,TitleStr,TitleContentStr);
	}
}
