using UnityEngine;

public class CambiarAccesorio : MonoBehaviour
{
    [Header("Accesorios")]
    public GameObject trofeo;
    public GameObject balon;

    private int estadoAccesorio = 0;

    public void SiguienteAccesorio()
    {
        estadoAccesorio++;
        if (estadoAccesorio > 2) estadoAccesorio = 0;

        // Apagamos todo primero
        trofeo.SetActive(false);
        balon.SetActive(false);

        // Prendemos solo el que corresponde
        switch (estadoAccesorio)
        {
            case 0:
                break;
            case 1:
                trofeo.SetActive(true);
                break;
            case 2:
                balon.SetActive(true);
                break;
        }
    }
}