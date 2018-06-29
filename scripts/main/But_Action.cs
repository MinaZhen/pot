using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class But_Action : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

    private Image _button;
    private AudioManager aM;
    private Settings settings;
    public Sprite but_up, but_dwn;
    [HideInInspector] public bool action = false;

    void Start () {
        _button = this.gameObject.GetComponent<Image>();
        //aM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        settings = GameObject.Find("Main").GetComponent<Settings>();
    }

    public virtual void OnDrag(PointerEventData ped) {
        Vector2 pos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_button.rectTransform, ped.position, ped.pressEventCamera, out pos)) {
        }
    }

    public virtual void OnPointerDown(PointerEventData ped) {
        OnDrag(ped);
        action = true;
        if ((this.gameObject.transform.name == "ButAction")|| (this.gameObject.transform.name == "ButRun")) {
           // aM.PlayFX(settings.fx[0]);
        }
        _button.sprite = but_dwn;
    }

    public virtual void OnPointerUp(PointerEventData ped) {
        action = false;
        _button.sprite = but_up;
    }
}
