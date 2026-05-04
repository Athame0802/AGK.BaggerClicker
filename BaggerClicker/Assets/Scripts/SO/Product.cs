using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProductData", menuName = "ScriptableObject/productScriptable/CreateProductData")]
public class Product : ScriptableObject, IIndexable
{
    [SerializeField] string productName;
    [SerializeField] string productKoreanName;
    [SerializeField] Sprite productSprite;
    [SerializeField] ulong productPrice;

    public object this[string index]
    {
        get
        {
            if (index == "productName")
            {
                return productName;
            }
            else if (index == "productKoreanName")
            {
                return productKoreanName;
            }
            else if (index == "productSprite")
            {
                return productSprite;
            }
            else if (index == "productPrice")
            {
                return productPrice;
            }
            else if (index == "productPriceWithWon")
            {
                return productPrice + "원";
            }
            else
            {
                D.LogError($"Product 인덱서에 잘못된 접근이 발생했습니다. 이는 심각한 오류를 발생시킬 수 있습니다. index로 입력된 값 : {index}", this);
                return null;
            }
        }
    }
}
