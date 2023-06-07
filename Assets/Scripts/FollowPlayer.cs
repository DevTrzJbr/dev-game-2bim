using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform; // Referência à transformação do jogador
    private Vector3 offset; // A distância e ângulo entre a câmera e o jogador

    private void Start()
    {
        // Calcula e armazena a distância inicial e o ângulo entre a câmera e o jogador
        offset = transform.position - playerTransform.position;
    }

    private void LateUpdate()
    {
        // A câmera segue o jogador mantendo a mesma distância e ângulo
        transform.position = playerTransform.position + offset;
    }
}
