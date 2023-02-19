using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public SpriteRenderer[] bgSprites;
    public Transform playerTrans;
    public float cameraMoveSpeed;
    Vector3 cameraPosition = new Vector3(0, 0, -10);

    void Start()
    {

    }

    void FixedUpdate()
    {
        Vector3 newPos = new Vector3(0, playerTrans.position.y + Camera.main.orthographicSize / 2, 0) + cameraPosition;
        transform.position = Vector3.Lerp(transform.position, newPos,Time.deltaTime * cameraMoveSpeed);
    }
}
