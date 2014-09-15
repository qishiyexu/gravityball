using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

	public List<Clip> clips;
	
	[System.Serializable]
	public class Clip {
		public string key;
		
		public AudioClip audioClip;
	}

	public AudioSource repeatSpeaker;
	[HideInInspector]
	public bool repeatSpeakerPlay = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public AudioClip GetClip (string key) {
		AudioClip ac = clips.Find (c => c.key == key).audioClip;
		if (ac == null) {
			Debug.LogError ("cannot find audio clip with key " + key);
			return null;
		} else {
			return ac;
		}
	}
	
	public void PlayClip (string clip) {
		audio.PlayOneShot (GetClip (clip));
	}
	
	public void PlayClip (AudioClip clip) {
		audio.PlayOneShot (clip);
	}

	private IEnumerator ReplayAfterDelay (AudioClip music, float delay) {
		repeatSpeaker.clip = music;
		repeatSpeaker.PlayDelayed(delay);
		while (repeatSpeakerPlay) {
			repeatSpeaker.Play ();
			yield return new WaitForSeconds (music.length + delay);
		}
	}

	public void PlayRepeat(string key) {
		AudioClip clip = GetClip (key);
		StartCoroutine (ReplayAfterDelay(clip, 0));
	}

	public void StopRepeat () {
		repeatSpeakerPlay = false;
	}
}
