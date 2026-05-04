using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlyButtonPurchase : MonoBehaviour
{
    [SerializeField] private Product product;
    private ShopManager _shopManager;

    void Awake()
    {
        _shopManager = FindAnyObjectByType<ShopManager>();
    }

    public void ClickButton()
    {
        _shopManager.Purchase(1, product);
    }
}
