using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody playerRigidbody; // 이동에 사용할 리지드바디 컴포넌트
    public float speed = 8f; // 이동 속력
    public float jumpForce = 10.0f;
    private bool isGrounded = true;
    public float GravityScale = 1f;


    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;
    public Transform groundCheck;

    private bool isReversed = false;
    void Start() {
        // 게임 오브젝트에서 Rigidbody 컴포넌트를 찾아 playerRigidbody에 할당
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update() {
        float xSpeed = Input.GetAxis("Horizontal") * speed;
        float zSpeed = Input.GetAxis("Vertical") * speed;

        if(isReversed) {
            xSpeed = -xSpeed;
            zSpeed = - zSpeed;
        }

        Vector3 moveDirection = new Vector3(xSpeed, 0, zSpeed);
        playerRigidbody.velocity = new Vector3(moveDirection.x, playerRigidbody.velocity.y, moveDirection.z);

        if(moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

    }

    void FixedUpdate()
    {
        Vector3 customGravity = Physics.gravity * GravityScale;
        playerRigidbody.AddForce(customGravity, ForceMode.Acceleration);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void Die() {
        // 자신의 게임 오브젝트를 비활성화
        gameObject.SetActive(false);

        // 씬에 존재하는 GameManager 타입의 오브젝트를 찾아서 가져오기
        GameManager gameManager = FindObjectOfType<GameManager>();
        // 가져온 GameManager 오브젝트의 EndGame() 메서드 실행
        gameManager.EndGame();
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Hipnogumelo")) {

            StartCoroutine(ReverseMovementRoutine());
            Destroy(other.gameObject);
        }
    }

    IEnumerator ReverseMovementRoutine() {
        isReversed = true;
        yield return new WaitForSeconds(3f);
        isReversed = false;
    }
}