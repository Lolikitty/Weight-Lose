using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;

public class WMG_Pie_Graph : WMG_Graph_Manager {
	
	public bool updateGraph;
	public bool updateGraphEveryFrame;
	public float animationDuration;
	public float sortAnimationDuration;
	public GameObject slicesParent;
	public GameObject legendParent;
	public GameObject legendBackground;
	public Object legendEntryPrefab;
	public Object nodePrefab;
	public List<float> sliceValues;
	public List<string> sliceLabels;
	public List<Color> sliceColors;
	public enum sortMethod {None, Largest_First, Smallest_First, Alphabetically, Reverse_Alphabetically};
	public sortMethod sortBy;
	public bool swapColorsDuringSort;
	public float explodeLength;
	public enum labelTypes {None, Labels_Only, Labels_Percents, Labels_Values, Labels_Values_Percents, Values_Only, Percents_Only, Values_Percents};
	public labelTypes legendType;
	public enum legendLocations {Right, Left, Below};
	public legendLocations legendLocation;
	public float legendWidth;
	public float legendRowHeight;
	public bool hideLegendEntriesWhen0;
	public labelTypes sliceLabelType;
	public float sliceLabelExplodeLength;
	public int numberDecimalsInPercents;
	public bool limitNumberSlices;
	public int maxNumberSlices;
	public bool includeOthers;
	public string includeOthersLabel;
	public Color includeOthersColor;
	
	private List<GameObject> slices = new List<GameObject>();
	private List<GameObject> sliceLegendEntries = new List<GameObject>();
	
	private int numSlices = 0;
	private bool isAnimating = false;
	private bool isSortAnimating = false;
	private List<float> slicePercents = new List<float>();
	private List<float> sliceExplodeAngles = new List<float>();
	private float numberToMultDecimalsInPercents = 0;
	private bool isOtherSlice = false;
	private float otherSliceValue = 0;
	private float totalVal = 0;
	private float animateDuration;
	
	void Update () {
		if (updateGraph && !updateGraphEveryFrame && !isAnimating && !isSortAnimating) {
			updateGraph = false;
			animateDuration = animationDuration;
			refreshGraph();
		}
		if (updateGraphEveryFrame) {
			updateGraph = false;
			animationDuration = 0;
			sortAnimationDuration = 0;
			animateDuration = 0;
			refreshGraph();
		}
	}
	
	public List<GameObject> getSlices() {
		return slices;
	}
	
	public List<GameObject> getSliceLegendEntries() {
		return sliceLegendEntries;
	}
	
	public void refreshGraph() {
		// Calculate variables that will be used in the other update functions
		UpdateData();
		
		// Creates and deletes slices and slice legend objects based on the slice values
		CreateOrDeleteSlicesBasedOnValues();
		
		// Sets labels, colors, and background dimensions for legend
		UpdateLegend();
		
		// Sets the colors, fill amount, and rotation of the pie slices, as well as set labels for pie slices
		UpdateSliceVisuals();
	}
	
	void UpdateData() {
		// Find the total number of slices
		isOtherSlice = false;
		numSlices = sliceValues.Count;
		if (limitNumberSlices) {
			if (numSlices > maxNumberSlices) {
				numSlices = maxNumberSlices;
				if (includeOthers) {
					isOtherSlice = true;
					numSlices++;
				}
			}
		}
		
		numberToMultDecimalsInPercents = Mathf.Pow(10f, numberDecimalsInPercents+2);
		
		// Find Other Slice Value and Total Value
		otherSliceValue = 0;
		totalVal = 0;
		for (int i = 0; i < sliceValues.Count; i++) {
			totalVal += sliceValues[i];
			if (isOtherSlice && i >= maxNumberSlices) {
				otherSliceValue += sliceValues[i];
			}
			if (limitNumberSlices && !isOtherSlice && i >= maxNumberSlices) {
				totalVal -= sliceValues[i];
			}
		}
	}
	
