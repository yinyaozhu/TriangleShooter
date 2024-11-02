using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SuperStatus
{
    private bool currentSuperStatus;
    public Action<bool> SuperStatusUpdate;

    public SuperStatus() {
        currentSuperStatus = false;
    }

    public bool GetSuperStatus()
    {
        return currentSuperStatus;
    }

    public void ChargeSuperStatus()
    {
        currentSuperStatus = true;
        SuperStatusUpdate?.Invoke(currentSuperStatus);
    }

    public void turnSuperStatusOff() { 
        currentSuperStatus = false;
        SuperStatusUpdate?.Invoke(currentSuperStatus);
    }
}
