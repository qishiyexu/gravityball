using UnityEngine;
using System.Collections;

public class UIActions : MonoBehaviour {

	public Window bottomFunc;
	public Window timePanel;
	public Window settingWindow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TestButtonClicked () {
		GameState gs = GameState.GetInstance();
		gs.setGameState(GameState.GAME_STATE_BEGIN);
		bottomFunc.Hide();
		StartCoroutine(delayOpenWindow(0.1f, timePanel));
	}

	public void TrainButtonClicked () {
		Debug.Log("train button clicked");
	}

	public void SettingButtonClicked() {
		settingWindow.Show();
	}

	public void ExitButtonClicked() {
		Application.Quit();
	}

	IEnumerator delayOpenWindow (float delay, Window window) {
		yield return new WaitForSeconds(delay);
		window.Show ();
	}
}
