using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour {

    private Image bar, bg;
    private Color col;
    private GameObject gO_Loading;

    public float loading = 0f;

	void Start () {
        gO_Loading = GameObject.Find("Loading");
        bar = GameObject.Find("BarFG").GetComponent<Image>();
        bg = GameObject.Find("LoadingIMG").GetComponent<Image>();
        col = bg.color;
    }
	
	void Update () {
        if (bar.fillAmount < loading) {
            bar.fillAmount = Mathf.Lerp(bar.fillAmount, loading, 0.01f);
        }

        if (bar.fillAmount > 0.99f) {
            col.a  -= 0.5f * Time.deltaTime;
            bg.color = col;
            if (bg.color.a < 0.01) Destroy(this.gameObject);
        } 
	}
}
