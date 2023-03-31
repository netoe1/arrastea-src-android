using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfigurarBtns : MonoBehaviour
{
    [SerializeField] private Button __botao;
    [SerializeField] private string __nome_fase;


    private void Start()
    {
        __botao.onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene(__nome_fase);
        });
    }
}
