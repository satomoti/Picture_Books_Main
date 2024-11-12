using System.Collections.Generic;
using UnityEngine;


public class DialogueLoader : MonoBehaviour
{
    // NPC���Ƃ̉�b���e��ێ����鎫��
    public Dictionary<int, List<string>> npcDialogues = new Dictionary<int, List<string>>();

    // CSV�t�@�C���̖��O�iResources�t�H���_���ɔz�u����j
    public string csvFileName = "NPC_Dialogue";  // �t�@�C�����̂݁A�g���q�͕s�v

    void Start()
    {
        LoadDialogueFromCSV();
    }

    // CSV�t�@�C����ǂݍ��ރ��\�b�h
    void LoadDialogueFromCSV()
    {
        try
        {
            // Resources�t�H���_����CSV�t�@�C����ǂݍ���
            TextAsset csvFile = Resources.Load<TextAsset>(csvFileName);
            if (csvFile == null)
            {
                Debug.LogError("CSV�t�@�C����������܂���: " + csvFileName);
                return;
            }

            // CSV�t�@�C���̓��e�𕶎���Ƃ��Ď擾���A�e�s�𕪊�
            string[] lines = csvFile.text.Split('\n');
            bool isFirstLine = true;

            foreach (string line in lines)
            {
                // �w�b�_�[�s���X�L�b�v
                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue;
                }

                // �J���}�ŕ�������NPC_ID�Ɖ�b���e���擾
                string[] values = line.Split(',');
                if (values.Length < 2) continue;  // �f�[�^���s���S�ȏꍇ���X�L�b�v

                int npcID = int.Parse(values[0]);
                string dialogue = values[1].Trim();

                // NPC��ID�ɑΉ����郊�X�g���܂����݂��Ȃ��ꍇ�A���X�g���쐬
                if (!npcDialogues.ContainsKey(npcID))
                {
                    npcDialogues[npcID] = new List<string>();
                }

                // NPC�̉�b���X�g�ɒǉ�
                npcDialogues[npcID].Add(dialogue);

                // �f�o�b�O�p��NPC ID�Ɖ�b���e���o��
                Debug.Log("CSV�f�[�^�ǂݍ���: NPC ID = " + npcID + ", ��b = " + dialogue);
            }

            // �ŏI�I�ȃf�[�^�̏�Ԃ��m�F
            Debug.Log("�S�Ă�NPC�̉�b�f�[�^: " + npcDialogues.Count + " entries loaded.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("CSV�t�@�C���̓ǂݍ��ݒ��ɃG���[���������܂���: " + e.Message);
        }
    }

    // �����NPC�̉�b���X�g���擾
    public List<string> GetDialoguesForNPC(int npcID)
    {
        if (npcDialogues.ContainsKey(npcID))
        {
            return npcDialogues[npcID];
        }
        // NPC ID �����݂��Ȃ��ꍇ�͋�̃��X�g��Ԃ�
        return new List<string>();
    }
}
