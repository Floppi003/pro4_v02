using UnityEngine;
using System.Collections.Generic;
using System.Timers;

public class AudioManager : Object {

	private static AudioManager instance;
	private static Queue<AudioClip> audioQueue;
	private static AudioSource audioSource;

	private AudioManager() {
		audioSource = GameObject.Find ("Player").GetComponent<AudioSource> ();
		audioQueue = new Queue<AudioClip> ();

		if (audioSource == null) {
			Debug.LogError ("AudioManager: AudioSource could not be loaded from Player gameObject!");
		}
	}	

	public static AudioManager getInstance() {
		if (instance == null) {
			instance = new AudioManager();
		}

		return instance;
	}

	public void testAudioManager() {
		Debug.Log ("Test Audio Manager executed");
	}

	public void queueAudioClip(AudioClip audioClip) {

		// Queue the audioSource
		audioQueue.Enqueue(audioClip);

		// calculate time for next audio playback
		AudioClip[] audioClips = audioQueue.ToArray();
		float totalWaitingTime = 0.0f; // seconds
		foreach (AudioClip ac in audioClips) {
			totalWaitingTime += ac.length;
		}
			
		Debug.Log ("totalWaitingTime: " + totalWaitingTime);
		Timer audioTimer = new Timer(totalWaitingTime);
		audioTimer.Elapsed += new ElapsedEventHandler (playNextClipInQueue);
	}

	public void playAudioClipIfFree(AudioClip audioClip) {
		
	}

	public void playAudioClipOnceOnly(AudioClip audioCip) {

	}


	private void playNextClipInQueue(object source, ElapsedEventArgs args) {
		Debug.Log ("Playing next shot out of queeu");
		audioSource.PlayOneShot (audioQueue.Dequeue ());
	}
}
