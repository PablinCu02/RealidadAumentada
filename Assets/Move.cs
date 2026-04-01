//using System.Collections;
//using UnityEngine;

//public class Move : MonoBehaviour
//{
//    // Variables 
//    public GameObject model;
//    public Transform[] TargetPositions;
//    public int currentTarget = 0;
//    public float speed = 1.0f;
//    private bool isMoving = false;

//    // Variables de Animación
//    public Animator playerAnimator;
//    public string animationBoolParameter = "IsWalking";

//    public void MoveToNextTarget()
//    {
//        // Solo se mueve si no está moviéndose ya, y si hay posiciones asignadas
//        if (!isMoving && TargetPositions.Length > 0)
//        {
//            StartCoroutine(MoveModel());
//        }
//    }

//    private IEnumerator MoveModel()
//    {
//        isMoving = true;

//        // Obtenemos el target al que vamos a mover
//        Transform target = TargetPositions[currentTarget];

//        if (target != null)
//        {
//            // Encender animación
//            if (playerAnimator != null)
//            {
//                playerAnimator.SetBool(animationBoolParameter, true);
//            }

//            Vector3 startPosition = model.transform.position;
//            Vector3 endPosition = target.transform.position;

//            float journey = 0;

//            // Mover el modelo, control de velocidad
//            while (journey <= 1f)
//            {
//                journey += Time.deltaTime * speed;
//                model.transform.position = Vector3.Lerp(startPosition, endPosition, journey);
//                yield return null;
//            }

//            // Apagar animación al llegar
//            if (playerAnimator != null)
//            {
//                playerAnimator.SetBool(animationBoolParameter, false);
//            }

//            // Preparar el siguiente destino
//            currentTarget = (currentTarget + 1) % TargetPositions.Length;
//        }

//        isMoving = false;
//    }
//}

//using System.Collections;
//using UnityEngine;
//using TMPro; // Necesario para controlar el texto en pantalla

//public class Move : MonoBehaviour
//{
//    public GameObject model;
//    public Transform[] TargetPositions;
//    public Transform ManagerTransform;

//    // Nombres para la narrativa (deben coincidir con el orden de tus TargetPositions)
//    public string[] TeamNames = { "Sporting", "Real Madrid", "Juventus", "Manchester United" };

//    public int currentTarget = 0;
//    private int nextTarget = 0;
//    public float speed = 1.0f;
//    private bool isMoving = false;
//    private bool offerReceived = false; // Controla si ya tenemos destino

//    public Animator playerAnimator;
//    public string animationBoolParameter = "IsWalking";

//    // Variable para la interfaz
//    public TextMeshProUGUI mensajeUI;

//    void Start()
//    {
//        if (mensajeUI != null)
//            mensajeUI.text = "Manager, CR7 está listo. ¡Presiona para buscar un fichaje!";
//    }

//    public void ActionButton()
//    {
//        if (isMoving) return;

//        if (!offerReceived)
//        {
//            // FASE 1: Sorteo Aleatorio
//            SorteoAleatorio();
//        }
//        else
//        {
//            // FASE 2: Viajar
//            StartCoroutine(MoveModel());
//        }
//    }

//    private void SorteoAleatorio()
//    {
//        // Elegir un destino al azar que NO sea el equipo actual
//        nextTarget = currentTarget;
//        while (nextTarget == currentTarget)
//        {
//            nextTarget = Random.Range(0, TargetPositions.Length);
//        }

//        // Narrativa en pantalla
//        if (mensajeUI != null)
//            mensajeUI.text = "¡Oferta del " + TeamNames[nextTarget] + "! Pon los 2 escudos en la mesa y presiona para viajar.";

//        offerReceived = true;
//    }
//    private IEnumerator MoveModel()
//    {
//        isMoving = true;
//        Transform target = TargetPositions[nextTarget];

//        if (target != null)
//        {
//            if (playerAnimator != null) playerAnimator.SetBool(animationBoolParameter, true);

//            Vector3 startPosition = model.transform.position;
//            Vector3 endPosition = target.transform.position;
//            float journey = 0;

//            // --- INICIO DE LA NUEVA JUGADA: MIRADA TÁCTICA --- //

//            // Obtenemos la dirección hacia el target
//            Vector3 directionToTarget = (endPosition - startPosition).normalized;
//            // Obtenemos la dirección hacia el manager
//            Vector3 directionToManager = (ManagerTransform.position - model.transform.position).normalized;

//            // Ignoramos la altura (Y) para que no se incline
//            directionToTarget.y = 0;
//            directionToManager.y = 0;

//            // 1. Primero, hacemos que voltee en la dirección de caminata
//            if (directionToTarget !=  Vector3.zero)
//            {
//                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
//                model.transform.rotation = targetRotation; // Gira instantáneamente
//            }
//            // --- FIN DE LA NUEVA JUGADA --- //

//            while (journey <= 1f)
//            {
//                journey += Time.deltaTime * speed;
//                model.transform.position = Vector3.Lerp(startPosition, endPosition, journey);
//                yield return null;
//            }

//            if (playerAnimator != null) playerAnimator.SetBool(animationBoolParameter, false);

