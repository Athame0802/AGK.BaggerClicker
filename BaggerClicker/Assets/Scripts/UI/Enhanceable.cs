using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enhanceable : ChangesWithAsset
{
    protected override void Start()
    {
        base.Start();

        EnhanceManager.RenewalEvent += new Action(Renewal);
        EnhanceManager.EnhanceSuccessEvent += new Action(Next);
        EnhanceManager.EnhanceFailEvent += new Action(ResetToFirst);
    }

    private void Next()
    {
        data = (IIndexable)data["next"];
    }

    private void ResetToFirst()
    {
        data = (IIndexable)data["first"];
    }
}
