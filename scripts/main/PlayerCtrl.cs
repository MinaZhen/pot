using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {

    private VirtualJoystic joystic;
    private Rigidbody rB;
    private Animator anim;
    private CapsuleCollider caps;
    private But_Action bRun;
    private Vector3 vectorMovement = Vector3.zero;

    private float floatAnim, capsRad = 0f;

    [HideInInspector] public float walkSpd, runSpd;
    
    void Start () {

        rB = this.GetComponent<Rigidbody>();
        if (rB == null) Debug.LogError("404: rB in PlayerCtrl");
        anim = this.GetComponent<Animator>();
        if (anim == null) Debug.LogError("404: anim in PlayerCtrl");
        caps = this.GetComponent<CapsuleCollider>();
        if (caps == null) Debug.LogError("404: caps in PlayerCtrl");
        joystic = GameObject.Find("JoysticBG").GetComponent<VirtualJoystic>();
        if (rB == null) Debug.LogError("404: joystic in PlayerCtrl");
        bRun = GameObject.Find("ButRun").GetComponent<But_Action>();
        if (bRun == null) Debug.LogError("404: bRun in PlayerCtrl");

        walkSpd = 2f;
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.L)) bRun.action = true; 
        if (Input.GetKeyUp(KeyCode.L)) bRun.action = false;
        if (!bRun.action) {
            walkSpd = 2f;
        } else {
            walkSpd = 4f;
        }
        vectorMovement = Direccion();
        Anims();
    }

    void FixedUpdate() {
        Move();
    }

    private void Anims() {

        floatAnim = 0f;
        if (!bRun.action) {
            floatAnim = vectorMovement.magnitude;
        } else { 
            floatAnim = vectorMovement.magnitude * 1.5f;
        }
        caps.radius = 0.3f + (vectorMovement.magnitude * 0.15f);
        anim.SetFloat("Forward", floatAnim);
    }

    private void Move() {
        
        if (vectorMovement != Vector3.zero) {
            anim.Play("Locomotive");
           transform.rotation = Quaternion.LookRotation(vectorMovement);
            this.transform.Translate(Vector3.forward * (walkSpd * vectorMovement.magnitude) * Time.deltaTime);
            anim.SetInteger("break", 99);
        }
    }

    private Vector3 Direccion() {
        Vector3 move = Vector3.zero;

        move.x = joystic.Horizontal();
        move.z = joystic.Vertical();

        if (move.magnitude > 1) move.Normalize();

        return move;
    }

}