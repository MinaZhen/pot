using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystic : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

    private Image _joysticBG, _joy;
    public Sprite joy1, joy2;

    private Vector3 inputVector;
    [HideInInspector] public Vector3 vector;
    [HideInInspector] public bool move = true;


    void Start() {
        _joysticBG = this.GetComponent<Image>();
        _joy = this.transform.GetChild(0).GetComponent<Image>();
        move = true;

    }
    public virtual void OnDrag(PointerEventData ped) {
        
            Vector2 pos;
            if(RectTransformUtility.ScreenPointToLocalPointInRectangle(_joysticBG.rectTransform, ped.position, ped.pressEventCamera, out pos)) {
                pos.x = (pos.x / _joysticBG.rectTransform.sizeDelta.x);
                pos.y = (pos.y / _joysticBG.rectTransform.sizeDelta.y);

                inputVector = new Vector3(pos.x * 2, 0, pos.y * 2);
                inputVector = (inputVector.magnitude > 1f) ? inputVector.normalized : inputVector;

                _joy.rectTransform.anchoredPosition = new Vector3((inputVector.x * (_joysticBG.rectTransform.sizeDelta.x / 3)), (inputVector.z * (_joysticBG.rectTransform.sizeDelta.y / 3)));
            }
        
    }

    public virtual void OnPointerDown(PointerEventData ped) {
        OnDrag(ped);
        _joy.sprite = joy2;
    }

    public virtual void OnPointerUp(PointerEventData ped) {

        inputVector = Vector3.zero;
        _joy.rectTransform.anchoredPosition = Vector3.zero;
        _joy.sprite = joy1;
    }

    public float Horizontal() {
        if (!move) inputVector.x = 0f;
        if (inputVector.x != 0) {
            return inputVector.x;
        } else {
            return Input.GetAxis("Horizontal");
        }    
    }

    public float Vertical() {
        if (!move) inputVector.z = 0f;
        if (inputVector.z != 0) {
            return inputVector.z;
        } else {
            return Input.GetAxis("Vertical");
        }
    }
}
