using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    private string name { get; set; }
    private int position { get; set; }

    public void Move(string direction) {
        switch (direction) {
            case "left":
                position += 1;
            break; 
            
            case "right":
                position -= 1;
            break;
        }
    }

    public void Return() {
        position = 0;
    }

    
}
