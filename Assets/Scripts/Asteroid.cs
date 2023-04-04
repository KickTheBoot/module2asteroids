using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    Monkey monkey;
    // Start is called before the first frame update
    void Start()
    {
        monkey = GameObject.Instantiate(Monkey.MonkeyPrefab(),transform.position, transform.rotation).GetComponent<Monkey>();       
        monkey.initialize(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
