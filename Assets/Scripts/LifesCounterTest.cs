using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifesCounterTest : Singleton<LifesCounterTest>
{
    private int _maxLifes = 6;
    private int _minLifes = 1;
    private int _lifes;

    private void Start()
    {
        _lifes = _maxLifes;
    }

    public void Raise()
    {
        float oldLifes = _lifes;
        _lifes++;
        ClampLifes();

        if (oldLifes < _maxLifes)
            DisplayMessage();
    }

    public void Lower()
    {
        float oldLifes = _lifes;
        _lifes--;
        ClampLifes();

        if (oldLifes > _minLifes)
            DisplayMessage();
    }

    private void DisplayMessage()
    {
        Debug.Log($"Lifes = {_lifes}");
    }

    private void ClampLifes()
    {
        _lifes = Mathf.Clamp(_lifes, _minLifes, _maxLifes);
    }
}
