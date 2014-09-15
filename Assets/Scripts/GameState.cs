using UnityEngine;
using System.Collections;

public class GameState {

	private static GameState instance = null;

	public static GameState GetInstance() {
		if (instance == null) {
			instance = new GameState();
			instance.setGameState(GAME_STATE_NONE);
		}
		return instance;
	}

	public static int GAME_STATE_NONE = 0x0;
	public static int GAME_STATE_BEGIN = 0x1;
	public static int GAME_STATE_PAUSE = 0x2;
	public static int GAME_STATE_END = 0x3;

	private int gameState;

	public int getGameState() {
		return gameState;
	}
	public void setGameState(int gs) {
		gameState = gs;
	}
}
