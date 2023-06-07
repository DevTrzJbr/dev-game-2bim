using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // A velocidade de movimento do jogador
    public float jumpForce = 5f; // A for�a do pulo do jogador
    private bool isJumping = false; // Se o jogador est� pulando ou n�o
    private Rigidbody rb; // Refer�ncia ao componente Rigidbody do jogador

    private void Start()
    {
        // Obt�m a refer�ncia ao componente Rigidbody
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

        // Normalizar o vetor de movimento para que o jogador n�o se mova mais r�pido na diagonal
        movement.Normalize();

        // Converter o vetor de movimento de espa�o local (relativo � c�mera) para espa�o do mundo
        movement = Camera.main.transform.TransformDirection(movement);

        // Ignora a rota��o da c�mera no eixo Y (para manter o jogador no ch�o)
        movement.y = 0;

        // Aplicar o movimento ao jogador
        transform.position += movement * moveSpeed * Time.deltaTime;

        // Faz o jogador sempre olhar na dire��o que a c�mera est� apontando horizontalmente
        Vector3 cameraEulerAngles = Camera.main.transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, cameraEulerAngles.y, transform.rotation.eulerAngles.z);

        // Verifica se o jogador pressionou a tecla de pulo
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
    }

    // Verifica se o jogador tocou o ch�o
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
