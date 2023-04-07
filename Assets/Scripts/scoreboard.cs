using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class scoreboard : MonoBehaviour
{
    [SerializeField]
    VisualElement[] elements;
    public TemplateContainer Container;
    UIDocument doc;
    // Start is called before the first frame update
    void Start()
    {
        doc = GetComponent<UIDocument>();
        elements = new TextElement[8];


        for(int i = 0; i < elements.Length; i++)
        {
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
