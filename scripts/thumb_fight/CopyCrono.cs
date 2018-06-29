using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyCrono : MonoBehaviour {

    private Text cronoTxt;
    private Crono crono;

    // Use this for initialization
    void Start () {
        cronoTxt = this.GetComponent<Text>();                                           // Para acceder hacer gameobject find este script va al texto
        if (cronoTxt == null) Debug.LogError("404: cronoTxt in CopyCrono");

        crono = GameObject.Find("Crono").GetComponent<Crono>();
        if (crono == null) Debug.LogError("404: crono in CopyCrono");
    }
	
	// Update is called once per frame
	void Update () {
        CronoClon();
    }

    void CronoClon() {
        float minutes = Mathf.Floor(crono.secs / 60);
        float seconds = crono.secs % 60;
        cronoTxt.text = (minutes.ToString("00") + ":" + seconds.ToString("00.000"));
    }
}
