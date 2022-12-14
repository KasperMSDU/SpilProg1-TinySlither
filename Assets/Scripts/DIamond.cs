using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIamond : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        sceneCenter = GameObject.Find("SceneCenter");
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        force = Random.Range(minForce, maxForce);

        forceVector = new Vector2(sceneCenter.transform.position.x - gameObject.transform.position.x, sceneCenter.transform.position.y - gameObject.transform.position.y);
        forceVector = forceVector.normalized;
        rb.AddForce(forceVector * force);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y <= minYPos)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<MovementController>().speed >= 9)
            {
                gm.increaseScore(scoreValue);
                Instantiate(shatterEffect, new Vector3(gameObject.transform.position.x, transform.position.y, 0), Quaternion.identity);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Bounce! *KLACK*");
            }
            
        }
    }
}
