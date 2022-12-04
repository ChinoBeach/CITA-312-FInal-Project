using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBoard : MonoBehaviour
{
    //variables
    [SerializeField] private GameObject prefab_Tile;        //the perfab that is the tile
    [SerializeField] private Material material_brown;       //color for brown tiles
    [SerializeField] private Material material_tan;         //color fro tan tiles

    //called before update
    void Start()
    {
        //generate a the entire board
        IEnumerator coroutine = Generate();
        //begin the coroutine
        StartCoroutine(coroutine);
    }

    //Generate method is used to generate the board and called in start
    private IEnumerator Generate()
    {
        //variables 
        float flt_width = prefab_Tile.GetComponent<Renderer>().bounds.size.x;   //this is the width of the tile
        float flt_xposition = transform.position.x;                             //left/right position
        float flt_zposition = transform.position.z;                             //up/down position. because this is 3D it is top down ish. 
                                                                                //this is not y because that would be hovering above the board
        int int_TileNumber = 0;         //counts which tile you are making
        bool bol_isBrown = true;        //which color is the tile

        int int_boardDimensions = 8;    //this is a 8x8 grid. meanin 8 rows and 8 coluns

        int int_start3rdRowFar = 16;    //starts at 0 so this is the first tile on the 3rd row
        int int_start3rdRowNear = 48;   //starts at 0 so the last tile on the 2nd now is 47

        float flt_loadingSpeed = .03f;  //how quickly the game will place the tiles at the start

        //for each row(8) (it is less than 8 because coords start at 0,0)
        for (int int_row = 0; int_row < int_boardDimensions; int_row++) 
        {
            //for each column
            for (int int_column = 0; int_column < 8; int_column++)
            {
                //make tile
                var currentTile = Instantiate(prefab_Tile, new Vector3(flt_xposition, 0, flt_zposition), Quaternion.identity, transform);
                //name the tile the corrdinates and the tile ID
                currentTile.name = int_row + ", " + int_column + ", Tile " + int_TileNumber;
                //get the script for the tile
                TileScript script_Tile = currentTile.GetComponent<TileScript>();
                //set the index for the current tile in its script
                script_Tile.int_index = int_TileNumber;

                //if in the first two rows on each side
                if (int_TileNumber < int_start3rdRowFar)
                {
                    //tell the tile that it needs a piece
                    script_Tile.bol_piecePlacedHere = true;
                }//end if in first two rows on the far side

                if (int_TileNumber >= int_start3rdRowNear)
                {
                    //tell the tile tile that it needs a piece
                    script_Tile.bol_piecePlacedHere = true;
                    //tell the tile the piece is on the blue team
                    script_Tile.bol_isBlueteam = true;
                }//end if in the first two rows in th near side

                //set the color for the tile
                if (bol_isBrown)
                {
                    //if it should be a brown tile, make it brown
                    currentTile.GetComponent<Renderer>().material = material_brown;
                }//end if brown

                else
                {   
                    //if its not brown, its tan, make it so.
                    currentTile.GetComponent<Renderer>().material = material_tan;
                }//end if tan(not brown)
                
                //get the array of tiles from the game manager and add the current tile into the array
                FindObjectOfType<GameManager>().GetComponent<GameManager>().array_boardTiles[int_TileNumber] = currentTile;
                
                //move the x position over one (move over one column) based on the width of the tile
                flt_xposition += flt_width;
                //switch the color of the tile
                bol_isBrown = !bol_isBrown;
                //generate the tile number for the next tile
                int_TileNumber++;
                // Adjust value to adjust generation speed
                yield return new WaitForSeconds(flt_loadingSpeed);
            }//end for loop for the columns

            //go to the next row based on the width of the tile
            flt_zposition -= flt_width;
            //reset the x back to the left of the board from the position of the game manager(the object this script is attached to)
            flt_xposition = transform.position.x;
            //change the color of the first tile in the row so that it is alternating
            bol_isBrown = !bol_isBrown;
        }//end the for loop for the rows
    }//end the generate method
}//end of class generate board
