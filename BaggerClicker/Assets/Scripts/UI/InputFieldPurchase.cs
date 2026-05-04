using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(InputFieldOnlyNumber), typeof(TMP_InputField))]
public class InputFieldPurchase : MonoBehaviour
{
    private TMP_InputField _inputComponent;
    private ShopManager _shopManager;
    [SerializeField] private Product productAsset;

    void Awake()
    {
        _inputComponent = GetComponent<TMP_InputField>();
        _shopManager = FindAnyObjectByType<ShopManager>();
    }

    public void InputEnd()
    {
        if(!System.Int32.TryParse(_inputComponent.text, out int result))
        {
            return;
        }

        _inputComponent.text = "0";

        _shopManager.Purchase(result, productAsset);
    }

    public void PlusButton()
    {
        if(!System.Int32.TryParse(_inputComponent.text, out int result))
        {
            return;
        }

        result++;

        _inputComponent.text = result.ToString();
    }

    public void MinusButton()
    {
        if (!System.Int32.TryParse(_inputComponent.text, out int result))
        {
            return;
        }

        result--;

        _inputComponent.text = result.ToString();
    }
}
