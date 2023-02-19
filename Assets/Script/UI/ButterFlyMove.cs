using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterFlyMove : MonoBehaviour
{
    Vector2 initPos;
    RectTransform rect;

    float delta = 0.5f;

    float speed = 5.0f; 
    void Start()
    {
        rect = GetComponent<RectTransform>();
        initPos = rect.position;
    }


    void Update()
    {
        Vector3 v = rect.position;
        v.x += delta * Mathf.Sin(Time.time * speed);
        rect.position = v;
    }

    public void Init()
    {
        rect.position = initPos;
    }
}
