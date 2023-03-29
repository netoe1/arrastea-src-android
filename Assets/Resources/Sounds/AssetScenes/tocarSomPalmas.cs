using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tocarSomPalmas : MonoBehaviour
{
    const string PALMASPATH = "/Sounds/Palmas/sound_palmas";

    private void Start()
    {
        AudioSource asrc;
        asrc = gameObject.AddComponent<AudioSource>();

        asrc.volume = 0.5f;
        asrc.playOnAwake = false;
        asrc.loop = false;
        asrc.Play();

    }

}
