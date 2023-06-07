using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform; // Refer�ncia � transforma��o do jogador
    private Vector3 offset; // A dist�ncia e �ngulo entre a c�mera e o jogador

    private void Start()
    {
        // Calcula e armazena a dist�ncia inicial e o �ngulo entre a c�mera e o jogador
        offset = transform.position - playerTransform.position;
    }

    private void LateUpdate()
    {
        // A c�mera segue o jogador mantendo a mesma dist�ncia e �ngulo
        transform.position = playerTransform.position + offset;
    }
}
