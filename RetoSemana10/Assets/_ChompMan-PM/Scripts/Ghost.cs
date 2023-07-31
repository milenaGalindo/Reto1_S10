using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3[] posiciones;
    public Vector3[] rotaciones;
    public float movementSpeed = 5f;
    private bool restart;

    private void Start()
    {
        StartCoroutine("GhostMoveRotate");
        restart = false;
    }

    private void Update()
    {
        if (restart)
        {
            StartCoroutine("GhostMoveRotate");
            restart = false;
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<CollisionsAdmin>().esInvencible)
            {
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
                StartCoroutine("GhostDeath");
            }
        }
    }



    IEnumerator GhostMoveRotate()
    {
        int positionIndex = 0;

        while (positionIndex < posiciones.Length)
        {
            Vector3 nextPosition = posiciones[positionIndex];
            while (transform.position != nextPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPosition, movementSpeed * Time.deltaTime);
                yield return null;
            }

            if (positionIndex < rotaciones.Length)
            {
                transform.rotation = Quaternion.Euler(rotaciones[positionIndex]);
            }
                           
            positionIndex++;
        }

        restart = true;

    }



    IEnumerator GhostDeath()
    {

        float elapsedTime = 0;
        float waitTime = 0.7f;
        Vector3 originalScale = transform.localScale;
        Vector3 finalScale = new Vector3(0f, 2f, 0f);

        while (elapsedTime < waitTime)
        {
            transform.localScale = Vector3.Lerp(originalScale, finalScale, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;           
            yield return null;
        }

        Destroy(gameObject);

    }


}
