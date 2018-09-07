using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

    public AudioClip[] levelMusicChangeArray;
    private AudioSource audioSource;
	private static MusicPlayer instance;
	private bool firstObject;

	//Verhindert das Löschen des Music Players beim wechseln der Szenen.
    void Awake() {
		if (!instance){
			firstObject = true;
			instance = this;
		} else {
			firstObject = false;
			Destroy(gameObject);
		}
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        audioSource = GetComponent<AudioSource>();                  //Erstellt eine Audiosource Komponente im Objekt
        audioSource.volume = PlayerPrefsManager.GetMasterVolume();  //setzt die Lautstärke der Musik auf die gespeicherte Lautstärke
		audioSource.clip = levelMusicChangeArray[0];
		audioSource.loop = true;
		audioSource.Play();
	}

	
    void OnLevelWasLoaded(int level) {
		if (firstObject) {
			AudioClip thisLevelMusic = levelMusicChangeArray[level];
			if (thisLevelMusic) {
				audioSource.clip = thisLevelMusic;
				audioSource.loop = true;
				audioSource.Play();
			}
		}
    }
	

	public void SetVolume(float volume) {
        audioSource.volume = volume;
    }
}