	void UpdateLegend() {
		// Determine whether to display the legend
		if (legendType != labelTypes.None && !activeInHierarchy(legendParent)) SetActive(legendParent,true);
		if (legendType == labelTypes.None && activeInHierarchy(legendParent)) SetActive(legendParent,false);
		
		int numLegendEntries = numSlices; // May decrease based on hide entries when 0
		
		// Update Legend Entries
		for (int i = 0; i < numSlices; i++) {
			sliceLegendEntries[i].transform.localPosition = new Vector3(-getSpriteWidth(legendBackground) / 2 + 30, getSpriteHeight(legendBackground) / 2 - 25 - i*40, 0); 
			WMG_Node sliceLegend = sliceLegendEntries[i].GetComponent<WMG_Node>();
			
			float slicePercent = sliceValues[i] / totalVal;
			if (isOtherSlice && i == numSlices - 1) {
				slicePercent = otherSliceValue / totalVal;
				setLabelData(sliceLegend.objectToLabel, legendType, includeOthersLabel, slicePercent, otherSliceValue);
				changeSpriteColor(sliceLegend.objectToColor, includeOthersColor);
			}
			else {
				setLabelData(sliceLegend.objectToLabel, legendType, sliceLabels[i], slicePercent, sliceValues[i]);
				changeSpriteColor(sliceLegend.objectToColor, sliceColors[i]);
				if (hideLegendEntriesWhen0) {
					if (sliceValues[i] == 0) {
						SetActive(sliceLegendEntries[i], false);
						numLegendEntries--;
					}
					else {
						SetActive(sliceLegendEntries[i], true);
					}
				}
			}
			
		}
		
		// Update the legend background
		changeSpriteWidth(legendBackground, Mathf.RoundToInt(legendWidth));
		changeSpriteHeight(legendBackground, Mathf.RoundToInt(10 + legendRowHeight * numLegendEntries));
		WMG_Node legendSlice = slices[0].GetComponent<WMG_Node>();
		if (legendLocation == legendLocations.Right) {
			legendParent.transform.localPosition = new Vector3 (getSpriteWidth(legendBackground) / 2 + getSpriteWidth(legendSlice.objectToColor) / 2 + 20, 0, legendParent.transform.localPosition.z);
		}
		else if (legendLocation == legendLocations.Left) {
			legendParent.transform.localPosition = new Vector3 (-getSpriteWidth(legendBackground) / 2 - getSpriteWidth(legendSlice.objectToColor) / 2 - 20, 0, legendParent.transform.localPosition.z);
		}
		else if (legendLocation == legendLocations.Below) {
			legendParent.transform.localPosition = new Vector3 (0, -getSpriteHeight(legendBackground) / 2 - getSpriteHeight(legendSlice.objectToColor) / 2 - 20, legendParent.transform.localPosition.z);
		}
	}
	
	void UpdateSliceVisuals() {
		float curTotalRot = 0;
		for (int i = 0; i < numSlices; i++) {
			// Update Pie Slices
			float newAngle =  -1 * curTotalRot;
			if (newAngle < 0) newAngle += 360;
			WMG_Node pieSlice =  slices[i].GetComponent<WMG_Node>();
			if (sliceLabelType != labelTypes.None && !activeInHierarchy(pieSlice.objectToLabel)) SetActive(pieSlice.objectToLabel,true);
			if (sliceLabelType == labelTypes.None && activeInHierarchy(pieSlice.objectToLabel)) SetActive(pieSlice.objectToLabel,false);
			
			// Set Slice Data and maybe Other Slice Data
			float slicePercent = sliceValues[i] / totalVal;
			if (isOtherSlice && i == numSlices - 1) {
				slicePercent = otherSliceValue / totalVal;
				StartCoroutine(AnimateSpriteFill(i, animateDuration, slicePercent, newAngle, numSlices - 1, includeOthersColor)); // Animate fill and rotation of slice sprites
				setLabelData(pieSlice.objectToLabel, sliceLabelType, includeOthersLabel, slicePercent, otherSliceValue);
			}
			else {
				StartCoroutine(AnimateSpriteFill(i, animateDuration, slicePercent, newAngle, numSlices - 1, sliceColors[i])); // Animate fill and rotation of slice sprites
				setLabelData(pieSlice.objectToLabel, sliceLabelType, sliceLabels[i], slicePercent, sliceValues[i]);
				// Hide if 0
				if (sliceValues[i] == 0) {
					SetActive(pieSlice.objectToLabel, false);
				}
			}
			
			curTotalRot += slicePercent * 360;
		}
	}
	
	
	
	void CreateOrDeleteSlicesBasedOnValues() {
		// Create pie slices based on sliceValues data
		for (int i = 0; i < numSlices; i++) {
			if (sliceLabels.Count <= i) sliceLabels.Add("");
			if (sliceColors.Count <= i) sliceColors.Add(Color.white);
			if (slices.Count <= i) {
				GameObject curObj = CreateNode(nodePrefab, slicesParent);
				slices.Add(curObj);
			}
			if (sliceLegendEntries.Count <= i) {
				GameObject curObj = CreateNode(legendEntryPrefab, legendParent);
				sliceLegendEntries.Add(curObj);
			}
			if (slicePercents.Count <= i) {
				slicePercents.Add(0);
			}
			if (sliceExplodeAngles.Count <= i) {
				sliceExplodeAngles.Add(0);
			}
		}
		for (int i = slices.Count - 1; i >= 0; i--) {
			if (slices[i] != null && i >= numSlices) {
				WMG_Node theSlice = slices[i].GetComponent<WMG_Node>();
				DeleteNode(theSlice);
				slices.RemoveAt(i);
			}
		}
		
		// If there are more sliceLegendEntries or slices than sliceValues data, delete the extras
		for (int i = sliceLegendEntries.Count - 1; i >= 0; i--) {
			if (sliceLegendEntries[i] != null && i >= numSlices) {
				WMG_Node theEntry = sliceLegendEntries[i].GetComponent<WMG_Node>();
				DeleteNode(theEntry);
				sliceLegendEntries.RemoveAt(i);
			}
		}
	}
	
