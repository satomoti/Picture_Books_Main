using UnityEngine;
using TMPro;  // TextMeshPro���g�p���邽�߂̖��O��Ԃ�ǉ�
using System.Collections.Generic;

public class NPCInteraction : MonoBehaviour
{
    public GameObject player;  // �v���C���[�I�u�W�F�N�g
    public GameObject talkButton;  // �b���{�^���iUI�j
    public GameObject npcSpeechBubble;  // NPC�̐����o��UI
    public TextMeshProUGUI npcSpeechText;  // NPC�����o�����̃e�L�X�g
    public GameObject playerSpeechBubble;  // �v���C���[�̐����o��UI
    public TextMeshProUGUI playerSpeechText;  // �v���C���[�����o�����̃e�L�X�g
    public float interactionDistance = 2.0f;  // �v���C���[��NPC�̋�������
    public DialogueLoader dialogueLoader;  // CSV�����b��ǂݍ��ރX�N���v�g
    public int npcID;  // ����NPC�̎���ID
    public Transform npcHead;  // NPC�̓��̈ʒu�iTransform�Q�Ɓj

    private List<DialogueLoader.Dialogue> dialogues;  // NPC�̉�b���X�g
    private int currentDialogueIndex = 0;
    private Camera mainCamera;
    private PlayerController playerController;  // �v���C���[�R���g���[���[�̎Q��
    private bool isTalking = false;  // ��b�����ǂ����������t���O

    void Start()
    {
        mainCamera = Camera.main;  // ���C���J�������擾
        playerController = player.GetComponent<PlayerController>();  // PlayerController�̎Q�Ƃ��擾
        // NPC�̉�b���X�g���擾
        dialogues = dialogueLoader.GetDialoguesForNPC(npcID);
    }

    void Update()
    {
        // �v���C���[�Ƃ̋����𑪒�
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // NPC�ɋ߂Â�����u�b���v�{�^����\��
        if (distance <= interactionDistance && !isTalking)
        {
            talkButton.SetActive(true);

            // B�{�^���������ꂽ���b���J�n
            if (Input.GetButtonDown("Bbutton")) // �R���g���[����B�{�^�����g�p
            {
                OnTalkButtonPressed();
            }
            if (Input.GetKeyDown(KeyCode.Z)) // �f�o�b�O�p��Z�L�[���g�p
            {
                OnTalkButtonPressed();
            }
        }
        else
        {
            talkButton.SetActive(false);
            if (!isTalking)
            {
                npcSpeechBubble.SetActive(false);  // ���ꂽ��NPC�̐����o�����\��
                playerSpeechBubble.SetActive(false);  // �v���C���[�̐����o������\��
                playerController.canMove = true;  // ��b���I�������v���C���[�̑��������
            }
        }

        // �����o���̈ʒu��NPC�̓���ɐݒ�
        if (npcSpeechBubble.activeSelf)
        {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(npcHead.position);  // NPC�̓��̈ʒu���X�N���[�����W�ɕϊ�
            npcSpeechBubble.transform.position = screenPosition;  // �����o����NPC�̓���ɔz�u
        }

        // ��b����B�{�^���������ꂽ�玟�̉�b�e�L�X�g��\��
        if (isTalking && Input.GetButtonDown("Bbutton")) // �R���g���[����B�{�^�����g�p
        {
            ShowNextDialogue();
        }
        if (Input.GetKeyDown(KeyCode.Z)) // �f�o�b�O�p��Z�L�[���g�p
        {
            ShowNextDialogue();
        }
    }

    // �u�b���v�{�^���������ꂽ�Ƃ��̏���
    public void OnTalkButtonPressed()
    {
        // ��b���X�g���󂩂ǂ������`�F�b�N
        if (dialogues.Count == 0)
        {
            // �ēx��b�f�[�^���擾
            dialogues = dialogueLoader.GetDialoguesForNPC(npcID);
        }

        // ��b���X�g����łȂ��ꍇ�ɂ̂݉�b���J�n
        if (dialogues.Count > 0)
        {
            isTalking = true;
            currentDialogueIndex = 0;
            ShowNextDialogue();

            // �v���C���[�̑���𖳌���
            playerController.canMove = false;
        }
        else
        {
            // NPC�Ƃ̉�b���Ȃ��ꍇ�̏����i�K�v�ɉ����āj
            Debug.Log("����NPC�ɂ͉�b������܂���B");
        }
    }

    // ���̉�b�e�L�X�g��\�����郁�\�b�h
    private void ShowNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Count)
        {
            var currentDialogue = dialogues[currentDialogueIndex];
            if (currentDialogue.speaker == DialogueLoader.SpeakerType.NPC)
            {
                npcSpeechText.text = currentDialogue.text;
                npcSpeechBubble.SetActive(true);
                playerSpeechBubble.SetActive(false);
            }
            else if (currentDialogue.speaker == DialogueLoader.SpeakerType.Player)
            {
                playerSpeechText.text = currentDialogue.text;
                playerSpeechBubble.SetActive(true);
                npcSpeechBubble.SetActive(false);
            }

            currentDialogueIndex++;
        }
        else
        {
            // ��b���I�������ꍇ
            EndDialogue();
        }
    }

    // ��b�I�����̏���
    private void EndDialogue()
    {
        isTalking = false;
        npcSpeechBubble.SetActive(false);
        playerSpeechBubble.SetActive(false);
        playerController.canMove = true;  // �v���C���[�̑��������
    }
}
