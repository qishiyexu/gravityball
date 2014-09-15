using UnityEngine;
using System.Collections;
using System;

public class GameData : MonoBehaviour {

	private DateTime startTime;
	private long elapseTime;

	private bool startCountTime = false;
	private bool stopCountTime = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameState gs = GameState.GetInstance();
		if (gs.getGameState() == GameState.GAME_STATE_NONE) {
			elapseTime = 0;
		}
		else if (gs.getGameState() == GameState.GAME_STATE_BEGIN) {
			if (startCountTime == false) {
				startCountTime = true;
				startTime = DateTime.Now;
				elapseTime = 0;
			}
			else {
				elapseTime = DateTime.Now.Subtract(startTime).Ticks;
			}
		}
		else {
			//
		}
	}

	public String GetElapseTimeStr () {
		if (elapseTime == 0) 
			return "00:00:00";
		else {
			TimeSpan elapseTimeSpan = new TimeSpan(elapseTime);	
			String minStr = string.Format("{0:00}", elapseTimeSpan.Minutes);
			String secStr = string.Format("{0:00}", elapseTimeSpan.Seconds);
			String milliSecStr = string.Format("{0:00}", elapseTimeSpan.Milliseconds).Substring(0,2);
			String elapseTimeStr = minStr+":"+secStr+":"+milliSecStr;
			return elapseTimeStr;
		}
	}
}
