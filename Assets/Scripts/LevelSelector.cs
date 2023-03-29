using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public int level;


    void usarBotao_zerarTudo()
    {

        /*
            Limpa listas para evitar bugs;
         */
        CenaDinamicaCores.rand_sortear.clearList();
        CenaDinamicaFrutas.rand_sortear.clearList();
        CenaDinamicaPintarFrutas.rand_sortear.clearList();
        CenaDinamicaFormas.rand_sortear.clearList();


        /*
            Zera todas as pontuações e afins!
         */

        CenaDinamicaCores.zerarTudo();
        CenaDinamicaFrutas.zerarTudo();
        CenaDinamicaPintarFrutas.zerarTudo();
        CenaDinamicaFormas.zerarTudo();


        return;
    }
    public void OpenScene(){


        usarBotao_zerarTudo();
        SceneManager.LoadScene(level);
    }
}
