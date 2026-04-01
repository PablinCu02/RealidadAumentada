using UnityEngine;
using System.Collections;
using TMPro;

public class Move : MonoBehaviour
{
    public GameObject model;
    public Transform[] TargetPositions; 
    public Transform ManagerTransform;
    public float speed = 1.0f;
    public Animator playerAnimator;

    // Accesorios
    public GameObject sportingBall;
    public GameObject portugalBadge; 
    public GameObject plTrophy; 
    public GameObject clTrophy; 
    public GameObject masterTrophy; 

    // Narrativa //
    public TextMeshProUGUI mensajeUI;
    private string[] teamNames = { "Sporting", "Portugal", "Man Utd", "Real Madrid", "Juventus" };
    private string[] teamNarratives = {
        "CR7 debuta en el Sporting y maravilla a toda Europa!!!!.",
        "Debido a su rendimiento en club lo convocan a la Seleccion de Portugal.",
        "¡Gana su primer Balón de Oro y Champions con lo Red Devils!.",
        "¡Fichaje Galáctico! 4 Champions, 450 goles y leyenda con los merengues.",
        "Dominó la Serie A y marcó más de 100 goles."
    };

    // Control de viaje entre targets //
    public int currentTarget = 0;
    private int nextTarget = 0;
    private bool isMoving = false;
    private bool offerReceived = false;

    void Start()
    {
        ApagarUtileria();
        ActivarUtileriaCorrecta(0); // Iniciamos con el balón
        currentTarget = 0;
        if (mensajeUI != null) mensajeUI.text = "Manager, CR7 está listo. ¡Presiona para buscar un fichaje!";
    }

    // Botón de Sorteo y Movimiento//
    public void ActionButton()
    {
        if (isMoving) return;

        if (!offerReceived)
        {
            SorteoAleatorio();
        }
        else
        {
            StartCoroutine(MoveModel());
        }
    }

    private void SorteoAleatorio()
    {
        nextTarget = currentTarget;
        // Elige un equipo diferente al actual
        while (nextTarget == currentTarget)
        {
            nextTarget = Random.Range(0, TargetPositions.Length);
        }

        if (mensajeUI != null)
            mensajeUI.text = "¡Oferta del " + teamNames[nextTarget] + "! Pon los 2 escudos y presiona para viajar.";

        offerReceived = true;
    }

    private IEnumerator MoveModel()
    {
        isMoving = true;
        Transform target = TargetPositions[nextTarget];

        if (playerAnimator != null) playerAnimator.SetBool("IsWalking", true);

        Vector3 startPosition = model.transform.position;
        Vector3 endPosition = target.position;
        float journey = 0;

        Vector3 directionToTarget = (endPosition - startPosition).normalized;
        directionToTarget.y = 0;
        if (directionToTarget != Vector3.zero)
        {
            model.transform.rotation = Quaternion.LookRotation(directionToTarget);
        }

        // Caminar
        while (journey <= 1f)
        {
            journey += Time.deltaTime * speed;
            model.transform.position = Vector3.Lerp(startPosition, endPosition, journey);
            yield return null;
        }

        if (playerAnimator != null) playerAnimator.SetBool("IsWalking", false);
        currentTarget = nextTarget;
        offerReceived = false;

        // Rotamos para que mire al manager al llegar
        Vector3 directionToManager = (ManagerTransform.position - model.transform.position).normalized;
        directionToManager.y = 0;
        if (ManagerTransform != null && directionToManager != Vector3.zero)
        {
            model.transform.rotation = Quaternion.LookRotation(directionToManager);
        }

        // Automatizar firma de contrato
        ActivarUtileriaCorrecta(currentTarget);
        if (mensajeUI != null) mensajeUI.text = teamNarratives[currentTarget];

        isMoving = false;
    }

    // Botón para reiniciar la temática//
    public void ReiniciarCarrera()
    {
        if (isMoving) return;

        offerReceived = false;
        currentTarget = 0;

        // Teletransportar al inicio
        if (TargetPositions.Length > 0)
        {
            model.transform.position = TargetPositions[0].position;
        }

        ActivarUtileriaCorrecta(0);
        if (mensajeUI != null) mensajeUI.text = "¡Una nueva promesa en Portugal! Iniciemos la carrera de nuevo.";
    }

    // Funciones Internas//
    private void ApagarUtileria()
    {
        if (sportingBall) sportingBall.SetActive(false);
        if (clTrophy) clTrophy.SetActive(false);
        if (plTrophy) plTrophy.SetActive(false);
        if (masterTrophy) masterTrophy.SetActive(false);
        if (portugalBadge) portugalBadge.SetActive(false);
    }

    private void ActivarUtileriaCorrecta(int index) // Funcion para activar el accesorio correcto según el equipo actual
    {
        ApagarUtileria();
        switch (index)
        {
            case 0: if (sportingBall) sportingBall.SetActive(true); break; // Sporting
            case 1: if (portugalBadge) portugalBadge.SetActive(true); break; // Portugal
            case 2: if (plTrophy) plTrophy.SetActive(true); break; // Man Utd
            case 3: if (clTrophy) clTrophy.SetActive(true); break; // Real Madrid
            case 4: if (masterTrophy) masterTrophy.SetActive(true); break; // Juve
        }
    }
}