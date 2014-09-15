using UnityEngine;
using System.Collections;

public class CloseWindow : WindowWidget {

	public float closeDelay = 0f;

	void OnClick ()
	{
		StartCoroutine (DoCloseWindow());
	}

	IEnumerator DoCloseWindow () {
		yield return new WaitForSeconds(closeDelay);
		window.Hide ();
	}
}
