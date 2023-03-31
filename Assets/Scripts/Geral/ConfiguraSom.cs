using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ConfiguraSom : MonoBehaviour
{
    /*Esse script é resposável por configurar o som de acerto em todas as fases!*/

    private static AudioSource aSrc_oth;
    private static AudioSource aSrc_sonsClick;
    public static int  esperarSom = 1000;
    const string ACERTOPATH = "Sounds/Acerto/sound_acerto";
    const string CORPATH = "Sounds/Cores/sound_cores";
    const string FORMAPATH = "Sounds/Formas/sound_formas";
    const string FRUTAPATH = "Sounds/Frutas/sound_frutas";
    const string PINTARFRUTAPATH = "Sounds/PintarFrutas/sound_pintarfrutas";
    const string SOMPASSARFASE = "Sounds/Acerto/sound_acertomuitobem";

    const float ACERTO_VOLUME = 0.5f;
    const float OTH_VOLUME = 1f;


    private void Start()
    {
        aSrc_oth = gameObject.AddComponent<AudioSource>();
        aSrc_oth.clip = Resources.Load<AudioClip>(ACERTOPATH);
        aSrc_oth.playOnAwake = false;
        aSrc_oth.volume = ACERTO_VOLUME;


        aSrc_sonsClick = gameObject.AddComponent<AudioSource>();
        aSrc_sonsClick.volume = OTH_VOLUME;
        aSrc_sonsClick.playOnAwake = false;

    }

    public static void tocarSomAcerto()
    {
        aSrc_oth.clip = Resources.Load<AudioClip>(ACERTOPATH);
        aSrc_oth.Play();
    }

    public static async Task somPassarFase() 
    {
        await Task.Delay(0);
        aSrc_oth.clip = Resources.Load<AudioClip>(SOMPASSARFASE);
        aSrc_oth.Play();
    }


    public static void tocarSomCores(string tag) 
    {
        aSrc_sonsClick.clip = Resources.Load<AudioClip>(CORPATH + tag);
        aSrc_sonsClick.Play();
    }
    public static void tocarSomFormas(string tag)
    {
        aSrc_sonsClick.clip = Resources.Load<AudioClip>(FORMAPATH + tag);
        aSrc_sonsClick.Play();
    }
    public static void tocarSomFrutas(string tag)
    {
       // Debug.Log("Nome do arquivo:" + FRUTAPATH + tag);
        aSrc_sonsClick.clip = Resources.Load<AudioClip>(FRUTAPATH + tag);
        aSrc_sonsClick.Play();
    }

    public static void tocarSomPalmas(string tag)
    {
        aSrc_oth.clip = Resources.Load<AudioClip>(FRUTAPATH + tag);
        aSrc_oth.Play();
    }

    public static void tocarSomPintarFrutas(string tag) 
    {
        Debug.Log("Nome do arquivo:" + PINTARFRUTAPATH.ToString() + tag);
        aSrc_sonsClick.clip = Resources.Load<AudioClip>(PINTARFRUTAPATH + tag);
        aSrc_sonsClick.Play();
    }


}
