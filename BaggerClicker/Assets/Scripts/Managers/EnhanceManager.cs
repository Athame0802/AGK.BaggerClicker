using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhanceManager
{
    public static event Action EnhanceSuccessEvent;
    public static event Action EnhanceFailEvent;
    public static event Action RenewalEvent;

    public void Enhance(int enhanceProbablity)
    {
        int random = UnityEngine.Random.Range(1, 100);

        if (random <= enhanceProbablity)
        {
            EnhanceSuccessEvent?.Invoke();
            RenewalEvent?.Invoke();
        }
        else
        {
            EnhanceFailEvent?.Invoke();
            RenewalEvent?.Invoke();
        }
    }
}
