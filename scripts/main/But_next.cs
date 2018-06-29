using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class But_next : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    private Image _button;
    public Sprite but_up, but_dwn;
    [HideInInspector]
    public bool next = false;
    private AudioManager aM;
    private Settings settings;

    void Start() {
        _button = this.gameObject.GetComponent<Image>();
        aM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        settings = GameObject.Find("Main").GetComponent<Settings>();
    }

    public virtual void OnPointerDown(PointerEventData ped) {
        OnDrag(ped);
        next = true;
        aM.PlayFX(settings.fx[0]);
        _button.sprite = but_dwn;
    }

    public virtual void OnPointerUp(PointerEventData ped) {
        next = false;
        _button.sprite = but_up;
    }

    public void Desmarca() {
        next = false;
        _button.sprite = but_up;
    }

    
}
