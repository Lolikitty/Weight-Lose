using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpdateBarGraph : MonoBehaviour {

		public GameObject barGraph;
		public GameObject backGround;
		public GameObject series;
		// Use this for initialization
		
		//barGraph 的 屬性
		public float xAxisMaxValue = 20.0f; 
		public float xAxisMinValue = 6.0f;
		public int xAxisNumTicks = 16;	//切割n份，要n+1
		public float xAxisLength = 280.0f;	//x軸實際長度
		
		public float yAxisMaxValue = 4000.0f; 
		public float yAxisMinValue = 0;
		public int yAxisNumTicks = 11; 
		public float yAxisLength = 180.0f;
		
		public List<string> xAxisLabels; // = xAxisNumTicks-1 ，兩個要對等
		public List<string> yAxisLabels;
		

		List<Vector2> sV1; //Vector 數目對應 int，長條的數據
		
		//15
		float[] v = new float[]{400.0f,1200.0f,1600.0f,2800.0f,2200.0f
							,2400.0f,2000.0f,2800.0f,4000.0f,800.0f
							,3200.0f,1200.0f,2000.0f,400.0f,1600.0f};
		
		
		void Start () {
			InitGraphSet ();
		}
		
		void InitGraphSet(){
			
			//對x軸設定
			barGraph.GetComponent<WMG_Axis_Graph>().xAxisMaxValue = xAxisMaxValue; 
			barGraph.GetComponent<WMG_Axis_Graph>().xAxisMinValue = xAxisMinValue;
			barGraph.GetComponent<WMG_Axis_Graph>().xAxisNumTicks = xAxisNumTicks;	//
			barGraph.GetComponent<WMG_Axis_Graph>().xAxisLength = xAxisLength;	//x軸實際長度
			barGraph.GetComponent<WMG_Axis_Graph>().xAxisLabels = xAxisLabels; // = xAxisNumTicks ，兩個要對等
			
			//對y軸設定
			barGraph.GetComponent<WMG_Axis_Graph>().yAxisMaxValue = yAxisMaxValue; 
			barGraph.GetComponent<WMG_Axis_Graph>().yAxisMinValue = yAxisMinValue;
			barGraph.GetComponent<WMG_Axis_Graph>().yAxisNumTicks = yAxisNumTicks;	
			barGraph.GetComponent<WMG_Axis_Graph>().yAxisLength = yAxisLength;	
			barGraph.GetComponent<WMG_Axis_Graph>().yAxisLabels = yAxisLabels; 
			
			//設定x軸長條的數據
			sV1 = new List<Vector2>();
			for (int i=0; i < xAxisNumTicks-1; i++) {
				sV1.Add(new Vector2(0,v[i]));	
				//series.GetComponent<WMG_Series> ().pointValues.Add(new Vector2(0,v[i]));
				//print(series.GetComponent<WMG_Series> ().pointValues[i]);
			}
			series.GetComponent<WMG_Series> ().pointValues = sV1; //預設只有一種長條
			
		}
		
		/*
	void InitGraphSet(float xAxisMaxValue,float xAxisMinValue,int xAxisNumTicks,float xAxisLength
	                  ,float yAxisMaxValue,float yAxisMinValue,int yAxisNumTicks,float yAxisLength
	                  ,List<string> xAxisLabels,List<string> yAxisLabels,List<Vector2> sV1){

		//對x軸設定
		barGraph.GetComponent<WMG_Axis_Graph>().xAxisMaxValue = xAxisMaxValue; 
		barGraph.GetComponent<WMG_Axis_Graph>().xAxisMinValue = xAxisMinValue;
		barGraph.GetComponent<WMG_Axis_Graph>().xAxisNumTicks = xAxisNumTicks;	//
		barGraph.GetComponent<WMG_Axis_Graph>().xAxisLength = xAxisLength;	//x軸實際長度
		barGraph.GetComponent<WMG_Axis_Graph>().xAxisLabels = xAxisLabels; // = xAxisNumTicks ，兩個要對等

		//對y軸設定
		barGraph.GetComponent<WMG_Axis_Graph>().yAxisMaxValue = yAxisMaxValue; 
		barGraph.GetComponent<WMG_Axis_Graph>().yAxisMinValue = yAxisMinValue;
		barGraph.GetComponent<WMG_Axis_Graph>().yAxisNumTicks = yAxisNumTicks;	
		barGraph.GetComponent<WMG_Axis_Graph>().yAxisLength = yAxisLength;	
		barGraph.GetComponent<WMG_Axis_Graph>().yAxisLabels = yAxisLabels; 

		//設定x軸長條的數據
		/*
		for (int i=0; i < xAxisNumTicks-1; i++) {
			sV1.Add(new Vector2(0,v[i]));	
			//series.GetComponent<WMG_Series> ().pointValues.Add(new Vector2(0,v[i]));
			//print(series.GetComponent<WMG_Series> ().pointValues[i]);
		}

		series.GetComponent<WMG_Series> ().pointValues = sV1; //預設只有一種長條
	}
	*/
	}

