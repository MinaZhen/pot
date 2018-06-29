using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLab : MonoBehaviour {

    public uint a, b;
    public float scaling = 1f;
    public GameObject pfWall, pfCol;

    [HideInInspector] public int available;


    [System.Serializable]
    public struct Cells {
        public bool visited;
        public uint ways;
        public GameObject w_N;
        public GameObject w_S;
        public GameObject w_E;
        public GameObject w_W;

    }

    private GameObject lab;

    public Cells[,] cells;
    private Cells checkCell;
    private Loading load;
    private LabAlim labAlim;

    private int i, id, dir, tx, ty, d = 0;
    private bool ch, hoek = false;

    void Start() {

        load = GameObject.Find("Loading").GetComponent<Loading>();
        labAlim = this.gameObject.GetComponent<LabAlim>();

        i = Random.Range(0, (int)a);
        id = Random.Range(0, (int)b);
        available = ((int)a * (int)b)-1;

        cells = new Cells[a, b];
        lab = GameObject.Find("Labyrinth");
        checkCell = cells[i, id];
        CreateWalls();
    }

    void Update() {

        if (!hoek) {
            if (available > 0) {
                load.loading = 0.7f;
                Crea();
                if (ch) {
                    CheckNewCell();
                }
            }
            if (available == 0) {
                Corners();
            }
        } 


    }

    private void CreateWalls() {

        GameObject[,,] tempWall = new GameObject[2, (a + 1), (b + 1)];

        for (int wa = 0; wa <= a; wa++) {
            for (int wb = 0; wb < b; wb++) {
                tempWall[0, wa, wb] = Instantiate(pfWall, new Vector3((float)wa * 2 - 1f, 0.001f, (float)wb * 2), Quaternion.Euler(-90f, 90f, 0f)) as GameObject;
                tempWall[0, wa, wb].transform.parent = lab.transform;
                tempWall[0, wa, wb].transform.name = ("W_[0," + wa.ToString("") + "," + wb.ToString("") + "]");
            }
        }

        for (int wa = 0; wa < a; wa++) {
            for (int wb = 0; wb <= b; wb++) {
                tempWall[1, wa, wb] = Instantiate(pfWall, new Vector3((float)wa * 2, 0f, (float)wb * 2 - 1f), Quaternion.Euler(-90f, 0f, 0f)) as GameObject;
                tempWall[1, wa, wb].transform.parent = lab.transform;
                tempWall[1, wa, wb].transform.name = ("W_[1," + wa.ToString("") + "," + wb.ToString("") + "]");
            }
        }
        
        for (int wa = 0; wa < a; wa++) {
            for (int wb = 0; wb < b; wb++) {
                
                cells[wa, wb].w_E = tempWall[0, (wa + 1), wb];
                cells[wa, wb].w_W = tempWall[0, wa, wb];
                cells[wa, wb].w_N = tempWall[1, wa, (wb + 1)];
                cells[wa, wb].w_S = tempWall[1, wa, wb];

                cells[wa, wb].ways = 4;
            }
        }
        load.loading = 0.3f;
    }


    private void Crea() {
        
        if (cells[i, id].ways > 0) { 
            cells[i, id].visited = true; 
            NewDirection();
        } else {
            cells[i, id].visited = true;
            tx = ty = 0;
            ch = true;
        }
    }

    private void Check() {  

        if ( ( (i == (a - 1)) || (((i + 1) <= (a - 1)) && (cells[(i + 1), id].visited))) &&
            ( (i == 0) || (((i - 1) >= 0) && (cells[(i - 1), id].visited))) &&
            ( (id == (b - 1)) || (((id + 1) <= (b - 1)) && (cells[i, (id + 1)].visited))) &&
            ( (id == 0) || (((id - 1) >= 0) && (cells[i, (id - 1)].visited))) )  {
            cells[i, id].ways = 0;
        }    
    }

    private void CheckNewCell() {

        for (tx = 0; tx < a; tx++) {
            for (ty = 0; ty < b; ty++) {
                if ((cells[tx, ty].visited) && ch) {
                    i = tx;
                    id = ty;
                    Check();
                    if (cells[i, id].ways != 0) {
                        ch = false;
                    }
                }
            }
        }
    }

    private void NewDirection() {
        d = Random.Range(1, 5);
        Direction();
    }

    private void Direction() {

        switch (d) {
            case 0:
                if (cells[i, id].ways != 0) {
                    
                    NewDirection();
                } else {
                    d = 5;
                }
                break;
            case 1:        // East
                if (((i + 1) <= (a - 1)) && (!cells[(i + 1), id].visited) && (cells[i, id].w_E != null)) {
                    Destroy(cells[i, id].w_E);
                    available--;
                    i++;
                    Check();
                    Crea();
                } else {
                    d = 0;
                }
                break;
            case 2:        // West
                if (((i - 1) >= 0) && (!cells[(i - 1), id].visited) && (cells[i, id].w_W != null)) {
                    Destroy(cells[i, id].w_W);
                    available--;
                    i--;
                    Check();
                    Crea();
                } else {
                    d = 0;
                }
                break;
            case 3:         // North
                if (((id + 1) <= (b - 1)) && (!cells[i, (id + 1)].visited) && (cells[i, id].w_N != null)) {
                    Destroy(cells[i, id].w_N);
                    available--;
                    id++;
                    Check();
                    Crea();
                } else {
                    d = 0;
                }
                break;
            case 4:         // South
                if (((id - 1) >= 0) && (!cells[i, (id - 1)].visited) && (cells[i, id].w_S != null)) {
                    Destroy(cells[i, id].w_S);
                    available--;
                    id--;
                    Check();
                    Crea();
                } else {
                    d = 0;
                }
                break;
            default:
                break;

        }
    }

    private void Corners() {
        
        
        GameObject[,] tempCol = new GameObject[(a + 1), (b + 1)];
        load.loading = 0.8f;

        for (uint wa = 0; wa <= a; wa++) {
            for (uint wb = 0; wb <= b; wb++) {
                uint wam = wa - 1;
                uint wbm = wb - 1;
                
                if (((GameObject.Find("W_[0," + wa.ToString("") + "," + wb.ToString("") + "]") != null) && (GameObject.Find("W_[0," + wa.ToString("") + "," + wbm.ToString("") + "]") == null))) {
                    if (((GameObject.Find("W_[1," + wa.ToString("") + "," + wb.ToString("") + "]") != null) && (GameObject.Find("W_[1," + wam.ToString("") + "," + wb.ToString("") + "]") == null))) {

                        tempCol[wa, wb] = Instantiate(pfCol, new Vector3((float)wa * 2 - 1, 0.001f, (float)wb * 2 - 1), Quaternion.Euler(-90f, 0f, 0f)) as GameObject;
                        tempCol[wa, wb].transform.parent = lab.transform;
                        tempCol[wa, wb].transform.name = ("Col_[" + wa.ToString("") + "," + wb.ToString("") + "]");

                    } else if (((GameObject.Find("W_[1," + wa.ToString("") + "," + wb.ToString("") + "]") == null) && (GameObject.Find("W_[1," + wam.ToString("") + "," + wb.ToString("") + "]") != null))) {

                        tempCol[wa, wb] = Instantiate(pfCol, new Vector3((float)wa * 2 - 1, 0.001f, (float)wb * 2 - 1), Quaternion.Euler(-90f, 90f, 0f)) as GameObject;
                        tempCol[wa, wb].transform.parent = lab.transform;
                        tempCol[wa, wb].transform.name = ("Col_[" + wa.ToString("") + "," + wb.ToString("") + "]");

                    }
                } else if (((GameObject.Find("W_[0," + wa.ToString("") + "," + wb.ToString("") + "]") == null) && (GameObject.Find("W_[0," + wa.ToString("") + "," + wbm.ToString("") + "]") != null))) {
                    if (((GameObject.Find("W_[1," + wa.ToString("") + "," + wb.ToString("") + "]") != null) && (GameObject.Find("W_[1," + wam.ToString("") + "," + wb.ToString("") + "]") == null))) {

                        tempCol[wa, wb] = Instantiate(pfCol, new Vector3((float)wa * 2 - 1, 0.001f, (float)wb * 2 - 1), Quaternion.Euler(-90f, 90f, 0f)) as GameObject;
                        tempCol[wa, wb].transform.parent = lab.transform;
                        tempCol[wa, wb].transform.name = ("Col_[" + wa.ToString("") + "," + wb.ToString("") + "]");

                    } else if (((GameObject.Find("W_[1," + wa.ToString("") + "," + wb.ToString("") + "]") == null) && (GameObject.Find("W_[1," + wam.ToString("") + "," + wb.ToString("") + "]") != null))) {

                        tempCol[wa, wb] = Instantiate(pfCol, new Vector3((float)wa * 2 - 1, 0.001f, (float)wb * 2 - 1), Quaternion.Euler(-90f, 0f, 0f)) as GameObject;
                        tempCol[wa, wb].transform.parent = lab.transform;
                        tempCol[wa, wb].transform.name = ("Col_[" + wa.ToString("") + "," + wb.ToString("") + "]");

                    }
                }
                
            }
        }
        
        lab.gameObject.transform.localScale = new Vector3(scaling, scaling, scaling);
        labAlim.doIt = true;
        load.loading = 1f;
        Debug.Log("Done");
        hoek = true;

    }
}
