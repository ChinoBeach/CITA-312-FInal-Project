using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour
{
    //varibales
    static Rigidbody rigidbody;
    public static Vector3 vector3_diceVelocity;

    SphereCollider[] array_diceSides = new SphereCollider[20];

    [SerializeField] SphereCollider colliderSide1;
    [SerializeField] SphereCollider colliderSide2;
    [SerializeField] SphereCollider colliderSide3;
    [SerializeField] SphereCollider colliderSide4;
    [SerializeField] SphereCollider colliderSide5;
    [SerializeField] SphereCollider colliderSide6;
    [SerializeField] SphereCollider colliderSide7;
    [SerializeField] SphereCollider colliderSide8;
    [SerializeField] SphereCollider colliderSide9;
    [SerializeField] SphereCollider colliderSide10;
    [SerializeField] SphereCollider colliderSide11;
    [SerializeField] SphereCollider colliderSide12;
    [SerializeField] SphereCollider colliderSide13;
    [SerializeField] SphereCollider colliderSide14;
    [SerializeField] SphereCollider colliderSide15;
    [SerializeField] SphereCollider colliderSide16;
    [SerializeField] SphereCollider colliderSide17;
    [SerializeField] SphereCollider colliderSide18;
    [SerializeField] SphereCollider colliderSide19;
    [SerializeField] SphereCollider colliderSide20;


    // Start is called before the first frame update
    void Start()
    {
        //get the ridgid body for the dice
        rigidbody = GetComponent<Rigidbody>();
        //fill the sphere collider Array

        
    }//end start method

    // Update is called when it is time to roll the dice
    public void RollDice()
    {
        //set the velocity
        vector3_diceVelocity = rigidbody.velocity;

        //set random directions for the dice
        float flt_directionX = Random.Range(0, 500);
        float flt_directionY = Random.Range(0, 500);
        float flt_directionZ = Random.Range(0, 500);


        //set the postion
        transform.position = new Vector3(0, 2, 0);
        //set the rotation
        transform.rotation = Quaternion.identity;
        //roll the dice
        rigidbody.AddForce(transform.up * 500);
        rigidbody.AddTorque(flt_directionX, flt_directionY, flt_directionZ);


    }//end update method
}
