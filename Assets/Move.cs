using System.Collections;
using UnityEngine;
using Vuforia;

public class Move : MonoBehaviour
{
    //Variables
    public GameObject model; 
    public ObserverBehaviour[] ImageTargets;
    public int currentTarget= 0;
    public float speed = 1.0f;
    private bool isMoving = false;
    public void MoveToNextTarget() //Función para iniciar el movimiento   
    {
        if (!isMoving)
        {
            StartCoroutine(MoveModel());
        }
    }

    private IEnumerator MoveModel() //Corrutina para mover el modelo
    {
        isMoving = true;
        ObserverBehaviour target = GetNextDetectedTarget();
        if (target == null)
        {
            isMoving = false;
            yield break;
        }
        Vector3 startPosition= model.transform.position;
        Vector3 endPosition = target.transform.position;

        float journey = 0;

        while (journey <= 1f)
        {
            journey += Time.deltaTime * speed;
            model.transform.position = Vector3.Lerp(startPosition, endPosition, journey);
            yield return null;
        }
        currentTarget= (currentTarget + 1) % ImageTargets.Length;
        isMoving = false;
    }

    private ObserverBehaviour GetNextDetectedTarget() //Obtener el siguiente target detectado
    {
        foreach(ObserverBehaviour target in ImageTargets)
        {
            if (target != null && (target.TargetStatus.Status==Status.TRACKED) || (target.TargetStatus.Status==Status.EXTENDED_TRACKED))
            {
                return target;
            }
        }
        return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
