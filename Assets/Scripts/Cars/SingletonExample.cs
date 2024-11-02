using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SingletonExample
{
    private static readonly SingletonExample instance = new SingletonExample();
    private SingletonExample() { 
        
    }

    public static SingletonExample Instance { 
        get { return instance; }
    }

}
