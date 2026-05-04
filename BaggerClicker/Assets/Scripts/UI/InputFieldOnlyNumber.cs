using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class InputFieldOnlyNumber : MonoBehaviour
{
    private TMP_InputField _inputField;

    void Awake()
    {
        _inputField = gameObject.GetComponent<TMP_InputField>();
    }

    void Update()
    {
        Regex.Replace(_inputField.text, @"[^0-9]+", "");
    }
}
