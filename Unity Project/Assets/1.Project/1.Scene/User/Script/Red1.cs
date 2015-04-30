using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class Red1 : MonoBehaviour {
	
	//red
	public GameObject lineGraph;
	public GameObject backGround;
	public GameObject series1;
	
	//barGraph 的 屬性 //預設值
	public float xAxisMaxValue = 100.0f; 
	public float xAxisMinValue = 0f;
	public int xAxisNumTicks = 11;	//切割n份，要n+1
	public float xAxisLength = 250.0f;	//x軸實際長度
	
	public float yAxisMaxValue = 30.0f; 
	public float yAxisMinValue = 22.0f;
	public int yAxisNumTicks = 9;
	public float yAxisLength = 320.0f;
	
	//public List<string> xAxisLabels; // = xAxisNumTicks-1 ，兩個要對等
	//public List<string> yAxisLabels;
	List<Vector2> sV1; //Vector 數目對應 int，長條的數據
	//public List<Vector2> sV2; //Vector 數目對應 int，長條的數據
	
	//9
	float[] v1 ;//= new float[]{29.0f,27.0f,26.0f,25.0f,25.0f,25.0f,26.0f,23.0f,24.0f,26.0f};
	
	// Use this for initialization
	void Start () {
		//從SD卡取出體重
		/*
		float [] w = new float[10];
		
		for(int i = 0; i < w.Length; i++){
			w [i] = PlayerPrefs.GetFloat("Weight" + i);
		}
		v1 = w;
		*/

		lineGraph.transform.localScale = new Vector2(2.0f,2.0f);
		lineGraph.transform.localPosition = new Vector2(-250.0f,-380.0f);

		//對x軸設定
		lineGraph.GetComponent<WMG_Axis_Graph>().xAxisMaxValue = xAxisMaxValue; 
		lineGraph.GetComponent<WMG_Axis_Graph>().xAxisMinValue = xAxisMinValue;
		lineGraph.GetComponent<WMG_Axis_Graph>().xAxisNumTicks = xAxisNumTicks;	//
		lineGraph.GetComponent<WMG_Axis_Graph>().xAxisLength = xAxisLength;	//x軸實際長度
		//lineGraph.GetComponent<WMG_Axis_Graph>().xAxisLabels = xAxisLabels; // = xAxisNumTicks ，兩個要對等
		
		//對y軸設定
		lineGraph.GetComponent<WMG_Axis_Graph>().yAxisMaxValue = yAxisMaxValue; 
		lineGraph.GetComponent<WMG_Axis_Graph>().yAxisMinValue = yAxisMinValue;
		//lineGraph.GetComponent<WMG_Axis_Graph>().yAxisMaxValue = w.Max() + 2.0f; //設定y軸 最大值
		//lineGraph.GetComponent<WMG_Axis_Graph>().yAxisMinValue = w.Min() - 2.0f; //設定y軸 最小值
		lineGraph.GetComponent<WMG_Axis_Graph>().yAxisNumTicks = yAxisNumTicks;	
		lineGraph.GetComponent<WMG_Axis_Graph>().yAxisLength = yAxisLength;	
		//lineGraph.GetComponent<WMG_Axis_Graph>().yAxisLabels = yAxisLabels; 


		Set (DateTime.Now);

		//設定x軸長條的數據
		sV1 = new List<Vector2>();
		for (int i=0; i < xAxisNumTicks-1; i++) {
			sV1.Add(new Vector2(0,v1[i]));	
		}
		
		series1.GetComponent<WMG_Series> ().pointValues = sV1; //預設只有一種長條
		//series2.GetComponent<WMG_Series> ().pointValues = sV2; //預設只有一種長條
	}

	void Set(DateTime day){
//		lineGraph.GetComponent<WMG_Axis_Graph> ().xAxisLabelSize = xAxisNumTicks - 1;
		BodyFat bf = new BodyFat(); //***

		v1 = new float[10];
	
		float lastHwTmp = 0;
		float hw = 0;
		int i = 1;
		int j = 0;
		int count = 0;
		for (DateTime d = day.AddDays(-69); d.Date <= day.Date; d = d.AddDays(1),i++) {
		
			float everyDayBodyFat = bf.GetDayBodyFat(d); 

			hw += everyDayBodyFat;

			if(everyDayBodyFat != 0){
				count ++;
			}

			if(count != 0){
				lastHwTmp = hw/count;
			}

			if(i%7==0){

				if(d.Date == day.Date || d.Date == day.AddDays(-63).Date){
					lineGraph.GetComponent<WMG_Axis_Graph> ().xAxisLabels[j] = d.Month.ToString()+ "/" + d.Day.ToString();
				}
				else{
					lineGraph.GetComponent<WMG_Axis_Graph> ().xAxisLabels[j] = d.Day.ToString();
				}

				v1[j] = lastHwTmp;
				count = 0;
				hw = 0;
				j++;
			}
			//

//			float everyDayBodyFat = bf.GetDayBodyFat(d); //***
//
//			hw += everyDayBodyFat;
//
//			if(i%7 == 0){
//				lastHwTmp = hw/7;
//			}else{
//				lastHwTmp = hw/(i%7);
//			}
//			if(i%7==0){
////				Debug.Log("j = " + j);
//				if(d.Date == day.Date || d.Date == day.AddDays(-63).Date){
//					lineGraph.GetComponent<WMG_Axis_Graph> ().xAxisLabels[j] = d.Month.ToString()+ "/" + d.Day.ToString();
//				}
//				else{
//					lineGraph.GetComponent<WMG_Axis_Graph> ().xAxisLabels[j] = d.Day.ToString();
//				}
//
//				v1[j] = lastHwTmp;
//				hw = 0;
//				j++;
//			}
		}


		float min = 0;
		if (v1.Min () != null) {
			min = v1.Min ();
		}
		min -= 2.0f;
		if (min <= 0) {
			min = 0;
		}

		float max = 0;
		if (v1.Max () != null) {
			max = v1.Max ();
		}
		max += 2.0f;



		yAxisMaxValue = max;
		yAxisMinValue = min;

		lineGraph.GetComponent<WMG_Axis_Graph>().yAxisMaxValue = yAxisMaxValue; 
		lineGraph.GetComponent<WMG_Axis_Graph>().yAxisMinValue = yAxisMinValue;

	}

}
