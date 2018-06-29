using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crono : MonoBehaviour {

    private Text cronoTxt;

    private bool timerTrue = false;

    public float extraTime;
    public float tiempoTotal;
    public bool tiempoCreciente;

    [HideInInspector] public float secs;
    [HideInInspector] public bool startCrono = false;
    [HideInInspector] public bool stopCrono = false;
    [HideInInspector] public bool extraCrono = false;

    void Start () {
        cronoTxt = this.GetComponent<Text>();                                           
        if (cronoTxt == null) Debug.LogError("Error: Cannot create cronometer");
        if (!tiempoCreciente) secs = tiempoTotal; else secs = 0; 
    }
	
	void Update () {

        if (startCrono) { // Pausa e Inicio
            timerTrue = true;
            startCrono = false;
        }
        if (stopCrono) {    // Para Pausa
            timerTrue = false;
            stopCrono = false;
        }
        
        if (extraCrono) {   // añade tiempo extra
            if (tiempoCreciente) {
                tiempoTotal += extraTime;
            } else {
                secs += extraTime;
            }
            extraCrono = false;
        }

        Cronometer();
        CronoGUI();
        
    }

    private void Cronometer() {
        if (tiempoCreciente) {
            if (timerTrue) {
                secs += Time.deltaTime;
                if (secs >= tiempoTotal) {
                    secs = tiempoTotal;
                    timerTrue = false;
                }
            }
        } 
        if (!tiempoCreciente) {
            
            if (timerTrue) {
                
                secs -= Time.deltaTime;
                if (secs <= 0) {
                    secs = 0;
                    timerTrue = false;
                }
            }
        }
    }

    private void CronoGUI() {
        float minutes = Mathf.Floor(secs / 60);
        float seconds = secs % 60;
        cronoTxt.text = (minutes.ToString("00") + ":" + seconds.ToString("00.000"));
    }
}