	IEnumerator AnimateSpriteFill(int sliceNum, float animateDuration, float afterFill, float newAngle, int lastSliceNum, Color newSliceColor) {
		if (sliceNum == 0) isAnimating = true;
		WMG_Node pieSlice =  slices[sliceNum].GetComponent<WMG_Node>();
		float t = 0f;
		float beforeFill = slicePercents[sliceNum];
		float beforeRot = pieSlice.objectToColor.transform.localEulerAngles.z;
		float fill = beforeFill;
		float rot = beforeRot;
		float beforeExplodeAngle = beforeRot * -1 + 0.5f * beforeFill * 360;
		float afterExplodeAngle = newAngle * -1 + 0.5f * afterFill * 360;
		float explodeFromAngle = beforeExplodeAngle;
		changeSpriteColor(pieSlice.objectToColor, newSliceColor);
		while (t < animateDuration) {
			float animationPercent = t/animateDuration;
			fill = Mathf.Lerp(beforeFill, afterFill, animationPercent);
			rot = Mathf.Lerp(beforeRot, newAngle, animationPercent);
			explodeFromAngle = Mathf.Lerp(beforeExplodeAngle, afterExplodeAngle, animationPercent);
			t += Time.deltaTime;
			changeSpriteFill(pieSlice.objectToColor, fill);
			pieSlice.objectToColor.transform.localEulerAngles = new Vector3(0, 0, rot);
			slices[sliceNum].transform.localPosition =  new Vector3(explodeLength * Mathf.Sin(explodeFromAngle * Mathf.Deg2Rad), 
																	explodeLength * Mathf.Cos(explodeFromAngle * Mathf.Deg2Rad), slices[sliceNum].transform.localPosition.z);
			pieSlice.objectToLabel.transform.localPosition = new Vector3(	(explodeLength + sliceLabelExplodeLength + getSpriteWidth(pieSlice.objectToColor) / 4) * Mathf.Sin(explodeFromAngle * Mathf.Deg2Rad), 
																			(explodeLength + sliceLabelExplodeLength + getSpriteHeight(pieSlice.objectToColor) / 4) * Mathf.Cos(explodeFromAngle * Mathf.Deg2Rad), 
																			pieSlice.objectToLabel.transform.localPosition.z);
			yield return null;
		}
		sliceExplodeAngles[sliceNum] = afterExplodeAngle;
		slicePercents[sliceNum] = afterFill;
		slices[sliceNum].name = sliceLabels[sliceNum];
		sliceLegendEntries[sliceNum].name = sliceLabels[sliceNum];
		changeSpriteFill(pieSlice.objectToColor, afterFill);
		pieSlice.objectToColor.transform.localEulerAngles = new Vector3(0, 0, newAngle);
		slices[sliceNum].transform.localPosition =  new Vector3(explodeLength * Mathf.Sin(afterExplodeAngle * Mathf.Deg2Rad), 
																explodeLength * Mathf.Cos(afterExplodeAngle * Mathf.Deg2Rad), slices[sliceNum].transform.localPosition.z);
		pieSlice.objectToLabel.transform.localPosition = new Vector3(	(explodeLength + sliceLabelExplodeLength + getSpriteWidth(pieSlice.objectToColor) / 4) * Mathf.Sin(afterExplodeAngle * Mathf.Deg2Rad), 
																		(explodeLength + sliceLabelExplodeLength + getSpriteHeight(pieSlice.objectToColor) / 4) * Mathf.Cos(afterExplodeAngle * Mathf.Deg2Rad), 
																		pieSlice.objectToLabel.transform.localPosition.z);
		if (sliceNum == lastSliceNum) {
			isAnimating = false;
			bool wasASwap = false;
			if (sortBy != sortMethod.None) wasASwap = sortData();
			if (wasASwap) {
				isSortAnimating = true;
				shrinkSlices();
			}
		}
	}
	
