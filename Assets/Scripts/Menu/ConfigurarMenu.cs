using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfigurarMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Button botaoPlay = null;
    public Button botaoVoltar = null;

    
    const string cenaSelecionarFase = "selecionarFase";
    const string cenaMenuPrincipal = "menuInicial";

    private void Start()
    {

        if(botaoPlay != null) 
        {
            botaoPlay.onClick.AddListener(delegate () 
            {
                SceneManager.LoadScene(cenaSelecionarFase);
            });
        }
        else if(botaoVoltar != null)
        {
            botaoVoltar.onClick.AddListener(delegate ()
            {
                SceneManager.LoadScene(cenaMenuPrincipal);
            });
        }
       

    }
}
