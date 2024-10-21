using UnityEngine;

public class PlayerXboxController : MonoBehaviour
{
    public float moveSpeed = 5f;          // �v���C���[�̈ړ����x
    public float rotationSpeed = 100f;    // �v���C���[�̉�]���x
    public float jumpSpeed = 5f;          // �W�����v�̑��x
    private CharacterController controller; // CharacterController �R���|�[�l���g�̎Q��
    private float gravity = 9.81f;        // �d�͂̉����x
    private Vector3 velocity;             // �v���C���[�̐��������̑��x

    void Start()
    {
        // CharacterController �R���|�[�l���g���擾
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // �L�[�{�[�h��Xbox�R���g���[���[�̓��͂��擾
        float horizontalInput = Input.GetAxis("Horizontal");  // ���X�e�B�b�N (x ��) ����� A, D �L�[
        float verticalInput = Input.GetAxis("Vertical");      // ���X�e�B�b�N (y ��) ����� W, S �L�[

        // �E�X�e�B�b�N�̈ړ��ʂ��擾���ăv���C���[����]������
        float rightStickX = Input.GetAxis("RightStickHorizontal");  // Xbox�R���g���[���[�E�X�e�B�b�N�̉��ړ�
        transform.Rotate(Vector3.up, rightStickX * rotationSpeed * Time.deltaTime);
        float mouseX = Input.GetAxis("Mouse X"); //�}�E�X�|�C���^�ł̉�]
        transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.fixedDeltaTime);

        // �ړ������̌v�Z
        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        // �W�����v�{�^���̎擾 (A �{�^���܂��̓X�y�[�X�L�[)
        if (controller.isGrounded)
        {
            velocity.y = -0.5f;  // ���n���Ă���Ƃ��́A�y���n�ʂɗ}����

            if (Input.GetButtonDown("Jump"))  // �f�t�H���g�ł́uJump�v�̓X�y�[�X�L�[�ɐݒ肳��Ă��܂�
            {
                velocity.y = jumpSpeed;  // �W�����v
            }
        }
        else
        {
            // �󒆂ɂ���Ƃ��ɏd�͂�K�p
            velocity.y -= gravity * Time.deltaTime;
        }

        // �ړ�
        controller.Move((moveDirection * moveSpeed + velocity) * Time.deltaTime);
    }
}
