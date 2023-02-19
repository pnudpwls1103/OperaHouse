using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    Vector2 initPos;
    void Start()
    {
        initPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y < initPos.y - 50f)
        {
            Destroy(this.gameObject);
        }
    }
}
