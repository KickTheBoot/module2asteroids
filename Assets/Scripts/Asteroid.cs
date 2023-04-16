using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : SpaceBody
{
    [SerializeField]
    SpriteVariantScriptableObject SpriteVariants;

    [SerializeField]
    AudioClip ExplosionSound;

    [SerializeField]
    GameObject ExplosionParticleSystem;

    const float MaxVelocity = 5;

    const float MaxAngularVelocity = 100;
    Monkey monkey;
    // Start is called before the first frame update
    void Start()
    {
        if(SpriteVariants)
        {
            Sprite sprite = SpriteVariants.RandomSprite();
            List<Vector2> shapes = new List<Vector2>(sprite.GetPhysicsShapePointCount(0));
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            PolygonCollider2D coll = GetComponent<PolygonCollider2D>();
            sprite.GetPhysicsShape(0,shapes);
            coll.points = shapes.ToArray();
        }
        monkey = GameObject.Instantiate(Monkey.MonkeyPrefab(),transform.position, transform.rotation).GetComponent<Monkey>();       
        monkey.initialize(this.gameObject);
        
        float SpeedMod = 1/ transform.localScale.magnitude;

        float RotSpeed = Random.Range(0,AngularTerminalVelocity) - AngularTerminalVelocity*0.5f;
        
        Vector2 speed;

        speed.x = (Random.Range(0,TerminalVelocity*2) - TerminalVelocity)*SpeedMod;
        speed.y = (Random.Range(0,TerminalVelocity*2) - TerminalVelocity)*SpeedMod;
        speed = Vector2.ClampMagnitude(speed,TerminalVelocity);
        
        AddVelocity(speed,RotSpeed);
        
        Spawner.asteroidCount += 1;

        GameManager.instance.game.OnGameOver += OnGameOver;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        transform.position = EdgeWrapSystem.screenWrapPosition(transform.position);
    }

    void OnDestroy()
    {
        GameManager.instance.game.OnGameOver -= OnGameOver;
        Spawner.asteroidCount -= 1;
        if(monkey)Destroy(monkey.gameObject);


        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        SendMessage($"OnHit{other.tag}",SendMessageOptions.DontRequireReceiver);
    }

    void OnHitBullet()
    {
        if(ExplosionParticleSystem)
        {
            GameObject Explosion = Instantiate(ExplosionParticleSystem, transform.position, Quaternion.Euler(0,0,0));
            Explosion.transform.localScale = transform.localScale * 0.5f;
        }
        if(GameManager.instance)GameManager.instance.game.AddScore((int)transform.localScale.magnitude * 10);
        if(ExplosionSound)AudioSource.PlayClipAtPoint(ExplosionSound,transform.position);
        if(transform.localScale.magnitude >= 2)
        {
            GameObject[] HalfAsteroids = {GameObject.Instantiate(this.gameObject, transform.position, transform.rotation),GameObject.Instantiate(this.gameObject, transform.position, transform.rotation)};
            foreach(GameObject asteroid in HalfAsteroids)
            {
                asteroid.transform.localScale = transform.localScale / 2;
            }
        }
        Destroy(this.gameObject);
    }

    public void OnGameOver()
    {
        Destroy(this.gameObject);
    }


}
