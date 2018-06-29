using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cards : MonoBehaviour {

    private Card card;
    private Crono crono;
    private GameObject cronoCopy, board;
    private Text ScP1, ScP2;
    private Image[,] place = new Image[4, 4];
    //private Images imgs;
    [HideInInspector] public uint puntos_p1, puntos_p2, p1, p2;

    //public Sprite bolita;
    public Sprite[] food = new Sprite[15];
    //private Image img;

    public struct imgs {
        public bool used;
    }
    public imgs[] choose = new imgs[15];
    public uint id;
    private float timer1, timer2;
    private int score1, score2;
    private bool sc1, sc2;

    [HideInInspector] public Image[,] p = new Image[2, 4];
    [HideInInspector] public Image[,] top = new Image[2, 4];
    [HideInInspector] public Color c1, c2;
    [HideInInspector] public bool del1, del2, inGame, finish;
    [HideInInspector] public int totalScore1, totalScore2;

    // Use this for initialization
    void Start () {
        card = GameObject.Find("Main").GetComponent<Card>();
        crono = GameObject.Find("Crono").GetComponent<Crono>();
        cronoCopy = GameObject.Find("CronoCopy");
        board = GameObject.Find("Game");
        
        p[0, 0] = GameObject.Find("p1_1").GetComponent<Image>();
        p[0, 1] = GameObject.Find("p1_2").GetComponent<Image>();
        p[0, 2] = GameObject.Find("p1_3").GetComponent<Image>();
        p[0, 3] = GameObject.Find("p1_4").GetComponent<Image>();
        p[1, 0] = GameObject.Find("p2_1").GetComponent<Image>();
        p[1, 1] = GameObject.Find("p2_2").GetComponent<Image>();
        p[1, 2] = GameObject.Find("p2_3").GetComponent<Image>();
        p[1, 3] = GameObject.Find("p2_4").GetComponent<Image>();

        ScP1 = GameObject.Find("ScP1").GetComponent<Text>();
        ScP2 = GameObject.Find("ScP2").GetComponent<Text>();

        del1 = del2 = inGame = finish = false;
        c1 = new Color(0.35f, 0.25f, 0.15f);
        c2 = new Color(0.34f, 0.24f, 0.14f);

        for (uint n = 0; n < 2; n++) {
            for (uint m = 0; m < 4; m++) {
                if (n == 0) {
                    p[n, m].color = c1;
                } else {
                    p[n, m].color = c2;
                }
            }
        }
        
        FindPlaces();

        cronoCopy.SetActive(false);
        crono.gameObject.SetActive(false);
        board.SetActive(false);
        ScP1.gameObject.SetActive(false);
        ScP2.gameObject.SetActive(false);
        inGame = false;
    }

    // Update is called once per frame
    void Update() {
        if(inGame) crono.startCrono = true;
        if (del1) DeleteSpots1();
        if (del2) DeleteSpots2();
        if (ScP1) Score1();
        if (ScP2) Score2();
        if (crono.secs < 0.01) EndGame();
    }

    public void StartGame() {
        cronoCopy.SetActive(true);
        crono.gameObject.SetActive(true);
        board.SetActive(true);
        ScP1.gameObject.SetActive(true);
        ScP2.gameObject.SetActive(true);
        
    }

    private void EndGame() {
        inGame = false;
        finish = true;
        timer1 += Time.deltaTime;
        if (timer1 > 3f) {
            cronoCopy.SetActive(false);
            crono.gameObject.SetActive(false);
            if (timer1 > 6f) board.SetActive(false);
        }
 
    }

    private void FindPlaces() {
        for (uint a = 0; a < 4; a++) {
            for (uint b = 0; b < 4; b++) {
                place[a, b] = GameObject.Find("c" + a.ToString() + b.ToString()).GetComponent<Image>();
                //place[a, b].gameObject.transform.Rotate(0f, 0f, Random.Range(0, 4) * 90f);
            }
        }
    }

    private void DeleteSpots1() {

        timer1 += Time.deltaTime;
        
        if (puntos_p1 != 0) {
            score1 = totalScore1;
            totalScore1 += (AddScore(puntos_p1) - 1);
            puntos_p1 = 0;
        }
        
        if (timer1 > 1f) {
            for (uint n = 0; n < 4; n++) {
                p[0, n].color = c1;
                p[0, n].sprite = null;
                timer1 = 0;
                del1 = false;
            } 
        } 
    }
    private void DeleteSpots2() {

        timer2 += Time.deltaTime;

        if (puntos_p2 != 0) {
            score2 = totalScore2;
            totalScore2 += (AddScore(puntos_p2) - 1);
            puntos_p2 = 0;
        }

        if (timer2 > 1f) {
            for (uint n = 0; n < 4; n++) {
                p[1, n].color = c2;
                p[1, n].sprite = null;
                timer2 = 0;
                del2 = false;
            }
        }
    }

    private int AddScore(uint puntos, int temp = 0) {
        if (puntos == 211) {
            temp = 501;
            
        } else if ((puntos > 111) && (puntos < 122)) {
            temp = 201;
            
        } else {
            temp = 1;
        }
        return temp;
    }

    private void Score1() {
        score1 += 17;
        if (score1 > totalScore1) {
            score1 = totalScore1;
        }
        ScP1.text = score1.ToString();
    }
    private void Score2() {
        score2 += 17;
        if (score2 > totalScore2) {
            score2 = totalScore2;
        }
        ScP2.text = score2.ToString();
    }

}
