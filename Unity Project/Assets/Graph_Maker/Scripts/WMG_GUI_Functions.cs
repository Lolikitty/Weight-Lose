using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Contains GUI system dependent functions

public class WMG_GUI_Functions : MonoBehaviour {
	
	public enum WMGpivotTypes {Bottom, BottomLeft, BottomRight, Center, Left, Right, Top, TopLeft, TopRight};
	
	public void SetActive(GameObject obj, bool state) {
		obj.SetActive(state);
	}
	
	public bool activeInHierarchy(GameObject obj) {
		return obj.activeInHierarchy;
	}
	
	public void SetActiveAnchoredSprite(GameObject obj, bool state) {
		SetActive(obj, state);
	}
	
	public void changeSpriteFill(GameObject obj, float fill) {
		Image theSprite = obj.GetComponent<Image>();
		theSprite.fillAmount = fill;
	}
	
	public void changeSpriteColor(GameObject obj, Color aColor) {
		Image theSprite = obj.GetComponent<Image>();
		theSprite.color = aColor;
	}
	
	public void changeSpriteWidth(GameObject obj, int aWidth) {
		RectTransform theSprite = obj.GetComponent<RectTransform>();
		if (theSprite == null) return;
		theSprite.sizeDelta = new Vector2(aWidth, theSprite.rect.height);
	}
	
	public void changeSpriteHeight(GameObject obj, int aHeight) {
		RectTransform theSprite = obj.GetComponent<RectTransform>();
		if (theSprite == null) return;
		theSprite.sizeDelta = new Vector2(theSprite.rect.width, aHeight);
	}
	
	public void setTextureMaterial(GameObject obj, Material aMat) {
//		UITexture curTex = obj.GetComponent<UITexture>();
//		curTex.material = new Material(aMat);
		Image curTex = obj.GetComponent<Image>();
		curTex.material = new Material(aMat);
	}
	
	public Material getTextureMaterial(GameObject obj) {
//		UIDrawCall drawCall = obj.GetComponent<UIWidget>().drawCall;
//		if (drawCall == null) return null;
//		return drawCall.dynamicMaterial;
		Image curTex = obj.GetComponent<Image>();
		if (curTex == null) return null;
		return curTex.material;
	}
	
	public void changeAreaShadingWidthHeight(GameObject obj, int aWidth, int aHeight) {
		RectTransform theSprite = obj.GetComponent<RectTransform>();
		if (theSprite == null) return;
		theSprite.sizeDelta = new Vector2(aWidth, aHeight);
//		// hide the object and save on draw calls
//		if (theSprite.drawCall != null) {
//			if (aWidth < 2 || aHeight < 2) {
//				theSprite.drawCall.renderer.enabled = false;
//			}
//			else {
//				theSprite.drawCall.renderer.enabled = true;
//			}
//		}
	}
	
	public void changeBarWidthHeight(GameObject obj, int aWidth, int aHeight) {
		RectTransform theSprite = obj.GetComponent<RectTransform>();
		if (theSprite == null) return;
		theSprite.sizeDelta = new Vector2(aWidth, aHeight);
//		// hide the object
//		if (aWidth < 2 || aHeight < 2) {
//			SetActive(obj, false);
//		}
//		else {
//			SetActive(obj, true);
//		}
	}
	
	public float getSpriteWidth(GameObject obj) {
		RectTransform theSprite = obj.GetComponent<RectTransform>();
		return theSprite.rect.width;
	}
	
	public float getSpriteHeight(GameObject obj) {
		RectTransform theSprite = obj.GetComponent<RectTransform>();
		return theSprite.rect.height;
	}
	
	public void changeLabelText(GameObject obj, string aText) {
		Text theLabel = obj.GetComponent<Text>();
		theLabel.text = aText;
	}
	
	public float getSpritePositionX(GameObject obj) {
		return obj.transform.localPosition.x;
	}
	
	public float getSpritePositionY(GameObject obj) {
		return obj.transform.localPosition.y;
	}
	
	public float getSpriteOffsetX(GameObject obj) {
		return 0;
	}
	
	public float getSpriteFactorX(GameObject obj) {
		return 0;
	}
	
	public float getSpriteOffsetY(GameObject obj) {
		return 0;
	}
	
	public float getSpriteFactorY(GameObject obj) {
		return 0;
	}
	
	public float getSpriteFactorY2(GameObject obj) {
		RectTransform theSprite = obj.GetComponent<RectTransform>();
		return 1 - theSprite.pivot.y; // Top corresponds to pivot of 1, return 1 for bottom
	}
	
	public void changeSpritePositionTo(GameObject obj, Vector3 newPos) {
		obj.transform.localPosition = new Vector3(newPos.x, newPos.y, newPos.z);
	}
	
	public void changeSpritePositionToX(GameObject obj, float newPos) {
		Vector3 thePos = obj.transform.localPosition;
		obj.transform.localPosition = new Vector3(newPos, thePos.y, thePos.z);
	}
	
	public void changeSpritePositionToY(GameObject obj, float newPos) {
		Vector3 thePos = obj.transform.localPosition;
		obj.transform.localPosition = new Vector3(thePos.x, newPos, thePos.z);
	}
	
