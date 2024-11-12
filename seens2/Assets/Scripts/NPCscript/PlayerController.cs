using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    private CharacterController controller;
    private float gravity = 9.81f;
    public GameObject itembar;

    // �v���C���[�̈ړ��⎋�_�ړ��𐧌䂷��t���O
    public bool canMove = true;

    // �A�C�e���o�[�̕\����Ԃ��Ǘ�����t���O
    private bool isItemBarVisible = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        // ������ԂŃA�C�e���o�[���\���ɐݒ�
        itembar.SetActive(false);
    }

    void Update()
    {
        // canMove��true�̂Ƃ��̂ݑ��������
        if (canMove)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // LB��RB�{�^���Ŏ��_����]
            float rotationInput = 0f;
            if (Input.GetButton("RBbutton"))
            {
                rotationInput = 1f; // RB�{�^����������Ă���Ƃ��͉E��]
            }
            else if (Input.GetButton("LBbutton"))
            {
                rotationInput = -1f; // LB�{�^����������Ă���Ƃ��͍���]
            }

            // �v���C���[����]������
            transform.Rotate(Vector3.up, rotationInput * rotationSpeed * Time.fixedDeltaTime);

            // �ړ������̌v�Z
            Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

            if (!controller.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            controller.Move(moveDirection * moveSpeed * Time.deltaTime);

            // X�{�^���ŃA�C�e���o�[�̕\��/��\����؂�ւ�
            if (Input.GetButtonDown("Xbutton")) // X�{�^���������ꂽ�Ƃ�
            {
                isItemBarVisible = !isItemBarVisible; // ��Ԃ�؂�ւ�
                itembar.SetActive(isItemBarVisible); // �A�C�e���o�[�̕\����Ԃ�ݒ�
            }
        }
    }
}

