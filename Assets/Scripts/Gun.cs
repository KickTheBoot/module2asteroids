using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private GameObject Bullet;

    [SerializeField]
    private Ship ship;

    [SerializeField]
    private AudioClip ShootNoise;

    // Start is called before the first frame update
    void Start()
    {
        ship.OnFire += Fire;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Fire()
    {
        if(ShootNoise) AudioSource.PlayClipAtPoint(ShootNoise, transform.position);
        GameObject.Instantiate(Bullet, transform.position, transform.rotation);
    }
}
