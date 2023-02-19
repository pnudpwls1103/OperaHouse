using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float hAxis;
    public float moveSpeed;
    public float angleSpeed;
    float xWallpos;
    float angle = 0;
    int angleDir = 1;
    Vector2 dest;
    bool isMove = false;
    bool isGameOver = false;

    Rigidbody2D rigid;
    SpriteRenderer playerRenderer;
    Animator animator;

    float maxY = 0f;

    public Transform wall;
    public AngleBar angleBar;
    void Start()
    {
        Init();
    }

    void Update()
    {
        rigid.velocity = new Vector2(0f, -2f);

        if(isMove)
        {
            if(Mathf.Abs(dest.x - rigid.position.x) <= 0.5f)
            {
                rigid.position = new Vector2(dest.x, rigid.position.y);
                isMove = false;
                animator.SetBool("isMove", false);
                playerRenderer.flipX = (playerRenderer.flipX)? false: true;
            }
            else 
            {
                float newX = Mathf.Lerp(rigid.position.x, dest.x, Time.deltaTime * moveSpeed);
                float newY = Mathf.Lerp(rigid.position.y, dest.y, Time.deltaTime * moveSpeed);
                rigid.position = new Vector2(newX, newY);
            }
            CheckMaxY(rigid.position.y);
        }
        
        if(rigid.position.y + Camera.main.orthographicSize * 2 >= Managers.Background.GetCurrentBackgroudPos().y)
        {
            Managers.Background.SetnextBackgroundPos();
            Managers.Background.SetWallPos(1);
        }


        if(!isMove && !isGameOver)
        {
            if(Input.GetMouseButton(0))
            {
                SetAngle();
                angleBar.SetAngle(angle);
            }
            if(Input.GetMouseButtonUp(0))
            {
                xWallpos *= -1;
                isMove = true;
                animator.SetBool("isMove", true);

                float dy = Mathf.Abs(xWallpos) * Mathf.Tan((angle+22) * Mathf.Deg2Rad);
                dest = new Vector2(xWallpos, rigid.position.y + dy);
                angle = 0;
                angleDir = 1;
                Managers.Instance.CallObstacle();
            }
        }
    }

    void SetAngle()
    {
        if (angle < 0 || angle > 45)
        {
            angleDir *= -1;
            angle = angle > 45 ? 45 : 0;
        }

        float delta = Time.deltaTime * angleSpeed;
        angle += delta * angleDir;
    }

    void CheckMaxY(float curY)
    {
        if(maxY < curY)
        {
            maxY = curY;
            Managers.Score.SetScore(maxY);
        }
    }

    public Vector2 GetPlayerDestPos()
    {
        return dest;
    }

    public void Init()
    {
        maxY = 0f;
        angle = 0;
        angleDir = 1;
        isMove = false;
        isGameOver = false;
        rigid = GetComponent<Rigidbody2D>();
        xWallpos = wall.position.x - 1.4052735f;
        playerRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.SetBool("isMove", false);
        Managers.Score.Init();
        angleBar.SetAngle(angle);
        rigid.position = new Vector2(xWallpos, -3f);
        playerRenderer.flipX = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            isGameOver = true;
            Managers.Instance.GameOver();
        }
    }
}
