using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Put : MonoBehaviour {

    private GameObject prota = null;
    private AudioManager aM;
    private Camera cam;
    private CtrlLab pCtrl;

    private float smth = 20f;

    void Start () {
        prota = GameObject.Find("Prota_01");
        prota.transform.position = new Vector3(21f, -0.5f, 21f);
        prota.transform.rotation = Quaternion.identity;
        prota.gameObject.GetComponent<Animator>().applyRootMotion = false;

        pCtrl = prota.AddComponent<CtrlLab>();

        aM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        aM.Play(1, 2f);

        cam = Camera.main;
        cam.transform.parent = prota.transform;
        cam.transform.position = prota.transform.position;
        cam.transform.rotation = prota.transform.rotation;
        cam.transform.position = prota.gameObject.transform.position + (-2f * prota.transform.forward) + (3f * prota.transform.up);
    }
	
	void Update () {
        MoveCam();
    }

    private void MoveCam() {
        Vector3 follow = prota.gameObject.transform.position + (-2f * prota.transform.forward) + (3.5f * prota.transform.up);
        Vector3 here = Vector3.Lerp(cam.transform.position, follow, smth);
        cam.transform.position = here;
        cam.transform.LookAt(prota.gameObject.transform.position + (1.2f * prota.transform.up));
    }
}
