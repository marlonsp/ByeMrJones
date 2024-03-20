using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMovement : MonoBehaviour
{
    private Transform playerTransform;
    public float fleeSpeed = 2f;
    public float proximityDistance = 5f; // Distância para começar a se mexer.
    public float minXLimit = -10f; // Limite mínimo no eixo X.
    public float maxXLimit = 10f; // Limite máximo no eixo X.
    public float minZLimit = -10f; // Limite mínimo no eixo Z.
    public float maxZLimit = 10f; // Limite máximo no eixo Z.

    public void SetPlayerTransform(Transform player)
    {
        playerTransform = player;
    }

    void Update()
    {
        if (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) < proximityDistance)
        {
            Vector3 fleeDirection = transform.position - playerTransform.position;

            // Ajusta a direção para restringir o movimento aos eixos X e Z.
            fleeDirection.y = 0f;

            transform.position += fleeDirection.normalized * fleeSpeed * Time.deltaTime;

            // Restringe a posição nos eixos X e Z.
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, minXLimit, maxXLimit),
                transform.position.y,
                Mathf.Clamp(transform.position.z, minZLimit, maxZLimit)
            );

            // Adiciona o código para fazer com que o pickup olhe para a direção oposta.
            transform.LookAt(transform.position + fleeDirection);
        }
    }
}
