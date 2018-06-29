using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kitchen : MonoBehaviour { //GESTION DE LOS PUNTOS

    private ScoreUI score_UI;
    private Crono crono;
    private GameObject dialogs;
    private Rico rico;
    private Canvas controles;
    private GestorDeDialogos dial;
    private GameObject protaCube, prota;
    private AudioListener aL_player;
    private AudioManager aM;
    private Settings setting;
    private TotalScore tScore;

    public GameObject pfplato, colliders;
    public int puntos, score = 0;
    private int temp = 0;
    private float timer = 0f;
    private bool add = false;

    [HideInInspector]
    public static bool endKitchen = false;

    void Start() {
        prota = GameObject.Find("Prota_01");
        prota.AddComponent<PlayerCtrl>();
        prota.AddComponent<PlayerAnimKitchen>();
        protaCube = GameObject.Find("platoEmpty");
        protaCube.AddComponent<PlatoCollider>();

        colliders = Instantiate(colliders, Vector3.zero, Quaternion.identity);

        aL_player = prota.AddComponent<AudioListener>();

        aM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        aM.Play(1, 2f);

        dialogs = GameObject.Find("Dialogs");
        if (dialogs == null) Debug.LogError("404: dialogs_ini in Kitchen");

        dial = dialogs.GetComponent<GestorDeDialogos>();

        setting = GameObject.Find("Main").GetComponent<Settings>();
        if (setting == null) Debug.LogError("404: settings in Kitchen");

        crono = GameObject.Find("Crono").GetComponent<Crono>();
        if (crono == null) Debug.LogError("404: crono in Kitchen");

        score_UI = GameObject.Find("Score").GetComponent<ScoreUI>();
        if (score_UI == null) Debug.LogError("404: scoreUI in Kitchen");

        controles = GameObject.Find("Controls").GetComponent<Canvas>();
        if (controles == null) Debug.LogError("404: controls in Kitchen");

        rico = GameObject.Find("rico").GetComponent<Rico>();
        if (rico == null) Debug.LogError("404: rico in Kitchen");

        tScore = GameObject.Find("TotalScore").GetComponent<TotalScore>();
        if (tScore == null) Debug.LogError("404: tScore in Kitchen");
        tScore.gameObject.SetActive(false);

        score = score_UI._scoreUI;
        score = 0;
        dialogs.SetActive(true);
        controles.renderMode = RenderMode.WorldSpace;

        prota.transform.position = new Vector3(0.18f, 0.0289f, -1.733f);
        prota.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
    }

    void Update() {
        if (dial.startGame) {
            crono.startCrono = true;
            dialogs.SetActive(false);
            controles.renderMode = RenderMode.ScreenSpaceOverlay;
        }

        if (crono.secs < 0.0001) {
            dialogs.SetActive(true);
            controles.renderMode = RenderMode.WorldSpace;
            if (score == 0) {
                dial.SacaRespuesta(14, true);
            }else if (score > 1000) {
                dial.SacaRespuesta(12, true);
            } else {
                dial.SacaRespuesta(12, false);
            }
        }
        if (dial.finish) EndGame();

        score_UI._scoreUI = score;
    }

    public void AddScore() {
        if (puntos == 211) {
            temp = 500;
            add = true;
        } else if ((puntos > 111) && (puntos < 122)) {
            temp = 200;
            add = true;
        } else {
            temp = 0;
        }

        if (add) {
            score += temp;
            add = false;
        }

        temp = 0;
        puntos = 0;
    }

    private void EndGame() {
        endKitchen = true;
        if (rico.state == 0) {
            timer += Time.deltaTime;
            if (timer > 4f) {
                Destroy(protaCube.GetComponent<PlatoCollider>());
                Destroy(prota.GetComponent<PlayerCtrl>());
                Destroy(prota.GetComponent<PlayerAnimKitchen>());
                
                endKitchen = false;
                Settings.lvlScore = score;
                tScore.gameObject.SetActive(true);
                tScore.sc = true;
            }
        }
    }
}

