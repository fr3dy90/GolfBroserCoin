using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBH : MonoBehaviour {

    public GameObject prefbTarget;
    public GameObject target;
    public Vector3 offset = Vector3.up * 0.01f;
    
    public float distance = 1000;
    // Update is called once per frame

    private void Start()
    {
        target = Instantiate(prefbTarget, Vector3.zero, Quaternion.Euler(90,0,0));
        target.SetActive(false);

        

        float t = (2 + 22) * 2 / 9.81f;
        t = Mathf.Sqrt(t);
        print(t);
    }

    void Update () {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hit, distance))
        {
            if(hit.transform.tag == "Ground")
            {
                target.SetActive(true);
                target.transform.position = hit.point + offset;
            }
            else
            {
                target.SetActive(false);
            }
        }
	}
}
