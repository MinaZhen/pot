using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [HideInInspector] public int pels, colorPels = 0;
    [HideInInspector] public string nombre = "Prota";

    void Awake () {
        DontDestroyOnLoad(this);
	}
}
