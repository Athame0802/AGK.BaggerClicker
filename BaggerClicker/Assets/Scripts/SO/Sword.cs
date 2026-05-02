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
public class Sword : ScriptableObject
{
    [SerializeField] private string swordName;
    [SerializeField] private Sprite swordSprite;
    [SerializeField] private int enhanceCount;
    [SerializeField] private int enhanceProbablity;
    [SerializeField] private int enhanceCost;
    [SerializeField] private int sellValue;
}
