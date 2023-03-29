using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class itemColadoFormas :
    MonoBehaviour,
    IDropHandler
{
    RectTransform rt;

    void Start()
    {
        rt = this.gameObject.GetComponent<RectTransform>();
    }

    private void trocarImagem(string tag)
    {
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("NewAssets/Formas/" + tag);
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null && eventData.pointerDrag.gameObject.tag == gameObject.tag)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = rt.anchoredPosition;
            CenaDinamicaFormas.acrescentaAcertos();
            this.trocarImagem(gameObject.tag);
            Debug.Log("Acertou!");
            gameObject.tag = "acerto";
            ConfiguraSom.tocarSomAcerto();
            CenaDinamicaFormas.verificaFase();
        }
    }
}
