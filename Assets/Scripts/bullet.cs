using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : SpaceBody
{
    [SerializeField]
    float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
        AddVelocity(transform.up * speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        byte areainfo = PlayArea.instance.ObjectWithinBoundsInfo(transform.position);
        if(areainfo != 0)Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Renderer OtherRenderer;
        if(other.TryGetComponent<Renderer>(out OtherRenderer))
        {
            if(OtherRenderer.isVisible)
            {
                Debug.Log($"Hit {other.name}");
                if(other.tag != "Ship")Destroy(this.gameObject);
            }
        }
        
    }
}
