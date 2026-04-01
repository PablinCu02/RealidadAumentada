using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Hacemos la variable pública para verla en el Inspector
    public float rotationSpeed = 90f;

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}