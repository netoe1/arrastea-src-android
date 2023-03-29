using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ConfigurarBtnFases : MonoBehaviour
{
    // Start is called before the first frame update
    public Button botaoCores;
    public Button botaoFrutas;
    public Button botaoFormas;
    public Button botaoPintarFrutas;

    private void Start()
    {
        botaoCores.onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene("TutorialCores");
        });

        botaoFormas.onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene("TutorialFormas");
        });

        botaoFrutas.onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene("TutorialFrutas");
        });

        botaoPintarFrutas.onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene("TutorialPintarFrutas");
        });
    }

}
