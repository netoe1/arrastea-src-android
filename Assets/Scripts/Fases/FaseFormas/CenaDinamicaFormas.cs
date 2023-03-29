using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RandForCs;
using System.Threading.Tasks;


/*
 VER BOTAO DE PASSAR FASE E EFEITOS
BACKUP FEITO E NO ONEDRIVE
 */
public class CenaDinamicaFormas : MonoBehaviour
{
    /*Meta*/
    private static float scaleFactor;
    [SerializeField] GameObject painelEsquerdo;
    [SerializeField] GameObject painelDireito;

    /*CONST MARGEM*/

    const float posInicialPadraoY = 4.25f;
    const float posDesconto = -2.25f;
    /*PARAMETROS FASE*/

    const int LIMITETAPA = 2;
    const int LIMITLEVEL = 4;
    static int faseAtual = 1;
    static int etapaAtual = 1;

    /*PATH PREFABS!*/

    const string FORMAPATH = "PreFabs/FormasGeometricas/Forma";
    const string SOMBRAPATH = "PreFabs/FormasGeometricas/Sombra";


    /*PATH IMAGES*/

    const string IMAGESPATHFORMAS = "NewAssets/Formas";
    /*GAMEOBJECTS*/

    GameObject forma;
    GameObject sombra;
    public static RandForCs.Rand rand_sortear = new RandForCs.Rand();

    /*Tags permitidas!*/

    string[] TAGS = { "Triangulo", "Circulo", "Quadrado","Pentagono","Hexagono","Estrela" };
    private static List<string> tags = new List<string>();

    /*Dados da fase*/

    static int qtdInstanciados = 0;
    static int qtdAcertos = 0;

    /*Vector3 posicoes*/
    private static Vector3 posForma;
    private static Vector3 posSombra;

    /*VARIACOES POSICOES*/

    float distanciaFormaX;
    private static float posYGeral;
    const float deltaPosForma = 1.5f;

    private void Start()
    {
        scaleFactor = this.gameObject.GetComponent<Canvas>().scaleFactor;
        if (faseAtual != 1)
        {
            tags.Clear();
        }

        if (faseAtual == 4)
        {
            CenaDinamicaFormas.redirecionarFimDeJogo();
        }

        qtdAcertos = 0;
        qtdInstanciados = faseAtual;
        posYGeral = posInicialPadraoY;
        distanciaFormaX = -6f;
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
        forma = Resources.Load<GameObject>(FORMAPATH);
        sombra = Resources.Load<GameObject>(SOMBRAPATH);
        Debug.Log("Gameobjects carregados!");
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
            sombra.tag = tagssortaux2[i];
            Debug.Log("Carregar sombra:"  + IMAGESPATHFORMAS + "/" + sombra.tag + "sombra");
            sombra.GetComponent<Image>().sprite = Resources.Load<Sprite>(IMAGESPATHFORMAS + "/" + sombra.tag + "sombra");
            sombra.GetComponent<RectTransform>().localScale = new Vector3(2f, 2f);
            sombra.GetComponent<Image>().color = Color.black;

            posYGeral += posDesconto;
            posSombra = new Vector2((distanciaFormaX + deltaPosForma) * -1,posYGeral);
            Instantiate(sombra, posSombra,transform.rotation,gameObject.transform);
        }

        posYGeral = posInicialPadraoY;
        for (i = 0; i < faseAtual; i++)
        {
            forma.tag = tagssortaux1[i];
            Debug.Log("Carregar sombra:" + IMAGESPATHFORMAS + "/" + forma.tag);
            forma.GetComponent<Image>().sprite = Resources.Load<Sprite>(IMAGESPATHFORMAS + "/" + forma.tag);
          //  forma.GetComponent<RectTransform>().localScale = new Vector3(2f, 2f);
            posYGeral += posDesconto;
            posForma = new Vector3(distanciaFormaX, posYGeral);
            Instantiate(forma, posForma, transform.rotation,gameObject.transform);
        }
    }
    public static async void verificaFase()
    {
        if (qtdAcertos == qtdInstanciados)
        {
            await Task.Delay(1000);
            await ConfiguraSom.somPassarFase();
            await CenaDinamicaFormas.passarDeFase();
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
        SceneManager.LoadScene("FormasFase");
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
