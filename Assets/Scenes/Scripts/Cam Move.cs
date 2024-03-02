using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 100f;
    public float minZoomDistance = 5f;
    public float maxZoomDistance = 20f;

    private Vector3 lastMousePosition;

    void Update()
    {
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

            transform.Rotate(Vector3.up, rotationY, Space.World);
            transform.Rotate(Vector3.right, -rotationX, Space.Self);

            lastMousePosition = Input.mousePosition;
        }
    }
}
