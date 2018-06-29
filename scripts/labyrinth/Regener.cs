using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regener : MonoBehaviour {

    private GameObject explode;
    private GameObject player;
    private BoxCollider colli;
    private bool start_change = true;
    private Vector3 pos;
    private float timer = 0f;

	void Start () {
        player = GameObject.Find("Prota_01");
        if (player == null) Debug.LogError("404: Player in Regener");
        
        explode = GameObject.Find("Main").GetComponent<LabAlim>().explode;
        pos = this.gameObject.transform.position;
        pos.y += 0.5f;

        InitialChange();
    }
	
	void Update () {
		if (player != null) {
            Vector3 dist = player.transform.position - this.transform.position;
            Quaternion newRot = Quaternion.LookRotation(dist);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, 5f);
        }
        if (start_change) {
            InitialChange();
        }
	}

    private void InitialChange() {
        timer += Time.deltaTime;
        if (timer > 2f) {
            Vector3 resize = this.GetComponent<BoxCollider>().size;
            resize.x *= 3;
            resize.y *= 3;
            resize.z *= 3;

            Destroy(this.gameObject.GetComponent<Rigidbody>());
            this.GetComponent<BoxCollider>().isTrigger = true;
            this.GetComponent<BoxCollider>().size = resize;
            start_change = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.transform.tag == "Player") {
            Instantiate(explode, pos, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
