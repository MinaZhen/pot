using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ThumbFight : MonoBehaviour {

    private GameObject txt1, txt2, txt3, txt4, txt5, but, woodini, prota;
    private But_next next;
    private Cards cards;
    private AudioManager aM;
    private Text tcP1, tcP2;
    private Color col, colW, colTS;
    private float timer = 0f;
    private int state, sc1, sc2 = 0;
    private bool _timer, _sc1, _sc2 = true;

	// Use this for initialization
	void Start () {
        prota = GameObject.Find("Prota_01");
        cards = GameObject.Find("Main").GetComponent<Cards>();
        but = GameObject.Find("ButNext");
        woodini = GameObject.Find("woodini");
        colW = woodini.GetComponent<Image>().color;
        next = but.GetComponent<But_next>();
        _timer = true;
        txt1 = GameObject.Find("Text1");
        txt2 = GameObject.Find("Text2");
        txt3 = GameObject.Find("Text3");
        txt4 = GameObject.Find("Text4");
        txt5 = GameObject.Find("Text5");
        col = txt5.GetComponent<Image>().color;
        col.a = 0f;
        tcP1 = GameObject.Find("tsP1").GetComponent<Text>();
        tcP2 = GameObject.Find("tsP2").GetComponent<Text>();
        colTS = tcP1.color;
        colTS.a = 0f;

        aM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        aM.Play(2, 2f);

        _sc1 =_sc2 = false;

        txt1.SetActive(false);
        txt2.SetActive(false);
        txt3.SetActive(false);
        txt4.SetActive(false);
        txt5.SetActive(false);
        
        tcP1.gameObject.SetActive(false);
        tcP2.gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {

        if (_timer) {
            timer += Time.deltaTime;
            if (timer > 1) {
                state++;
                timer = 0f;
            }
        }

        if (cards.finish) {
            End();
        } else {
            StateMachine();
        }

    }

    private void StateMachine() {
        switch (state) {
            case 1:
                txt1.SetActive(true);
                if (timer > 0.5f) {
                    txt2.SetActive(true);
                }
                break;
            case 3:
                txt3.SetActive(true);
                txt4.SetActive(true);
                break;
            case 5:
                
                cards.StartGame();
                txt1.SetActive(false);
                txt2.SetActive(false);
                break;
            case 6:
                _timer = false;
                txt5.SetActive(true);
                but.SetActive(false);
                colW.a -= 0.003f;
                if (colW.a < 0.85f) {
                    col.a += 0.005f;
                    if (col.a > 0.8f) {
                        col.a = 0.85f;
                        state++;
                    }
                }

                txt5.GetComponent<Image>().color = col;
                woodini.GetComponent<Image>().color = colW;
                break;
            case 7:
                colW.a -= 0.003f;
                woodini.GetComponent<Image>().color = colW;
                if (colW.a < 0.05f) {
                    colW.a = 0f;
                    woodini.SetActive(false);
                    but.SetActive(true);
                }
                
                
                if (next.next) {
                    txt3.SetActive(false);
                    txt4.SetActive(false);
                    txt5.SetActive(false);
                    _timer = true;
                }
                break;
            case 8:
                _timer = false;
                cards.inGame = true;
                break;
           
            default:
                break;
        }
    }

    private void End() {
         
        woodini.SetActive(true);
        woodini.GetComponent<Image>().color = colW;
        colW.a += 0.005f;
        if (colW.a > 0.99f) {
            colW.a = 1f;
            tcP1.gameObject.SetActive(true);
            tcP2.gameObject.SetActive(true);

            Winning();

            tcP1.color = tcP2.color = colTS;
                    colTS.a += 0.05f;
                    if (colTS.a > 0.95f) {
                        colTS.a = 1f;
                    }
                }
               

    }

    private void Winning() {
        sc1 += 27;
        sc2 += 27;


        if ((sc1 > cards.totalScore1) || (sc2 > cards.totalScore2)) {
            if (sc1 > cards.totalScore1) {
                sc1 = cards.totalScore1;
                _sc1 = true;
            }
            if (sc2 > cards.totalScore2) {
                sc2 = cards.totalScore2;
                _sc2 = true;
            }
            timer += Time.deltaTime;
            if (_sc1 && _sc2) {
                if (sc1 > sc2) { 
                    if (timer > 1f) Winner(tcP1, tcP2);
                } else if (sc1 < sc2) {
                    if (timer > 1f) Winner(tcP2, tcP1);
                } else {
                    if (timer > 5f) SceneManager.LoadScene("mainMenu");
                }

               
            }
        }
        tcP1.text = sc1.ToString();
        tcP2.text = sc2.ToString();

    }

    private void Winner(Text wins, Text loose) {
        
        wins.rectTransform.anchoredPosition = Vector3.Lerp(wins.rectTransform.anchoredPosition, woodini.GetComponent<Image>().rectTransform.anchoredPosition, 2f * Time.deltaTime);
        if (timer > 3f) {
            loose.gameObject.SetActive(false);
            
            if (timer > 7f) SceneManager.LoadScene("mainMenu");
        }

    }
}
