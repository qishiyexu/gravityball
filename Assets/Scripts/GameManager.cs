using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public UILabel timeLabel;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		GameState gs = GameState.GetInstance();
		if (gs.getGameState() == GameState.GAME_STATE_BEGIN) {
			string timeStr = Singleton<GameData>.Instance.GetElapseTimeStr();
			timeLabel.text = timeStr;
		}
	}
}
