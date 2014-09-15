using UnityEngine;
using System.Collections;

public class OpenWindow : WindowWidget
{
	public float openDelay = 0f;

	void OnClick ()
	{
		StartCoroutine (DoOpenWindow());
	}

	IEnumerator DoOpenWindow () {
		yield return new WaitForSeconds(openDelay);
		window.Show ();
	}
}
