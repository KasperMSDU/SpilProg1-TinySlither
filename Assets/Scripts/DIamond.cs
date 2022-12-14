using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIamond : MonoBehaviour
{
    //declaring and initializing variables
    private Rigidbody2D rb;
    public Vector2 forceVector;
    public float minForce = 500;
    public float maxForce = 700;
    public float force;
    public float minYPos;
    private GameManager gm;
    public int scoreValue = 1;
    private GameObject sceneCenter;
    public GameObject shatterEffect;

    void Start()
    {
        //Assigning veriables their respective components
        sceneCenter = GameObject.Find("SceneCenter");
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        //Assigning the force variable a random value, in the range of minForce and maxForce.
        force = Random.Range(minForce, maxForce);

        //finding the forceVecter, by subtracting this gameobjects vector2 coorditates, from the sceneCenter gameobjects vector2 coordinates
        forceVector = new Vector2(sceneCenter.transform.position.x - gameObject.transform.position.x, sceneCenter.transform.position.y - gameObject.transform.position.y);
        
        //Normalizing the forceVector
        forceVector = forceVector.normalized;

        //using the "AddForce" function, along the forceVector times the force
        rb.AddForce(forceVector * force);
    }

    // Update is called once per frame
    void Update()
    {
        //checking if this gameobject is below a certain y pos, if so, it is destroyed
        if (gameObject.transform.position.y <= minYPos)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //checking if this gameobject collides with the player, if so:
        if (collision.gameObject.CompareTag("Player"))
        {
            //checking if the speed variable on the player object is greater or equal to 9, if so:
            if (collision.gameObject.GetComponent<MovementController>().speed >= 9)
            {
                // call the "IncreaseScore method form the gamemanager class, and passing in the "ScoreValue" variable;
                gm.IncreaseScore(scoreValue);

                //intiating the "ShatterEffect" gameobject, at the position of this gameobject;
                Instantiate(shatterEffect, new Vector3(gameObject.transform.position.x, transform.position.y, 0), Quaternion.identity);

                //destroy this gameobject
                Destroy(gameObject);
            }

            //if the speed is not greater or equal to 9:
            else
            {
                //play the "Clank" sound from the player class
                collision.gameObject.GetComponent<MovementController>().playClank();
            }
            
        }
    }
}
