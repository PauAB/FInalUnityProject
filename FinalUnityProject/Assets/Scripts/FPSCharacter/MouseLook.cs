using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    float MouseSensitivity = 100f;
    [SerializeField]
    Transform PlayerBody;

    private float mRotationX = 0f;
    private float mMouseX;
    private float mMouseY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        mMouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        mMouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        mRotationX -= mMouseY;
        mRotationX = Mathf.Clamp(mRotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(mRotationX, 0f, 0f);
        PlayerBody.Rotate(Vector3.up * mMouseX);
    }
}