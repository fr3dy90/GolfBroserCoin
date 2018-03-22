using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    [Header("Physics")]
    public Transform target;
    public float angle;
    public Rigidbody rb;
    public TargetBH m_targetBh;

    [Header("Visuals")]
    public bool isRender;
    public LineRenderer lr;
    Vector3[] points;
    public int res = 30;
    public float simulationTime;

    public Vector3 enesima(Transform target, float angle)
    {
        Vector3 dir = (target.position - rb.transform.position);
        float h = dir.y;
        dir.y = 0;
        float dist = dir.magnitude;
        float a = angle * Mathf.Deg2Rad;
        dir.y = dist * Mathf.Tan(a);
        dist += h / Mathf.Tan(a);
        float vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return vel * dir.normalized;
    }

    private void Awake()
    {
        rb.useGravity = false;
        points = new Vector3[res];
    }

    private void Start()
    {
        target = m_targetBh.target.transform;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if (isRender)
        {
            RenderPath(enesima(target, angle), res, simulationTime);
        }
    }

    public void Shoot()
    {
        lr.gameObject.SetActive(false);
        target = m_targetBh.target.transform;
        rb.useGravity = true;
        rb.AddForce(enesima(target, angle), ForceMode.Impulse);
    }

    public void RenderPath(Vector3 vel, int res, float simulationTime)
    {
        Vector3 refG = new Vector3(0f, -9.81f, 0f);
        for (int i = 0; i < points.Length; i++)
        {
            float t = i / (float)res * simulationTime;
            Vector3 desp = vel * t + refG * t * t / 2f;
            desp += rb.transform.position;
            points[i] = desp;
            lr.SetPositions(points);
        }
    }
}
