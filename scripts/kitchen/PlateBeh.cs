using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateBeh : MonoBehaviour {

    private GameObject q1, q2, q3, q4, ricoEmpty = null;
    private Rico rico;
    private Kitchen kitchen;
    private PlayerAnimKitchen pScript;
    public GameObject quart;
    [HideInInspector]  public int puntos = 0;
    [HideInInspector] public uint count_plate = 0;

    private Animator pass;
    private bool puntua = false;

    void Start() {
        pass = GameObject.Find("ImagePase").GetComponent<Animator>();
        if (pass == null) Debug.LogError("404: anim in PlayerAnimKitchen");

        rico = GameObject.Find("rico").GetComponent<Rico>();
        if (rico == null) Debug.LogError("404: rico in PlatoBeh");

        ricoEmpty = GameObject.Find("RicoEmpty");
        if (ricoEmpty == null) Debug.LogError("404: ricoEmpty in PlatoBeh");

        pScript = GameObject.Find("Prota_01").GetComponent<PlayerAnimKitchen>(); ;
        if (ricoEmpty == null) Debug.LogError("404: player in PlatoBeh");

        kitchen = GameObject.Find("kitchen").GetComponent<Kitchen>();
        if (kitchen == null) Debug.LogError("404: kitchen in PlatoBeh");
    }

    void Update() {
        Taked();
    }

    public void Quarto(float offset) { 
        count_plate++;

        switch (count_plate) {
            case 1:
                q1 = Instantiate(quart, this.transform.position, this.transform.rotation) as GameObject;
                q1.name = "fq1";
                q1.transform.parent = this.gameObject.transform;
                q1.transform.rotation = this.transform.rotation;

                q1.GetComponent<Renderer>().material.mainTextureOffset += new Vector2 (offset, 0f);
                break;
            case 2:
                q2 = Instantiate(quart, this.transform.position, this.transform.rotation) as GameObject;
                q2.name = "fq2";
                q2.transform.parent = this.gameObject.transform;
                q2.transform.Rotate(new Vector3(0f, 0f, 90f));

                q2.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(offset, 0f);

                pass.SetBool("PaseLR", true);
                break;
            case 3:
                q3 = Instantiate(quart, this.transform.position, this.transform.rotation) as GameObject;
                q3.name = "fq3";
                q3.transform.parent = this.gameObject.transform;
                q3.transform.Rotate(new Vector3(0f, 0f, 180f));

                q3.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(offset, 0f);
                break;
            case 4:
                q4 = Instantiate(quart, this.transform.position, this.transform.rotation) as GameObject;
                q4.name = "fq4";
                q4.transform.parent = this.gameObject.transform;
                q4.transform.Rotate(new Vector3(0f, 0f, 270f));

                q4.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(offset, 0f);
                puntua = true;
                pScript.full = true;
                break;
            default:
                break;
        }
    }

    private void Taked() {
        if (this.gameObject.transform.tag == "platoPase") { 
            if (puntua) {
                kitchen.puntos = puntos;
                kitchen.AddScore();
                puntua = false;  
            }
            if ((rico.taked) && (rico.platos == 1)) {
                this.gameObject.transform.parent = ricoEmpty.transform;
                this.gameObject.transform.rotation = new Quaternion(-1f, 0f, 0f, 1f);
                this.gameObject.transform.position = (Vector3.Lerp(this.gameObject.transform.position, ricoEmpty.transform.position, Time.smoothDeltaTime * 20f));
            }
        }
    }



    
}
