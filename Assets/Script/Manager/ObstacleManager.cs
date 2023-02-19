using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    List<Object> brickPrefs;
    List<Object> poopPrefs;
    List<GameObject> curBricks;
    Queue<Vector2> curBricksPos;
    float generateRange = 12f;
    int dequeCount = 0;

    public void Init()
    {
        curBricks = new List<GameObject>();
        curBricksPos = new Queue<Vector2>();
        dequeCount = 0;

        brickPrefs = new List<Object>();
        LoadBrickPref();
        poopPrefs = new List<Object>();
        LoadPoopPref();


    }

    void LoadBrickPref()
    {
        for (int i = 1; i <= 4; i++)
        {
            brickPrefs.Add(Resources.Load($"Prefabs/Obstacle/Brick{i}") as GameObject);
        }
    }

    void LoadPoopPref()
    {
        for (int i = 1; i <= 3; i++)
        {
            poopPrefs.Add(Resources.Load($"Prefabs/Obstacle/Poop{i}") as GameObject);
        }
    }

    public IEnumerator GeneratePoop()
    {
        Object newPoop = poopPrefs[Random.Range(0, 3)];

        if(newPoop != null)    
        {
            Instantiate(newPoop, GetRandomPoopPosition(), Quaternion.identity);
        }

        yield return new WaitForSeconds(Random.Range(3f, 6f));
    }

    public void GernerateBrick()
    {
        CheckCurBrick();
        Object newBrick = brickPrefs[Random.Range(0, 4)];

        if(newBrick != null)    
        {
            GameObject newObject = (GameObject) Instantiate(newBrick, GetRandomBrickPosition((GameObject) newBrick), Quaternion.identity);  
            curBricks.Add(newObject); 
        }  
    }

    Vector2 GetRandomBrickPosition(GameObject _newBrick)
    {
        GameObject player = GameObject.Find("Player");
        PlayerController playerController = player.GetComponent<PlayerController>();
        SpriteRenderer playerRenderer = player.GetComponent<SpriteRenderer>();
        Vector3 playerDestPos = playerController.GetPlayerDestPos();
        Vector2 brickSize = _newBrick.GetComponent<SpriteRenderer>().bounds.size;

        float playerSizeY = playerRenderer.bounds.size.y;
        float posX = Managers.Background.GetBackgroundSize().x / 2 + Random.Range(0.1f, brickSize.x / 4) * Random.Range(-1, 2);
        posX = (playerDestPos.x > 0)? -posX : posX;
        
        Vector2 newPos;
        if(curBricksPos.Count < 2)
        {
            float posY = playerDestPos.y + playerSizeY + Random.Range(6f, generateRange);
            newPos = new Vector2(posX, posY);
        }
        else
        {
            Vector2 prevPos = curBricksPos.Dequeue();
            float posY = prevPos.y + playerSizeY + Camera.main.orthographicSize + Random.Range(5f, 12f);
            newPos = new Vector2(posX, posY);
        }
        curBricksPos.Enqueue(newPos);

        return newPos; 
    }

    Vector2 GetRandomPoopPosition()
    {
        GameObject player = GameObject.Find("Player");
        PlayerController playerController = player.GetComponent<PlayerController>();
        SpriteRenderer playerRenderer = player.GetComponent<SpriteRenderer>();
        Vector3 playerDestPos = playerController.GetPlayerDestPos();

        float backgroundSize = Managers.Background.GetBackgroundSize().x / 2;
        float posX = Random.Range(-backgroundSize + 5f, backgroundSize - 5f);

        float playerSizeY = playerRenderer.bounds.size.y;
        float posY = playerDestPos.y + playerSizeY + 25f;

        return new Vector2(posX, posY); 
    }

    void CheckCurBrick()
    {
        GameObject player = GameObject.Find("Player");
        Vector2 playerPos = player.transform.position;
        List<GameObject> removeBricks = new List<GameObject>();
        foreach(GameObject curBrick in curBricks)
        {
            Vector2 brickPos = curBrick.transform.position;
            if(playerPos.y - brickPos.y > Camera.main.orthographicSize * 2 + 10f)
            {
                removeBricks.Add(curBrick);
                Destroy(curBrick);
            }
        }

        foreach(GameObject removeBrick in removeBricks)
        {
            curBricks.Remove(removeBrick);
        }
    }
}
