using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Ship : SpaceBody
{
    [Header("Ship specific settings")]
    [SerializeField]
    GameObject ExplosionParticleSystem;
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
    float maxVelocity = 10;

    float angularVelocity;

    float AngularAcceleration = 200;


    [SerializeField]
    float RapidFireEnd;
    int NextFire;

    const int NormalFireInterval = 30;
    const int RapidFireInterval = 10;

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

            if (thrust.IsPressed())
            {
                if(!ThrustParticles.isPlaying)  ThrustParticles.Play();
            }
            else 
            {
                ThrustParticles.Stop(false, ParticleSystemStopBehavior.StopEmitting);
            }

            Velocity = Vector2.ClampMagnitude(Velocity, maxVelocity);

            Accelerate(transform.up *(thrust.IsPressed() ? 1:0) * ThrustForce, steer.ReadValue<float>() * AngularAcceleration);
            Move();
            //Imitating the screen wrap effect
            transform.position = EdgeWrapSystem.screenWrapPosition(transform.position);
            //Firing the gun
            

            bool RapidFire = RapidFireEnd < Time.time;
            if(NextFire > 0) NextFire--;
            else if(fire.IsPressed()){
                NextFire = RapidFire ? NormalFireInterval:RapidFireInterval; 
                OnFire.Invoke();
            }
        }
    }
        void FixedUpdate()
        {

        }

        void OnGUI()
        {
            //GUILayout.Label($"Position: {transform.position} \nVelocity: {velocity}\nAngular Velocity: {angularVelocity}");
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

            ZeroVelocities();
            if(ExplosionParticleSystem)Instantiate(ExplosionParticleSystem, transform.position, Quaternion.Euler(0,0,0));

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

        public void AddRapidFireTime(float Amount)
        {
            if(RapidFireEnd < Time.time)RapidFireEnd = Time.time + Amount;
            else RapidFireEnd += Amount;
        }

        public void PowerUp(PowerUp powerUp)
        {
            if(powerUp.GetType() == typeof(RapidFIrePowerUp))
            {
                RapidFIrePowerUp Rfpu = (RapidFIrePowerUp)powerUp;
                AddRapidFireTime(Rfpu.Duration);
            }
        }

        void OnHitRapidFirePowerUp()
        {
            AddRapidFireTime(10);
        }

        public delegate void Fire();
        public event Fire OnFire; 


    }
