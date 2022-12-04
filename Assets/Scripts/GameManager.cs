using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//**Project by Chino Beach
//**Note/Credit: Alexander Woods and Professor Rohne Nyberg have helped contribute to this project. 

public class GameManager : MonoBehaviour
{
    //variables
    [SerializeField] private GameObject gamepiece_rogue;    //the actual piece (load in the prefab)
    [SerializeField] private Material material_blue;        //the material for any piece on the blue team
    [SerializeField] private Material material_green;       //the material for any piece on the green team

    // Every tile in the board.
    public GameObject[] array_boardTiles = new GameObject[64];

    // Temp var to make the pieces being set every frame.
    public List<GameObject> list_gamePiecesOnBoard = new List<GameObject>();


    // Team piece totals
    static int int_StartingTeamSize = 16;             //how many pieces each team starts with
    int int_GreenRemaining = int_StartingTeamSize;    //how many green pieces are currently on the board
    int int_BlueRemaining = int_StartingTeamSize;     //how many blue pieces are currently on the board

    public GameObject tile_selected;                  //any tile that gets selected

    // Change this to whoever starts First
    private bool bol_isBlueTurn = true;                    //this determines whoevers turn it is. If its not blues turn, it is greens

    // Piece Offsets. (how to get to other tiles from the array) 
    int upLeft = -9;
    int up = -8;
    int upRight = -7;
    int left = -1;
    int right = 1;
    int downLeft = 7;
    int down = 8;
    int downRight = 9;

    //start is called before update
    void Start()
    {
        //figure out who is going first
        //bol_isBlueTurn = DetermineFirstTeam();    //if this is true, blue goes first, otherwise green will
        //debug statement


    }//end start

    // Update is called once per frame
    void Update()
    {
        //constantly update visuals where there is a piece and where there is not a piece
        PlacePieces();
    }//end update

    //DetermineFirstTeam is called from start to determine which team is going to go first. 
    //bool DetermineFirstTeam()
    //{
        //variables 
        int int_blueTeamInitative;      ///d20 roll
        int int_greenTeamInitative;     //d20 roll

        int int_randomPicker_1_2;       //which number the random number picks
        int int_blueTeam = 1;           //incase of a tie, random number 1, blue goes first 
        int int_greenTeam = 2;          //incase of a tie random number 2, green  goes first 
                                        //determine who is going first

        //roll a d20 for the blue team
        //assign that to initative

        //do the same for green team

        //if tie, pick a random team to go first 
        //pick a number between 1 and 2

        //debuging

        //if blue 
        //blue goes first
        //else if green (just else)
        //green goes first 


        //else if blue team scored higher
        //blue goes first 
        //else
        //green goes first 

    //}//end method determindFirst Team

    //ValudateSelectedPieceForMovement is called when the player is selecting the piece that they want to move
    public void ValidateSelectedPieceForMovement()
    {

        //access the tiles information(stored in script)
        TileScript script_tile = tile_selected.GetComponent<TileScript>();
       
        // If the there is no piece on the tile, Tile is invalid (cannot move a piece from this tile) 
        if(!script_tile.bol_piecePlacedHere)
        {
            //no "piece" was selected to move
            tile_selected = null;

            // When the tile is valid, set a bool to be able to move
            // then when that variable is set, if its within range and move there
            // eg: check if the tile meets all criteria for the movements

            // When you move, check !hasPiece to be valid movement
        }//end if statement for does not have piece
        
        //debug
        Debug.Log("Selected Tile:" + tile_selected);
    }//end method for validating movement selection

    //ValudateSelectedTileForMovement is called when the player is selecting the tile that they want to move to
    public void ValidateSelectedTileForMovement()
    {

        //access the tiles information(stored in script)
        TileScript script_tile = tile_selected.GetComponent<TileScript>();

        // If the there is no piece on the tile, Tile is valid (can move a piece to this tile) 
        if (!script_tile.bol_piecePlacedHere)
        {
            //no "piece" was selected to move
            tile_selected = null;

            // When the tile is valid, set a bool to be able to move
            // then when that variable is set, if its within range and move there
            // eg: check if the tile meets all criteria for the movements

            // When you move, check !hasPiece to be valid movement
        }//end if statement for does not have piece

        //debug
        Debug.Log("Selected Tile:" + tile_selected);
    }//end method for validating movement selection

    //ValidateTargetToAttack is called when a player goes to attack a piece. It checks to see if it is in range.
    public void ValidateTargetToAttack()
    {
        // Same thing as other tile, but make sure there is a piece there and its opposite of the teams color
    }//end method for validationg attacking opponents

    //PlacePieces is called from update so that there is only ever the correct number of visal pieces on the
    //board and they are always in the correct place
    private void PlacePieces()
    {
        //variables
        int int_tile = 0;     //the current tile that is being processesed
        
  

        //for every game piece that is already on the board, delete it
        for (int int_listPosition = 0; int_listPosition < list_gamePiecesOnBoard.Count; int_listPosition++)
        {
            Destroy(list_gamePiecesOnBoard[int_listPosition]);
        }//end for loop

        //clear the current list of game pieces
        list_gamePiecesOnBoard.Clear();

        //for every tile on in the board
        foreach (var tile in array_boardTiles)
        {
            //get the script attached to the tile
            TileScript script_tile = tile.GetComponent<TileScript>();

            //If there should be a piece on this the tile
            if(script_tile.bol_piecePlacedHere)
            {
                //put a piece there
                var piece = Instantiate(gamepiece_rogue, new Vector3(tile.transform.position.x, 1, tile.transform.position.z), Quaternion.identity, transform);
               //at this point in devolpment there are only rogues, so name it as such
                piece.name = "rouge " + int_tile;

                // Scale down the pieces. becuase them models tooooooo big and THICK
                piece.transform.localScale = new Vector3(.3f, .3f, .3f);

                // Set layer to ignore raycasts. this is done so that the use can only select the tiles.
                piece.layer = LayerMask.GetMask("Ignore Raycast");

                // If the team is blue, rotate to face green and make material blue
                if (script_tile.bol_isBlueteam)
                {
                    //roation 180
                    piece.transform.eulerAngles = new Vector3(0, 180, 0);
                    //set the material
                    piece.GetComponentInChildren<Renderer>().material = material_blue;
                }//if if tile is blue team

                // Else make the piece green.
                else
                {
                    //set the material. Green is already facing correct way so doesnt need to be rotated.
                    piece.GetComponentInChildren<Renderer>().material = material_green;
                }//end else statement (green team)

                //put the new piece back into the list
                list_gamePiecesOnBoard.Add(piece);
            }//end if the tile has a piece

            //go to the next tile
            int_tile++;
        }//end the for each tile in the list loop
    }//end method place pieces
}//end class