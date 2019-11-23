using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CarBehaviour))]
public class CarAutoController : MonoBehaviour
{


    public float breaking = 5f;
    public float aceleration = 8;
    public float steering = 5;
    public float maxSpeed = 20;
    public float maxTurnAngle = 40;

    public float currentSpeed;

    private float horizontalAxis = 0;
    private float verticalAxis = 0;


    private Rigidbody2D rb;
    private CarBehaviour carBehaviour;
    private bool shouldMove = false;


// Start is called before the first frame update
    void Start()
    {
        carBehaviour = GetComponent<CarBehaviour>();
        rb = GetComponent<Rigidbody2D>();
        shouldMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (carBehaviour.IsCarOff()) {
            carBehaviour.StartCar();
        }
        
        ////No movement if car is off
        //if (!carBehaviour.CheckLights())
        //{
        //    shouldMove = false;
        //    return;
        //}
        //if (Input.GetMouseButtonDown(0))
        //{
        //    shouldMove = true;
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //    shouldMove = false;
        //}
    }
    void FixedUpdate()
    {
        if (carBehaviour.IsCarOff()) return;

        //float h = 0;
        //float v = 0;

        //if (shouldMove)
        //{
        //    var heightMovement = Screen.height * 0.5f;
        //    var mousePosition = Input.mousePosition.y;
        //    h = Mathf.Clamp((mousePosition - heightMovement) / (heightMovement), -1, 1);
        //    v = 1;

        //} else
        //{
        //    v = 0;
        //}
        
        float h = 0;
        float v = 1;


        //rb.AddForce(transform.right * aceleration * v);
        //rb.AddTorque(h * steering, ForceMode2D.Impulse);

        Vector2 speed = transform.right * (v * aceleration);
        rb.AddForce(speed);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(maxTurnAngle * h, Vector3.forward), steering * Time.fixedDeltaTime);

        //float direction = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.right));
        //if (direction >= 0.0f)
        //{
        //    rb.rotation += h * steering * (rb.velocity.magnitude / maxSpeed);
        //}
        //else
        //{
        //    rb.rotation -= h * steering * (rb.velocity.magnitude / maxSpeed);
        //}

        float driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.down)) * 4f;
        Vector2 relativeForce = Vector2.up * driftForce;
        rb.AddForce(rb.GetRelativeVector(relativeForce));

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        } else if (v == 0)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, this.breaking* Time.fixedDeltaTime);
        }
        currentSpeed = rb.velocity.magnitude;


    }


}
