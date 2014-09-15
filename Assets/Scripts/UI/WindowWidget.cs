using UnityEngine;
using System.Collections;

/// <summary>
/// UI window widget.
/// </summary>
public abstract class WindowWidget : MonoBehaviour {
	/// <summary>
	/// The window this widget belongs to.
	/// </summary>
	public Window window;
	
	protected void Awake () {
		if (window == null) {
			Debug.LogWarning (gameObject.name + "'s window widget is missing settings.");
			window = GetComponent<Window> ();
			Transform t = transform;
			while (window == null && t.parent != null) {
				t = t.parent;
				window = t.GetComponent<Window> ();
			}
		}
		window.shown += OnShow;
		window.hidden += OnHide;
	}
	
	protected void OnDestroy () {
		window.shown -= OnShow;
		window.hidden -= OnHide;
	}
	
	protected virtual void OnShow () {
	}
	
	protected virtual void OnHide () {
	}
}
