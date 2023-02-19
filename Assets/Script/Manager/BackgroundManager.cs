using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class BackgroundManager
{
    int currentBackgroundIdx;
    GameObject backgroundParent;
    List<Transform> backgrounds;

    List<Transform> walls;

    Vector2 bgSize;

    public void Init()
    {
        currentBackgroundIdx = 0;
        backgroundParent = GameObject.Find("Background");
        backgrounds = new List<Transform>();
        for (int i = 0; i < backgroundParent.transform.childCount; i++)
        {
            backgrounds.Add(backgroundParent.transform.GetChild(i));
        }
        bgSize = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size;

        GameObject wallParent = GameObject.Find("Wall");
        walls = new List<Transform>();
        for (int i = 0; i < wallParent.transform.childCount; i++)
        {
            walls.Add(wallParent.transform.GetChild(i));
        }

        SetBackgroundPos();
        SetWallPos(0);
    }

    public Vector2 GetCurrentBackgroudPos()
    {
        return backgrounds[currentBackgroundIdx].position;
    }

    public Vector2 GetBackgroundSize()
    {
        return bgSize;
    }

    public void SetnextBackgroundPos()
    {
        int nextBackgroundIdx = (currentBackgroundIdx + 1) % 2;
        Transform nextCurrentBackground = backgrounds[nextBackgroundIdx];
        nextCurrentBackground.position = new Vector2(nextCurrentBackground.position.x, nextCurrentBackground.position.y + bgSize.y);
    
        currentBackgroundIdx = nextBackgroundIdx;
    }

    public void SetWallPos(int type)
    {
        if(type == 1)
        {
            foreach(Transform wall in walls)
            {
                wall.position = new Vector2(wall.position.x, wall.position.y + bgSize.y);
            }
        }
        else
        {
            foreach(Transform wall in walls)
            {
                wall.position = new Vector2(wall.position.x, 0);
            }
        }
        
    }

    void SetBackgroundPos()
    {
        backgrounds[0].position = new Vector2(0, 0);
        backgrounds[1].position = new Vector2(0, -129.69f);
    }
}
