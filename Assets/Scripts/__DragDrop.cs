using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class __DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler 
{
    private RectTransform rt;
    private CanvasGroup grupo;

    private void Awake() {
        rt = GetComponent<RectTransform>();
        grupo = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData){
        grupo.alpha = 0.3f;
        grupo.blocksRaycasts = false;
        Debug.Log("BeginDrag");
    }    
    public void OnDrag(PointerEventData eventData){
        rt.anchoredPosition += eventData.delta;
        Debug.Log("OnDrag");
    }
    public void OnEndDrag(PointerEventData eventData){
        grupo.alpha = 1f;
        grupo.blocksRaycasts = true;
        Debug.Log("EndDrag");
    }
    public void OnPointerDown(PointerEventData eventData){
        Debug.Log("Pointer");
    }

}
