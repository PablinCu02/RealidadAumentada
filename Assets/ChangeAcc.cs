//using UnityEngine;

//public class CambiarAccesorio : MonoBehaviour
//{
//    [Header("Accesorios")]
//    public GameObject trofeo;
//    public GameObject balon;

//    private int estadoAccesorio = 0;

//    public void SiguienteAccesorio()
//    {
//        estadoAccesorio++;
//        if (estadoAccesorio > 2) estadoAccesorio = 0;

//        // Apagamos todo primero
//        trofeo.SetActive(false);
//        balon.SetActive(false);

//        // Prendemos solo el que corresponde
//        switch (estadoAccesorio)
//        {
//            case 0:
//                break;
//            case 1:
//                trofeo.SetActive(true);
//                break;
//            case 2:
//                balon.SetActive(true);
//                break;
//        }
//    }
//}

using UnityEngine;

public class ChangeAcc : MonoBehaviour
{
    // Referencia al componente Renderer del balón (el que dibuja la textura)
    public Renderer balonRenderer;

    // Lista de los Materiales que descargaste
    public Material[] materialesBalon;

    private int indiceActual = 0;

    void Start()
    {
        // Al iniciar, le ponemos el primer material de la lista por seguridad
        if (balonRenderer != null && materialesBalon.Length > 0)
        {
            balonRenderer.material = materialesBalon[0];
        }
    }

    // Función conectada al botón "Cambiar Accesorio"
    public void CambiarBalonManual()
    {
        if (balonRenderer == null || materialesBalon.Length == 0) return;

        // Sumamos 1 al índice y lo ciclamos
        indiceActual = (indiceActual + 1) % materialesBalon.Length;

        // Le aplicamos el nuevo material al balón
        balonRenderer.material = materialesBalon[indiceActual];
    }
}