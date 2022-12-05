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

    [SerializeField] private GameObject object_d20;             //the dice;
    [SerializeField] private Material material_blueDice;        //the material for d20 on the blue team
    [SerializeField] private Material material_greenDice;       //the material for d20 on the green team

    // Every tile in the board.
    public GameObject[] array_boardTiles = new GameObject[64];

    // Temp var to make the pieces being set every frame.
    public List<GameObject> list_gamePiecesOnBoard = new List<GameObject>();


    // Team piece totals
    static int int_StartingTeamSize = 16;             //how many pieces each team starts with
    int int_GreenRemaining = int_StartingTeamSize;    //how many green pieces are currently on the board
    int int_BlueRemaining = int_StartingTeamSize;     //how many blue pieces are currently on the board

    public GameObject tile_selected;                             //any tile that gets selected
    static int int_invalidIndex = 100;                           //if its not valid for selection
    int int_pieceSelectedTileIndex = int_invalidIndex;           //where the the selected piece for movement was 
    bool bol_canMove = false;                                    //if the piece can move

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
        bol_isBlueTurn = DetermineFirstTeam();    //if this is true, blue goes first, otherwise green will
        
        
        //debug statement
        if (bol_isBlueTurn)
        { Debug.Log("Blue team starting"); }
        else { Debug.Log("Green team starting"); }
        
    }//end start

    // Update is called once per frame
    void Update()
    {
        //constantly update visuals where there is a piece and where there is not a piece
        PlacePieces();
    }//end update

    //DetermineFirstTeam is called from start to determine which team is going to go first. (default blue team goes first. 
    //if returns true then the blue team goes first, else green team goes first
    bool DetermineFirstTeam()
    {
        //variables 
        int int_blueTeamInitative;      ///d20 roll
        int int_greenTeamInitative;     //d20 roll

        int int_randomPicker_1_2;       //which number the random number picks
        int int_blueTeam = 1;           //incase of a tie, random number 1, blue goes first 
        //int int_greenTeam = 2;          //incase of a tie random number 2, green  goes first 
                                        //determine who is going first

        bool bol_blue = true;           //is blue team
        bool bol_green = false;         //is green team


        //roll a d20 for the blue team and assign that to initative
        int_blueTeamInitative = Random.Range(1, 20);
        RollDice(int_blueTeamInitative, material_blueDice);

        //do the same for green team
        int_greenTeamInitative = Random.Range(1, 20);
        RollDice(int_greenTeamInitative, material_greenDice);

        //if tie, pick a random team to go first 
        if (int_blueTeamInitative == int_greenTeamInitative)
        {
            //pick a number between 1 and 2
            int_randomPicker_1_2 = Random.Range(1, 2);
            //debuging
            Debug.Log("There was a tie. Initative Blue: "+ int_blueTeamInitative +" Intative Green: " + int_greenTeamInitative);
            Debug.Log("Random Picker: "+int_randomPicker_1_2);
            //if blue 
            if(int_randomPicker_1_2 == int_blueTeam)
            {
                //blue goes first
                return bol_blue;
            }//end if blue was selected

            //else if green (just else)
            else 
            {
                //green goes first
                return bol_green;
            }//end else(greeen team first)

        }//end if for is tie

        //else if blue team scored higher
        else if (int_blueTeamInitative>int_greenTeamInitative)
        {
            //blue goes first 
            return bol_blue;
        }//end else blue team goes first 
        //else
        else
        {
            //green team goes first 
            return bol_green;
        }//end else (green team goes first)
        
    }//end method determindFirst Team

    //ValudateSelectedPieceForMovement is called when the player is selecting the piece that they want to move
    public bool ValidateSelectedPieceForMovement()
    {
        //variables
        bool bol_isSelected = false;

        //access the tiles information(stored in script)
        TileScript script_tile = tile_selected.GetComponent<TileScript>();
       
        // If the there is no piece on the tile, Tile is invalid (cannot move a piece from this tile) 
        if(!script_tile.bol_piecePlacedHere)
        {
            //no "piece" was selected to move
            bol_isSelected = false;
            tile_selected = null;
            int_pieceSelectedTileIndex = int_invalidIndex;
        }//end if statement for does not have piece
        else
        {
            //check to see which team it is 
            if(bol_isBlueTurn)
            {
                //if the piece is blue
                if(script_tile.bol_isBlueteam)
                {
                    int_pieceSelectedTileIndex = script_tile.int_index;
                }//end blue turn blue piece
                else
                {
                    //still invalid piece
                    bol_isSelected = false;
                    tile_selected = null;
                    int_pieceSelectedTileIndex = int_invalidIndex;
                }//end blue turn green piece
            }//end if blue teams turn
            //its the geen teams turn
            else
            {
                //if its a blue piece
                if(script_tile.bol_isBlueteam)
                {
                    //piece is invalid
                    bol_isSelected = false;
                    tile_selected = null;
                    int_pieceSelectedTileIndex = int_invalidIndex;
                }//end green turn blue piece
                else
                {
                    //its a green piece
                    int_pieceSelectedTileIndex = script_tile.int_index;

                }//end green turn green piece
            }//end else green teams turn
            
            //if a piece was selected
            if(tile_selected !=null && int_pieceSelectedTileIndex != int_invalidIndex)
            {
               //remove the tile
               script_tile.bol_piecePlacedHere = false;
               //set  movable
               bol_canMove = true;

                //return that one was selected 
                bol_isSelected = true; 
            }//end if not equal to null.
        }//end else there was a piece
        
        //debug
        Debug.Log("Selected Piece:" + tile_selected);
        Debug.Log("Selected Piece Index: "+int_pieceSelectedTileIndex);
        Debug.Log("Selected Piece Can Move: " + bol_canMove);

        return bol_isSelected;
    }//end method for validating movement selection

    //ValudateSelectedTileForMovement is called when the player is selecting the tile that they want to move to
    public void ValidateSelectedTileForMovement()
    {

        //access the tiles information(stored in script)
        TileScript script_tile = tile_selected.GetComponent<TileScript>();
        Debug.Log(script_tile.int_index);
        // If the there is no piece on the tile, Tile is valid (can move a piece to this tile) 
        if (!script_tile.bol_piecePlacedHere)
        {
            //this is is part of a valid move
            //if its within range
            if(
                script_tile.int_index == int_pieceSelectedTileIndex + upLeft ||
                script_tile.int_index == int_pieceSelectedTileIndex + up ||
                script_tile.int_index == int_pieceSelectedTileIndex + upRight ||
                script_tile.int_index == int_pieceSelectedTileIndex + left ||
                script_tile.int_index == int_pieceSelectedTileIndex + right ||
                script_tile.int_index == int_pieceSelectedTileIndex + downLeft ||
                script_tile.int_index == int_pieceSelectedTileIndex + down ||
                script_tile.int_index == int_pieceSelectedTileIndex + downRight
               )
            {
                // move there by setting this tile to have a piece on it
                script_tile.bol_piecePlacedHere = true;
                //set the piece to the correct color
                if(bol_isBlueTurn)
                {
                    script_tile.bol_piecePlacedHere = true;
                    Debug.Log("got here");
                }//end if blue team turn

                //set can move to false
                bol_canMove = false;

            }   //end if in range


        }//end if statement for does not have piece
        else
        {
            //invalid move
            tile_selected = null;
            
        }//end for if it does have a piece

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

    //Roll Dice Method this is called when determining who goes first and on every attack
    void RollDice(int int_LandingSide, Material material_teamColor)
    {
        //generate a dice on the board 
        if(material_teamColor == material_blueDice)
        {
            //generate a blue dice

        }//end if blue
        else
        {
            //generate a green dice

        }//end if green

        //Roll the dice 

        //make it land on the corect side.

    }//end of roll dice method
}//end class
