using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BGedit : MonoBehaviour {

    public GameObject[] hair;
    private GameObject emptyHair, gO_hair;
    private InputField nom;
    private Buttons b_jugar, b_pel_b, b_pel_a, b_col_a, b_col_b;
    private int id, color_id = 0;
    public Color[] col = new Color[8];
    private Vector3 pos = Vector3.zero;
    private bool changeHair = false;

    void Start () {
        emptyHair = GameObject.Find("emptyHair");
        if (emptyHair == null) Debug.LogError("404: emptyHair in BGedit");

        nom = GameObject.Find("Nom").GetComponent<InputField>();
        if (nom == null) Debug.LogError("404: nom in BGedit");
        nom.characterLimit = 10;

        id = GameObject.Find("Prota_01").GetComponent<Player>().pels;
        color_id = GameObject.Find("Prota_01").GetComponent<Player>().colorPels;

        b_jugar = GameObject.Find("b_Jugar").GetComponent<Buttons>();
        b_col_a = GameObject.Find("b_col_a").GetComponent<Buttons>();
        b_col_b = GameObject.Find("b_col_b").GetComponent<Buttons>();
        b_pel_a = GameObject.Find("b_pel_a").GetComponent<Buttons>();
        b_pel_b = GameObject.Find("b_pel_b").GetComponent<Buttons>();

        if (GameObject.Find("Prota_01").GetComponent<Player>().nombre != "Prota") {
            nom.text = GameObject.Find("Prota_01").GetComponent<Player>().nombre;
        }

        gO_hair = GameObject.Find("pelos");
        if (gO_hair != null) Destroy(gO_hair);
        Instancia(id);
    }

	void Update () {

        CheckButtons();
        if (changeHair) Instancia(id);
    }

    private void CheckButtons() {
        
        if (b_jugar.doit) {
            b_jugar.doit = false;
            GameObject.Find("Prota_01").GetComponent<Rigidbody>().useGravity = true;
            SceneManager.LoadScene("lvl_laberinto");
        }

        if (b_pel_a.doit) ButHair(-1);
        if (b_pel_b.doit) ButHair(1);
        if (b_col_a.doit) ColorChange(-1);
        if (b_col_b.doit) ColorChange(1);
    }

    public void ButHair(int num) {

        id += num;
        if (id < 0) id = 4;
        if (id > 4) id = 0;

        Destroy(gO_hair);
        changeHair = true;
        b_pel_a.doit = b_pel_b.doit = false;
    }

    private void Instancia(int idx) {
        idx = id;

        
        if ((idx == 0) || (idx == 2)) {
            pos = new Vector3(0f, -0.07f, -1.42f);
        } else {
            pos = Vector3.zero;
        }

            gO_hair = Instantiate(hair[idx], emptyHair.transform.position, emptyHair.transform.localRotation) as GameObject;
            gO_hair.transform.parent = emptyHair.transform;
            gO_hair.transform.localPosition = pos;
            gO_hair.transform.rotation = emptyHair.transform.rotation;
            gO_hair.GetComponent<Renderer>().material.color = col[color_id];
            gO_hair.transform.name = "pelos";
            GameObject.Find("Prota_01").GetComponent<Player>().pels = id;

        changeHair = false;

    }

    public void ChangeName() {
        GameObject.Find("Prota_01").GetComponent<Player>().nombre = nom.text;
    }
    
    private void ColorChange(int idx) {
        color_id += idx;
        if (color_id < 0) color_id = (col.Length - 1);
        if (color_id > (col.Length - 1)) color_id = 0;
        b_col_a.doit = b_col_b.doit = false;
        GameObject.Find("Prota_01").GetComponent<Player>().colorPels = color_id;
        if (gO_hair != null) gO_hair.GetComponent<Renderer>().material.color = col[color_id];
    }
}
