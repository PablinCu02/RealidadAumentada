using System.Collections;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Variables 
    public GameObject model;
    public Transform[] TargetPositions;
    public int currentTarget = 0;
    public float speed = 1.0f;
    private bool isMoving = false;

    // Variables de Animación
    public Animator playerAnimator;
    public string animationBoolParameter = "IsWalking";

    public void MoveToNextTarget()
    {
        // Solo se mueve si no está moviéndose ya, y si hay posiciones asignadas
        if (!isMoving && TargetPositions.Length > 0)
        {
            StartCoroutine(MoveModel());
        }
    }

    private IEnumerator MoveModel()
    {
        isMoving = true;

        // Obtenemos el target al que vamos a mover
        Transform target = TargetPositions[currentTarget];

        if (target != null)
        {
            // Encender animación
            if (playerAnimator != null)
            {
                playerAnimator.SetBool(animationBoolParameter, true);
            }

            Vector3 startPosition = model.transform.position;
            Vector3 endPosition = target.transform.position;

            float journey = 0;

            // Mover el modelo, control de velocidad
            while (journey <= 1f)
            {
                journey += Time.deltaTime * speed;
                model.transform.position = Vector3.Lerp(startPosition, endPosition, journey);
                yield return null;
            }

            // Apagar animación al llegar
            if (playerAnimator != null)
            {
                playerAnimator.SetBool(animationBoolParameter, false);
            }

            // Preparar el siguiente destino
            currentTarget = (currentTarget + 1) % TargetPositions.Length;
        }

        isMoving = false;
    }
}