using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovParabola : MonoBehaviour {

    public TargetBH m_targetBH;

    [Header("Eje X,Z")]
    public float angle;
    public float disX;
    public float disZ;
    public float t;

    [Header("Eje Y")]
    public float h;
    public float g;
    public float velI;

    [Header("Pelota")]
    public Rigidbody rb;

    public float tiempoVuelo;
    bool haDisparado;

    void Start ()
    {
        haDisparado = false;
    }

    void Update()
    {
        if (m_targetBH.target != null)
        {
            Calculate();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Disparo();
            haDisparado = true;
        }
        if (haDisparado)
        {
            tiempoVuelo -= Time.deltaTime;
            if(tiempoVuelo <= 0)
            {
                haDisparado = false;
                rb.velocity = Vector3.zero;
                rb.useGravity = false;
            }
        }
	}

    void Calculate()
    {
        //Eje X
        float radian = angle * Mathf.PI / 180f;
        float resX = Mathf.Cos(radian);
        float resZ = Mathf.Cos(radian);
        disX = (m_targetBH.target.transform.position.x - rb.transform.position.x);
        disZ = (m_targetBH.target.transform.position.z - rb.transform.position.z);
        resX = disX / resX;
        resZ = disZ / resZ;

        //Eje Y
        float resY = Mathf.Sin(radian);

        /*
            Tener en cuenta que la velocidad y la gravedad
            deben tener signos diferentes ya que las velocidades son opuestas
            velocidad acendente y   (+)
            gravedad                (-)
        */
        resY = resY * resX;
        resY = resY - h;
        resY = resY / (-g / 2);
        t = resY / 2;
        velI = resX / t;

        rb.useGravity = false;
        tiempoVuelo = (velI * 2) * Mathf.Sin(radian) / -g;
    }

    void Disparo()
    {
        float radian = angle * Mathf.PI / 180f;
        rb.useGravity = true;
        rb.AddForce(new Vector2(Mathf.Cos(radian)*velI, Mathf.Sin(radian)*velI), ForceMode.Impulse);
    }
}
