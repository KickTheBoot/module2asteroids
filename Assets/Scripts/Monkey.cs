using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Monkey : MonoBehaviour
{
    [SerializeField]

    GameObject Copiable;

    [SerializeField]
    // Start is called before the first frame update
    void Start()
    {
    }

    public void initialize(GameObject Copiable)
    {
        PolygonCollider2D coll = Copiable.GetComponent<PolygonCollider2D>();
        GetComponent<PolygonCollider2D>().points = coll.points;
        this.Copiable = Copiable;
        SpriteRenderer sprenderer;
        sprenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer CopiableRenderer;
        if(Copiable.TryGetComponent<SpriteRenderer>(out CopiableRenderer))sprenderer.sprite = CopiableRenderer.sprite;
        transform.localScale = Copiable.transform.localScale;
        this.tag = Copiable.tag;
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Copiable.transform.rotation;
        
        Vector2 targetposition;

        float width = PlayaArea.instance.bounds.width;
        float height = PlayaArea.instance.bounds.height;

        bool xMarginReached = Mathf.Abs(Copiable.transform.position.x) >= width * 0.5f - transform.localScale.x*PlayaArea.instance.margin;
        bool yMarginReached = Mathf.Abs(Copiable.transform.position.y) >= height * 0.5 - transform.localScale.x*PlayaArea.instance.margin;
        bool MarginReached = xMarginReached || yMarginReached;

        if(MarginReached)
        {
            if(xMarginReached){
                targetposition.x = width * (Copiable.transform.position.x >= 0 ? -1 : 1) + Copiable.transform.position.x;
            }
            else targetposition.x = Copiable.transform.position.x;
            if(yMarginReached){
                targetposition.y = height * (Copiable.transform.position.y >= 0 ? -1 : 1) + Copiable.transform.position.y;
            }
            else targetposition.y = Copiable.transform.position.y;
        }
        else{
            targetposition = new Vector2(width, height);
        }
        

        transform.position = targetposition;
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
}
