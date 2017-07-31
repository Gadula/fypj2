using System.Collections;
using UnityEngine;

public class Horizon : MonoBehaviour
{
    private float initialX;
    private float rotateZ, translateY;
    private bool Affect;
    private bool r, t;
    private bool generated;
    private bool moved;

    private float affectTimer;
    private float stabiliseTimer;

    private GameObject horizonContainer;
    private VJController controller;

    // Use this for initialization
    private void Start()
    {
        initialX = gameObject.GetComponent<RectTransform>().localPosition.x;
        rotateZ = 0;
        translateY = 0;
        Affect = true;
        generated = false;
        moved = false;

        affectTimer = Random.Range(0f, 5f);
        stabiliseTimer = 2f;

        //rand duration to call affect (ONLY IF NOT TRUE)

        horizonContainer = GameObject.Find("Stabiliser Container");
        controller = horizonContainer.GetComponentInChildren<VJController>();
    }

    private void countdownAffect()
    {
        affectTimer -= Time.deltaTime;

        if (affectTimer < 0)
        {
            Affect = true;
            generated = false;
            affectTimer = Random.Range(2f, 5f);
        }
    }

    private void randomMovements()
    {
        int dir = Random.Range(0, 2);
        float movement = Random.Range(2.5f, 25f);
        // up
        if (dir == 0)
        {
            translateY = movement;
        }
        // down
        else
        {
            translateY = -movement;
        }

        //rand rotation
    }

    private void RotatePlane()
    {
        gameObject.GetComponent<RectTransform>().Rotate(0, 0, rotateZ);
    }

    private void TranslatePlane()
    {
        gameObject.GetComponent<RectTransform>().localPosition = Vector3.MoveTowards(gameObject.GetComponent<RectTransform>().localPosition, new Vector3(initialX, translateY, 0), 10 * Time.deltaTime);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Affect)
        {
            Affect = false;
            if (!generated)
            {
                randomMovements();
                generated = true;
                moved = false;
            }
        }

        countdownAffect();

        if (controller.GetY() < 0)
        {
            --translateY;
            moved = true;
        }

        if (controller.GetY() > 0)
        {
            ++translateY;
            moved = true;
        }

        TranslatePlane();

        if (gameObject.GetComponent<RectTransform>().localPosition.y <= 2.5f && gameObject.GetComponent<RectTransform>().localPosition.y >= -2.5f)
        {
            stabiliseTimer -= Time.deltaTime;

            if (stabiliseTimer < 0)
            {
                stabiliseTimer = 2f;
                Scoring.instance.AddScore(5);
            }
        }
    }

    //need function to correct/apply to horizon when button press
}