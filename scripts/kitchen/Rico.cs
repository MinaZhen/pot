using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rico : MonoBehaviour {

    private GameObject player, waypoint, plat;

    private NavMeshAgent nav;
    private Animator anim;
    private CapsuleCollider caps;

    public GameObject wp_ini, wp_olla, wp_tabla, wp_grill, wp_pick, wp_pase;
    

   
    public float patrolSpeed = 3.5f;                            
    [HideInInspector] public uint platos = 0;
    [HideInInspector] public bool taked = false;

    private int curWaypoint = 1;
    private int maxWaypoint = 6;
    private bool rota = false;
    private Vector3 posStop = Vector3.zero;

    private uint walk = 0;
    private float timer = 0f;
    

    public float minWaypointDistance = 0.1f;

    [HideInInspector] public uint state = 0;

    void Start() {

        nav = this.gameObject.GetComponent<NavMeshAgent>();

        anim = this.GetComponent<Animator>();
        if (anim == null) Debug.Log("404: anim in Rico");

        caps = this.GetComponent<CapsuleCollider>();
        if (caps == null) Debug.Log("404: caps in Rico");

        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) Debug.LogError("404: Player in Rico");

        nav.acceleration = 200f;

        
    }

    void Update() {
        switch (state) {
            case 0: //wp_Ini
                timer = 0;
                if (!Kitchen.endKitchen) wp_Ini();
                break;
            case 1: 
                timer += (Time.deltaTime);
                if (timer > 1f) state++;
                Rota(wp_pick);
                break;
            case 2: //GO
                Gogogo(wp_pick);
                timer = 0f;
                break;
            case 3:
                timer += (Time.deltaTime);
                if (timer > 1f) state++;
                anim.SetFloat("Forward", 0f);
                anim.SetBool("dish", true);
                Rota(GameObject.Find("fork"));

                break;
            case 4: // pick plate
                timer += (Time.deltaTime);
                if (timer > 0.5f) {
                    anim.SetLayerWeight(1, 1f);
                    timer = 0f;
                    state = 5;
                }
                break;
            case 5:
                timer += (Time.deltaTime);

                if (timer > 2f) {
                    timer = 0;
                    taked = true;
                    if ((anim.GetCurrentAnimatorStateInfo(1).IsName("picked"))) {
                        taked = false;
                        state++;  // goes to pass decission
                    }
                }
                break;
            case 6:
                timer += (Time.deltaTime);
                if (timer > 1f) state++;

                Rota(wp_pase);
                break;
            case 7:
                
                timer = 0f;
                Gogogo(wp_pase);
                break;
            case 8:
                timer += (Time.deltaTime);
                if (timer > 1f) state++; 
                this.gameObject.transform.position = posStop;
                anim.SetFloat("Forward", 0f);
                anim.SetBool("dish", false);
                Rota(GameObject.Find("ImageEmpty"));
                break;

            case 9:
                Destroy(plat.gameObject);
                timer -= (Time.deltaTime);
                anim.SetLayerWeight(1, timer);
                if ((anim.GetCurrentAnimatorStateInfo(1).IsName("idle"))) {
                    if (anim.GetLayerWeight(1) < 0f) {
                        anim.SetLayerWeight(1, 0f);
                        timer = 0f;
                        state++;
                    }
                }
                    break;
            case 10:     
                timer += (Time.deltaTime);
                if (timer > 1f) state++;

                Rota(wp_ini);
                break;

            case 11:
                Gogogo(wp_ini);
                break;
            case 12:
                this.gameObject.transform.position = posStop;
                anim.SetFloat("Forward", 0f);
                platos -= 1;
                state = 0;
                break;
            case 13:
                
                break;

            default:
                break;
        }
    }

    public void Gogogo(GameObject wp) {

        anim.SetFloat("Forward", 1f);
        nav.speed = patrolSpeed;

        Vector3 tempLocalPosition;
        Vector3 tempWaypointPosition;

        tempLocalPosition = this.gameObject.transform.position;
        tempLocalPosition.y = 0f;

        tempWaypointPosition = wp.transform.position;
        tempWaypointPosition.y = 0f;

        if (Vector3.Distance(tempLocalPosition, tempWaypointPosition) <= minWaypointDistance) {
            posStop = this.gameObject.transform.position;
            nav.speed = 0f;
            nav.velocity = Vector3.zero;
            state ++;
        }
            nav.SetDestination(wp.transform.position);
    }

    void Rota(GameObject wp) {
        
        Vector3 rPos = wp.transform.position - this.gameObject.transform.position;
        rPos.y = this.gameObject.transform.position.y;
        Quaternion rot = Quaternion.LookRotation(rPos);
        Quaternion current = this.gameObject.transform.localRotation;

        this.gameObject.transform.localRotation = Quaternion.Slerp(current, rot, Time.deltaTime * 3);
    }
    
    private void wp_Ini() {
        if (platos > 0) {
            if (plat == null) {
                plat = GameObject.FindGameObjectWithTag("platoPase");
                state = 1;
            }
            
        }
    }
}