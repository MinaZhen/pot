using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCollider : MonoBehaviour {

    private Animator anim;
    private GameObject obj = null;
    private GameObject pfPlato;
    private Rico rico;
    private PlayerAnimKitchen pScript;
    private float timer = 0f;
    private GameObject player;

    void Start() {
        anim = GameObject.Find("Prota_01").GetComponent<Animator>();
        if (anim == null) Debug.LogError("404: anim in PlatoBeh");

        player = GameObject.Find("Prota_01");

        pScript = player.GetComponent<PlayerAnimKitchen>();
        if (pScript == null) Debug.LogError("404: pAK in PlatoBeh");

        rico = GameObject.Find("rico").GetComponent<Rico>();
        if (rico == null) Debug.LogError("404: Rico in PlatoBeh");

        pfPlato = GameObject.Find("kitchen").GetComponent<Kitchen>().pfplato;
        if (pfPlato == null) Debug.LogError("404: pfPlato in PlatoBeh");
    }

    void Update() {
        
        if (anim.GetBool("dish") && (obj == null)) {
            timer += (Time.deltaTime);
            if ((timer > 0.5f) && (obj == null)) {
                obj = Instantiate(pfPlato, this.transform.position, this.transform.rotation) as GameObject;
                obj.transform.parent = this.gameObject.transform;
                obj.transform.Rotate(0f, 0f, -90f);
                obj.transform.tag = "plato";
                pScript.plat = true;
                pScript.reset = false;
                timer = 0f;
            }
        }

        if ((!Kitchen.endKitchen) && (!anim.GetBool("dish") && (obj != null))) {
            timer += (Time.deltaTime);
            if ((timer > 0.5f) && (obj != null)) {
                    obj.transform.tag = "platoPase";
                    pScript.plat = false;
                    pScript.reset = true;
                    obj.transform.parent = null;
                    obj.transform.position = GameObject.Find("EmptyPlato").transform.position;
                    obj.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
                    rico.platos++;
                    obj = null;
                    timer = 0f;
            }
        }

        if ((Kitchen.endKitchen) && (obj != null)) {
            anim.SetBool("dish", false);
            timer += (Time.deltaTime);
            if (timer > 1f) {
                Destroy(obj.gameObject);
                obj = null;
                timer = 0;
            }
        }
    }
}
