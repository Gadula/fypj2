using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    private enum dir
    {
        N,
        S,
        E,
        W,
        NE,
        SE,
        SW,
        NW,
        MAX
    };

    private dir NextDir;
    public GameObject Needle;
    public Text Order;
    private float nextAngle;
    private float currentAngle;
    private bool bombNextDir;

    private bool reacted;
    private bool reached;

    private GameObject compassContainer;
    private VJController controller;

    private float delayTimer;
    private float holdTimer = 2f;

    public float moveSpeed = 0.5f;
    // public VJHandler jsMovement;

    // Use this for initialization
    private void Start()
    {
        nextAngle = 0;
        reacted = false;
        reached = true;
        bombNextDir = false;
        compassContainer = GameObject.Find("Compass Container");
        controller = compassContainer.GetComponentInChildren<VJController>();
    }

    private void GiveNextPosition()
    {
        int nextDir = Random.Range(0, (int)dir.MAX);

        switch (nextDir)
        {
            case 0:
                {
                    //Go North
                    nextAngle = 0;
                    break;
                }
            case 1:
                {
                    //Go NorthWest
                    nextAngle = 45;
                    break;
                }
            case 2:
                {
                    //Go West
                    nextAngle = 90;
                    break;
                }
            case 3:
                {
                    //Go SouthWest
                    nextAngle = 135;
                    break;
                }
            case 4:
                {
                    //Go South
                    nextAngle = 180;
                    break;
                }
            case 5:
                {
                    //Go SouthEast
                    nextAngle = 225;
                    break;
                }
            case 6:
                {
                    //Go East
                    nextAngle = 270;
                    break;
                }
            case 7:
                {
                    //Go NorthEast
                    nextAngle = 315;
                    break;
                }
        }

        if (nextAngle == currentAngle)
        {
            GiveNextPosition();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log(nextAngle.ToString());

        currentAngle = Needle.GetComponent<RectTransform>().rotation.eulerAngles.z;
        //Debug.Log((360 - currentAngle).ToString());
        if (reached == true && !bombNextDir)
        {
            reached = false;
            if (Random.Range(0, 4) >= 2)
            {
                bombNextDir = true;
                GiveNextPosition();
                Order.text = "Turning onto heading " + nextAngle.ToString();
            }
            else
            {
                nextAngle = -1f;
                delayTimer = Random.Range(1f, 5f);
            }
        }

        if (!reached && !bombNextDir && delayTimer >= 0)
        {
            Order.text = "";
            delayTimer -= Time.deltaTime;

            if (delayTimer < 0)
            {
                reached = true;
            }
        }

        if (controller.GetX() < 0)
        {
            reacted = true;
            Needle.GetComponent<RectTransform>().Rotate(0, 0, 1);
        }
        if (controller.GetX() > 0)
        {
            reacted = true;
            Needle.GetComponent<RectTransform>().Rotate(0, 0, -1);
        }
        //Direction achieved
        if (360f - currentAngle <= nextAngle + 5 && 360f - currentAngle >= nextAngle - 5)
        {
            holdTimer -= Time.deltaTime;

            if (holdTimer < 0)
            {
                reacted = false;
                reached = true;
                bombNextDir = false;

                holdTimer = 2f;
                Scoring.instance.AddScore(5);
            }
        }
        else
        {
            holdTimer = 2f;
        }
    }
}