using UnityEngine;

public class FreeMoveController : MonoBehaviour
{
    public float moveSpeed = 6f;          // 移动速度
    public float mouseSensitivity = 200f; // 视角灵敏度

    private CharacterController controller;
    private Transform cam;

    float cameraPitch = 0f;  // 上下视角角度，不限制

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>().transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        LookAround();
        Move();
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        cameraPitch -= mouseY;  // 不做 Clamp → 上下360都可以
        cam.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX); // 左右转 Player
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // 方向
        Vector3 move = transform.right * x + transform.forward * z;

        // CharacterController 会自动进行"不穿墙"检测
        // 也可以顺着楼梯自然走上去
        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}
