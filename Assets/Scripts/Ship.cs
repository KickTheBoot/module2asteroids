using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Ship : MonoBehaviour
{
    public InputActionAsset controls;

    private InputAction fire, steer, thrust;
    
    Color spritecolor;
    
    [SerializeField]
    AudioClip DeathNoise;

    SpriteRenderer ShipRenderer;

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

    [SerializeField]
    ParticleSystem ThrustParticles;

    void Awake()
    {
        InputActionMap map = controls.FindActionMap("Controls");
        thrust = map.FindAction("Thrust");
        fire = map.FindAction("Fire");
        steer = map.FindAction("Steer");

        thrust.Enable();
        fire.Enable();
        steer.Enable();

    }

    void Start()
    {
        ShipRenderer = GetComponent<SpriteRenderer>();
        spritecolor = ShipRenderer.color;

        body = GetComponent<Rigidbody2D>();

        monkey = GameObject.Instantiate(Monkey.MonkeyPrefab(), transform.position, transform.rotation).GetComponent<Monkey>();
        monkey.initialize(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead )
        {

            Debug.Log(thrust.ReadValue<float>());
            if (thrust.IsPressed())
            {
                velocity.x += transform.up.x * ThrustForce * Time.deltaTime;
                velocity.y += transform.up.y * ThrustForce * Time.deltaTime;
                if(!ThrustParticles.isPlaying)  ThrustParticles.Play();
            }
            else 
            {
                if (velocity.magnitude != 0) velocity -= velocity.normalized * DecelerationOverTime * Time.deltaTime;
                ThrustParticles.Stop(false, ParticleSystemStopBehavior.StopEmitting);
            }

            velocity = Vector2.ClampMagnitude(velocity, maxVelocity);

            angularVelocity += steer.ReadValue<float>() * AngularAcceleration * Time.deltaTime;

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

            if(fire.WasPerformedThisFrame() && OnFire != null)OnFire.Invoke();
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
            velocity = Vector2.zero;
            angularVelocity = 0;

            GameManager.instance.game.Death();
            if(DeathNoise)AudioSource.PlayClipAtPoint(DeathNoise,transform.position);
            dead = true;
            monkey.gameObject.SetActive(false);
            Vector2 newPos;
            newPos.x = PlayArea.instance.bounds.width;
            newPos.y = PlayArea.instance.bounds.height;
            transform.position = newPos;
            yield return new WaitForSeconds(5);
            yield return new WaitUntil(()=>!GameManager.instance.game.GameOver);
            dead = false;
            monkey.gameObject.SetActive(true);
            newPos.x = 0;
            newPos.y = 0;
            transform.position = newPos;
            StartCoroutine(invincibleTime(4));
        }

        IEnumerator invincibleTime(float time)
        {
            invincible = true;

            float invincibleTime = Time.time + time;

            Color invisible = new Color(0,0,0,0);

            while(Time.time < invincibleTime)
            {
                ShipRenderer.color = invisible;
                yield return null;
                ShipRenderer.color = spritecolor;
                yield return null;
            }

            yield return null;
            invincible = false;
        }

        public delegate void Fire();
        public event Fire OnFire; 


    }
