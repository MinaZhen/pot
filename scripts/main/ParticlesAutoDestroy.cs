using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesAutoDestroy : MonoBehaviour {

    private float timer = 0f;

	void Update () {
        timer += Time.deltaTime;
        if (timer > 5f) Destroy(this.gameObject);
	}
}
