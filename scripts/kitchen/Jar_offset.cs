using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar_offset : MonoBehaviour {


    public float offset;

    void Start () {
        Vector2 v2 = new Vector2(offset, 0f);
        this.gameObject.GetComponent<Renderer>().material.mainTextureOffset += v2;
    }
	
}
