using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enhanceable : ChangesWithAsset
{
    protected override void Awake()
    {
        base.Awake();

        MainManager.RenewalEvent += new Action(Renewal);
        MainManager.ToNextSwordEvent += new Action(Next);
        MainManager.ToFirstSwordEvent += new Action(ResetToFirst);
        MainManager.MoveToEvent += new Action<Sword>(MoveTo);
    }

    private void Next()
    {
        _data = (IIndexable)_data["next"];
        data = _data;
    }

    private void ResetToFirst()
    {
        _data = (IIndexable)_data["first"];
        data = _data;
    }

    private void MoveTo(Sword sword)
    {
        _data = sword;
        data = _data;
    }

    void OnDestroy()
    {
        MainManager.RenewalEvent -= Renewal;
        MainManager.ToNextSwordEvent -= Next;
        MainManager.ToFirstSwordEvent -= ResetToFirst;
        MainManager.MoveToEvent -= MoveTo;
    }
}
