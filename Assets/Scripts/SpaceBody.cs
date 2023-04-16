using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that handles
public abstract class SpaceBody : MonoBehaviour
{
    protected Vector2 Velocity;
    [Header("SpaceBody settings")]
    [SerializeField]
    protected float TerminalVelocity;
    [SerializeField]
    protected float Deceleration;

    protected float AngularVelocity;
    [SerializeField]
    protected float AngularTerminalVelocity;
    [SerializeField]
    protected float AngularDeceleration;

    
    //Adds inputted values multiplied by Time.deltatime to velocity, Call this before Move
    protected void Accelerate(Vector2 amount,float angularAmount)
    {
        Velocity = Vector2.ClampMagnitude(Velocity + amount * Time.deltaTime, TerminalVelocity);
        AngularVelocity = Mathf.Clamp( AngularVelocity + angularAmount * Time.deltaTime,-AngularTerminalVelocity, AngularTerminalVelocity);
    }

    //Adds inputted values to velocity instantly
    protected void AddVelocity(Vector2 amount, float angularAmount)
    {
        Velocity = Vector2.ClampMagnitude(Velocity + amount, TerminalVelocity);
        AngularVelocity = Mathf.Clamp( AngularVelocity + angularAmount,-AngularTerminalVelocity, AngularTerminalVelocity);
    }
    
    //Translates and rotates the object, decelerates if the boolean's
    protected void Move()
    {
        transform.Translate(Velocity * Time.deltaTime, Space.World);
        transform.Rotate(new Vector3(0,0,AngularVelocity * Time.deltaTime));
            
        //Decelerate
        if(Velocity.magnitude > 0) Velocity -= Velocity.normalized * Time.deltaTime * Deceleration;
        //is angular velocity positive?
        bool AngPosi = AngularVelocity >= 0;
        float DeceleratedVelocity = AngularVelocity - (AngularDeceleration * (AngPosi?1:-1)) * Time.deltaTime;
        //is decelerated angular velocity positive?
        bool DecAngPosi = DeceleratedVelocity >= 0;
        //if both signs are equal, set it to DeceleratedVelocity, otherwise return 0
        if(AngularVelocity != 0) AngularVelocity = (AngPosi == DecAngPosi)?DeceleratedVelocity:0;
        
    
    }
    protected void ZeroVelocities()
    {
        Velocity = Vector2.zero;
        AngularVelocity = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
