using UnityEngine;

public class InstiantateTest : MonoBehaviour
{
    public Transform brick;


    // Update is called once per frame
    void Update()
    {

        // Detecta si se hace clic con el botón izquierdo del ratón
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Realiza el Raycast
            if (Physics.Raycast(ray, out hit))
            {
                // Obtiene el punto de colisión y instancia el prefab
                Vector3 puntoDeInstancia = hit.point;
                Instantiate(brick, new Vector3(puntoDeInstancia.x, 1, puntoDeInstancia.z), Quaternion.identity);
                Debug.Log("Objeto instanciado en: " + puntoDeInstancia);
            }
        }

    }
}
