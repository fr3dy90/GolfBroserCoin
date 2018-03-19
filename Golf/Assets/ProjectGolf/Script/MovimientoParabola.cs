using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoParabola : MonoBehaviour {

    public bool isCalculate;
    public float disX;
    public float angle;
    public TargetBH m_target;

    private void Start()
    {
        m_target = FindObjectOfType<TargetBH>();
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
        float radian = angle * Mathf.PI / 180f;
        if (m_target.target != null)
        {
            disX = m_target.target.transform.position.x - transform.position.x;
        }
    }
}
