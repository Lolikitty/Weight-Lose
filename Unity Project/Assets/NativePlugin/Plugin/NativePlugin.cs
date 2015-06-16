using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class NativePlugin : MonoBehaviour
{

    private const string debugLogTag = "[Native Plugin] ";

    static public NativePlugin instance { private set; get; }

    private Action DialogNativeOnClickOk;

    private Action DialogNativeOnClickCancel;

    private Action DialogNativeOnClickMaybe;

    public bool immersiveMode = true;

    public string rateAppTitle = "Our Great Application";

    public string rateAppMesage = "If you enjoy our game, would you mind taking a moment to rate it? It won't take more than a minute. Thanks for your support!";

    public string rateAppButtonRate = "Rate";

    public string rateAppButtonLater = "Remind me later";

    public string rateAppButtonNever = "No, thanks";

    public string appSoreAppID = "";

    private int emulationUpTime {
		get { return (int)Time.realtimeSinceStartup; }
	}

#if UNITY_ANDROID && !UNITY_EDITOR
    private AndroidJavaObject androidJavaObject;
#endif


    #region Unity3d callbacks
    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        androidJavaObject = new AndroidJavaObject("net.furylion.nativeunityplugin.UnityPlugin");
#endif
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (immersiveMode)
            TurnImmersiveModeOn();
    }

    void OnApplicationFocus(bool focusStatus)
    {
        if (focusStatus && immersiveMode)
            TurnImmersiveModeOn();
    }
    #endregion

    #region Handlers clicked buttons on native dialogs
    private void NativeDialogOnClickOk()
    {
        if (DialogNativeOnClickOk == null)
            return;

        DialogNativeOnClickOk();
        ResetActionsDialog();
    }

    private void NativeDialogOnClickCancel()
    {
        if (DialogNativeOnClickCancel == null)
            return;

        DialogNativeOnClickCancel();
        ResetActionsDialog();
    }

    private void NativeDialogOnClickMaybe()
    {
        if (DialogNativeOnClickMaybe == null)
            return;

        DialogNativeOnClickMaybe();
        ResetActionsDialog();
    }

    private void ResetActionsDialog()
    {
        DialogNativeOnClickOk = null;
        DialogNativeOnClickCancel = null;
        DialogNativeOnClickMaybe = null;
    }
    #endregion

    #region Dialogs
    /// <summary>
    /// Shows the 1-buton dialog.
    /// </summary>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Message body.</param>
    /// <param name="textButton">Button text.</param>
    /// <param name="onClickButton">On button click action.</param>
    public void ShowDialog(string title, string message, string textButton, Action onClickButton)
    {
        DialogNativeOnClickOk += onClickButton;
#if UNITY_ANDROID && !UNITY_EDITOR
        CallAndroidMethod("showAlertDialogOk", gameObject.name, "NativeDialogOnClickOk", title, message, textButton);
#elif UNITY_IPHONE && !UNITY_EDITOR
		_ShowAlertView(title, message, textButton, gameObject.name);
#elif UNITY_EDITOR
        Debug.Log(debugLogTag + "Dialogs can be shown only on a real device or an emulator");
#endif
    }

    /// <summary>
    /// Shows the 2-butons dialog.
    /// </summary>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Message body.</param>
	/// <param name="textCancelButton">Left (cancel) button text.</param>
	/// <param name="textOkButton">Right (ok) button text.</param>
	/// <param name="onClickCancelButton">On left button click action.</param>
	/// <param name="onClickOkButton">On right button click action.</param>
    public void ShowDialog(string title, string message, string textCancelButton, string textOkButton, Action onClickCancelButton, Action onClickOkButton)
    {
		DialogNativeOnClickOk += onClickOkButton;
		DialogNativeOnClickCancel += onClickCancelButton;
#if UNITY_ANDROID && !UNITY_EDITOR
		CallAndroidMethod("showAlertDialogOkCancel", gameObject.name, "NativeDialogOnClickCancel", "NativeDialogOnClickOk", title, message, textCancelButton, textOkButton);
#elif UNITY_IPHONE && !UNITY_EDITOR
		_ShowYesNoAlertView(title, message, textOkButton, textCancelButton, gameObject.name);
#elif UNITY_EDITOR
        Debug.Log(debugLogTag + "Dialogs can be shown only on a real device or an emulator");
#endif
    }

    /// <summary>
    /// Shows the 3-buttons dialog.
    /// </summary>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Message body.</param>
	/// <param name="textCancelButton">Left (cancel) button text.</param>
	/// <param name="textMiddleButton">Middle (other) button text.</param>
	/// <param name="textOkButton">Right (ok) button text.</param>
	/// <param name="onClickCancelButton">On left button click action.</param>
	/// <param name="onClickMiddleButton">On middle button click action.</param>
	/// <param name="onClickOkButton">On right button click action.</param>
	public void ShowDialog(string title, string message, string textCancelButton, string textMiddleButton, string textOkButton, Action onClickCancelButton, Action onClickMiddleButton, Action onClickOkButton)
    {
		DialogNativeOnClickOk += onClickOkButton;
		DialogNativeOnClickMaybe += onClickMiddleButton;
		DialogNativeOnClickCancel += onClickCancelButton;
#if UNITY_ANDROID && !UNITY_EDITOR
		CallAndroidMethod("showAlertDialogOkMaybeCancel", gameObject.name, "NativeDialogOnClickCancel", "NativeDialogOnClickMaybe", "NativeDialogOnClickOk", title, message, textCancelButton, textMiddleButton, textOkButton);
#elif UNITY_IPHONE && !UNITY_EDITOR
		_ShowYesNoOtherAlertView(title, message, textOkButton, textCancelButton, textMiddleButton, gameObject.name);
#elif UNITY_EDITOR
        Debug.Log(debugLogTag + "Dialogs can be shown only on a real device or an emulator");
#endif
    }

    /// <summary>
    /// Shows the Rate Me dialog.
    /// Please, set parameters of scene's NativePlugin object before calling this method.
    /// </summary>
    public void ShowRateMe()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        CallAndroidMethod("showRateMe", rateAppTitle, rateAppMesage, rateAppButtonRate, rateAppButtonLater, rateAppButtonNever);
