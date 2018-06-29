using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimKitchen : MonoBehaviour {

    private Animator anim;
    public bool plat, reset, fly, pick, leave, full = false;
    private bool flee = false;

    void Start () {
        anim = this.GetComponent<Animator>();
        if (anim == null) Debug.LogError("404: anim in PlayerAnimKitchen");
    }
	
	void Update () {
        
        if (fly) flee = true;
        
        PickFly();
        PickDish();
        LeaveDish();

      if ((anim.GetCurrentAnimatorStateInfo(1).IsName("idle")) && !anim.GetBool("dish")) anim.SetLayerWeight(1, 0f);
    }

    public void PickDish() {

        if (pick) {
            anim.SetLayerWeight(1, 1f);
            anim.SetBool("dish", true);
            if (anim.GetCurrentAnimatorStateInfo(1).IsName("pickDishLast")) pick = false;
        }
    }

    public void LeaveDish() {

        if (leave) {
            anim.SetBool("dish", false);
            if (anim.GetCurrentAnimatorStateInfo(1).IsName("idle")) leave = false;
        }
    }

    public void PickFly() {
        
        if (flee) { 
            anim.SetLayerWeight(2, 1f);
            anim.SetBool("fly", true);
            if (anim.GetCurrentAnimatorStateInfo(2).IsName("pickFly12last")) fly = false;

            if (anim.GetCurrentAnimatorStateInfo(2).IsName("white")) {
                anim.SetBool("fly", false);
                anim.SetLayerWeight(2, 0f);
                flee = false;
            }
        }
    }
}
