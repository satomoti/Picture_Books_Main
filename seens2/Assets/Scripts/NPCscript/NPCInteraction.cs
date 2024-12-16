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

    private bool isChoosingOption = false;  // �I������I��ł��邩�̃t���O
    private int selectedOptionIndex = -1;  // �I�����ꂽ�I�����̃C���f�b�N�X�i�������j
    private List<string> currentOptions = new List<string>();  // ���ݕ\�����̑I�������X�g

    public bool shouldChangeNPCID = false;  // NPCID��ύX���邩�ǂ��������肷��t���O
    public int newNPCID;  // �V����NPCID�i�ύX����ꍇ�ɐݒ�j

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
        if (isTalking && !isChoosingOption && Input.GetButtonDown("Bbutton")) // �R���g���[����B�{�^�����g�p
        {
            ShowNextDialogue();
        }
        if (Input.GetKeyDown(KeyCode.Z)) // �f�o�b�O�p��Z�L�[���g�p
        {
            ShowNextDialogue();
        }

        // �I�������\������Ă���Ƃ��ɁA�L�[�{�[�h�őI��
        if (isChoosingOption)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) { SelectOption(0); }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) { SelectOption(1); }
            else if (Input.GetKeyDown(KeyCode.Alpha3)) { SelectOption(2); }
            // ���̃L�[���ǉ��\
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

            // ���̉�b���I�������܂ޏꍇ
            if (currentDialogue.options.Count > 0)
            {
                ShowOptions(currentDialogue);  // �I������\��
            }
            else
            {
                currentDialogueIndex++;
            }
        }
        else
        {
            // ��b���I�������ꍇ
            EndDialogue();
        }
    }

    // �I������\��
    private void ShowOptions(DialogueLoader.Dialogue currentDialogue)
    {
        isChoosingOption = true;
        currentOptions = currentDialogue.options;
        // �Ⴆ�΁A�I����������ꍇ�̓e�L�X�g�ŕ\������iUI��ɕ\�����邱�Ƃ��\�j
        npcSpeechText.text = "�I������I��ł�������:";  // ���b�Z�[�W��ύX
    }

    // �I������I��������
    private void SelectOption(int optionIndex)
    {
        if (optionIndex < currentOptions.Count)
        {
            selectedOptionIndex = optionIndex;  // �I�����ꂽ�C���f�b�N�X��ۑ�
            var selectedOption = currentOptions[selectedOptionIndex];
            Debug.Log($"�I�����ꂽ�I����: {selectedOption}");

            // �I�����Ɋ�Â��Ď��̉�b��\��
            var currentDialogue = dialogues[currentDialogueIndex];
            currentDialogueIndex = currentDialogue.nextDialogueIndices[selectedOptionIndex];  // �I�����ɑΉ����鎟�̉�b�C���f�b�N�X��ݒ�
            ShowNextDialogue();  // ���̉�b��\��

            isChoosingOption = false;  // �I�����̕\�����I��
        }
    }

    // ��b�I�����̏���
    private void EndDialogue()
    {
        isTalking = false;
        npcSpeechBubble.SetActive(false);
        playerSpeechBubble.SetActive(false);
        playerController.canMove = true;  // �v���C���[�̑��������

        // NPCID��ύX����ꍇ�A�ݒ肳�ꂽ�V����npcID�𔽉f
        if (shouldChangeNPCID)
        {
            npcID = newNPCID;
            dialogues = dialogueLoader.GetDialoguesForNPC(npcID);  // �V����NPC�̉�b�f�[�^�����[�h
            Debug.Log($"NPC ID���ύX����܂���: �V����NPC ID = {npcID}");
        }
    }
}
