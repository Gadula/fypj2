using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool alive;
    private Vector2 dir = new Vector2();
    private Vector2 EnemyPos = new Vector2();
    public int time;

    public GameObject lastHit;

    public bool collide = false;

    // Use this for initialization
    private void Start()
    {
        //default constructor
        //EnemyPos = new Vector2();
        //dir = new Vector2();
        alive = false;
    }

    //from outside set
    public void SetEnemyPos(float x, float y)
    {
        EnemyPos.x = x; EnemyPos.y = y;
        gameObject.transform.position.Set(x, y, 0);
    }

    //from outside set
    public void SetEnemyDir(Vector2 _dir)
    {
        dir = _dir;
    }

    public void SetLife(bool life)
    {
        alive = life;
    }

    public bool isEnemyAlive()
    {
        return alive;
    }

    public void setTime(int t)
    {
        time = t;
        StartCountDown();
    }

    private void StartCountDown()
    {
        StartCoroutine(countDownRelease(time));
    }

    public Vector2 GetEnemyPos()
    {
        return EnemyPos;
    }

    //countdown to making enemy alive
    private IEnumerator countDownRelease(int _time)
    {
        yield return new WaitForSeconds(_time);
        alive = true;
    }

    // Update is called once per frame
    private void Update()
    {
        //set it so that the total of dir.x and dir.y cannot be more than 1
        dir.Normalize();
        dir.x = dir.x * 0.2f;
        dir.y = dir.y * 0.2f;
        //moving
        if (alive)
        {
            gameObject.GetComponent<RectTransform>().Translate(new Vector3(dir.x, dir.y, 0));
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("Trigger");
        if (col.gameObject.name == "InnerCircle" || col.gameObject.name == "MiddleCircle" || col.gameObject.name == "OuterCircle")
        {
            collide = true;
            lastHit = col.gameObject;
        }
    }
}