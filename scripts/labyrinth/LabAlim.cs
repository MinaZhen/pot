using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabAlim : MonoBehaviour {

    public GameObject explode, al01, al02, al03, al04, al05;
    private GameObject[] alims;
    private GameObject folder;
    private Vector3[] poses;
    public uint totAlim = 50;
    private uint n, m = 0;
    public bool doIt = false;

    void Start() {
        alims = new GameObject[totAlim];
        folder = GameObject.Find("Alimentos");
    }

    void Update() {
        if (doIt) {
            Adding();
            doIt = false;
        }
    }
	    
    public void Adding() {
        for (uint i = 0; i < (totAlim); i++) {
            Vector3 pos = Vector3.zero;
            m++;
            uint p = m;
            
            p %= 15;
            pos.z = p * 3;
            if ((m % 2) == 0) {
                pos.x = (uint)Mathf.RoundToInt(Random.Range(0, 7));
                pos.x *= 3;
            } else {
                pos.x = (uint)Mathf.RoundToInt(Random.Range(7, 15));
                pos.x *= 3;
            }
            n++;
            n %= 5;
            switch (n) {
                case 0:
                    alims[i] = Instantiate(al01, pos, Quaternion.identity);
                    break;
                case 1:
                    alims[i] = Instantiate(al02, pos, Quaternion.identity);
                    break;
                case 2:
                    alims[i] = Instantiate(al03, pos, Quaternion.identity);
                    break;
                case 3:
                    alims[i] = Instantiate(al04, pos, Quaternion.identity);
                    break;
                case 4:
                    alims[i] = Instantiate(al05, pos, Quaternion.identity);                
                    break;
                default:
                    Debug.Log("WTF");
                    break;
            }
            alims[i].AddComponent<Regener>();
            alims[i].transform.parent = folder.transform;
            if (i == (totAlim - 1)) Destroy(this.gameObject.GetComponent<LabAlim>());
        }
        
    }
}
