using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // A velocidade de movimento do jogador
    public float jumpForce = 5f; // A força do pulo do jogador
    private bool isJumping = false; // Se o jogador está pulando ou não
    private Rigidbody rb; // Referência ao componente Rigidbody do jogador

    private void Start()
    {
        // Obtém a referência ao componente Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Obter o input do jogador
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Criar um novo vetor de movimento baseado no input do jogador
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        // Normalizar o vetor de movimento para que o jogador não se mova mais rápido na diagonal
        movement.Normalize();

        // Converter o vetor de movimento de espaço local (relativo à câmera) para espaço do mundo
        movement = Camera.main.transform.TransformDirection(movement);

        // Ignora a rotação da câmera no eixo Y (para manter o jogador no chão)
        movement.y = 0;

        // Aplicar o movimento ao jogador
        transform.position += movement * moveSpeed * Time.deltaTime;

        // Faz o jogador sempre olhar na direção que a câmera está apontando horizontalmente
        Vector3 cameraEulerAngles = Camera.main.transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, cameraEulerAngles.y, transform.rotation.eulerAngles.z);

        // Verifica se o jogador pressionou a tecla de pulo
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
    }

    // Verifica se o jogador tocou o chão
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
