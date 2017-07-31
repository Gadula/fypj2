using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public SceneChange sc;
    public GameObject enemySample;
    private List<GameObject> enemyArray;

    private int left = 0;
    private int right = 1;
    private int up = 0;
    private int down = 1;
    private int Horizontal, Vertical; //left right, up down
    private Vector3 playerPos;

    // Use this for initialization
    private void Start()
    {
        enemyArray = new List<GameObject>();

        playerPos.x = gameObject.GetComponent<RectTransform>().position.x;
        playerPos.y = gameObject.GetComponent<RectTransform>().position.y;
        playerPos.z = 0;
        for (int i = 0; i < 1; i++)
        {
            //enemy prefab
            //enemyArray[i] = Instantiate(enemySample.GetComponent<Enemy>());
            enemyArray.Add(Instantiate(enemySample));

            enemyArray[i].GetComponent<RectTransform>().SetParent(gameObject.GetComponent<RectTransform>());

            //Random directions
            Horizontal = Random.Range(left, right + 1);
            Vertical = Random.Range(up, down + 1);

            //Debug.Log(gameObject.GetComponent<RectTransform>().position);
            //Random positions
            if (Horizontal == left && Vertical == up)
            {
                enemyArray[i].GetComponent<RectTransform>().position = new Vector3(Random.Range(-80f, -50f) + gameObject.GetComponent<RectTransform>().position.x, Random.Range(55, 71) + gameObject.GetComponent<RectTransform>().position.y, 0);
            }
            else if (Horizontal == left && Vertical == down)
            {
                enemyArray[i].GetComponent<RectTransform>().position = new Vector3(Random.Range(-80f, -50f) + gameObject.GetComponent<RectTransform>().position.x, Random.Range(-55, -71) + gameObject.GetComponent<RectTransform>().position.y, 0);
            }
            else if (Horizontal == right && Vertical == up)
            {
                enemyArray[i].GetComponent<RectTransform>().position = new Vector3(Random.Range(80f, 50f) + gameObject.GetComponent<RectTransform>().position.x, Random.Range(55, 71) + gameObject.GetComponent<RectTransform>().position.y, 0);
            }
            else
            {
                enemyArray[i].GetComponent<RectTransform>().position = new Vector3(Random.Range(80f, 50f) + gameObject.GetComponent<RectTransform>().position.x, Random.Range(-55, -71) + gameObject.GetComponent<RectTransform>().position.y, 0);
            }

            //Debug.Log(enemyArray[i].GetComponent<RectTransform>().position);

            //Get direction to head to
            enemyArray[i].GetComponent<Enemy>().SetEnemyDir(playerPos - enemyArray[i].GetComponent<RectTransform>().position);
            //Release Enemy
            enemyArray[i].GetComponent<Enemy>().setTime(Random.Range(3, 5));
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //if (enemyArray != null)
        //{
        //    for (int i = 0; i < enemyArray.Count; i++)
        //    {
        //        //x 20, y 22
        //        if ((enemyArray[i].transform.position - gameObject.transform.position).sqrMagnitude < 20)
        //        {
        //            sc.goToFight();
        //        }
        //    }
        //}
    }

    public void Fight()
    {
        if (enemyArray[0].GetComponent<Enemy>().collide)
        {
            if (enemyArray[0].GetComponent<Enemy>().lastHit.name == "OuterCircle")
            {
                Scoring.instance.AddScore(10);
            }
            else if (enemyArray[0].GetComponent<Enemy>().lastHit.name == "MiddleCircle")
            {
                Scoring.instance.AddScore(5);
            }
            else
            {
                Scoring.instance.AddScore(2);
            }

            Scoring.instance.UpdateScore();

            SceneChange.instance.goToFight();
        }
    }
}