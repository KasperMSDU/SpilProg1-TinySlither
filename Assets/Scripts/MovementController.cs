using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    //declaring and initializing variables
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

    public AudioSource clankSource;


    // Start is called before the first frame update
    void Start()
    {
        //assigning variables their respective compoents and values;
        sprite = GetComponent<SpriteRenderer>();
        trail = GetComponentInChildren<TrailRenderer>();
        rb = GetComponent<Rigidbody2D>();

        speed = defaultSpeed;
        movementVector = new Vector2(0, 1);
        trail.startColor = defaultColor;
        sprite.color = defaultColor;
        whiteNoiseSource.clip = whiteNoise;
        whiteNoiseSource.pitch = defaultPitch;
    }

    private void FixedUpdate()
    {
        //Movement code to capture input
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

        //If space is pressed and canBoost is true, call the boost function
        if (Input.GetKey(KeyCode.Space) && canBoost == true)
        {
            boost();
        }
        // if not:
        else
        {
            //"Lerping" the speed back to normal speed
            speed = Mathf.Lerp(speed, defaultSpeed, 0.5f);

            //"Lerping" the sprite color back to the default color (Black)
            sprite.color = Color.Lerp(sprite.color, defaultColor, 0.025f);

            //"Lerping" the trail color bakc to the default color (Black)
            trail.startColor = Color.Lerp(trail.startColor, defaultColor, 0.025f);

            //"Lerping" the pitch of the white noise soind back to default pitch
            whiteNoiseSource.pitch = Mathf.Lerp(whiteNoiseSource.pitch, defaultPitch, 0.25f);
        }

        //rotating the movementVector by rotateangle
        movementVector = Quaternion.AngleAxis(rotateAngle, Vector3.forward) * movementVector;

        //normalising the movementVector
        movementVector = movementVector.normalized;

        //setting the velocity of this gameobject's rigidbody to the Movementvector times the speed variable
        rb.velocity = movementVector * speed;
        
        //Starting the boostTimeout coroutine, if the color of the sprite i almost red.
        if (sprite.color.r >= 0.995)
        {
            StartCoroutine(BoostTimeout(2));
        }
    }


    //Bostfunction, that boost the speed of the player
    void boost()
    {
        //"Lerping" the speed up to the boostSpeed
        speed = Mathf.Lerp(speed, boostSpeed, 0.5f);

        //"Lerping" the sprite color to the boostColor
        sprite.color = Color.Lerp(sprite.color, boostColor, 0.1f);

        //"Lerping the trail color to the boostColor
        trail.startColor = Color.Lerp(trail.startColor, boostColor, 0.1f);

        //"Lerping" the whitenoise pitch to the boostPitch
        whiteNoiseSource.pitch = Mathf.Lerp(whiteNoiseSource.pitch, boostPitch, 0.25f);
    }


    //Coroutine that times out the boost
    IEnumerator BoostTimeout(float timeout)
    {
        //Times out the canBoost variable for "timeout" seconds
        canBoost = false;
        yield return new WaitForSeconds(timeout);
        canBoost = true;
    }


    //Plays a clank sound...
    public void playClank()
    {
        //Choosing a random pitch between 0.8 and 1.2
        clankSource.pitch = Random.Range(0.8f, 1.2f);
        
        //plays. the. clank.
        clankSource.Play();

    }
}