	public Vector2 getChangeSpritePositionTo(GameObject obj, Vector2 newPos) {
		return new Vector2(newPos.x, newPos.y);
	}
	
	public void changeSpritePositionRelativeToObjBy(GameObject obj, GameObject relObj, Vector3 changeAmt) {
		Vector3 thePos = relObj.transform.localPosition;
		obj.transform.localPosition = new Vector3(thePos.x + changeAmt.x, thePos.y + changeAmt.y, thePos.z + changeAmt.z);
	}
	
	public void changeSpritePositionRelativeToObjByX(GameObject obj, GameObject relObj, float changeAmt) {
		Vector3 thePos = relObj.transform.localPosition;
		Vector3 curPos = obj.transform.localPosition;
		obj.transform.localPosition = new Vector3(thePos.x + changeAmt, curPos.y, curPos.z);
	}
	
	public void changeSpritePositionRelativeToObjByY(GameObject obj, GameObject relObj, float changeAmt) {
		Vector3 thePos = relObj.transform.localPosition;
		Vector3 curPos = obj.transform.localPosition;
		obj.transform.localPosition = new Vector3(curPos.x, thePos.y + changeAmt, curPos.z);
	}
	
	public void changeSpritePivot(GameObject obj, WMGpivotTypes theType) {
		RectTransform theSprite = obj.GetComponent<RectTransform>();
		if (theSprite == null) return;
		if (theType == WMGpivotTypes.Bottom) {
			theSprite.pivot = new Vector2(0.5f, 0f);
		}
		else if (theType == WMGpivotTypes.BottomLeft) {
			theSprite.pivot = new Vector2(0f, 0f);
		}
		else if (theType == WMGpivotTypes.BottomRight) {
			theSprite.pivot = new Vector2(1f, 0f);
		}
		else if (theType == WMGpivotTypes.Center) {
			theSprite.pivot = new Vector2(0.5f, 0.5f);
		}
		else if (theType == WMGpivotTypes.Left) {
			theSprite.pivot = new Vector2(0f, 0.5f);
		}
		else if (theType == WMGpivotTypes.Right) {
			theSprite.pivot = new Vector2(1f, 0.5f);
		}
		else if (theType == WMGpivotTypes.Top) {
			theSprite.pivot = new Vector2(0.5f, 1f);
		}
		else if (theType == WMGpivotTypes.TopLeft) {
			theSprite.pivot = new Vector2(0f, 1f);
		}
		else if (theType == WMGpivotTypes.TopRight) {
			theSprite.pivot = new Vector2(1f, 1f);
		}
	}
	
	public void changeSpriteParent(GameObject child, GameObject parent) {
		child.transform.parent = parent.transform;
		child.transform.localPosition = Vector3.zero;
	}
	
	public void bringSpriteToFront(GameObject obj) {
		obj.transform.SetAsLastSibling();
	}
	
	public void sendSpriteToBack(GameObject obj) {
		obj.transform.SetAsFirstSibling();
	}
	
	public string getDropdownSelection(GameObject obj) {
//		UIPopupList dropdown = obj.GetComponent<UIPopupList>();
//		return dropdown.value;
		return null;
	}
	
	public void setDropdownSelection(GameObject obj, string newval) {
//		UIPopupList dropdown = obj.GetComponent<UIPopupList>();
//		dropdown.value = newval;
	}
	
	public void addDropdownItem(GameObject obj, string item) {
//		UIPopupList dropdown = obj.GetComponent<UIPopupList>();
//		dropdown.items.Add(item);
	}
	
	public void deleteDropdownItem(GameObject obj) {
//		UIPopupList dropdown = obj.GetComponent<UIPopupList>();
//		dropdown.items.RemoveAt(dropdown.items.Count-1);
	}
	
	public void setDropdownIndex(GameObject obj, int index) {
//		UIPopupList dropdown = obj.GetComponent<UIPopupList>();
//		dropdown.value = dropdown.items[index];
	}
	
	public void setButtonColor(Color aColor, GameObject obj) {
//		UILabel aButton = obj.GetComponent<UILabel>();
//		aButton.color = aColor;
	}
	
	public bool getToggle(GameObject obj) {
//		UIToggle theTog = obj.GetComponent<UIToggle>();
//		return theTog.value;
		return false;
	}
	
	public void setToggle(GameObject obj, bool state) {
//		UIToggle theTog = obj.GetComponent<UIToggle>();
//		theTog.value = state;
	}
	
	public float getSliderVal(GameObject obj) {
//		UISlider theSlider = obj.GetComponent<UISlider>();
//		return theSlider.value;
		return 0;
	}
	
	public void setSliderVal(GameObject obj, float val) {
//		UISlider theSlider = obj.GetComponent<UISlider>();
//		theSlider.value = val;
	}
	
	public void showControl(GameObject obj) {
		SetActive(obj, true);
	}
	
	public void hideControl(GameObject obj) {
		SetActive(obj, false);
	}
	
	public bool getControlVisibility(GameObject obj) {
		return activeInHierarchy(obj);
	}
	
	public void unfocusControl(GameObject obj) {
		// Only needed in Daikon
	}
	
	public bool isDaikon() {
		// Sometimes this may be needed, usually hacky quick fix workaround
		return false;
	}
}
