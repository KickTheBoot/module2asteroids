using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    [SerializeField]
    Transform Copiable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Copiable.rotation;
        
        Vector2 targetposition;

        float width = PlayaArea.instance.bounds.width;
        float height = PlayaArea.instance.bounds.height;

        bool xMarginReached = Mathf.Abs(Copiable.position.x) >= width * 0.5f - PlayaArea.instance.margin;
        bool yMarginReached = Mathf.Abs(Copiable.position.y) >= height * 0.5 - PlayaArea.instance.margin;
        bool MarginReached = xMarginReached || yMarginReached;

        if(MarginReached)
        {
            if(xMarginReached){
                targetposition.x = width * (Copiable.position.x >= 0 ? -1 : 1) + Copiable.position.x;
            }
            else targetposition.x = Copiable.position.x;
            if(yMarginReached){
                targetposition.y = height * (Copiable.position.y >= 0 ? -1 : 1) + Copiable.position.y;
            }
            else targetposition.y = Copiable.position.y;
        }
        else{
            targetposition = new Vector2(width, height);
        }
        

        transform.position = targetposition;
    }
}
