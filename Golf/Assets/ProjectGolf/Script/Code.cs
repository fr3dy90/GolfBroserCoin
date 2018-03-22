using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Code : MonoBehaviour {

    public Transform launch;
    public Transform destiny;
    public float launchAngle;

    public Vector3 finalVelocity;
    public Rigidbody body;

    bool online = false;

    void Awake () {
        //Obtenemos los vectores de los transforms para poder hacer calculos con
        //ellos sin modificar los transforms
        Vector3 launchVector = launch.position;
        Vector3 destinyVector = destiny.position;
        //transladamos los vectores de modo que la posicion de lanzamiento sea
        //el origen en XZ, esto facilitara futuros calculos
        launchVector -= new Vector3(launch.position.x, 0, launch.position.z);
        destinyVector -= new Vector3(launch.position.x, 0, launch.position.z);
        //Obtenemos un vector auxiliar que nos ayudara a encontrar el angulo
        //con el eje X. Este vector sera destinyVector con la Y en 0
        Vector3 angleAuxiliar = destinyVector;
        angleAuxiliar.y = 0;
        //Obtenemos el angulo con el eje X
        float axisAngleX = Vector3.Angle(angleAuxiliar, Vector3.right);
        //Rotamos el vector de destino el angulo indicado en el eje XZ
        float cos = Mathf.Cos(-axisAngleX * Mathf.PI / 180);
        float sin = Mathf.Sin(-axisAngleX * Mathf.PI / 180);
        destinyVector = new Vector3(destinyVector.x * cos - destinyVector.z * sin, destinyVector.y, destinyVector.x * sin + destinyVector.z * cos);
        Debug.Log(destinyVector);
        //Ahora el punto rotado esta alineado con el eje X, por lo que la
        //ecuacion es valida para este punto rotado. La distancia vendria
        //siendo el X del vector.
        float x = destinyVector.x;
        //y0 es la altura del lanzamiento
        float y0 = launchVector.y;
        //Y :u
        float y = destinyVector.y;
        //aplicamos la ecuacion
        float v0 = 2 * (y - y0 - x * Mathf.Tan(launchAngle * Mathf.PI / 180)) / Physics.gravity.y;
        v0 = Mathf.Sqrt(v0) * Mathf.Cos(launchAngle * Mathf.PI / 180);
        v0 = x / v0;
        Debug.Log(v0);
        //creamos un vector que reportara la velocidad y se le aplica el angulo
        //de lanzamiento
        cos = Mathf.Cos(launchAngle * Mathf.PI / 180);
        sin = Mathf.Sin(launchAngle * Mathf.PI / 180);
        finalVelocity = new Vector3(cos, sin, 0);
        //Cancelamos la rotacion inicial para reportar la velocidad correcta
        //(como el Z es cero se evita parte de la ecuacion de rotacion)
        cos = Mathf.Cos(axisAngleX * Mathf.PI / 180);
        sin = Mathf.Sin(axisAngleX * Mathf.PI / 180);
        finalVelocity = new Vector3(finalVelocity.x * cos, finalVelocity.y, finalVelocity.x * sin);
        //Multiplicamos el vector por la velocidad obtenida en la ecuacion para
        //obtener la velocidad real en XYZ
        finalVelocity *= v0;
        Debug.Log(finalVelocity);
    }

	void Start () {
        Debug.Log("Aqui explota todo y no se por que");
        body.velocity = finalVelocity;
	}

	void Update () {
        if(!online){
            Debug.Log("Aqui todo se ralentiza, excepto cuando lo pruebo online");
            gameObject.SetActive(false);
        }
	}
}
