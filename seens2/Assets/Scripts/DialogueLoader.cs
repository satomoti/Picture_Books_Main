using System.Collections.Generic;
using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
    // �����҂���ʂ��邽�߂̗񋓌^
    public enum SpeakerType { NPC, Player }

    // ��b���e��ێ�����N���X
    [System.Serializable]
    public class Dialogue
    {
        public SpeakerType speaker;  // �����ҁiNPC�܂���Player�j
        public string text;          // ��b�e�L�X�g

        public Dialogue(SpeakerType speaker, string text)
        {
            this.speaker = speaker;
            this.text = text;
        }
    }

    // NPC���Ƃ̉�b���e��ێ����鎫��
    public Dictionary<int, List<Dialogue>> npcDialogues = new Dictionary<int, List<Dialogue>>();

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

                // �J���}�ŕ�������NPC_ID�A�����ҁA��b���e���擾
                string[] values = line.Split(',');
                if (values.Length < 3) continue;  // �f�[�^���s���S�ȏꍇ���X�L�b�v

                int npcID = int.Parse(values[0]);
                SpeakerType speaker = (SpeakerType)System.Enum.Parse(typeof(SpeakerType), values[1].Trim());
                string dialogueText = values[2].Trim();

                // NPC��ID�ɑΉ����郊�X�g���܂����݂��Ȃ��ꍇ�A���X�g���쐬
                if (!npcDialogues.ContainsKey(npcID))
                {
                    npcDialogues[npcID] = new List<Dialogue>();
                }

                // ��b�f�[�^���쐬���ă��X�g�ɒǉ�
                Dialogue dialogue = new Dialogue(speaker, dialogueText);
                npcDialogues[npcID].Add(dialogue);

                // �f�o�b�O�p�ɓǂݍ��ݓ��e���o��
                Debug.Log($"CSV�f�[�^�ǂݍ���: NPC ID = {npcID}, ������ = {speaker}, ��b = {dialogueText}");
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
    public List<Dialogue> GetDialoguesForNPC(int npcID)
    {
        if (npcDialogues.ContainsKey(npcID))
        {
            return npcDialogues[npcID];
        }
        // NPC ID �����݂��Ȃ��ꍇ�͋�̃��X�g��Ԃ�
        return new List<Dialogue>();
    }
}

