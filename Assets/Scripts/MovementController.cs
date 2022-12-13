using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Vector2 movementVector;
    private Rigidbody2D rb;
    public float speed;
    public float defaultSpeed;
    public float boostSpeed;
    private float rotateAngle = 0;
    public float rotateSpeed;
    private TrailRenderer trail;
    private SpriteRenderer sprite;
    public bool canBoost = true;

    public Color defaultColor = Color.black;
    public Color boostColor = Color.red;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        trail = GetComponentInChildren<TrailRenderer>();
        speed = defaultSpeed;
        rb = GetComponent<Rigidbody2D>();
        movementVector = new Vector2(0, 1);
        trail.startColor = defaultColor;
        sprite.color = defaultColor;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.A))
        {
            rotateAngle = rotateSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotateAngle = -rotateSpeed;
        }
        else
        {
            rotateAngle = 0;
        }

        if (Input.GetKey(KeyCode.Space) && canBoost == true)
        {
            boost();
            Debug.Log(sprite.color);
        }
        else
        {
            speed = Mathf.Lerp(speed, defaultSpeed, 0.5f);
            sprite.color = Color.Lerp(sprite.color, defaultColor, 0.025f);
            trail.startColor = Color.Lerp(trail.startColor, defaultColor, 0.025f);
        }
        movementVector = Quaternion.AngleAxis(rotateAngle, Vector3.forward) * movementVector;
        movementVector = movementVector.normalized;
        rb.velocity = movementVector * speed;
        

        if (sprite.color.r >= 0.995)
        {
            StartCoroutine(BoostTimeout(2));
        }
    }

    void boost()
    {
        speed = Mathf.Lerp(speed, boostSpeed, 0.5f);
        sprite.color = Color.Lerp(sprite.color, boostColor, 0.1f);
        trail.startColor = Color.Lerp(trail.startColor, boostColor, 0.1f);
    }
    IEnumerator BoostTimeout(float timeout)
    {
        canBoost = false;
        yield return new WaitForSeconds(timeout);
        canBoost = true;
    }
}