#elif UNITY_IPHONE && !UNITY_EDITOR
        _ShowRateMe(appSoreAppID, rateAppTitle, rateAppMesage, rateAppButtonRate, rateAppButtonLater, rateAppButtonNever);
#elif UNITY_EDITOR
        Debug.Log(debugLogTag + "Rate me dialogs work only on a real device or an emulator");
#endif
    }
    #endregion

    #region Local notification
    /// <summary>
    /// Ask user permission to show local notifications on iOS 8.
    /// </summary>
    /// <remarks>
    /// "Apps that use local or push notifications must explicitly register the types of alerts
    /// that they display to users by using a UIUserNotificationSettings object.
    /// This registration process is separate from the process for registering remote notifications,
    /// and users must grant permission to deliver notifications through the requested options."
    /// From the section about UIKit Framework <see cref="https://developer.apple.com/library/prerelease/ios/releasenotes/General/WhatsNewIniOS/Articles/iOS8.html"/>
    /// </remarks>
    public static void RegisterForNotifications()
    {
#if UNITY_IPHONE && !UNITY_EDITOR
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			_EnableLocalNotificationIOS8();
#elif UNITY_EDITOR
        Debug.Log(debugLogTag + "Notifications enabler works only on a real iOS device");
#endif
    }

    /// <summary>
    /// Shows local notification.
    /// </summary>
    /// <param name="message">Message body.</param>
    /// <param name="seconds">Time before notification expire.</param>
    /// <param name="title">Notification title. Note: ignored on iOS devices.</param>
    /// <param name="ticker">Ticker. Note: ignored on iOS devices.</param>
    public void ShowNotification(string message, int seconds = 0, string title = "", string ticker = "")
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        CallAndroidMethod("showNotification", message, seconds, title, ticker, 0);
#elif UNITY_IPHONE && !UNITY_EDITOR
        var notify = new LocalNotification();
        notify.fireDate = System.DateTime.Now.AddSeconds(seconds);
        notify.alertBody = message;
        NotificationServices.ScheduleLocalNotification(notify);
#elif UNITY_EDITOR
        Debug.Log(debugLogTag + "Local notifications work only on a real device or an emulator");
#endif
    }

    /// <summary>
    /// Cancel scheduled local notifications.
    /// </summary>
    public void CancelNotification()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        CallAndroidMethod("cancelNotification");
#elif UNITY_IPHONE && !UNITY_EDITOR
        NotificationServices.CancelAllLocalNotifications();
#elif UNITY_EDITOR
        Debug.Log(debugLogTag + "Local notifications work only on a real device or an emulator");
#endif
    }
    #endregion

    #region Device up time
    /// <summary>
    /// Gets the device up time.
    /// </summary>
    /// <returns>The device uptime in seconds. In unity editor return NativePluginSettings.emulationUpTime</returns>
    public int GetUptime()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return CallAndroidMethodReturnInt("getUptime");
#elif UNITY_IPHONE && !UNITY_EDITOR
        return (int)_UpTime ();
