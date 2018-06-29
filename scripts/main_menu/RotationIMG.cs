using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RotationIMG : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

    private Image img;
    private GameObject player;
    private Vector3 inputVector;
    [HideInInspector] public Vector3 vector;

    void Start() {
        img = this.GetComponent<Image>();
    }

    void Update() {
        Rotacion();       
    }
    
    public virtual void OnDrag(PointerEventData ped) {
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(img.rectTransform, ped.position, ped.pressEventCamera, out pos)) {
            
                pos.x = (pos.x / img.rectTransform.sizeDelta.x);
                pos.y = (pos.y / img.rectTransform.sizeDelta.y);
                inputVector = new Vector3(pos.x * 2, 0, pos.y * 2);
            }   
    }

    public virtual void OnPointerDown(PointerEventData ped) {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped) {
        inputVector = Vector3.zero;
    }

    private void Rotacion() {
        if (player == null) {
            player = GameObject.Find("Prota_01");
        } else {
            player.transform.Rotate(Vector3.up * Horizontal());
        }
    }

    private float Horizontal() {
        if (inputVector.x != 0) {
            return inputVector.x;
        } else {
            return Input.GetAxis("Horizontal");
        }
    }
}
