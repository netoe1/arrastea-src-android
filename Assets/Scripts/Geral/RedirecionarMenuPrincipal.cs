using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedirecionarMenuPrincipal: MonoBehaviour
{
    private void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(chamarRedirecionar);
    }
    public void chamarRedirecionar()
    {
        CenaDinamicaFormas.redirecionarMenu();
    }
}
