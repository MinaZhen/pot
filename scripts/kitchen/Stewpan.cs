using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stewpan : MonoBehaviour {

    private float angle = 0f;
    private Vector3 pose;

    void Start() {
        pose = transform.position;
    }

    void Update() {
        this.transform.Rotate(Vector3.forward * 0.2f);
        angle += (Time.deltaTime * 20f);
        float y = Mathf.Sin(angle) * 0.05f;
        Vector3 p = pose;
        p.y += y;
        this.transform.position = Vector3.Lerp(this.transform.position, p, (Time.smoothDeltaTime * 2f));

    }
}