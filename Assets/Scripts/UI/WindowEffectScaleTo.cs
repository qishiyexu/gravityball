using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Window))]
public class WindowEffectScaleTo : MonoBehaviour {

	public EaseType showEaseType = EaseType.easeOutBack;
	public float showEaseTime = 0.3f;

	public EaseType hideEaseType = EaseType.easeInQuad;
	public float hideEaseTime = 0.3f;


	private Window window;

	private Vector3 originalScale;

	void Awake () {
		originalScale = transform.localScale;
		window = GetComponent<Window>();
		if (window == null)
			window = gameObject.AddComponent<Window>();
		window.shown += Show;
		window.hidden += Hide;
	}

	void OnDestroy () {
		window.shown -= Show;
		window.hidden -= Hide;
	}
	

	public void Show () {			
		transform.localScale = Vector3.one * 0.01f;
		gameObject.ScaleTo (originalScale, showEaseTime, 0, showEaseType);
	}

	public void Hide () {
		gameObject.ScaleTo (Vector3.one * 0.01f, 0.3f, 0, EaseType.easeInQuad);
	}
}
