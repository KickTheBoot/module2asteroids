using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    [SerializeField]

    GameObject Copiable;

    [SerializeField]
    GameObject Target1, Target2, Target3;

    [SerializeField]
    // Start is called before the first frame update
    void Start()
    {
    }

    public void initialize(GameObject Copiable)
    {
        this.Copiable = Copiable;
        this.name = $"Monkey ({this.Copiable.name})";

        GameObject[] Targets = { Target1, Target2, Target3 };
        
        //Try to get the copiable polygon collider
        PolygonCollider2D coll;
        Copiable.TryGetComponent<PolygonCollider2D>(out coll);
 
        //Try to get the copiable Rigidbody2D
        Rigidbody2D rigidBody;
        Copiable.TryGetComponent<Rigidbody2D>(out rigidBody);

        //Try to get the copiable sprite renderer
        SpriteRenderer renderer;
        Copiable.TryGetComponent<SpriteRenderer>(out renderer);

        if(rigidBody)
            {
                Rigidbody2D currentRigidBody = gameObject.AddComponent<Rigidbody2D>();
                currentRigidBody.isKinematic = true;
                currentRigidBody.collisionDetectionMode = rigidBody.collisionDetectionMode;
            }

        //Add the components that matter to the
        foreach (GameObject Current in Targets)
        {
            
            if(coll)
            {
                PolygonCollider2D CurrentColl = Current.AddComponent<PolygonCollider2D>();
                CurrentColl.points = coll.points;
                CurrentColl.isTrigger = coll.isTrigger;
            }
            
            if(renderer)
            {
                SpriteRenderer Currentrenderer = Current.AddComponent<SpriteRenderer>();
                Currentrenderer.sprite = renderer.sprite;
            }

            Current.transform.localScale = Copiable.transform.localScale;
            Current.tag = Copiable.tag;
        }
        

    }
    // Update is called once per frame
    void Update()
    {
        Target1.transform.localScale = Copiable.transform.localScale;
        Target2.transform.localScale = Copiable.transform.localScale;
        Target3.transform.localScale = Copiable.transform.localScale;

        Target1.transform.rotation = Copiable.transform.rotation;
        Target2.transform.rotation = Copiable.transform.rotation;
        Target3.transform.rotation = Copiable.transform.rotation;

        Vector2 targetpos1 = Vector2.zero;
        Vector2 targetpos2 = Vector2.zero;
        Vector2 targetpos3 = Vector2.zero;

        float width = PlayaArea.instance.bounds.width;
        float height = PlayaArea.instance.bounds.height;

        float xtarget = width * (Copiable.transform.position.x >= 0 ? -1 : 1) + Copiable.transform.position.x;
        targetpos1.x = xtarget;
        targetpos2.x = xtarget;
        targetpos3.x = Copiable.transform.position.x;

        float ytarget = height * (Copiable.transform.position.y >= 0 ? -1 : 1) + Copiable.transform.position.y;
        targetpos1.y = Copiable.transform.position.y;
        targetpos2.y = ytarget;
        targetpos3.y = ytarget;

        Target1.transform.position = targetpos1;
        Target2.transform.position = targetpos2;
        Target3.transform.position = targetpos3;

    }

    public void OnShot()
    {
        Copiable.SendMessage("OnShot");
    }

    public void OnHit()
    {
        Copiable.SendMessage("OnHit");
    }

    //returns the prefab for monkey
    public static GameObject MonkeyPrefab()
    {
        return Resources.Load<GameObject>("Prefabs/Monkey");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        SpriteRenderer otherrenderer;
        if(other.TryGetComponent<SpriteRenderer>(out otherrenderer))
        {
            if(otherrenderer.isVisible)
            {
                Copiable.SendMessage($"OnHit{other.tag}",SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
