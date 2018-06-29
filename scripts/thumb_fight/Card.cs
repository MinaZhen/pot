using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

    private Cards cards;
    private Image space, zone, me;
    private AudioManager aM;
    private Settings settings;

    private Color c1b, c2b, cF, cR, cE, cA;
    private Vector2 posIni;
    private Vector3 pose;
    private Quaternion rot1, rot2;

    [HideInInspector] public uint kind, used = 0;

    // Use this for initialization
    void Start () {

        zone = GameObject.Find("zone").GetComponent<Image>();
        me = this.gameObject.GetComponent<Image>();
        cards = GameObject.Find("Main").GetComponent<Cards>();


        aM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        settings = GameObject.Find("Main").GetComponent<Settings>();

        c1b = new Color(0.55f, 0.45f, 0.35f);
        c2b = new Color(0.54f, 0.44f, 0.34f);
        cR = new Color(0f, 1f, 0f, 0.15f);
        cF = new Color(1f, 0f, 0f, 0.15f);
        cE = new Color(1f, 1f, 0f, 0.15f);
        cA = new Color(1f, 1f, 1f, 0f);


        rot1 = new Quaternion(0f, 0f, 0.7f, 0.7f);
        rot2 = new Quaternion(0f, 0f, -0.7f, 0.7f);
        space = this.GetComponent<Image>();
        posIni = space.rectTransform.anchoredPosition;
        AddCard();
    }

    public virtual void OnDrag(PointerEventData ped) {
        
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(zone.rectTransform, ped.position, ped.pressEventCamera, out pos)) {
            pos.x = (pos.x / zone.rectTransform.sizeDelta.x);
            pos.y = (pos.y / zone.rectTransform.sizeDelta.y);

            pose = new Vector3(pos.x, 0f, pos.y);
            
            space.rectTransform.anchoredPosition = new Vector3((pose.x * zone.rectTransform.sizeDelta.x), (pose.z * zone.rectTransform.sizeDelta.y));
            //Debug.Log(space.rectTransform.anchoredPosition);
        }
        
    }
        
    public virtual void OnPointerDown(PointerEventData ped) {
        if (cards.inGame) {
            OnDrag(ped);
            aM.PlayFX(settings.fx[0]);
        }
    }
        
    public virtual void OnPointerUp(PointerEventData ped) {
        
        for (uint a = 0; a < 2; a++) {
            for (uint b = 0; b < 4; b++) {
                if (cards.inGame) CheckSpot(cards.p[a, b]);
            }
        }
        
        space.rectTransform.anchoredPosition = posIni;
        
    }

    private void CheckSpot(Image spot) {
        
        if ((space.rectTransform.anchoredPosition.x > (spot.rectTransform.anchoredPosition.x - 50f))
            &&
            (space.rectTransform.anchoredPosition.x < (spot.rectTransform.anchoredPosition.x + 50f))
            &&
            (space.rectTransform.anchoredPosition.y > (spot.rectTransform.anchoredPosition.y - 50f))
            &&
            (space.rectTransform.anchoredPosition.y < (spot.rectTransform.anchoredPosition.y + 50f))) {
            
            if ((spot.color == cards.c1)|| (spot.color == cards.c2)) {
                SpotBeh(spot);
                me.sprite = null;
                AddCard();
                aM.PlayFX(settings.fx[1]);
            } 
            
        }
    }

    private void SpotBeh(Image spot) {
        uint pp = 0;
        bool suma = false;

        switch (kind) {
            case 1:
                pp = 1;
                suma = true;
               break;
            case 2:
                pp = 10;
                suma = true;
                break;
            case 3:
                pp = 100;
                suma = true;
                break;

        }
        if (suma) { 
            if (spot.color == cards.c1) {
                spot.sprite = me.sprite;
                spot.color = c1b; //cambia de color
                spot.gameObject.transform.rotation = rot1;
                cards.puntos_p1 += pp;
                cards.p1 ++;
                if (cards.p1 == 4) {
                    cards.del1 = true;
                    //suma puntos a score
                    cards.p1 = 0;
                }
                
                suma = false;
            } else {
                spot.sprite = me.sprite;
                spot.color = c2b; //cambia de color
                spot.gameObject.transform.rotation = rot2;
                cards.puntos_p2 += pp;
                cards.p2++;
                if (cards.p2 == 4) {
                    cards.del2 = true;
                    //suma puntos a score
                    cards.p2 = 0;
                }
                
                suma = false;
            }
        }


    }

    public void AddCard() {
        
        if (me.sprite == null) {
            int idx = Random.Range(0, 15);
            if (!cards.choose[idx].used) {
                me.gameObject.transform.Rotate(0f, 0f, Random.Range(0, 4) * 90f);
                me.sprite = cards.food[idx];

                // Damos propiedades
                if (idx < 5) {
                    //me.color = Color.red;
                    kind = 1;
                } else if (idx > 9) {
                    //me.color = Color.yellow;
                    kind = 2;
                } else {
                    //me.color = Color.green;
                    kind = 3;
                }
                cards.choose[idx].used = true;
                cards.id++;
            } else {
                if (cards.id == 15) {
                    for (uint i = 0; i < 15; i++) {
                        cards.choose[i].used = false;
                    }
                    cards.id = 0;
                }
                AddCard();
            }

        }
    }
}
