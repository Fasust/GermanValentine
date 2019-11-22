using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GasPaddle : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {
    public bool pressed;
    public void OnPointerDown(PointerEventData eventData) {
        pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        pressed = false;
    }

}