#else
        return emulationUpTime;
#endif
    }
    #endregion

    #region Vibrator
    /// <summary>
    /// Start device vibration for the specified time in milliseconds.
    /// </summary>
    /// <remarks>
    /// iOS devices has only one way to vibrate — wait for 0.1 second and vibrate for 0.4 second.
    /// </remarks>
    /// <param name="milliSeconds">Vibration time in milliseconds. Note: ignored on iOS devices.</param>
    public void Vibrate(long milliseconds)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
		CallAndroidMethod("vibrate", milliseconds);
#elif !UNITY_EDITOR
        Handheld.Vibrate();
#elif UNITY_EDITOR
        Debug.Log(debugLogTag + "Vibration works only on a real device");
#endif
    }

    /// <summary>
    /// Cancels the vibration.
    /// </summary>
    /// <remarks>
    /// Android only.
    /// </remarks>
    public void VibrateCancel()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        CallAndroidMethod("vibrateCancel");
#elif UNITY_EDITOR
        Debug.Log(debugLogTag + "Vibration works only on a real device");
#endif
    }
    #endregion

    #region Toast (Android only)
    /// <summary>
    /// Shows Toast.
    /// </summary>
    /// <remarks>
    /// Android only.
    /// </remarks>
    /// <param name="message">Message.</param>
    /// <param name="duration">Showing duration.</param>
    public void ShowToast(string message, toastLength duration = toastLength.LENGTH_SHORT)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        duration = (duration > toastLength.LENGTH_LONG || duration < toastLength.LENGTH_SHORT) ? toastLength.LENGTH_SHORT : duration;
        CallAndroidMethod("showToast", message, (int)duration);
#elif UNITY_EDITOR
        Debug.Log(debugLogTag + "Toasts work only on a real device or an emulator");
#endif
    }
    #endregion

    #region Immersive mode
    /// <summary>
    /// Turns the android immersive mode on.
    /// </summary>
    /// <remarks>
    /// Android only.
    /// </remarks>
    public void TurnImmersiveModeOn()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        immersiveMode = true;
        CallAndroidMethod("turnImmersiveModeOn");
#elif UNITY_EDITOR
        Debug.Log(debugLogTag + "Immersive mode works only on a real device or an emulator (Android 4.4+)");
#endif
    }

    /// <summary>
    /// Turns the immersive mode off.
    /// </summary>
    /// <remarks>
    /// Android only.
    /// </remarks>
    public void TurnImmersiveModeOff()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        immersiveMode = false;
        CallAndroidMethod("turnImmersiveModeOff");
#elif UNITY_EDITOR
        Debug.Log(debugLogTag + "Immersive mode works only on a real device or an emulator (Android 4.4+)");
#endif
    }
    #endregion


    #region Import native iOS functions
#if UNITY_IPHONE && !UNITY_EDITOR
    // Import native iOS functions
    [DllImport("__Internal")]
    private static extern void _EnableLocalNotificationIOS8();

    [DllImport("__Internal")]
    private static extern void _ShowRateMe(string appID, string title, string message, string rateButtonTitle, string laterButtonTitle, string neverButtonTitle);

    [DllImport("__Internal")]
	private static extern void _ShowAlertView(string title, string message, string okButtonTitle, string callbackObjectName);

    [DllImport("__Internal")]
	private static extern void _ShowYesNoAlertView(string title, string message, string okButtonTitle, string cancelButtonTitle, string callbackObjectName);

    [DllImport("__Internal")]
	private static extern void _ShowYesNoOtherAlertView(string title, string message, string okButtonTitle, string cancelButtonTitle, string otherButtonTitle, string callbackObjectName);

    [DllImport("__Internal")]
    private static extern uint _UpTime();
#endif
    #endregion

    #region Calling android native methods
    private int CallAndroidMethodReturnInt(string methodName, params object[] args)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return androidJavaObject.Call<int>(methodName, args);
#else
        return int.MinValue;
#endif
    }

    private void CallAndroidMethod(string methodName, params object[] args)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        androidJavaObject.Call(methodName, args);
#endif
    }
    #endregion

    private void CallbackFunction(string buttonIndex)
    {
        int index = int.Parse(buttonIndex);

        switch (index)
        {
		case 0: NativeDialogOnClickOk(); break;
		case 1: NativeDialogOnClickCancel(); break;
		case 2: NativeDialogOnClickMaybe(); break;
            default: break;
        }
    }

}

/// <summary>
/// Toast duration
/// </summary>
public enum toastLength
{
    LENGTH_LONG = 1,    // 3.5 seconds
    LENGTH_SHORT = 0    // 2 seconds
}