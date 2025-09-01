using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerCtl : MonoBehaviour
{
    public InputActionReference moveAction; // Player/Move
    public InputActionReference jumpAction; // Player/Jump
    //public InputActionReference downAction; // Player/Down
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
        //downAction.action.Enable();
    }

    void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
        //downAction.action.Disable();
    }

    void Update()
    {
        // 땅 체크
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // 이동 입력 (Vector2)
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);

        // 월드 좌표 → 로컬 좌표 변환 (카메라 방향 기준 이동)
        move = transform.TransformDirection(move);
        move.y = 0;

        controller.Move(move * moveSpeed * Time.deltaTime);

        // if (downAction.action.triggered)
        // {
        //     velocity.y = -Mathf.Sqrt(jumpForce * -2f * gravity) * 2f;
        //     Debug.Log("down");
        // }

        // 점프 입력
        if (jumpAction.action.triggered && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // 중력 적용
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}