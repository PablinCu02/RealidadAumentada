using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Variable pública para modificarla en Unity
    public float rotationSpeed = 90f;

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}