using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayaArea : MonoBehaviour
{
    public static PlayaArea instance;
    public Rect bounds;

    public float margin = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        if(!instance)
        {
            instance = this;
        }
        else GameObject.Destroy(this.gameObject);

        float aspect = (float)Screen.width / Screen.height;
 
        float worldHeight = Camera.main.orthographicSize * 2;
 
        float worldWidth = worldHeight * aspect;

        bounds.width = worldWidth;
        bounds.height = worldHeight;
    }

    // Update is called once per frame
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(bounds.width,bounds.height,0));
    }

    //returns bit flags that are 1 when position's out of bounds
    public byte ObjectWithinBoundsInfo(Vector3 other)
    {
        byte output = 0x00000000;

        Debug.Log($"{other}, {bounds.xMin}, {bounds.xMax}, {bounds.yMin}, {bounds.yMax},");
        //is outside bounds on the right
        if(other.x > bounds.xMax  - bounds.width * 0.5)      output += 0b0001;
        //is outside bounds on the left
        else if(other.x < bounds.xMin - bounds.width * 0.5) output += 0b0010;
        //is outside bounds on the top
        if(other.y > bounds.yMax - bounds.height * 0.5)      output += 0b0100;        
        //is outside bounds on the bottox
        else if(other.y < bounds.yMin - bounds.height * 0.5) output += 0b1000;

        return output;
    }

}
