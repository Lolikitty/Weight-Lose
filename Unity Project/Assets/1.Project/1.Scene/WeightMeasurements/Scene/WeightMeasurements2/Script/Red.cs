using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class Red : MonoBehaviour {
	
	//red
	public GameObject lineGraph;
	public GameObject backGround;
	public GameObject series1;
	
	//barGraph 的 屬性 //預設值
	public float xAxisMaxValue = 100.0f; 
	public float xAxisMinValue = 0f;
	public int xAxisNumTicks = 11;	//切割n份，要n+1
	public float xAxisLength = 280.0f;	//x軸實際長度
	
	public float yAxisMaxValue = 30.0f; 
	public float yAxisMinValue = 22.0f;
	public int yAxisNumTicks = 9;
	public float yAxisLength = 180.0f;
	
	//public List<string> xAxisLabels; // = xAxisNumTicks-1 ，兩個要對等
	//public List<string> yAxisLabels;
	List<Vector2> sV1; //Vector 數目對應 int，長條的數據
	//public List<Vector2> sV2; //Vector 數目對應 int，長條的數據
	
	//9
	float[] v1 = new float[]{29.0f,27.0f,26.0f,25.0f,25.0f,25.0f,26.0f,23.0f,24.0f,26.0f};

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


//		Set (DateTime.Now); //****


		lineGraph.transform.localScale = new Vector2(2.0f,2.0f);
		lineGraph.transform.localPosition = new Vector2 (-250.0f,-380.0f);

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
		//xAxisNumTicks-1
		//print (xAxisNumTicks);
		sV1 = new List<Vector2> ();
		//print (xAxisNumTicks);
		for (int i=0; i < xAxisNumTicks-1; i++) {
			//print(i);
			sV1.Add(new Vector2(0,v1[i]));	
		}
		
		series1.GetComponent<WMG_Series> ().pointValues = sV1; //預設只有一種長條
	}

	void Set(DateTime day){
//		lineGraph.GetComponent<WMG_Axis_Graph> ().xAxisLabelSize = xAxisNumTicks - 1;
		BodyFat bf = new BodyFat();//***
	
		float lastTmp = 0;
		float hw = 0;
		int i = 0;

//		Debug.Log (" day.AddDays(-9) =" + day.AddDays(-9).Date);

		for (DateTime d = day.AddDays(-9); d.Date <= day.Date; d = d.AddDays(1),i++) {

			hw = bf.GetDayBodyFat(d); //***

//			Debug.Log(d.Date + " ,hw = " + hw);
//			Debug.Log("i = " + i + " hw =" + hw);
//			Debug.Log("hw = " + hw);

			if(hw==0){
				if(lastTmp == 0){
					lastTmp = bf.GetDayBodyFat(d); //***
					v1[i] = lastTmp;
				}else{
					v1[i] = lastTmp;
				}
			}
			else{
				lastTmp = hw;
				v1[i] = hw;
			}

//			Debug.Log(d.Date + " v1[i] = " + v1[i]);
		}

		float standardMin = v1.Min();

		if (standardMin != 0) {
			standardMin -= 2.0f;
		}

		if (standardMin <= 0) {
			standardMin = 0;
		}

		yAxisMaxValue = v1.Max() + 2.0f;
		yAxisMinValue = standardMin ;

		lineGraph.GetComponent<WMG_Axis_Graph>().yAxisMaxValue = yAxisMaxValue; 
		lineGraph.GetComponent<WMG_Axis_Graph>().yAxisMinValue = yAxisMinValue;

	}

	// Update is called once per frame
	/*
	void InitGraphSet(float xAxisMaxValue,float xAxisMinValue,int xAxisNumTicks,float xAxisLength
	                  ,float yAxisMaxValue,float yAxisMinValue,int yAxisNumTicks,float yAxisLength
	                  ,List<Vector2> sV1){

		//對x軸設定
		barGraph.GetComponent<WMG_Axis_Graph>().xAxisMaxValue = xAxisMaxValue; 
		barGraph.GetComponent<WMG_Axis_Graph>().xAxisMinValue = xAxisMinValue;
		barGraph.GetComponent<WMG_Axis_Graph>().xAxisNumTicks = xAxisNumTicks;	//
		barGraph.GetComponent<WMG_Axis_Graph>().xAxisLength = xAxisLength;	//x軸實際長度
		//barGraph.GetComponent<WMG_Axis_Graph>().xAxisLabels = xAxisLabels; // = xAxisNumTicks ，兩個要對等

		//對y軸設定
		barGraph.GetComponent<WMG_Axis_Graph>().yAxisMaxValue = yAxisMaxValue; 
		barGraph.GetComponent<WMG_Axis_Graph>().yAxisMinValue = yAxisMinValue;
		barGraph.GetComponent<WMG_Axis_Graph>().yAxisNumTicks = yAxisNumTicks;	
		barGraph.GetComponent<WMG_Axis_Graph>().yAxisLength = yAxisLength;	
		//barGraph.GetComponent<WMG_Axis_Graph>().yAxisLabels = yAxisLabels; 

		//設定x軸長條的數據
		/*
	 	sV1 = new List<Vector2> ();
		for (int i=0; i < xAxisNumTicks-1; i++) {
			sV1.Add(new Vector2(0,v[i]));	
			//series.GetComponent<WMG_Series> ().pointValues.Add(new Vector2(0,v[i]));
			//print(series.GetComponent<WMG_Series> ().pointValues[i]);
		}

		series1.GetComponent<WMG_Series> ().pointValues = sV1; //預設只有一種長條
	}
	*/
}
