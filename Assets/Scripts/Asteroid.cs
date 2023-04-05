using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    Vector2 Velocity;

    float MaxVelocity = 5;
    Monkey monkey;
    // Start is called before the first frame update
    void Start()
    {
        monkey = GameObject.Instantiate(Monkey.MonkeyPrefab(),transform.position, transform.rotation).GetComponent<Monkey>();       
        monkey.initialize(this.gameObject);
        Velocity.x = Random.Range(0,MaxVelocity*2) - MaxVelocity;
        Velocity.y = Random.Range(0,MaxVelocity*2) - MaxVelocity;
        Velocity = Vector2.ClampMagnitude(Velocity,MaxVelocity);
        Spawner.asteroidCount += 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime*Velocity);
        transform.position = EdgeWrapSystem.screenWrapPosition(transform.position);
    }

    void OnDestroy()
    {
        Spawner.asteroidCount -= 1;
        if(monkey)
        {
            Destroy(monkey.gameObject);
        }

        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        SendMessage($"OnHit{other.tag}",SendMessageOptions.DontRequireReceiver);
    }

    void OnHitBullet()
    {
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


}
