using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

    public SceneChange sc;
    //    sc.goToHome();
    // Use this for initialization
    void Start () {

        StartCoroutine(countDown(2));
    }

    IEnumerator countDown(int _time)
    {
        yield return new WaitForSeconds(_time);
        sc.goToHome();
    }

	//// Update is called once per frame
	//void Update () {
	
	//}
}
