using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I don't think we are using this script
public class LevelLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //create player
        Player player = new Player();

        //create enemies
        Enemy enemy = new Enemy();

        //create weapons
        Weapon gun1 = new Weapon();
       // Weapon gun2 = new Weapon("Assault Rifle", 50f);

        //give weapons
        player.weapon = gun1;
        //enemy.weapon = gun2;

        ConstantsEample.PrintMessage(ConstantsEample.AppName);
        ConstantsEample.Add(1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
