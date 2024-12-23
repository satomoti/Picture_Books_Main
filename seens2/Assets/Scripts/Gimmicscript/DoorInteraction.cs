using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public GameObject player; // �v���C���[�̃I�u�W�F�N�g
    public GameObject interactionUI; // �߂Â����Ƃ��ɕ\�������UI (�Ⴆ�΃{�^���A�C�R��)
    public float interactionDistance = 3.0f; // �h�A�ɋ߂Â�����
    public bool canOpenDoor = false; // �h�A���J����t���O
    public float rotationSpeed = 2.0f; // �h�A�̉�]���x
    public Vector3 openRotation = new Vector3(0, 90, 0); // �h�A���J�����Ƃ��̉�]�p�x

    private bool isNearDoor = false; // �v���C���[���߂��ɂ��邩�ǂ���
    private Quaternion initialRotation; // �h�A�̏�����]
    private Quaternion targetRotation; // �h�A���J�����Ƃ��̖ڕW��]
    private bool isDoorOpen = false; // �h�A���J���Ă��邩�ǂ���

    void Start()
    {
        interactionUI.SetActive(false); // ������ԂŔ�\��
        initialRotation = transform.rotation; // �h�A�̏�����]��ۑ�
        targetRotation = Quaternion.Euler(initialRotation.eulerAngles + openRotation); // �h�A���J�����Ƃ��̉�]���v�Z
    }

    void Update()
    {
        // �v���C���[�ƃh�A�̋������v�Z
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // �v���C���[���߂Â�����UI��\��
        if (distance <= interactionDistance)
        {
            interactionUI.SetActive(true);
            isNearDoor = true;
        }
        else
        {
            interactionUI.SetActive(false);
            isNearDoor = false;
        }

        // �L�[���͂Ńh�A���J���鏈��
        if (isNearDoor && canOpenDoor && Input.GetKeyDown(KeyCode.E)||isNearDoor && canOpenDoor && Input.GetButtonDown("Bbutton"))
        {
            if (!isDoorOpen)
            {
                isDoorOpen = true; // �h�A���J��
                Debug.Log("�h�A���J���܂��I");
            }
        }

        // �h�A���J����]����
        if (isDoorOpen)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
