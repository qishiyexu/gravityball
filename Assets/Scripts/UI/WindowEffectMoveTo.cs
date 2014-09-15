using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Window))]
public class WindowEffectMoveTo : MonoBehaviour {

	public EaseType showEaseType = EaseType.easeOutBack;
	public float showEaseTime = 0.3f;

	public EaseType hideEaseType = EaseType.easeInQuad;
	public float hideEaseTime = 0.3f;

	public Vector3 moveOffset;

	private Window window;

	private Vector3 originalPosition;

	void Awake () {
		originalPosition = transform.localPosition;
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
		gameObject.MoveTo (originalPosition+moveOffset, true, showEaseTime, 0, showEaseType);
	}

	public void Hide () {
	//	gameObject.ScaleTo (Vector3.one * 0.01f, hideEaseTime, 0, hideEaseType);
		gameObject.MoveTo(originalPosition, true, hideEaseTime, 0, hideEaseType);
	}
}
