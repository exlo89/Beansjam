using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

    /*  Speichert die Optionswerte. Dazu werden Schlüsselbezeichnungen verwendet, die unter 
        den Schlüssel dann als Float, Int, oder String Wert gespeichert wird.
    */

    const string MASTER_VOLUME_KEY = "master_volume";
    const string DIFF_KEY = "difficulty";
    const string LEVEL_KEY = "level_unlocked_";

    //========================================Setter==================================================================

    //Setter für die Gesamtlautstärker 

    public static void SetMasterVolume(float volume) {
        if (volume >= 0f && volume <= 1f) {						//kontrolliert ob der Wert zwischen 1 und 0 liegt
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        } else {
            Debug.LogError("Mastervolume out of range");
        }
    }

	//Setter für Schwierigkeit

    public static void SetDifficulty(float difficulty)
    {
        if (difficulty >= 1f && difficulty <= 3f)
        {
            PlayerPrefs.SetFloat(DIFF_KEY, difficulty);
        }
        else {
            Debug.LogError("Difficulty out of range");
        }
    }

    //========================================Getter=======================================================================

    public static float GetMasterVolume() {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }

    public static float GetDifficulty() {
        return PlayerPrefs.GetFloat(DIFF_KEY);
    }

    //=======================================================================================================

/*
    public static void UnlockLevel(int level) {
        if(level <= Application.levelCount - 1) {
            PlayerPrefs.SetInt(LEVEL_KEY + level.ToString(), 1);
        } else {
            Debug.LogError("Trying to unlock level not in build order");
        }
    }

    public static bool IsLevelUnlocked(int level) {
        int levelValue = PlayerPrefs.GetInt(LEVEL_KEY + level.ToString());
        bool isLevelUnlocked = (levelValue == 1);
        if(level <= Application.levelCount - 1) {
            return isLevelUnlocked;
        } else {
            Debug.LogError("Trying to unlock level not in build order");
            return false;
        }
    }
	*/
}
