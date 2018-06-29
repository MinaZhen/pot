using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {

    [HideInInspector] public int _scoreUI = 0;

    private Text scoreText;

	void Start () {
        scoreText = this.gameObject.GetComponent<Text>();
        if (scoreText == null) Debug.Log("404: scoreText in ScoreUI");
	}
	
	void Update () {
        scoreText.text = _scoreUI.ToString("000000");
    }
}
