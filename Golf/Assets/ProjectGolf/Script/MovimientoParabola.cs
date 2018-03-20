using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoParabola : MonoBehaviour {

    public bool isCalculate;
    public float disX;
    public float disZ;
    public float angle;
    
    public Transform tr;
    bool canShoot;
    private void Start()
    {
        canShoot = true;
    }

    private void Update()
    {
        if (isCalculate)
        {
            Calculate();
        }
    }

    void Calculate()
    {
        //float radian = angle * Mathf.PI / 180f;

        //float t = (0 + 22 + 2) * 2 / 9.81f;
        //t = Mathf.Sqrt(t);
        //print(t);

        //float vec = 22 / (Mathf.Cos(radian) * t);
        //print(vec);
        disX = tr.position.x - transform.position.x;
        disZ = tr.position.z - transform.position.z;
        float radian = angle * Mathf.PI / 180f;

        float t = ((-1 * tr.position.y) + transform.position.y + disX) * 2 / 9.81f;
        t = Mathf.Sqrt(t);

        float vec = disX / (Mathf.Cos(radian) * t);
        

        if (canShoot)
        {
            if (Input.GetMouseButtonDown(0))
            {
                canShoot = false;
                transform.GetComponent<Rigidbody>().useGravity = true;
                transform.GetComponent<Rigidbody>().AddForce(vec * Mathf.Cos(radian), vec * Mathf.Sin(radian), 0, ForceMode.Impulse);
            }
        }

    }
}
