using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    //varibales
    Team teamBlue;          //the blue team
    Team teamGreen;         //the green team
    
    Team teamTakingTurn;    //whose turn it is
    bool bolGameWon;        //has the game been won or not

    // Start is called before the first frame update
    void Start()
    {
        //create the teams
        teamBlue = new Team();
        teamGreen = new Team();

        //no one has won the game when it starts
        bolGameWon = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
