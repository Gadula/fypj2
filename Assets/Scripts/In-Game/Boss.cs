using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public GameObject spwanArea;

    public GameObject Controller;
    private VJController control;

    public GameObject Hori;
    public GameObject Verti;
    public GameObject Checker;
    public Button HIT;

    private float xPosition;
    private float yPosition;

    // Use this for initialization
    private void Start()
    {
        HIT.interactable = false;
        control = Controller.GetComponent<VJController>();
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(
        Random.Range(spwanArea.GetComponent<RectTransform>().rect.xMin, spwanArea.GetComponent<RectTransform>().rect.xMax),
        Random.Range(-300, 400), 0);
    }

    // Update is called once per frame
    private void Update()
    {
        Hori.GetComponent<RectTransform>().localPosition = new Vector3(0, Mathf.Clamp(Hori.GetComponent<RectTransform>().localPosition.y + control.GetY(), -300, 400), 0);
        Verti.GetComponent<RectTransform>().localPosition = new Vector3(Mathf.Clamp(Verti.GetComponent<RectTransform>().localPosition.x + control.GetX(), spwanArea.GetComponent<RectTransform>().rect.xMin, spwanArea.GetComponent<RectTransform>().rect.xMax), 0, 0);
        xPosition = Verti.GetComponent<RectTransform>().localPosition.x;
        yPosition = Hori.GetComponent<RectTransform>().localPosition.y;
        Checker.GetComponent<RectTransform>().localPosition = new Vector3(xPosition, yPosition, 0);
        if (gameObject.GetComponent<Collider2D>().IsTouching(Checker.GetComponent<Collider2D>()) == true)
        {
            //Debug.Log("HIT");
            if (HIT.interactable == false)
            {
                //Debug.Log("HERE");
                HIT.interactable = true;
            }
        }
    }

    public void Trigger()
    {
        Debug.Log("CHANGED");
        SceneChange.instance.goToGame();
    }
}