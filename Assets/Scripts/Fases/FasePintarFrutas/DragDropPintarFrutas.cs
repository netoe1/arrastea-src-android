using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDropPintarFrutas :
    MonoBehaviour,
    IPointerDownHandler,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{ 

    /*Esta classe pertence aos gameobjects que ser�o arrastados!*/


    /*obj UI*/
    const float alpha = 0.5f;
    Vector3 posicaoInicial;
    /*Propriedades*/
    private RectTransform rt;
    private CanvasGroup cg;


    void Awake()
    {
        rt = GetComponent<RectTransform>();
        posicaoInicial = rt.anchoredPosition;

        cg = GetComponent<CanvasGroup>();
        cg.blocksRaycasts = true;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        cg.alpha -= alpha;
        cg.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {

        rt.anchoredPosition += eventData.delta / CenaDinamicaPintarFrutas.get_scaleFactor();
      
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        cg.alpha += alpha;
        cg.blocksRaycasts = true;
        rt.anchoredPosition = posicaoInicial;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ConfiguraSom.tocarSomPintarFrutas(this.gameObject.tag);
    }
}
