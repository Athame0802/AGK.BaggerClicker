using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 들어가야 할 것 :
1. 검 강화 횟수 = 인덱스 넘버
2. 검의 이름
3. 강화 확률
4. 업그레이드 비용
5. 검의 스프라이트 이미지
6. 판매 금액

int 검 강화 횟수
string 검의 이름
int 확률
int 업그레이드 비용
Image 검의 스프라이트 이미지
Int 판매 금액

위의 변수 선언
 */

[CreateAssetMenu(fileName = "SwordData", menuName = "ScriptableObject/SwordScriptable/CreateSwordData")]
public class Sword : ScriptableObject, IIndexable
{
    [SerializeField] private string swordName;
    [SerializeField] private Sprite swordSprite;
    [SerializeField] private int enhanceCount;
    [SerializeField] private int enhanceProbability;
    [SerializeField] private int enhanceCost;
    [SerializeField] private int sellValue;
    [SerializeField] private Sword nextSword;
    [SerializeField] private Sword first;

    public object this[string index] // Sword[변수이름] 입력시 변수 반환
    {
        get
        {
            if (index == "swordName")
            {
                return swordName;
            }
            else if (index == "swordSprite")
            {
                return swordSprite;
            }
            else if (index == "enhanceCount")
            {
                return enhanceCount;
            }
            else if (index == "enhanceProbability")
            {
                return enhanceProbability;
            }
            else if (index == "enhanceCost")
            {
                return enhanceCost;
            }
            else if (index == "sellValue")
            {
                return sellValue;
            }
            else if (index == "next")
            {
                return nextSword;
            }
            else if (index == "first")
            {
                return first;
            }
            else
            {
                D.LogError($"Sword 인덱서에 잘못된 접근이 발생했습니다. 이는 심각한 오류를 발생시킬 수 있습니다. index로 입력된 값 : {index}", this);
                return null;
            }
        }
    }
}
