using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(RectTransform))]
public class BridgetManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    [SerializeField] Image[] children;

    RectTransform rTransform;

    // Start is called before the first frame update
    void Start()
    {
        rTransform = GetComponent<RectTransform>();
    }


    int GridToLinear(int i, int j)
    {
        return (i*GridSize.y)+j;
    }

    public void SetColor(int i, int j, Color color)
    {
        children[GridToLinear(i,j)].color = color;
    }

    public void SetColor(int i, int j, Vector3 rgb)
    {
        Color color = new Color(rgb.x, rgb.y, rgb.z);
        children[GridToLinear(i,j)].color = color;
    }

    public void CreateGrid()
    {
        if(rTransform == null)
        {
            rTransform = GetComponent<RectTransform>();
        }

        //colors = new Color[GridSize.x*GridSize.y];
        children = new Image[gridSize.x*gridSize.y];

        Vector2 quadSize = new Vector2();
        quadSize.x = rTransform.rect.width/gridSize.x;
        quadSize.y = rTransform.rect.height/gridSize.y;
        
        print(rTransform.rect);
        print(gridSize);
        print(quadSize);

        Type[] componentTypes = {typeof(RectTransform), typeof(Image)}; 

        for(int i = 0; i<gridSize.x; i++)
        {
            for(int j = 0; j<gridSize.y; j++)
            {
                string name = "Image"+i.ToString()+j.ToString();
                GameObject go = new GameObject(name, componentTypes);
                go.transform.SetParent(this.transform);
                
                RectTransform childTransform = go.GetComponent<RectTransform>();
                Image img = go.GetComponent<Image>();
                Rect childRect = childTransform.rect;

                childTransform.pivot = Vector2.zero;

                Vector3 position = new Vector3(i*quadSize.x, j*quadSize.y, 0f);
                position.x -= rTransform.rect.x/2f;
                position.y -= rTransform.rect.y/2f;

                childTransform.position = position;
                childTransform.sizeDelta = quadSize;
                //hildRect.width = quadSize.x;
                //childRect.height = quadSize.y;
                
                float r = UnityEngine.Random.Range(0f, 1f);
                float g = UnityEngine.Random.Range(0f, 1f);
                float b = UnityEngine.Random.Range(0f, 1f);

                img.color = new Color(r, g, b);
                img.SetAllDirty();

                //colors[(i*GridSize.x)+j] = img.color;
                children[GridToLinear(i,j)] = img;
            }
        }
    }

    public void Clear()
    {
        if(children == null)
        {
            return;
        }
        foreach(Image img in children)
        {
            GameObject.DestroyImmediate(img.gameObject);
        }
        children = null;
        //colors = null;
    }

    public Color GetColor(int i, int j)
    {
        return children[GridToLinear(i,j)].color;
    }

    public Vector2Int GridSize
    {
        get
        {
            return gridSize;
        }
    }
}
