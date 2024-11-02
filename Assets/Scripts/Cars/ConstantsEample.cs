using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConstantsEample
{
    public const string AppName = "MyApp";
    public const int MaxItems = 140;


    public static void PrintMessage(string message)
    {
        Debug.Log(message);
    }

    public static int Add(int a, int b)
    {
        return a + b;
    }

}
