using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Labyrinth : MonoBehaviour {

    private GenerateLab gen;
    private Loading load;
    private Canvas controls;

    private bool initialCheck = true;

    void Start () {

        controls = GameObject.Find("Controls").GetComponent<Canvas>();
        if (controls == null) Debug.LogError("404: controls in labyrinth");
        controls.renderMode = RenderMode.WorldSpace;

        load = GameObject.Find("Loading").GetComponent<Loading>();
        gen = this.gameObject.GetComponent<GenerateLab>();

        if (GameObject.Find("Prota_01") != null) {
            this.gameObject.AddComponent<Put>();
        } 
	}
	
	void Update () {
        
        if (initialCheck) {
            if (load == null) {
                controls.renderMode = RenderMode.ScreenSpaceOverlay;
                Destroy(gen);
                initialCheck = false;
            }
        }
        
	}
}
