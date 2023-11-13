using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    Vector3 dragOffset;
    

    public void OnBeginDrag(PointerEventData EventData)
    {
        dragOffset = transform.position - Input.mousePosition;
    }
    public void OnDrag(PointerEventData EventData)
    {
        transform.position = Input.mousePosition + dragOffset;   
    }
}
