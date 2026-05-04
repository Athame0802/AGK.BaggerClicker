using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductManager : MonoBehaviour
{
    public Product[] ProductArray = new Product[3];

    void Awake()
    {
        for(int i = 0; i < ProductArray.Length; i++)
        {
            Transform currentChild = transform.GetChild(i);

            currentChild.GetChild(0).GetComponent<Image>().sprite = (Sprite)ProductArray[i]["productSprite"];
            currentChild.GetChild(1).GetComponent<TMP_Text>().text = (string)ProductArray[i]["productKoreanName"];
            currentChild.GetChild(2).GetComponent<TMP_Text>().text = (string)ProductArray[i]["productPriceWithWon"];
        }
    }
}
