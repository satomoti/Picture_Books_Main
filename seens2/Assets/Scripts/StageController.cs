using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public float moveSpeed = 5f;          // �v���C���[�̈ړ����x
    public float rotationSpeed = 100f;    // �v���C���[�̉�]���x
    public float jumpSpeed = 5f;          // �W�����v�̑��x
    private CharacterController controller; // CharacterController �R���|�[�l���g�̎Q��
    private Vector3 velocity;             // �v���C���[�̐��������̑��x
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        float rightStickX = Input.GetAxis("RightStickHorizontal");  // Xbox�R���g���[���[�E�X�e�B�b�N�̉��ړ�
        transform.Rotate(Vector3.up, rightStickX * rotationSpeed * Time.deltaTime);
        float mouseX = Input.GetAxis("Mouse X"); //�}�E�X�|�C���^�ł̉�]
        transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.fixedDeltaTime);
    }
}
