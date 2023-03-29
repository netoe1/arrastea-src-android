using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using RandForCs;
using System.Threading.Tasks;
using Unity.VisualScripting;


/*
 VER BOTAO DE PASSAR FASE E EFEITOS
BACKUP FEITO E NO ONEDRIVE
 */
public class CenaDinamicaPintarFrutas : MonoBehaviour
{
    /*Meta*/
    private static float scaleFactor;
    [SerializeField] GameObject painelEsquerdo;
    [SerializeField] GameObject painelDireito;

    const int DELAY = 1000;
    /*PARAMETROS FASE*/

    const int LIMITETAPA = 2;
    const int LIMITLEVEL = 4;
    static int faseAtual = 1;
    static int etapaAtual = 1;

    /*PATH PREFABS!*/

    const string BALDEPATH = "PreFabs/Balde/Balde_pintarfrutas";
    const string FRUTAPATH = "PreFabs/PintarFrutas/Fruta";


    /*PATH IMAGES*/

    const string IMAGESPATHfruta = "NewAssets/PintarFrutas";
    const string IMAGESPATHBALDE = "NewAssets/PintarFrutas";

    /*GAMEOBJECTS*/

    GameObject balde;
    GameObject fruta;
    public static RandForCs.Rand rand_sortear = new RandForCs.Rand();

    /*Tags permitidas!*/

    string[] TAGS = { "macavermelho", "cocomarrom", "uvaroxo", "melanciaverde", "bananaamarelo", "laranjalaranja" };
    private static List<string> tags = new List<string>();

    /*Dados da fase*/

    static int qtdInstanciados = 0;
    static int qtdAcertos = 0;

    /*Vector3 posicoes*/
    Vector3 posFruta;
    Vector3 posBalde;
    Vector3 posIndicativo;

    /*VARIACOES POSICOES*/

    float distanciaBaldeX;
    private static float posYGeral;
    const float deltaPosCubo = 1.5f;
    const float posicaoInicial = 4.25f;

    /*
      AUDIOS
     */

    private void Start()
    {
        scaleFactor = this.gameObject.GetComponent<Canvas>().scaleFactor;
        if (faseAtual != 1)
        {
            tags.Clear();
        }

        if (faseAtual == 4)
        {
            CenaDinamicaPintarFrutas.redirecionarFimDeJogo();
        }

        qtdAcertos = 0;
        qtdInstanciados = faseAtual;
        posYGeral = posicaoInicial;
        distanciaBaldeX = -5.5f;
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
        balde = Resources.Load<GameObject>(BALDEPATH);
        fruta = Resources.Load<GameObject>(FRUTAPATH);
 
        //    Debug.Log("Gameobjects carregados!");
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
        for (int x = 0; x < tags.Count; x++)
        {
            Debug.Log("TAGBUFFER:" + tags[x]);
        }
        */

        for (i = 0; i < faseAtual; i++)
        {
            fruta.tag = tagssortaux2[i];
            fruta.GetComponent<Image>().sprite = Resources.Load<Sprite>(IMAGESPATHfruta + "/" + fruta.tag + "semcor");
            //fruta.GetComponent<RectTransform>().localScale = new Vector3(2f, 2f);


            /*
               Instanciando o indicativo
             */

            posYGeral += -2.25f;
            posFruta = new Vector3(distanciaBaldeX * (-1) - deltaPosCubo, posYGeral);
            Instantiate(fruta, posFruta, painelDireito.transform.rotation, painelDireito.transform);
        }

        posYGeral = posicaoInicial;
        for (i = 0; i < faseAtual; i++)
        {
            balde.tag = tagssortaux1[i];
            balde.GetComponent<Image>().sprite = Resources.Load<Sprite>(IMAGESPATHBALDE+ "/balde" + balde.tag);
            balde.GetComponent<RectTransform>().localScale = new Vector3(2f, 2f);
            posYGeral += -2.25f;
            posBalde = new Vector3(distanciaBaldeX, posYGeral);
            Instantiate(balde, posBalde, painelEsquerdo.transform.rotation, painelEsquerdo.transform);
        }
    }
    public static async void verificaFase()
    {
        if (qtdAcertos == qtdInstanciados)
        {
            await Task.Delay(1000);
            await ConfiguraSom.somPassarFase();
            await passarDeFase();
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
        SceneManager.LoadScene("PintarFrutasFase");

    }

    public static void repetirFase()
    {
        qtdAcertos = 0;
        SceneManager.LoadScene("CoresFase");
    }

    public static void redirecionarFimDeJogo()
    {
        qtdAcertos = 0;
        faseAtual = 1;
        SceneManager.LoadScene("AssetScenes");
    }

    public static void redirecionarMenu()
    {
        SceneManager.LoadScene("MenuInicial");
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
