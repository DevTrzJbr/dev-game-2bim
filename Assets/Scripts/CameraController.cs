using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 10f;

    private float xAxisClamp = 0.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Esconde e prende o cursor no centro da tela
    }

    private void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotAmountX = mouseX * mouseSensitivity;
        float rotAmountY = mouseY * mouseSensitivity;

        xAxisClamp -= rotAmountY;

        Vector3 targetRot = transform.rotation.eulerAngles;

        targetRot.x -= rotAmountY;
        targetRot.y += rotAmountX;

        // Limita a rotação no eixo X para evitar que a câmera gire demais e acabe olhando de cabeça para baixo ou para cima
        if (xAxisClamp > 90)
        {
            xAxisClamp = 90;
            targetRot.x = 90;
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            targetRot.x = 270;
        }

        transform.rotation = Quaternion.Euler(targetRot);
    }
}
