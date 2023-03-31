using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ConfiguraSprites : MonoBehaviour
{
    
    [SerializeField] private Image menino;
    [SerializeField] private Image menina;
    const int limit_sprites = 8;

    private const string __PATH = "NewAssets/CenaFinal";

    System.Random random;

    private void Start()
    {
        random = new System.Random();
        menino.sprite = Resources.Load<Sprite>(__PATH + "/Menino/sprite_" + random.Next(1,limit_sprites).ToString());
        menina.sprite = Resources.Load<Sprite>(__PATH + "/Menina/sprite_" + random.Next(1, limit_sprites).ToString());
    }
}
