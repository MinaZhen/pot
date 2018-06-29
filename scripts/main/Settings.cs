using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {

    [HideInInspector] public static float volum = 0.2f;
    [HideInInspector] public static int tema = 1;
    [HideInInspector] public static bool mute = false;

    [HideInInspector] public static float volumFX = 1f;
    [HideInInspector] public static bool muteFX = false;

    [HideInInspector]  public static int totalScore, lvlScore = 0;

    public AudioClip[] fx;
    public AudioClip[] music;
}
