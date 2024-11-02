using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SuperPower
{
    private Queue<string> currentSuperPower;
    //private float maxSuperPower;

    public Action<bool> SuperPowerUpdate;

    public SuperPower() { 
        currentSuperPower = new Queue<string>();
    }

    public float GetSuperPowerCount()
    {
        return currentSuperPower.Count;
    }

    public void ChargeSuperPower(string name)
    {
        Debug.Log("charge super power " + name);
        currentSuperPower.Enqueue(name);
        SuperPowerUpdate?.Invoke(true);
    }

    public string UseSuperPower()
    {
        // check whether user has super power or not
        if (currentSuperPower.Count > 0)
        {
            Debug.Log("use super power");
            SuperPowerUpdate?.Invoke(false);
            return currentSuperPower.Dequeue();
        }

        return null;
    }
}
