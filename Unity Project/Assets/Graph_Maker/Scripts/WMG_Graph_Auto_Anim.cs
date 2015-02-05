using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;

// Contains GUI system dependent functions

public class WMG_Graph_Auto_Anim : MonoBehaviour {
	
	public WMG_Axis_Graph theGraph;
	
	public void subscribeToEvents(bool val) {
		for (int j = 0; j < theGraph.lineSeries.Count; j++) {
			if (!theGraph.activeInHierarchy(theGraph.lineSeries[j])) continue;
			WMG_Series aSeries = theGraph.lineSeries[j].GetComponent<WMG_Series>();
			if (val) {
				aSeries.SeriesDataChanged += SeriesDataChangedMethod;
			}
			else {
				aSeries.SeriesDataChanged -= SeriesDataChangedMethod;
			}
		}
	}
	
	public void addSeriesForAutoAnim(WMG_Series aSeries) {
		aSeries.SeriesDataChanged += SeriesDataChangedMethod;
	}
	
	private void SeriesDataChangedMethod(WMG_Series aSeries) {
		// Animate the points, links, and bars
		List<GameObject> objects = aSeries.getPoints();
		for (int i = 0; i < objects.Count; i++) {
			Transform aComp = objects[i].transform;
			if (theGraph.graphType == WMG_Axis_Graph.graphTypes.line) {
				// For line graphs, need to animate links as well via callback functions
				HOTween.To(aComp, theGraph.autoAnimationsDuration, new TweenParms()
		            .Prop("localPosition", new Vector3(aSeries.AfterPositions()[i].x, aSeries.AfterPositions()[i].y), false)
		            .Ease(theGraph.autoAnimationsEasetype)
					.OnUpdate(animateLinkCallback,aSeries,objects[i])
					.OnComplete(animateLinkCallbackEnd,aSeries)
		        );
			}
			else {
				// For bar graphs, animate widths and heights in addition to position. Depending on pivot / GUI system, animating width / height also affects position
				if (theGraph.orientationType == WMG_Axis_Graph.orientationTypes.vertical) {
					HOTween.To(aComp, theGraph.autoAnimationsDuration, new TweenParms()
						.Prop("localPosition", new Vector3(aSeries.AfterPositions()[i].x, aSeries.AfterPositions()[i].y), false)
			            .Ease(theGraph.autoAnimationsEasetype)
			        );
				}
				else {
					HOTween.To(aComp, theGraph.autoAnimationsDuration, new TweenParms()
			            .Prop("localPosition", new Vector3(	aSeries.AfterPositions()[i].x,
															aSeries.AfterPositions()[i].y + aSeries.AfterHeights()[i] - theGraph.barWidth), false)
			            .Ease(theGraph.autoAnimationsEasetype)
			        );
				}
				
//				UIWidget aComp2 = objects[i].GetComponent<UIWidget>();
//				
//				HOTween.To(aComp2, theGraph.autoAnimationsDuration, new TweenParms()
//		            .Prop("width", aSeries.AfterWidths()[i], false)
//		            .Ease(theGraph.autoAnimationsEasetype)
//		        );
//				HOTween.To(aComp2, theGraph.autoAnimationsDuration, new TweenParms()
//		            .Prop("height", aSeries.AfterHeights()[i], false)
//		            .Ease(theGraph.autoAnimationsEasetype)
//		        );
				
			}
		}
		// Animate the data point labels
		List<GameObject> dataLabels = aSeries.getDataLabels();
		for (int i = 0; i < dataLabels.Count; i++) {
			Transform label = dataLabels[i].transform;
			if (theGraph.graphType == WMG_Axis_Graph.graphTypes.line) {
				HOTween.To(label, theGraph.autoAnimationsDuration, new TweenParms()
					.Prop("localPosition", new Vector3(
						aSeries.dataLabelsOffset.x + aSeries.AfterPositions()[i].x,
						aSeries.dataLabelsOffset.y + aSeries.AfterPositions()[i].y), false)
		            .Ease(theGraph.autoAnimationsEasetype)
		        );
			}
			else {
				if (theGraph.orientationType == WMG_Axis_Graph.orientationTypes.vertical) {
					float newY = aSeries.dataLabelsOffset.y + aSeries.AfterPositions()[i].y + aSeries.AfterHeights()[i];
					if (aSeries.getBarIsNegative(i)) {
						newY = -aSeries.dataLabelsOffset.y - aSeries.AfterHeights()[i] + Mathf.RoundToInt((theGraph.barAxisValue - theGraph.yAxisMinValue) / (theGraph.yAxisMaxValue - theGraph.yAxisMinValue) * theGraph.yAxisLength);
					}
					HOTween.To(label, theGraph.autoAnimationsDuration, new TweenParms()
						.Prop("localPosition", new Vector3(
							aSeries.dataLabelsOffset.x + aSeries.AfterPositions()[i].x + theGraph.barWidth / 2,
							newY), false)
			            .Ease(theGraph.autoAnimationsEasetype)
			        );
				}
				else {
					float newX = aSeries.dataLabelsOffset.x + aSeries.AfterPositions()[i].x + aSeries.AfterWidths()[i];
					if (aSeries.getBarIsNegative(i)) {
						newX = -aSeries.dataLabelsOffset.x - aSeries.AfterWidths()[i] + Mathf.RoundToInt((theGraph.barAxisValue - theGraph.xAxisMinValue) / (theGraph.xAxisMaxValue - theGraph.xAxisMinValue) * theGraph.xAxisLength);
					}
					HOTween.To(label, theGraph.autoAnimationsDuration, new TweenParms()
						.Prop("localPosition", new Vector3(
							newX,
							aSeries.dataLabelsOffset.y + aSeries.AfterPositions()[i].y + theGraph.barWidth / 2), false)
			            .Ease(theGraph.autoAnimationsEasetype)
			        );
				}
				
			}
		}
	}
	
	private void animateLinkCallback(TweenEvent data) {
		WMG_Series aSeries = (WMG_Series) data.parms[0];
		GameObject aGO = (GameObject) data.parms[1];
		WMG_Node aNode = aGO.GetComponent<WMG_Node>();
		WMG_Link theLine = aNode.links[aNode.links.Count-1].GetComponent<WMG_Link>();
		theLine.Reposition();
		if (aSeries.connectFirstToLast) { // One extra link to animate for circles / close loop series
			aNode = aSeries.getPoints()[0].GetComponent<WMG_Node>();
			theLine = aNode.links[0].GetComponent<WMG_Link>();
			theLine.Reposition();
		}
	}
	
	private void animateLinkCallbackEnd(TweenEvent data) {
		WMG_Series aSeries = (WMG_Series) data.parms[0];
		for (int i = 0; i < aSeries.getLines().Count; i++) {
			WMG_Link theLine = aSeries.getLines()[i].GetComponent<WMG_Link>();
			theLine.Reposition();
		}
	}
}
