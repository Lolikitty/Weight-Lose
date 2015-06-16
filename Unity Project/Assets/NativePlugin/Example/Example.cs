using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;

public class Example : MonoBehaviour
{
    public Text textOneButtonDialog;

    public Text textTwoButtonsDialogOne;

    public Text textTwoButtonsDialogTwo;

    public Text textThreeeButtonsDialogOne;

    public Text textThreeeButtonsDialogTwo;

    public Text textThreeeButtonsDialogThree;

    public InputField inputTextTitleNotification;

    public InputField inputTextMessageNotification;

    public InputField inputTextTickerNotification;

    public InputField inputTextTimeNotification;

    public Text upTimeText;

    public GameObject[] objectsToHideOnIOS;

    private void Start()
    {
        // Before sending the 1'st local notification,
        // you need to prompt user to allow sending local notifications on iOS 8.
        //
        // "Apps that use local or push notifications must explicitly register the types of alerts
        // that they display to users by using a UIUserNotificationSettings object.
        // This registration process is separate from the process for registering remote notifications,
        // and users must grant permission to deliver notifications through the requested options."
        //
        // From the section about UIKit Framework
        // https://developer.apple.com/library/prerelease/ios/releasenotes/General/WhatsNewIniOS/Articles/iOS8.html
        NativePlugin.RegisterForNotifications();

        // Get initial device up time
        UpdateUptime(upTimeText);

        // Remove buttons that are not used for iOS
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            foreach (var gameObject in objectsToHideOnIOS)
            {
                DestroyObject(gameObject);
            }
        }
    }

    public void ShowToast(UnityEngine.UI.InputField inputField)
    {
        NativePlugin.instance.ShowToast(inputField.text);
    }

    public void ImmersiveModeOn()
    {
        NativePlugin.instance.TurnImmersiveModeOn();
    }

    public void ImmersiveModeOff()
    {
        NativePlugin.instance.TurnImmersiveModeOff();
    }

    public void Vibrate(UnityEngine.UI.InputField inputField)
    {
        long milliseconds = (inputField.text == "") ? 0 : long.Parse(inputField.text);
        NativePlugin.instance.Vibrate(milliseconds);
    }

    public void VibrateCancel()
    {
        NativePlugin.instance.VibrateCancel();
    }

    public void ShowNotification()
    {
        int seconds = (inputTextTimeNotification.text == "") ? 0 : int.Parse(inputTextTimeNotification.text);
        NativePlugin.instance.ShowNotification(inputTextMessageNotification.text, seconds, inputTextTitleNotification.text, inputTextTickerNotification.text);
    }

    public void HideNotification()
    {
        NativePlugin.instance.CancelNotification();
    }

    public void UpdateUptime(UnityEngine.UI.Text text)
    {
        text.text = NativePlugin.instance.GetUptime().ToString();
    }

    public void ShowDialogOne()
    {
		NativePlugin.instance.ShowDialog("1-button dialog", "Are you satisfied with this dialog?", "There is no choice", () => {
			NativePlugin.instance.ShowDialog("Accepted!", "You've pressed a button", "Ok", null);
			textOneButtonDialog.text = (int.Parse(textOneButtonDialog.text) + 1).ToString();
		});
    }

    public void ShowDialogTwo()
    {
        NativePlugin.instance.ShowDialog("2-buttons dialog", "Are you satisfied with this dialog?", "Nope", "Yes", () => {
			NativePlugin.instance.ShowDialog("Accepted!", "Why no?", "Ok", null);
			textTwoButtonsDialogOne.text = (int.Parse(textTwoButtonsDialogOne.text) + 1).ToString();
		}, () => {
			NativePlugin.instance.ShowDialog("Accepted!", "Pleased to know it ^^", "Ok", null);
			textTwoButtonsDialogTwo.text = (int.Parse(textTwoButtonsDialogTwo.text) + 1).ToString();
		});
    }

    public void ShowDialogThree()
    {
        NativePlugin.instance.ShowDialog("3-buttons dialog", "Are you satisfied with this dialog?", "Nope", "Maybe", "I am happy", () => {
			NativePlugin.instance.ShowDialog("Accepted!", "Oh no!", "Ok", null);
			textThreeeButtonsDialogOne.text = (int.Parse(textThreeeButtonsDialogOne.text) + 1).ToString();
		}, () => {
			NativePlugin.instance.ShowDialog("Accepted!", "You are still in doubt", "Ok", null);
			textThreeeButtonsDialogTwo.text = (int.Parse(textThreeeButtonsDialogTwo.text) + 1).ToString();
		}, () => {
			NativePlugin.instance.ShowDialog("Accepted!", "You are happy!", "That's right!", null);
			textThreeeButtonsDialogThree.text = (int.Parse(textThreeeButtonsDialogThree.text) + 1).ToString();
		});
    }

    public void ShowRateApp()
    {
        NativePlugin.instance.ShowRateMe();
    }

}
