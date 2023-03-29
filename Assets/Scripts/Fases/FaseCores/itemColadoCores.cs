using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class itemColadoCores :
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
        const string IMAGESPATHCUBO = "NewAssets/Cores/Cubo/cubo";
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(IMAGESPATHCUBO + tag); 
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null && eventData.pointerDrag.gameObject.tag == gameObject.tag)
        {
            eventData.pointerDrag.gameObject.GetComponent<RectTransform>().anchoredPosition = rt.anchoredPosition;
            CenaDinamicaCores.acrescentaAcertos();
            this.trocaImagem(tag);
            Debug.Log("Acertou!");
            gameObject.tag = "acerto";
            ConfiguraSom.tocarSomAcerto();
            CenaDinamicaCores.verificaFase();
        }
    }
}
