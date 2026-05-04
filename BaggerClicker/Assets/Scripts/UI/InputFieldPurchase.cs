using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
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

        if (result <= 0)
        {
            return;
        }

        _shopManager.Purchase(result, productAsset);
    }

    public void PlusButton()
    {
        if(!System.Int32.TryParse(_inputComponent.text, out int result))
        {
            _inputComponent.text = "0";
            result = 0;
        }

        result++;

        _inputComponent.text = result.ToString();
    }

    public void MinusButton()
    {
        if (!System.Int32.TryParse(_inputComponent.text, out int result))
        {
            _inputComponent.text = "0";
            result = 0;
        }

        if (result > 0)
        {
            result--;
        }

        _inputComponent.text = result.ToString();
    }
}
