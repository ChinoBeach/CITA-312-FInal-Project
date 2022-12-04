using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    //variables
    string str_TeamColor;       //which team you are.
    int int_StartingPieces;     //how many pieces you start with.
    int int_AlivePieces;        //how many pieces you currently have uncaptured. (remaining)
    bool bol_IsAbleToWin;       //if you can still win the game.

    // Start is called before the first frame update
    void Start()
    {
        //get the team color
        str_TeamColor = GetTeamColor();

        //set the number of starting pieces
        int_StartingPieces = 16;

        //set the number of alive pieces equal to the number of starting pieces
        int_AlivePieces = int_StartingPieces;

        //set able to win to true
        bol_IsAbleToWin = true;

    }//end start method

    // Update is called once per frame
    void Update()
    {
        //check how many pieces are still alive
        int_AlivePieces = GetRemainingPieces();
        //if there are no pieces alive still, you lose the game. 
        if (int_AlivePieces <= 0)
        {
            //you are no longer able to win. 
            bol_IsAbleToWin = false;
        }

        //debuging
        Debug.Log("got to update");
        Debug.Log("Team Color: " + str_TeamColor);
        Debug.Log("Starting Pieces: " + int_StartingPieces);
        Debug.Log("Alive Pieces:  " + int_AlivePieces);


    }//end update method



    //Get Team Color looks to see what kind color pieces are held by this team, this is called in start.
    string GetTeamColor()
    {
        //set team color based off the tag attached to the team object.(which this script is attached to)
        if (gameObject.tag == "TeamBlue")            //if its tagged as blue team
        {
            str_TeamColor = "TeamBlue";
        }//end if 
        else if (gameObject.tag == "TeamGreen")      //or its tagged as green team
        {
            str_TeamColor = "TeamGreen";                //okay if it got here, there was no tag. this is an error.
        }//end else if
        else
        {
            str_TeamColor = null;
        }//end else

        //return the team color
        return str_TeamColor;

    }//end Get Team Color Method

    //Get Remaining Pieces looks to see how many pieces are still alive and have not been captured for your team, this is
    //called in update. 
    int GetRemainingPieces()
    {
        //variable 
        GameObject[] PiecesRemaining;

        //place eveery object in the scene with the tag into an array. 
        PiecesRemaining = GameObject.FindGameObjectsWithTag(str_TeamColor); //this might not work :( 

        //look for the number of objects in the world with the tag for the team
        int_AlivePieces = PiecesRemaining.Length - 1; //-1 is for the team object because that also has the tag.

        //for every one there is 
        return int_AlivePieces;
    }//end Get Remaining Pieces Method
}
