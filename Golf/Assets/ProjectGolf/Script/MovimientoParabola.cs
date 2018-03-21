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

    int res =30;
    Vector3[] points;
    public LineRenderer lr;

    private void Start()
    {
        points = new Vector3[res];
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
        disX = tr.position.x - transform.position.x;
        disZ = tr.position.z - transform.position.z;
        
        RenderPath(new Vector3(vec() * Mathf.Cos(radian()), vec() * Mathf.Sin(radian()), 0));

        if (canShoot)
        {
            if (Input.GetMouseButtonDown(0))
            {
                canShoot = false;
                transform.GetComponent<Rigidbody>().useGravity = true;
                transform.GetComponent<Rigidbody>().AddForce(vec() * Mathf.Cos(radian()), vec() * Mathf.Sin(radian()), 0, ForceMode.Impulse);
            }
        }
    }

    public float radian()
    {
        float radian = angle * Mathf.PI / 180f;
        return radian;
    }

    public float vec()
    {
        float vec = disX / (Mathf.Cos(radian()) * timeSimulate());
        return vec;
    }

    public float timeSimulate()
    {
        float t = ((-1 * tr.position.y) + transform.position.y + disX) * 2 / 9.81f;
        t = Mathf.Sqrt(t);
        return t;
    }

    public void RenderPath(Vector3 path)
    {
        Vector3 refG = new Vector3(0f,-9.81f, 0f);

        for (int i = 0; i < res; i++)
        {
            float t = i / (float)res * timeSimulate();
            Vector3 desp = path * t + refG * t * t / 2f;
            desp += transform.position;
            points[i] = desp;
            if (i > 0)
            {
                Debug.DrawLine(points[i], points[i - 1], Color.green);
            }
            lr.SetPositions(points);
        }
    }
}
