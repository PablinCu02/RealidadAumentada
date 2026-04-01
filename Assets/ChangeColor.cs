using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [Header("Partes del Modelo (Renderers)")]
    public SkinnedMeshRenderer playera;
    public SkinnedMeshRenderer shortRopa;
    public SkinnedMeshRenderer calcetas;

    [Header("Materiales por Equipo (Orden: Sporting, Portugal, ManU, RM, Juve)")]
    public Material[] matPlayeras;
    public Material[] matShorts;
    public Material[] matCalcetas;

    private int indiceActual = 0;

    void Start()
    {
        // Inicia con el uniforme del Sporting
        AplicarTodos(0);
    }

    public void CambiarUniformeManual()
    {
        if (matPlayeras.Length == 0) return;

        // Ciclamos el índice
        indiceActual = (indiceActual + 1) % matPlayeras.Length;
        AplicarTodos(indiceActual);
    }

    private void AplicarTodos(int indice)
    {
        if (matPlayeras.Length > indice) ReemplazarMateriales(playera, matPlayeras[indice]);
        if (matShorts.Length > indice) ReemplazarMateriales(shortRopa, matShorts[indice]);
        if (matCalcetas.Length > indice) ReemplazarMateriales(calcetas, matCalcetas[indice]);
    }

    // Funcion para reemplazar el material de una parte del modelo 
    private void ReemplazarMateriales(SkinnedMeshRenderer parte, Material nuevoMat)
    {
        if (parte == null || nuevoMat == null) return;
        Material[] nuevosMats = new Material[parte.materials.Length];

        // Aplicamos nuevo material 
        for (int i = 0; i < nuevosMats.Length; i++)
        {
            nuevosMats[i] = nuevoMat;
        }

        // Le regresamos el arreglo completo al modelo
        parte.materials = nuevosMats;
    }
}