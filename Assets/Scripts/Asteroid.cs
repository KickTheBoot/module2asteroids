using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    SpriteVariantScriptableObject SpriteVariants;

    Vector2 Velocity;
    float AngularVelocity;
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
        
        Velocity.x = Random.Range(0,MaxVelocity*2) - MaxVelocity;
        Velocity.y = Random.Range(0,MaxVelocity*2) - MaxVelocity;
        Velocity = Vector2.ClampMagnitude(Velocity,MaxVelocity);
        
        AngularVelocity = Random.Range(0, MaxAngularVelocity) - MaxAngularVelocity*0.5f;
        
        Spawner.asteroidCount += 1;

        GameManager.instance.game.OnGameOver += OnGameOver;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime*Velocity,Space.World);
        transform.Rotate(new Vector3(0,0,AngularVelocity)*Time.deltaTime,Space.World);
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
        GameManager.instance.game.AddScore((int)transform.localScale.magnitude * 10);
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