	void shrinkSlices() {
		if (sortAnimationDuration == 0) {
			isSortAnimating = false;
			animateDuration = 0;
			refreshGraph();
		}
		else {
			for (int i = 0; i < numSlices; i++) {
				if (i == 0) {
					HOTween.To(slices[i].transform, sortAnimationDuration / 2, new TweenParms()
			            .Prop("localScale", Vector3.zero, false)
			            .Ease(EaseType.Linear)
						.OnComplete(enlargeSlices, null)
			        );
				}
				else {
					HOTween.To(slices[i].transform, sortAnimationDuration / 2, new TweenParms()
			            .Prop("localScale", Vector3.zero, false)
			            .Ease(EaseType.Linear)
			        );
				}
			}
		}
	}
					
	private void enlargeSlices(TweenEvent data) {
		animateDuration = 0;
		refreshGraph();
		for (int i = 0; i < numSlices; i++) {
			if (i == 0) {
				HOTween.To(slices[i].transform, sortAnimationDuration / 2, new TweenParms()
		            .Prop("localScale", Vector3.one, false)
		            .Ease(EaseType.Linear)
					.OnComplete(endSortAnimating, null)
		        );
			}
			else {
				HOTween.To(slices[i].transform, sortAnimationDuration / 2, new TweenParms()
		            .Prop("localScale", Vector3.one, false)
		            .Ease(EaseType.Linear)
		        );
			}
		}
	}
	
	private void endSortAnimating(TweenEvent data) {
		isSortAnimating = false;
	}
	
	void setLabelData(GameObject theLabel, labelTypes theLabelType, string labelText, float slicePercent, float sliceValue) {
		string theText = labelText;
		
		if (theLabelType == labelTypes.Labels_Percents) {
			theText += " (" + (Mathf.Round(slicePercent*numberToMultDecimalsInPercents)/numberToMultDecimalsInPercents*100).ToString() + "%)";
		}
		else if (theLabelType == labelTypes.Labels_Values) {
			theText += " (" + Mathf.Round(sliceValue).ToString() + ")";
		}
		else if (theLabelType == labelTypes.Labels_Values_Percents) {
			theText += " - " + Mathf.Round(sliceValue).ToString() + " (" + (Mathf.Round(slicePercent*numberToMultDecimalsInPercents)/numberToMultDecimalsInPercents*100).ToString() + "%)";
		}
		else if (theLabelType == labelTypes.Values_Only) {
			theText = Mathf.Round(sliceValue).ToString();
		}
		else if (theLabelType == labelTypes.Percents_Only) {
			theText = (Mathf.Round(slicePercent*numberToMultDecimalsInPercents)/numberToMultDecimalsInPercents*100).ToString() + "%";
		}
		else if (theLabelType == labelTypes.Values_Percents) {
			theText = Mathf.Round(sliceValue).ToString() + " (" + (Mathf.Round(slicePercent*numberToMultDecimalsInPercents)/numberToMultDecimalsInPercents*100).ToString() + "%)";
		}
		changeLabelText(theLabel, theText);
	}
	
	bool sortData() {
		bool wasASwap = false;
		bool flag = true;
		bool shouldSwap = false;
		float temp;
		string tempL;
		GameObject tempGo;
		int numLength = numSlices;
		for (int i = 1; (i <= numLength) && flag; i++) {
			flag = false;
			for (int j = 0; j < (numLength - 1); j++ ) {
				shouldSwap = false;
				if (sortBy == sortMethod.Largest_First) {
					if (sliceValues[j+1] > sliceValues[j]) shouldSwap = true;
				}
				else if (sortBy == sortMethod.Smallest_First) {
					if (sliceValues[j+1] < sliceValues[j]) shouldSwap = true;
				}
				else if (sortBy == sortMethod.Alphabetically) {
					if (sliceLabels[j+1].CompareTo(sliceLabels[j]) == -1) shouldSwap = true;
				}
				else if (sortBy == sortMethod.Reverse_Alphabetically) {
					if (sliceLabels[j+1].CompareTo(sliceLabels[j]) == 1) shouldSwap = true;
				}
				if (shouldSwap) {
					// Swap values
					temp = sliceValues[j];
					sliceValues[j] = sliceValues[j+1];
					sliceValues[j+1] = temp;
					// Swap labels
					tempL = sliceLabels[j];
					sliceLabels[j] = sliceLabels[j+1];
					sliceLabels[j+1] = tempL;
					// Swap Percents
					temp = slicePercents[j];
					slicePercents[j] = slicePercents[j+1];
					slicePercents[j+1] = temp;
					// Swap Slices
					tempGo = slices[j];
					slices[j] = slices[j+1];
					slices[j+1] = tempGo;
					// Swap Colors
					if (swapColorsDuringSort) {
						Color tempC = sliceColors[j];
						sliceColors[j] = sliceColors[j+1];
						sliceColors[j+1] = tempC;
					}
					flag = true;
					wasASwap = true;
				}
			}
		}
		return wasASwap;
	}
}
