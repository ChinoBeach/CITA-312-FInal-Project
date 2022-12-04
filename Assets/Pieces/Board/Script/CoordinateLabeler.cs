using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//make this script always execute. 
[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    //variables
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    [SerializeField] GameObject text;
    /*label that is on each indivual tile of the game board.
    reads as (x,y to the player but is actually x,z).
    label that displays coordinates.*/

    //when the script is loaded
    void Awake()
    {
        //Make the TextMeshPro on the object that this script is on the "label"
        label = GetComponent<TextMeshPro>();

        //begin displaying coordinates 
        DisplayCoordinates();
    }

    private void Start()
    {
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //while the application is not playing
        if(!Application.isPlaying)
        {
            //display the coordinates
            DisplayCoordinates();
        }

    }

    //method that sets the text and shows the current coordinates 
    void DisplayCoordinates()
    {
        //grab the coordinates from the tile itself. Remember it is technically (x,z).
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x /*/UnityEditor.EditorSnapSettings.move.x*/);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z /*/UnityEditor.EditorSnapSettings.move.z*/);
        
        //set the text of the label
        label.text = coordinates.x + "," + coordinates.y;
    }
}
