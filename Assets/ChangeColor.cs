 using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [Header("Partes del Modelo")]
    public Renderer playera;
    public Renderer shortRopa;
    public Renderer botines;
    public Renderer calcetas;

    private int estadoColor = 0;

    public void CambiarSiguienteColor()
    {
        estadoColor++;
        if (estadoColor > 4) estadoColor = 0;

        Color colorAplicar = Color.white;
        switch (estadoColor)
        {
            case 0: colorAplicar = Color.white; break;
            case 1: colorAplicar = Color.blue; break;
            case 2: colorAplicar = new Color(0.8f, 0.8f, 0.8f); break;
            case 3: colorAplicar = Color.red; break;
            case 4: colorAplicar = Color.green; break;
        }

        AplicarColor(playera, colorAplicar);
        AplicarColor(shortRopa, colorAplicar);
        AplicarColor(botines, colorAplicar);
        AplicarColor(calcetas, colorAplicar);

    }

    private void AplicarColor(Renderer parte, Color colorNuevo)
    {
        foreach (Material mat in parte.materials)
        {
            mat.color = colorNuevo;
            mat.SetColor("_BaseColor", colorNuevo);
        }
    }
}