using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*****************************************************************************************************************************************************************************/
/*                                       Dialogs disabled by default                              */
/**********************************************************************************************************************/

public class GestorDeDialogos : MonoBehaviour {

    public enum who { Prota, Flora, Rico };

    private Text fraseBox, txtBYes, txtBNo;
    private Text pnjBox;
    private GameObject buttonYes, buttonNo;
    private But_next bNext;
    private Image dialogBG;
    private AudioManager aM;
    private Settings settings;

    [System.Serializable]
    public struct Dialogos {
        public who quien;
        public string dialogo;
        public bool botones;
    };

    public Dialogos[] dialogBox;
    [HideInInspector] public uint idx;
    private bool stGame, start = false;
    [HideInInspector]
    public bool startGame, finish = false;
    private string protaName = "";
    private float timer = 0f;

    void Start() {
        aM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        settings = GameObject.Find("Main").GetComponent<Settings>();

        fraseBox = GameObject.Find("Dialogos_Dialogo").GetComponent<Text>();
        if (fraseBox == null) Debug.LogError("404: fraseBox in GestorDeDialogos");
        pnjBox = GameObject.Find("Dialogos_Title").GetComponent<Text>();
        if (pnjBox == null) Debug.LogError("404: pnjBox in GestorDeDialogos");
        buttonYes = GameObject.Find("Dialogos_butYes");
        if (buttonYes == null) Debug.LogError("404: buttonYes in GestorDeDialogos");
        buttonNo = GameObject.Find("Dialogos_butNo");
        if (buttonNo == null) Debug.LogError("404: buttonNo in GestorDeDialogos");
        dialogBG = GameObject.Find("Dialogos_DialogBG").GetComponent<Image>();
        if (dialogBG == null) Debug.LogError("404: dialogBG in GestorDeDialogos");
        bNext = GameObject.Find("Dialogos_butNext").GetComponent<But_next>();
        if (bNext == null) Debug.LogError("404: dialogBG in GestorDeDialogos");

        protaName = GameObject.Find("Prota_01").GetComponent<Player>().nombre;
        if (protaName == null) Debug.LogError("404: el nombre in GestorDeDialogos");

        txtBYes = buttonYes.GetComponentInChildren<Text>();
        if (txtBYes == null) Debug.LogError("404: txtBYes in GestorDeDialogos");
        txtBNo = buttonNo.GetComponentInChildren<Text>();
        if (txtBNo == null) Debug.LogError("404: txtBNo in GestorDeDialogos");

        idx = 0;
        SacaDialogo(idx);
        BotonBeh();
        finish = false;
        start = true;
    }

    void Update() {             
        if (idx == 0) {
            buttonYes.SetActive(false);
            buttonNo.SetActive(false);
        }
        if (bNext.next) {
            if (!stGame) {
                if (!dialogBox[idx].botones) {
                    idx++;
                    idx %= (uint)dialogBox.Length;
                    SacaDialogo(idx);
                    bNext.next = false;
                } else if (dialogBox[idx].botones) {
                    Debug.Log("nuze");
                    bNext.next = false;
                }
            } else {

                timer += (Time.deltaTime);
                if (timer > 2f) {
                    timer = 0;
                    startGame = true;

                    bNext.next = false;
                }
            }
        }
    }

    private void SacaDialogo(uint indice) {
        idx = indice;
        switch (dialogBox[idx].quien) {      
            case (who.Flora):                         
                pnjBox.text = "Flora";
                dialogBG.color = new Color(0.6f, 1f, 0.6f, 0.5f);
                break;
            case (who.Prota):
                pnjBox.text = protaName;
                dialogBG.color = new Color(1f, 1f, 1f, 0.5f);
                break;
            case (who.Rico):
                pnjBox.text = "Rico";
                dialogBG.color = new Color(0.6f, 0.8f, 0.8f, 0.5f);
                break;
        }
        fraseBox.text = dialogBox[idx].dialogo;
        fraseBox.text = fraseBox.text.Replace("*** ", "\n");

        if (dialogBox[idx].botones) {
            bNext.Desmarca();
            bNext.gameObject.SetActive(false);
            buttonYes.SetActive(true);
            buttonNo.SetActive(true);
            BotonBeh();
        } else {
            bNext.gameObject.SetActive(true);
            bNext.Desmarca();
            if (idx > 9) bNext.gameObject.SetActive(false);
            buttonYes.SetActive(false);
            buttonNo.SetActive(false);
        }
    }

    public void BotonYes() {
        aM.PlayFX(settings.fx[1]);
        SacaRespuesta(idx, true);
    }
    public void BotonNo() {
        aM.PlayFX(settings.fx[1]);
        SacaRespuesta(idx, false);
    }

    
    //  Next funtions are specific by level
    public void SacaRespuesta(uint indice, bool qRespuesta) { 

        switch (indice) {
            case 1:
                if (qRespuesta) {
                    idx++;
                    SacaDialogo(idx);
                } else {
                    idx = 10;
                    SacaDialogo(idx);
                }
                break;
            case 8:
                if (qRespuesta) {
                    idx++;
                    SacaDialogo(idx);
                } else {
                    idx = 10;
                    SacaDialogo(idx);
                }
                break;
            case 9:
                if (qRespuesta) {
                    idx = 3;
                    SacaDialogo(idx);
                } else {
                    idx = 10;
                    SacaDialogo(idx);
                }
                break;
            case 10:
                if (qRespuesta) {
                    idx ++;
                    SacaDialogo(idx);
                    bNext.next = true;
                    stGame = true;
                } else {
                    idx --;
                    SacaDialogo(idx);
                }
                break;


            case 12: 
                if (stGame) {
                    timer += (Time.deltaTime);
                    if (qRespuesta) idx = 12; else idx = 13;
                    SacaDialogo(idx);
                    if (timer > 2f) {
                        startGame = false;
                        stGame = false;
                        finish = true;
                    }
                    
                }
                break;
            case 14:
                if (stGame) {
                    timer += (Time.deltaTime);
                    idx = 14; 
                    SacaDialogo(idx);
                    if (timer > 2f) {
                        startGame = false;
                        stGame = false;
                        finish = true;
                    }

                }
                break;
            default:
                break;
        }
    }

    private void BotonBeh() {

        switch (idx) {
            case 1:
                txtBYes.text = "No";
                txtBNo.text = "Si";
                break;
            case 8:
                txtBYes.text = "No sé";
                txtBNo.text = "Creo que si";
                break;
            case 9:
                txtBYes.text = "Si";
                txtBNo.text = "No";
                break;
            case 10:
                txtBYes.text = "Vale";
                txtBNo.text = "Espera";
                break;
            default:
                break;
        }
    }
}