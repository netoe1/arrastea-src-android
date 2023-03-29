using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class itemColadoPintarFrutas :
    MonoBehaviour,
    IDropHandler
{
    RectTransform rt;
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    void trocaImagem(string tag)
    {
        const string IMAGESPATHFRUTAS = "NewAssets/PintarFrutas";
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(IMAGESPATHFRUTAS + "/" + tag); 
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null && eventData.pointerDrag.gameObject.tag == gameObject.tag)
        {
            eventData.pointerDrag.gameObject.GetComponent<RectTransform>().anchoredPosition = rt.anchoredPosition;
            CenaDinamicaPintarFrutas.acrescentaAcertos();
            this.trocaImagem(tag);
            Debug.Log("Acertou!");
            gameObject.tag = "acerto";
            ConfiguraSom.tocarSomAcerto();
            CenaDinamicaPintarFrutas.verificaFase();
        }
    }
}
