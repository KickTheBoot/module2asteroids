using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ship : MonoBehaviour
{
    Rigidbody2D body;


    float  ThrustForce = 5;
    Vector2 velocity;
    bool dead;
    float maxVelocity = 10;

    float angularVelocity;

    float AngularAcceleration = 200;

    float DecelerationOverTime = 1f;

    float MaxAngAcc = 360;

    float angulardeceleration = 20f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Thrust"))
        {
            velocity.x += transform.up.x * ThrustForce * Time.deltaTime;
            velocity.y += transform.up.y * ThrustForce * Time.deltaTime;
        }
        else if(velocity.magnitude!=0)velocity -= velocity.normalized * DecelerationOverTime * Time.deltaTime;
        
        velocity = Vector2.ClampMagnitude(velocity,maxVelocity);
        
        angularVelocity += Input.GetAxis("Steer") * AngularAcceleration * Time.deltaTime;
        
        bool angvels = angularVelocity >= 0;

        if(angularVelocity != 0 && Input.GetAxis("Steer") == 0)
        {
            if(Mathf.Abs(angularVelocity) < angulardeceleration * Time.deltaTime) angularVelocity= 0;
            else angularVelocity += angulardeceleration*Time.deltaTime*(angvels? -1: 1);
        }
        

        angularVelocity = Mathf.Clamp(angularVelocity,MaxAngAcc*-1,MaxAngAcc);
        
        //Imitating the screen wrap effect

        byte areainfo = PlayaArea.instance.ObjectWithinBoundsInfo(transform.position);
        Debug.Log(areainfo);
        Vector3 screenwrapposition = transform.position;
        if((areainfo & 0b0001) == 0b0001)   screenwrapposition.x = PlayaArea.instance.bounds.width * -0.5f;
        if((areainfo & 0b0010) == 0b0010)   screenwrapposition.x = PlayaArea.instance.bounds.width * 0.5f;
        if((areainfo & 0b0100) == 0b0100)   screenwrapposition.y = PlayaArea.instance.bounds.height * -0.5f;
        if((areainfo & 0b1000) == 0b1000)   screenwrapposition.y = PlayaArea.instance.bounds.height * 0.5f;

        transform.position = screenwrapposition;

        transform.Rotate(new Vector3(0,0,angularVelocity * Time.deltaTime));
        transform.Translate(velocity * Time.deltaTime,Space.World);
    }

    void FixedUpdate()
    {
        
    }

    void OnGUI()
    {
        GUILayout.Label($"Position: {transform.position} \nVelocity: {velocity}\nAngular Velocity: {angularVelocity}");
    }

    
}
