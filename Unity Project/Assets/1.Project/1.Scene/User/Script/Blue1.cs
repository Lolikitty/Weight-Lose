using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class Blue1 : MonoBehaviour {
	
	//red
	public GameObject lineGraph;
	public GameObject backGround;
	public GameObject series2;
	public GameObject YAxisMarks;
	
	//barGraph 的 屬性 //預設值
	public float xAxisMaxValue = 0;//100.0f; 
	public float xAxisMinValue = 0f;
	public int xAxisNumTicks = 11;	//切割n份，要n+1
	public float xAxisLength = 250.0f;	//x軸實際長度
	
	public float yAxisMaxValue = 70.0f; 
	public float yAxisMinValue = 30.0f;
	public int yAxisNumTicks = 9;
	public float yAxisLength = 320.0f;
	
	//public List<string> xAxisLabels; // = xAxisNumTicks-1 ，兩個要對等
	//public List<string> yAxisLabels;
	//public List<Vector2> sV1; //Vector 數目對應 int，長條的數據
	List<Vector2> sV2; //Vector 數目對應 int，長條的數據

	string[] str;
	//9
	float[] v2 ;// = new float[]{30.0f,35.0f,40.0f,45.0f,50.0f,55.0f,60.0f,65.0f,70.0f};
	
	// Use this for initialization
	void Start () {

		//從SD卡取出體重
//		float [] w = new float[10];
//		
//		for(int i = 0; i < w.Length; i++){
//			w [i] = PlayerPrefs.GetFloat("Weight" + i);
//			Debug.Log ("w [i] = " + w [i]);
//		}
//		v2 = w;


//		Debug.Log ("xAxisNumTicks - 1 = " + (xAxisNumTicks - 1).ToString());

//		v2 = new float[xAxisNumTicks - 1];
//		for (int i=0; i < xAxisNumTicks - 1; i++) {
//			v2[i] = 0 ;
//		}


		/***********************/
//		Set (DateTime.Now);


//		Debug.Log ("yAxisMaxValue = " + yAxisMaxValue);
//		Debug.Log ("yAxisMinValue = " + yAxisMinValue);

		lineGraph.transform.localScale = new Vector2(2.0f,2.0f);
		lineGraph.transform.localPosition = new Vector2 (-250.0f,-380.0f);

		//Instantiate (yLine, new Vector3 (10.0f, 0, 0), Quaternion.identity);// as Transform;   //
		//對x軸設定
		lineGraph.GetComponent<WMG_Axis_Graph>().xAxisMaxValue = xAxisMaxValue; 
		lineGraph.GetComponent<WMG_Axis_Graph>().xAxisMinValue = xAxisMinValue;
		lineGraph.GetComponent<WMG_Axis_Graph>().xAxisNumTicks = xAxisNumTicks;	//
		lineGraph.GetComponent<WMG_Axis_Graph>().xAxisLength = xAxisLength;	//x軸實際長度
		//lineGraph.GetComponent<WMG_Axis_Graph>().xAxisLabels = xAxisLabels; // = xAxisNumTicks ，兩個要對等
		
		//對y軸設定
		lineGraph.GetComponent<WMG_Axis_Graph>().yAxisMaxValue = yAxisMaxValue; 
		lineGraph.GetComponent<WMG_Axis_Graph>().yAxisMinValue = yAxisMinValue;

//		lineGraph.GetComponent<WMG_Axis_Graph>().yAxisMaxValue = w.Max() + 2.0f; //設定y軸 最大值
//		lineGraph.GetComponent<WMG_Axis_Graph>().yAxisMinValue = w.Min() - 2.0f; //設定y軸 最小值

		lineGraph.GetComponent<WMG_Axis_Graph>().yAxisNumTicks = yAxisNumTicks;	
		lineGraph.GetComponent<WMG_Axis_Graph>().yAxisLength = yAxisLength;	
		//lineGraph.GetComponent<WMG_Axis_Graph>().yAxisLabels = yAxisLabels; 

		Set (DateTime.Now);


//		for (int i = 0; i < xAxisNumTicks-1; i++) {
//			lineGraph.GetComponent<WMG_Axis_Graph> ().xAxisLabels [i] = str [i];
//		}
		//設定x軸長條的數據
		//xAxisNumTicks-1
		//print (xAxisNumTicks);
		sV2 = new List<Vector2>();
		for (int i=0; i < xAxisNumTicks-1; i++) {
			//sV1.Add(new Vector2(0,v1[i]));	
			sV2.Add(new Vector2(0,v2[i]));
		}
//		Debug.Log (sV2.Count);
		//series.GetComponent<WMG_Series> ().pointValues = sV1; //預設只有一種長條
		series2.GetComponent<WMG_Series> ().pointValues = sV2; //預設只有一種長條
	}
	void Update(){
		YAxisMarks.transform.localPosition = new Vector2 (280.0f,0); //.position = new Vector3(315.0f,100.0f,0);
		//YAxisMarks.GetComponent<WMG_Grid> ().noHorizontalLinks = true;
	}

	void Set(DateTime day){
		SaveWeight w = new SaveWeight ();
		v2 = new float[10];

		float lastWeightTmp = 0;
		float wei = 0;
		int i = 1;
		int j = 0;
		int count = 0;
		for (DateTime d = day.AddDays(-69); d.Date <= day.Date; d = d.AddDays(1),i++) {

			float everyDayWeight = w.GetDayWeight(d);
			if(everyDayWeight == 0){
				everyDayWeight = w.GetLastDayWeight(d);
			}
			if(everyDayWeight != 0){
				count ++;
			}
			wei += everyDayWeight;
			if(count != 0){
				lastWeightTmp = wei/count;
			}
			if(i%7==0){
				v2[j] = lastWeightTmp;
				count = 0;
				wei = 0;
				j++;
			}
			//
//			float everyDayWeight = w.GetDayWeight(d);
//			if(everyDayWeight == 0){
//				everyDayWeight = w.GetLastDayWeight(d);
//			}
//			wei += everyDayWeight;
//
//			if(i%7 == 0){
//				lastWeightTmp = wei/7;
//			}else{
//				lastWeightTmp = wei/(i%7);
//			}
//
//			if(i%7==0){
//
//				v2[j] = lastWeightTmp;
////				Debug.Log("lastWeightTmp = " + lastWeightTmp);
////				Debug.Log("wei = " + wei);
//				wei = 0;
//				j++;
//			}
		}

//		for(int k = 0 ;k < 10;k++){
//			Debug.Log ("v2[k] = " + v2[k]);
//		}

		float min = 0;
//		if (v2.Min () != null) {
//			min = v2.Min ();
//		}
//		min -= 2.0f;
//		if (min <= 0) {
//			min = 0;
//		}
		
		float max = 0;
		if (v2.Max () != null) {
			max = v2.Max ();
		}
		max += 5.0f;
		
		
		
		yAxisMaxValue = max;
		yAxisMinValue = min;


		lineGraph.GetComponent<WMG_Axis_Graph>().yAxisMaxValue = yAxisMaxValue; //設定y軸 最大值
		lineGraph.GetComponent<WMG_Axis_Graph>().yAxisMinValue = yAxisMinValue; //設定y軸 最小值
	}
}
