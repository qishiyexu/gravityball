using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Handles the show/hide and other FX of HUD panels.
/// </summary>
public class HUDFX : MonoBehaviour
{

	public bool moveTo;
	public Vector3 offsetPosition;
	private Vector3 originalPosition;

	public delegate void HUDFXEventHandler ();

	public event HUDFXEventHandler hidden;
	
	/// <summary>
	/// Whether to show this panel on start.
	/// </summary>
	public bool showOnStart = false;

	public EaseType showEaseType = EaseType.easeOutBack;
	public float showEaseTime = 0.3f;
	
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

	public float _autoHideDelay = 1f;
	
	/// <summary>
	/// The window will fade out when hiding.
	/// </summary>
	public bool isFade = true;
		
	private bool willAutoHide;
	private float countdown;
	private bool willDeactivate = false;
	private List<UIWidget> mUIWidgets;
	private Vector3 mScale;
	
	void Awake ()
	{
		willAutoHide = autoHideDelay > 0;
		mScale = transform.localScale; 
		mUIWidgets = new List<UIWidget> (GetComponentsInChildren<UIWidget> ());
		originalPosition = transform.localPosition;
	}
	
	void Start ()
	{
		if (showOnStart) {
			Show ();
		} else {
			willDeactivate = true;
		}
	}
	
	void Update ()
	{
		if (willDeactivate) {
			if (iTween.Count (gameObject) == 0) {
				gameObject.SetActive (false);
				if (hidden != null)
					hidden ();
			}
		} else {
			if (willAutoHide) {
				countdown -= Time.deltaTime;
				if (countdown < 0) {
					Hide ();
				}
			} 
			if (hideOnClick && Input.GetMouseButtonDown (0)) {
				Hide ();					
			}
		} 

	}
	
	/// <summary>
	/// Show this instance.
	/// </summary>
	public void Show ()
	{
		willDeactivate = false;
		countdown = autoHideDelay;

		if (moveTo) {
			Vector3 newPosition = transform.localPosition + offsetPosition;
			gameObject.MoveTo (originalPosition + offsetPosition, true, showEaseTime, 0, showEaseType);
		}
		else {
			transform.localScale = Vector3.one * 0.01f;
			gameObject.ScaleTo (mScale, showEaseTime, 0, showEaseType);
		}
		if (isFade) {
			foreach (UIWidget w in mUIWidgets) {
				TweenAlpha.Begin (w.gameObject, 0.3f, 1f);
			}
		}
		gameObject.SetActive (true);
	}
	
	/// <summary>
	/// Show the specified delay.
	/// </summary>
	/// <param name='delay'>
	/// Delay.
	/// </param>
	public void Show (float delay)
	{
		autoHideDelay = delay;
		Show ();
	}
	
	/// <summary>
	/// Hide this instance.
	/// </summary>
	public void Hide ()
	{
		if (!gameObject.activeSelf)
			return;
	
		gameObject.ScaleTo (Vector3.one * 0.01f, 0.2f, 0, EaseType.easeInQuad);
		if (isFade) {
			foreach (UIWidget w in mUIWidgets) {
				TweenAlpha.Begin (w.gameObject, 0.1f, 0f);
			}
		}
		willDeactivate = true;
	}
}
