using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ship : MonoBehaviour
{
    bool invincible;
    bool dead;
    Monkey monkey;
    Rigidbody2D body;

    float ThrustForce = 5;
    Vector2 velocity;
    float maxVelocity = 10;

    float angularVelocity;

    float AngularAcceleration = 200;

    float DecelerationOverTime = 1f;

    float MaxAngAcc = 360;

    float angulardeceleration = 20f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        monkey = GameObject.Instantiate(Monkey.MonkeyPrefab(), transform.position, transform.rotation).GetComponent<Monkey>();
        monkey.initialize(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            if (Input.GetButton("Thrust"))
            {
                velocity.x += transform.up.x * ThrustForce * Time.deltaTime;
                velocity.y += transform.up.y * ThrustForce * Time.deltaTime;
            }
            else if (velocity.magnitude != 0) velocity -= velocity.normalized * DecelerationOverTime * Time.deltaTime;

            velocity = Vector2.ClampMagnitude(velocity, maxVelocity);

            angularVelocity += Input.GetAxis("Steer") * AngularAcceleration * Time.deltaTime;

            bool angvels = angularVelocity >= 0;

            if (angularVelocity != 0 && Input.GetAxis("Steer") == 0)
            {
                if (Mathf.Abs(angularVelocity) < angulardeceleration * Time.deltaTime) angularVelocity = 0;
                else angularVelocity += angulardeceleration * Time.deltaTime * (angvels ? -1 : 1);
            }


            angularVelocity = Mathf.Clamp(angularVelocity, MaxAngAcc * -1, MaxAngAcc);

            //Imitating the screen wrap effect

            transform.position = EdgeWrapSystem.screenWrapPosition(transform.position);

            transform.Rotate(new Vector3(0, 0, angularVelocity * Time.deltaTime));
            transform.Translate(velocity * Time.deltaTime, Space.World);
        }
    }
        void FixedUpdate()
        {

        }

        void OnGUI()
        {
            GUILayout.Label($"Position: {transform.position} \nVelocity: {velocity}\nAngular Velocity: {angularVelocity}");
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            SendMessage($"OnHit{other.tag}", SendMessageOptions.DontRequireReceiver);
        }

        void OnHitAsteroid()
        {
            Debug.Log("Asteroid Hit!");
            if(!dead && !invincible)StartCoroutine(Death());
        }

        IEnumerator Death()
        {
            dead = true;
            Vector2 newPos;
            newPos.x = PlayaArea.instance.bounds.width;
            newPos.y = PlayaArea.instance.bounds.height;
            transform.position = newPos;
            yield return new WaitForSeconds(5);
            dead = false;
            newPos.x = 0;
            newPos.y = 0;
            transform.position = newPos;
            StartCoroutine(invincibleTime(4));
        }

        IEnumerator invincibleTime(float time)
        {
            invincible = true;
            yield return new WaitForSeconds(time);
            invincible = false;
        }


    }
