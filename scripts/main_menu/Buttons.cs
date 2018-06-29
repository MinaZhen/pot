using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Buttons : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    private Image _button;
    private AudioManager aM;
    private Settings settings;
    public Sprite but_up, but_dwn;
    [HideInInspector] public bool doit = false;

    void Start() {
        _button = this.gameObject.GetComponent<Image>();
        aM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        settings = GameObject.Find("Main").GetComponent<Settings>();
    }

    public virtual void OnPointerDown(PointerEventData ped) {
        OnDrag(ped);
        _button.sprite = but_dwn;
        aM.PlayFX(settings.fx[0]);
    }

    public virtual void OnPointerUp(PointerEventData ped) {
        doit = true;
        _button.sprite = but_up;
    }


}