using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using RandForCs;
using System.Threading.Tasks;
using System;
using Unity.VisualScripting;

public class CenaDinamicaFrutas : MonoBehaviour
{

    /*Meta*/
    private static float scaleFactor;

    /*CONST MARGEM*/

    const float posInicialPadraoY = 4.25f;
    const float posDesconto = -2.25f;
    /*PARAMETROS FASE*/

    const int LIMITETAPA = 2;
    const int LIMITLEVEL = 4;
    static int faseAtual = 1;
    static int etapaAtual = 1;

    /*PATH PREFABS!*/

    const string FRUTAPATH = "PreFabs/Frutas/Fruta";
    const string FRUTASEMCORPATH = "PreFabs/Frutas/FrutaSemCor";

    /*PATH IMAGES*/

    const string IMAGESPATHFRUTAS = "NewAssets/Frutas";
    /*GAMEOBJECTS*/

    GameObject fruta;
    GameObject frutasemcor;
    public static RandForCs.Rand rand_sortear = new RandForCs.Rand();

    /*Tags permitidas!*/

    string[] TAGS = {"Maca","Banana","Uva","Coco","Kiwi","Laranja","Melancia","Melancia2"};
    private static List<string> tags = new List<string>();

    /*Dados da fase*/

    static int qtdInstanciados = 0;
    static int qtdAcertos = 0;

    /*Vector3 posicoes*/
    private static Vector3 posfruta;
    private static Vector3 posfrutasemcor;

    /*VARIACOES POSICOES*/

    float distanciafrutaX;
    private static float posYGeral;
    const float deltaPosfruta = 1.5f;

    private void Start()
    {
        scaleFactor = this.gameObject.GetComponent<Canvas>().scaleFactor;
        if (faseAtual != 1)
        {
            tags.Clear();
        }

        if (faseAtual == 4)
        {
            CenaDinamicaFrutas.redirecionarFimDeJogo();
        }
        qtdAcertos = 0;
        qtdInstanciados = faseAtual;
        posYGeral = posInicialPadraoY;
        distanciafrutaX = -5.5f;
        carregarGameObjects();
        organizarTagsEObjetos();
        mostrarDados();
    }
    public static int getLimitLevel()
    {
        return LIMITLEVEL;
    }

    void carregarGameObjects()
    {
        fruta = Resources.Load<GameObject>(FRUTAPATH);
        frutasemcor = Resources.Load<GameObject>(FRUTASEMCORPATH);

      //  Debug.Log("Gameobjects carregados!");
        return;
    }

    void organizarTagsEObjetos()
    {
        string[] tagssortaux1 = new string[faseAtual];
        string[] tagssortaux2 = new string[faseAtual];
        string aux_sort = " ";


        /*
         CONTROLADOR DE TAGS -> INÍCIO
         */

        if (faseAtual == LIMITETAPA && etapaAtual < 3)
        {
            rand_sortear.clearList();
        }

        if (faseAtual == 2 && etapaAtual == 1)
        {
            tags.Clear();
        }

        int i;

        for (i = 0; i < faseAtual; i++)
        {

            aux_sort = TAGS[rand_sortear.generateNumbers(TAGS.Length)].ToLower();
            if (tags.Contains(aux_sort) && faseAtual == 1)
            {
                i--;
            }
            else
            {
                tags.Add(aux_sort);
                tagssortaux1[i] = aux_sort;
            }
        }

        rand_sortear.clearList();

        for (i = 0; i < faseAtual; i++)
        {
            tagssortaux2[i] = tagssortaux1[rand_sortear.generateNumbers(tagssortaux1.Length)].ToLower();
        }

        /*
         CONTROLADOR DE TAGS -> FIM
         */


        for (i = 0; i < faseAtual; i++)
        {
            frutasemcor.tag = tagssortaux2[i];
         // Debug.Log("Sorteado fruta sem cor:" + frutasemcor.tag);
            frutasemcor.GetComponent<Image>().sprite = Resources.Load<Sprite>(IMAGESPATHFRUTAS + "/" + frutasemcor.tag + "semcor");
            frutasemcor.GetComponent<RectTransform>().localScale = new Vector3(2f, 2f);

            posYGeral += posDesconto;
            posfrutasemcor = new Vector3(distanciafrutaX * (-1) - deltaPosfruta, posYGeral);
            Instantiate(frutasemcor, posfrutasemcor, transform.rotation,gameObject.transform);
        }

        posYGeral = posInicialPadraoY;
        for (i = 0; i < faseAtual; i++)
        {
            fruta.tag = tagssortaux1[i];
           //Debug.Log("Sorteado fruta com cor:" + fruta.tag);
            fruta.GetComponent<Image>().sprite = Resources.Load<Sprite>(IMAGESPATHFRUTAS + "/" + fruta.tag);
            fruta.GetComponent<RectTransform>().localScale = new Vector3(2f, 2f);
            posYGeral += posDesconto;
            posfruta = new Vector3(distanciafrutaX, posYGeral);
            Instantiate(fruta, posfruta, transform.rotation,gameObject.transform);
        }
    }
    public static async void verificaFase()
    {
        if (qtdAcertos == qtdInstanciados)
        {
            await Task.Delay(1000);
            await ConfiguraSom.somPassarFase();
            await CenaDinamicaFrutas.passarDeFase();
        }
        else
        {
            Debug.Log("Você precisa acertar tudo!");
        }
    }

    public static void acrescentaAcertos()
    {
        if (qtdAcertos < qtdInstanciados)
        {
            qtdAcertos++;
        }
    }

    public static async Task passarDeFase()
    {
        qtdAcertos = 0;
        if (etapaAtual < LIMITETAPA && faseAtual <= LIMITLEVEL)
        {
            Debug.Log("PassarFase:passar de etapa");
            etapaAtual++;

        }
        else
        {
            Debug.Log("PassarFase:não passar de etapa");
            faseAtual++;
            etapaAtual = 1;

        }
        await Task.Delay(1000);
        SceneManager.LoadScene("FrutasFase");

    }

    public static void repetirFase()
    {
        qtdAcertos = 0;
        SceneManager.LoadScene("FrutasFase");
    }

    public static void redirecionarFimDeJogo()
    {
        qtdAcertos = 0;
        faseAtual = 1;
        SceneManager.LoadScene("AssetScenes");
    }

    public static void redirecionarMenu()
    {
        Debug.Log("Redirecionar Menu");
        SceneManager.LoadScene("MenuPrincipal");
    }

    void mostrarDados()
    {
        Debug.Log("Fase:" + faseAtual);
        Debug.Log("Etapa atual:" + etapaAtual);
    }

    public static int getFaseAtual()
    {
        return faseAtual;
    }

    public static void zerarTudo()
    {
        qtdAcertos = qtdInstanciados = 0;
        faseAtual = etapaAtual = 1;
        rand_sortear.clearList();
        tags.Clear();
    }

    public static float get_scaleFactor()
    {
        return scaleFactor;
    }

}
