using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    //variables 
    public int int_index;                   //The index of the array that holds tiles. 
    public bool bol_piecePlacedHere;        //If there is a piece on this tile, it is true, else its false
    public bool bol_isBlueteam;             //Which team is this piece on the tile. if true its blue, otherwise its green.

    private GameManager script_GameManager; //controls everything

    //Start is called before update
    private void Start()
    {
        //get the game manager
        script_GameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }//end of start method

    //OnMouseDown is called when a button on the mouse is clicked
    private void OnMouseDown()
    {
        //Select the tile and validate it.

        //set the selected tile 
        script_GameManager.tile_selected = script_GameManager.array_boardTiles[int_index];
        //check if there is a piece there for the player to move
        script_GameManager.ValidateSelectedPieceForMovement();

        //debug
        Debug.Log("Tile Clicked: " + gameObject);
    }//end on on mouse down method
}//end of class tile script
