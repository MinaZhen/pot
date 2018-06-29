using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidersBeh : MonoBehaviour {

    private VirtualJoystic ctrl; 
    public GameObject lookObj;
    [HideInInspector] public bool action = false;
    private GameObject player = null;
    private Animator anim = null;
    private PlayerAnimKitchen pScript = null;
    private But_Action bAction = null;
    private bool mira, canDo = false;
    private uint funcion, id_plat = 0;
    public bool forma, regula, energ, bote;

    void Start() {

        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) Debug.LogError("404: Player in ColliderBeh");

        ctrl = GameObject.Find("JoysticBG").GetComponent<VirtualJoystic>();
        if (ctrl == null) Debug.LogError("404: ctrl in ColliderBeh");

        anim = player.GetComponent<Animator>();
        if (anim == null) Debug.LogError("404: anim in ColliderBeh");

        pScript = player.GetComponent<PlayerAnimKitchen>();

        bAction = GameObject.Find("ButAction").GetComponent<But_Action>();
        if (bAction == null) Debug.LogError("404: ButtonAction in CollidersBeh");

        canDo = true;
    }

    void Update() {
        action = false;
        if (pScript.reset) id_plat = 0;
        if ((bAction.action == true)|| (Input.GetKeyDown(KeyCode.X))) action = true;
        if (mira) Rota();
        
        if ((!canDo)&& (funcion != 0)) Funcion();
    }

    void Rota() {
        Vector3 rPos = lookObj.transform.position - player.transform.position;
        rPos.y = player.transform.position.y;
        Quaternion rot = Quaternion.LookRotation(rPos);
        Quaternion current = player.transform.localRotation;

        if (Mathf.Abs(current.y - rot.y) < 0.05f) {
            Funcion();
            mira = false;
        }
        player.transform.localRotation = Quaternion.Slerp(current, rot, Time.deltaTime * 5);
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "colli_prota") this.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
    }
    void OnTriggerExit(Collider other) {
        if (other.tag == "colli_prota") this.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    }
    void OnTriggerStay (Collider other) {
        if (other.tag == "colli_prota") {

            if ((action) && (canDo)) {
                canDo = false;
                ctrl.move = false;
                if ((this.gameObject.name == "CPlatoCoje") && (!pScript.plat)) {
                    pScript.fly = true;
                    pScript.pick = true;
                    mira = true; 
                    funcion = 1;
                    
                } else if ((this.gameObject.name == "CPlatoDeja") && (pScript.plat)) {
                    mira = true;
                    if (pScript.full) pScript.leave = true;
                    funcion = 2;
                    
                } else if (((this.gameObject.name != "CPlatoCoje") && (this.gameObject.name != "CPlatoDeja")) && (pScript.plat)) { 
                    if (id_plat == 0) {
                        id_plat++;
                        if (GameObject.FindGameObjectWithTag("plato").GetComponent<PlatoBeh>().count_plate < 4) {
                            mira = true;
                            pScript.fly = true;
                            funcion = 3;
                        } else {
                            funcion = 0;
                            ctrl.move = true;
                        }
                    } else {
                        funcion = 0;
                        ctrl.move = true;
                    }
                } else {
                    funcion = 0;
                    ctrl.move = true;
                }
            }
        }
    }

    void Funcion() {
        
        switch (funcion) {
            case 1:             //Pick plate
                if (!pScript.pick) {
                    ctrl.move = true;
                    mira = false;
                    funcion = 0;
                    canDo = true;
                }
                break;
            case 2:             // Leave plate
                if (pScript.full) {
                    if (!pScript.leave) {
                        pScript.full = false;
                        ctrl.move = true;
                        mira = false;
                        funcion = 0;
                        canDo = true;

                    }
                } else {
                    funcion = 0;
                    ctrl.move = true;
                    canDo = true;
                }
                
                break;

            case 3: // Fill
                PlatoBeh platoBeh = GameObject.FindGameObjectWithTag("plato").GetComponent<PlatoBeh>();
                if (platoBeh == null) Debug.LogError("404: platoBeh en CollidersBeh");
                if (!pScript.fly) {
                    if (forma) {
                        platoBeh.Quarto(0.117f);
                        platoBeh.puntos += 1;
                        mira = false;
                        ctrl.move = true;
                        canDo = true;
                        funcion = 0;
                    }
                    if (energ) {
                        mira = false;
                        platoBeh.Quarto(0.0585f);
                        platoBeh.puntos += 10;
                        ctrl.move = true;
                        canDo = true;
                        funcion = 0;
                    }
                    if (regula) {
                        mira = false;
                        platoBeh.Quarto(0f);
                        platoBeh.puntos += 100;
                        ctrl.move = true;
                        canDo = true;
                        funcion = 0;
                    }
                }
                break;

            default:
                break;
        }
    }    
}