//            currentTarget = nextTarget;
//            offerReceived = false; // Reiniciamos para buscar otra oferta

//            // --- INICIO DE LA SEGUNDA JUGADA: VOLTEAR AL MANAGER AL LLEGAR --- //
//            // Al llegar, hacemos que voltee de frente al manager
//            if (ManagerTransform != null && directionToManager != Vector3.zero)
//            {
//                Quaternion managerRotation = Quaternion.LookRotation(directionToManager);
//                // Puedes hacerlo instantáneo con: model.transform.rotation = managerRotation;
//                // O suave con una Corrutina si quieres más estética (te lo dejo instantáneo para probar rápido)
//                model.transform.rotation = managerRotation;
//            }
//            // --- FIN DE LA SEGUNDA JUGADA --- //

//            if (mensajeUI != null)
//                mensajeUI.text = "¡Fichaje exitoso en " + TeamNames[currentTarget] + "! ¿Qué sigue?";
//        }
//        isMoving = false;
//    }

//private IEnumerator MoveModel()
//{
//    isMoving = true;
//    Transform target = TargetPositions[nextTarget];

//    if (target != null)
//    {
//        if (playerAnimator != null) playerAnimator.SetBool(animationBoolParameter, true);

//        Vector3 startPosition = model.transform.position;
//        Vector3 endPosition = target.transform.position;
//        float journey = 0;

//        while (journey <= 1f)
//        {
//            journey += Time.deltaTime * speed;
//            model.transform.position = Vector3.Lerp(startPosition, endPosition, journey);
//            yield return null;
//        }

//        if (playerAnimator != null) playerAnimator.SetBool(animationBoolParameter, false);

//        currentTarget = nextTarget;
//        offerReceived = false; // Reiniciamos para buscar otra oferta

//        if (mensajeUI != null)
//            mensajeUI.text = "¡Fichaje exitoso en " + TeamNames[currentTarget] + "! ¿Qué sigue?";
//    }
//    isMoving = false;
//}

using UnityEngine;
using System.Collections;
using TMPro;

public class Move : MonoBehaviour
{
    public GameObject model;
    public Transform[] TargetPositions; // ¡AQUÍ DEBEN SER 5 EN EL INSPECTOR!
    public Transform ManagerTransform;
    public float speed = 1.0f;
    public Animator playerAnimator;

    // --- UTILERÍA (ACCESORIOS) --- //
    public GameObject sportingBall;
    public GameObject portugalBadge; // La playera flotante
    public GameObject plTrophy; // ManU
    public GameObject clTrophy; // Real Madrid
    public GameObject masterTrophy; // Juve

    // --- NARRATIVA (UI) --- //
    public TextMeshProUGUI mensajeUI;
    private string[] teamNames = { "Sporting", "Portugal", "Man Utd", "Real Madrid", "Juventus" };
    private string[] teamNarratives = {
        "¡El inicio de la leyenda! CR7 debuta en el Sporting y maravilla a Europa.",
        "¡Orgullo Nacional! Debut con la Roja de Portugal, camino a la gloria.",
        "¡El Teatro de los Sueños! Su primer Balón de Oro y Champions.",
        "¡Fichaje Galáctico! Época dorada: 4 Champions, 450 goles y leyenda.",
        "¡A la conquista de Italia! Dominó la Serie A y marcó más de 100 goles."
    };

    // --- CONTROL DE VIAJE --- //
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

    // --- BOTÓN 1: EL SORTEO Y EL VIAJE --- //
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

        // 1. Mirar al destino
        Vector3 directionToTarget = (endPosition - startPosition).normalized;
        directionToTarget.y = 0;
        if (directionToTarget != Vector3.zero)
        {
            model.transform.rotation = Quaternion.LookRotation(directionToTarget);
        }

        // 2. Caminar
        while (journey <= 1f)
        {
            journey += Time.deltaTime * speed;
            model.transform.position = Vector3.Lerp(startPosition, endPosition, journey);
            yield return null;
        }

        if (playerAnimator != null) playerAnimator.SetBool("IsWalking", false);
        currentTarget = nextTarget;
        offerReceived = false;

        // 3. Mirar al Manager al llegar
        Vector3 directionToManager = (ManagerTransform.position - model.transform.position).normalized;
        directionToManager.y = 0;
        if (ManagerTransform != null && directionToManager != Vector3.zero)
        {
            model.transform.rotation = Quaternion.LookRotation(directionToManager);
        }

        // 4. Automatizar firma de contrato
        ActivarUtileriaCorrecta(currentTarget);
        if (mensajeUI != null) mensajeUI.text = teamNarratives[currentTarget];

        isMoving = false;
    }

    // --- BOTÓN 3: REINICIAR LA CARRERA --- //
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

    // --- FUNCIONES INTERNAS --- //
    private void ApagarUtileria()
    {
        if (sportingBall) sportingBall.SetActive(false);
        if (clTrophy) clTrophy.SetActive(false);
        if (plTrophy) plTrophy.SetActive(false);
        if (masterTrophy) masterTrophy.SetActive(false);
        if (portugalBadge) portugalBadge.SetActive(false);
    }

    private void ActivarUtileriaCorrecta(int index)
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