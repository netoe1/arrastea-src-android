using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine.Tilemaps;

public class configura_wait: MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string nome_cena;


    private int milisec = 6660;


    private async void Start()
    {
        await Task.Delay(milisec);
        SceneManager.LoadScene(nome_cena);
    }
   
}
