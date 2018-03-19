using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBH : MonoBehaviour {

    public GameObject prefbTarget;
    public GameObject target;
    public Vector3 offset;
    public Vector3 targetPos;
    // Update is called once per frame

    private void Start()
    {
        target = Instantiate(prefbTarget, Vector3.zero, Quaternion.Euler(90,0,0));
        target.SetActive(false);
    }

    void Update () {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hit, 100))
        {
            if(hit.transform.tag == "Ground")
            {
                target.SetActive(true);
                target.transform.position = hit.point + offset;
                targetPos = target.transform.position;
            }
            else
            {
                target.SetActive(false);
            }
        }
	}
}
