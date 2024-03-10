using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrower : MonoBehaviour
{
    public GameObject axePrefab; // Prefab del hacha
    public Transform throwPoint; // Punto desde donde se lanzará el hacha
    public float throwForce = 20f; // Fuerza de lanzamiento del hacha

    // Update is called once per frame
    void Update()
    {
        // Detectar el clic del ratón
        if (Input.GetMouseButtonDown(0))
        {
            ThrowAxe();
        }
    }

    void ThrowAxe()
    {
        // Instanciar un nuevo hacha desde el prefab
        GameObject axeInstance = Instantiate(axePrefab, throwPoint.position, throwPoint.rotation);

        // Acceder al Rigidbody del hacha
        Rigidbody axeRB = axeInstance.GetComponent<Rigidbody>();

        // Verificar que se ha encontrado el Rigidbody
        if (axeRB != null)
        {
            // Aplicar fuerza al hacha para lanzarla
            axeRB.AddForce(throwPoint.forward * throwForce, ForceMode.Impulse);
        }
    }
}
