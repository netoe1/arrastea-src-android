using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class itemColadoFrutas :
    MonoBehaviour,
    IDropHandler
{
    RectTransform rt;
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null && eventData.pointerDrag.gameObject.tag == gameObject.tag)
        {
            eventData.pointerDrag.gameObject.GetComponent<RectTransform>().anchoredPosition = rt.anchoredPosition;
            CenaDinamicaFrutas.acrescentaAcertos();
            //Debug.Log("Acertou!");
            gameObject.tag = "acerto";
            ConfiguraSom.tocarSomAcerto();
            CenaDinamicaFrutas.verificaFase();
        }
    }
}
