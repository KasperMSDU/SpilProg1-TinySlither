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
    public AudioSource whiteNoiseSource;
    public AudioClip whiteNoise;
    public float defaultPitch = 1;
    public float boostPitch;
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
        whiteNoiseSource.clip = whiteNoise;
        whiteNoiseSource.pitch = defaultPitch;
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
        }
        else
        {
            speed = Mathf.Lerp(speed, defaultSpeed, 0.5f);
            sprite.color = Color.Lerp(sprite.color, defaultColor, 0.025f);
            trail.startColor = Color.Lerp(trail.startColor, defaultColor, 0.025f);

            whiteNoiseSource.pitch = Mathf.Lerp(whiteNoiseSource.pitch, defaultPitch, 0.25f);
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

        whiteNoiseSource.pitch = Mathf.Lerp(whiteNoiseSource.pitch, boostPitch, 0.25f);
    }
    IEnumerator BoostTimeout(float timeout)
    {
        canBoost = false;
        yield return new WaitForSeconds(timeout);
        canBoost = true;
    }
}
