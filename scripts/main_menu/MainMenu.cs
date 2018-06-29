using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    private Buttons b_play, b_options, b_credits, b_exit, b_1p, b_2p, b_close, b_yes, b_no, b_close_ed;
    private GameObject cnv_menu, cnv_options, cnv_exit, cnv_bg, cnv_edit, cnv_players, player;
    private AudioManager aM;
    public GameObject pfPlayer, pfAudioManager;
    
    private bool music, checkini = false;

    void Awake() {
        if (GameObject.Find("AudioManager").GetComponent<AudioManager>() == null) {
            aM = GameObject.Find("AudioManager").AddComponent<AudioManager>();
            music = false;
            checkini = true;
        } else {
            aM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            music = checkini = true;
        }
    }

    void Start () {
        b_play = GameObject.Find("b_Play").GetComponent<Buttons>();
        b_options = GameObject.Find("b_Options").GetComponent<Buttons>();
        b_credits = GameObject.Find("b_Credits").GetComponent<Buttons>();
        b_exit = GameObject.Find("b_Exit").GetComponent<Buttons>();
        b_close = GameObject.Find("b_Close").GetComponent<Buttons>();
        b_yes = GameObject.Find("b_Yes").GetComponent<Buttons>();
        b_no = GameObject.Find("b_No").GetComponent<Buttons>();
        b_close_ed = GameObject.Find("b_Close_ed").GetComponent<Buttons>();
        b_1p = GameObject.Find("b_1p").GetComponent<Buttons>();
        b_2p = GameObject.Find("b_2p").GetComponent<Buttons>();

        cnv_menu = GameObject.Find("CNVmenu");
        cnv_options = GameObject.Find("CNVoptions");
        cnv_exit = GameObject.Find("CNVexit");
        cnv_bg = GameObject.Find("wood");
        cnv_edit = GameObject.Find("BGedit");
        cnv_players = GameObject.Find("CNVplayers");

        if (GameObject.Find("Prota_01") != null) {
            Destroy(GameObject.Find("Prota_01").GetComponent<AudioListener>());
        }

        cnv_exit.SetActive(false);
        cnv_options.SetActive(false);
        cnv_edit.SetActive(false);
        cnv_players.SetActive(false);
        cnv_bg.SetActive(true);
        cnv_menu.SetActive(true);   
    }

	void Update () {
        if (checkini) {
            if (!music) {
                aM.Play(0, 20f);

            } else {
                aM.Play(0, 1f);

            }
            if (player == null) {
                player = GameObject.Find("Prota_01");
                if (player == null) {
                    player = Instantiate(pfPlayer, Vector3.zero, Quaternion.identity);
                    player.transform.position = new Vector3(0f, 0.2f, -8f);
                    player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    player.transform.name = "Prota_01";
                    player.SetActive(false);
                    
                }
                player.gameObject.GetComponent<Rigidbody>().useGravity = false;
            }
            checkini = false;
        }
        if (cnv_menu.activeInHierarchy) {
            if (player != null) {
                player.transform.position = new Vector3(0f, 0.2f, -8f);
                player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                player.SetActive(false);
            }
            if (b_play.doit) {
                cnv_players.SetActive(true);
                b_play.doit = false;
                cnv_menu.SetActive(false);
            } else if (b_options.doit) {
                cnv_options.SetActive(true);
                b_options.doit = false;
                cnv_menu.SetActive(false);
            } else if (b_credits.doit) {
                
                b_credits.doit = false;

            } else if (b_exit.doit) {
                cnv_exit.SetActive(true);
                b_exit.doit = false;
                cnv_menu.SetActive(false);
            }

        }
        if (cnv_players.activeInHierarchy) {
            if (b_1p.doit) {
                cnv_edit.SetActive(true);
                player.SetActive(true);
                b_1p.doit = false;
                cnv_bg.SetActive(false);
                cnv_players.SetActive(false);
            } else if (b_2p.doit) {
                b_1p.doit = false;
                player.SetActive(true);
                player.transform.position = new Vector3(20f, -5f, -10f);
                cnv_players.SetActive(false);
                SceneManager.LoadScene("lvl_thumb");
            }
        }

        if (cnv_options.activeInHierarchy) {
            if (b_close.doit) {
                cnv_menu.SetActive(true);
                b_close.doit = false;
                cnv_options.SetActive(false);
            }
        }

        if (cnv_exit.activeInHierarchy) {
            if (b_no.doit) {
                cnv_menu.SetActive(true);
                b_no.doit = false;
                cnv_exit.SetActive(false);
            }
            if (b_yes.doit) {
                Application.Quit();
            }
        }

        if (b_close_ed.doit) {
            player.SetActive(false);
            b_close_ed.doit = false;
            cnv_bg.SetActive(true);
            cnv_menu.SetActive(true);
            cnv_edit.SetActive(false);

        }
    }
}
