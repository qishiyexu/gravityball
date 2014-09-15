using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour {

	public float xSensitivity = 1f;
	public float ySensitivity = 1f;
	public float objMoveSpeed = 1f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameState gs = GameState.GetInstance();
		if (gs.getGameState() == GameState.GAME_STATE_BEGIN) {
/*			float x = transform.position.x + Input.acceleration.x * xSensitivity;
			float z = transform.position.z + Input.acceleration.y * ySensitivity;
			Vector3 newPos = new Vector3(x, transform.position.y, z);
			transform.position = Vector3.Lerp(transform.position, newPos, Time.time * objMoveSpeed);
			*/

			Vector3 direction = Vector3.zero;
			direction.x = Input.acceleration.x * xSensitivity;
			direction.z = Input.acceleration.y * ySensitivity;
			if (direction.sqrMagnitude > 1)
				direction.Normalize();

			direction *= Time.deltaTime;

			rigidbody.AddForce(direction * objMoveSpeed, ForceMode.Force);

			if (Mathf.Abs(rigidbody.velocity.magnitude) > 0.5f) {
				if (Singleton<AudioManager>.Instance.repeatSpeakerPlay == false) {
					Singleton<AudioManager>.Instance.PlayRepeat ("friction");
				}
			}
			else {
				if (Singleton<AudioManager>.Instance.repeatSpeakerPlay)
					Singleton<AudioManager>.Instance.repeatSpeakerPlay = false;
			}
		}

		if (transform.position.y < -6f) {
			gs.setGameState(GameState.GAME_STATE_END);
		}

	}

	void OnGUI () {
		GUI.Label(new Rect(0,0,500,100),"position is " + Input.acceleration);
		GUI.Label(new Rect(0,30,500,100),"v is " + rigidbody.velocity.magnitude);
	}
}
