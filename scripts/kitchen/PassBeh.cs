using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassBeh : MonoBehaviour {

    private Animator pass;
    private Rico rico;
    private float timer, random ;

    void Start() {
        pass = this.GetComponent<Animator>();
        if (pass == null) Debug.Log("404: anim in PaseBeh");

        rico = GameObject.Find("rico").GetComponent<Rico>();
        if (rico == null) Debug.Log("404: rico in PaseBeh");

        random = Random.Range(500f, 1000f);
    }
	
	void Update () {
        timer++;
        if (rico.state == 7) {
            random = Random.Range(500f, 1000f);
            timer = 0;
        } else if (rico.state == 8) {
            pass.SetBool("PaseRL", true);
            random = Random.Range(500f, 1000f);
            timer = 0;
        } else if (timer > random) {
            if (random < 750) {
                pass.SetBool("PaseLR", true);
            } else {
                pass.SetBool("PaseRL", true);
            }

            random = Random.Range(500f, 1000f);
            timer = 0;
        }

        if (pass.GetCurrentAnimatorStateInfo(0).IsName("PaseLR")) {
            pass.SetBool("PaseLR", false);
        }
        if (pass.GetCurrentAnimatorStateInfo(0).IsName("PaseRL")) {
            pass.SetBool("PaseRL", false);
        }
    }
}
