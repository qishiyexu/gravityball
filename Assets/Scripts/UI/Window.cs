using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// UI Window.
/// </summary>
//[RequireComponent(typeof(HUDFX))]
public class Window : MonoBehaviour
{
	public static List<Window> allWindows = new List<Window> ();
	public static List<Window> activeWindows = new List<Window> ();
	
	public delegate void WindowEventHandler ();

	public static event WindowEventHandler anyShown;
	public static event WindowEventHandler anyHidden;
	public event WindowEventHandler shown;
	public event WindowEventHandler hidden;

	public bool useCurtain = true;
	
	/// <summary>
	/// Whether any window is showing
	/// </summary>
	public static bool isAnyShow {
		get {
			return allWindows.Exists (w => w.isShow);
		}
	}
	
//	[System.NonSerializedAttribute]
//	public bool isShow = false;
	private bool isShow;
//	[System.NonSerializedAttribute]
	//public HUDFX mFX;

	public bool showOnStart = true;

	/// <summary>
	/// The window will fade out when hiding.
	/// </summary>
	public bool isFade = true;

	/// <summary>
	/// Whether to hide on click.
	/// </summary>
	public bool hideOnClick = false;

	/// <summary>
	/// The auto hide delay. Disabled if set to 0.
	/// </summary>
	public float autoHideDelay {
		get { return _autoHideDelay; }
		set { 
			_autoHideDelay = value; 
			willAutoHide = value > 0;
		}
	}
	public float _autoHideDelay = 0f;

	public bool exclusive = true;

	private List<UIWidget> widgets;
	private bool willAutoHide;
	private float countdown;
	private bool willDeactivate = false;

	private float hideWindowTime = 0.5f;
	private float showDelay = 0f;
	private bool showFlag = true;

	protected void Awake ()
	{
		allWindows.Add (this);
		willAutoHide = autoHideDelay > 0;
	//	mFX = GetComponent<HUDFX> ();
	//	if (mFX == null)
	//		mFX = gameObject.AddComponent<HUDFX> ();
	//	mFX.hidden += Hidden;

		widgets = new List<UIWidget> (GetComponentsInChildren<UIWidget> ());
		isShow = showOnStart;
	}

	void Start () {
		if (showOnStart) {
			Show ();
		} else {
			gameObject.SetActive (false);
			//Hide ();
		//	willDeactivate = true;
			//gameObject.SetActive(false);
		}
	}

	void Update () {
/*		if (willDeactivate) {
			if (iTween.Count (gameObject) == 0) {
				if (hidden != null)
					hidden ();
		//		gameObject.SetActive (false);
			}
		} else {

		} 
		*/	
		if (willAutoHide) {
			countdown -= Time.deltaTime;
			if (countdown < 0) {
				Hide ();
			}
		} 
		if (hideOnClick && Input.GetMouseButtonDown (0)) {
			Hide ();					
		}

		if (showFlag) {
			showDelay -= Time.deltaTime;
			if (showDelay < 0f) {
				ShowWindow ();
				showFlag = false;
			}
		}
	}
	
	protected void OnDestroy ()
	{
		allWindows.Remove (this);
	//	mFX.hidden -= Hidden;
	}
	
	public void Show () {
		if (exclusive && activeWindows.Count > 0) {
			showDelay = hideWindowTime;
			showFlag = true;
			HideAllWindows ();
			gameObject.SetActive(true);
		}
		else {
			ShowWindow ();
		}
		activeWindows.Add (this);

	}

	private void ShowWindow () {
		willDeactivate = false;
		countdown = autoHideDelay;
		isShow = true;
	//	mFX.Show ();
		if (shown != null)
			shown ();
		if (anyShown != null)
			anyShown ();

		if (useCurtain)
			Singleton<WindowCurtain>.Instance.FadeIn ();

	//	if (isFade) {
	//		foreach (UIWidget w in widgets) {
	//		TweenAlpha.Begin (w.gameObject, 0.3f, 1f);
	//		}
	//	}
		gameObject.SetActive (true);		
	}



	
	public void Hide () {
		activeWindows.Remove (this);
		if (!gameObject.activeSelf)
			return;
	//	mFX.Hide ();
		if (useCurtain)
			Singleton<WindowCurtain>.Instance.FadeOut ();

		

		if (hidden != null)
			hidden ();

	//	if (isFade) {
	//		foreach (UIWidget w in widgets) {
	//			TweenAlpha.Begin (w.gameObject, 0.1f, 0f);
	//		}
	//	}

	//	willDeactivate = true;
		isShow = false;
	}

	public static void HideAllWindows () {
		foreach (Window w in allWindows) {
			w.Hide ();
			activeWindows.Clear ();
		}	
	}
	
/*	public IEnumerator Hidden () {
		isShow = false;
		if (hidden != null)
			hidden ();
		if (anyHidden != null)
			anyHidden ();
		yield return new WaitForSeconds(1);
	}
	*/
}
