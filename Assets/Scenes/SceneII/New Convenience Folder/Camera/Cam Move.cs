using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 100f;
    public float maxUpAngle = 90f;
    public float maxDownAngle = 90f;

    private Vector3 lastMousePosition;

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != null &&
            EventSystem.current.currentSelectedGameObject.GetComponent<TMPro.TMP_InputField>() != null)
        {
            // If an input field is selected or focused, return without processing camera movement
            return;
        }
        // Camera movement with arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.Self); // Use Space.Self for local movement

        // Camera rotation with right mouse button drag
        if (Input.GetMouseButtonDown(1))
        {
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            float rotationX = delta.y * rotationSpeed * Time.deltaTime;
            float rotationY = delta.x * rotationSpeed * Time.deltaTime;

            float currentRotationX = transform.eulerAngles.x - rotationX;

            // Ensure the rotation stays within bounds
            if (currentRotationX > 90f && currentRotationX < 180f + maxUpAngle)
            {
                rotationX = 0;
                currentRotationX = 180f + maxUpAngle;
            }
            else if (currentRotationX < 270f && currentRotationX > 180f - maxDownAngle)
            {
                rotationX = 0;
                currentRotationX = 180f - maxDownAngle;
            }
            
            transform.Rotate(Vector3.up, rotationY, Space.World);
            transform.Rotate(Vector3.right, -rotationX, Space.Self);

            lastMousePosition = Input.mousePosition;
        }
    }
}
