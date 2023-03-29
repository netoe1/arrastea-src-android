using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MostrarFaseCores : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Start()
    {
        text.text = CenaDinamicaCores.getFaseAtual() + "-" + CenaDinamicaCores.getLimitLevel();
    }
}
