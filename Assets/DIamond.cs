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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        force = Random.Range(minForce, maxForce);
        forceVector = new Vector2(0, 1);
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
}
