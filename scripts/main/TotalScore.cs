using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour {

    private ScoreUI score;
    private Text totalScore, lvlScore;
    private int tempTot, tempSc;
    private float timer;
    [HideInInspector] public bool sc = false;

	void Start () {
        score = GameObject.Find("woodScore").GetComponent<ScoreUI>();
        lvlScore = GameObject.Find("woodScore").GetComponent<Text>();
        totalScore = GameObject.Find("woodTotalScore").GetComponent<Text>();
        score._scoreUI = Settings.lvlScore;
        tempSc = score._scoreUI;
        tempTot = Settings.totalScore;
        totalScore.text = Settings.totalScore.ToString();
	}
	
	void Update () {
		if (sc) {
            score._scoreUI -= 17;
            if (score._scoreUI < 0) score._scoreUI = 0;
            Settings.totalScore += 17;
            
            if (Settings.totalScore > (tempSc + tempTot)) {
                Settings.totalScore = (tempTot + tempSc);
            }
            if ((Settings.totalScore == (tempTot + tempSc))&& score._scoreUI == 0) {
                timer += (Time.deltaTime);
                if (timer > 2) {
                    sc = false;
                    tempSc = tempTot = 0;
                    SceneManager.LoadScene("mainMenu");
                }
            }
            lvlScore.text = score._scoreUI.ToString();
            totalScore.text = Settings.totalScore.ToString();
        }
	}
}
