using System.Collections.Generic;
using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
    public enum SpeakerType { NPC, Player }

    [System.Serializable]
    public class Dialogue
    {
        public SpeakerType speaker;  // �����ҁiNPC�܂���Player�j
        public string text;          // ��b�e�L�X�g
        public List<string> options; // �I�����i�I�������Ȃ��ꍇ��null�܂��͋󃊃X�g�j
        public List<int> nextDialogueIndices; // �e�I�����ɑΉ����鎟�̉�b�C���f�b�N�X

        public Dialogue(SpeakerType speaker, string text, List<string> options = null, List<int> nextDialogueIndices = null)
        {
            this.speaker = speaker;
            this.text = text;
            this.options = options ?? new List<string>();  // �I�������Ȃ���΋󃊃X�g
            this.nextDialogueIndices = nextDialogueIndices ?? new List<int>();  // ���̉�b���Ȃ��ꍇ�͋󃊃X�g
        }
    }

    public Dictionary<int, List<Dialogue>> npcDialogues = new Dictionary<int, List<Dialogue>>();
    public string csvFileName = "NPC_Dialogue";  // CSV�t�@�C�����iResources�t�H���_���j

    void Start()
    {
        LoadDialogueFromCSV();
    }

    void LoadDialogueFromCSV()
    {
        try
        {
            TextAsset csvFile = Resources.Load<TextAsset>(csvFileName);
            if (csvFile == null)
            {
                Debug.LogError("CSV�t�@�C����������܂���: " + csvFileName);
                return;
            }

            string[] lines = csvFile.text.Split('\n');
            bool isFirstLine = true;

            foreach (string line in lines)
            {
                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue;
                }

                string[] values = line.Split(',');
                if (values.Length < 3) continue;

                int npcID = int.Parse(values[0]);
                SpeakerType speaker = (SpeakerType)System.Enum.Parse(typeof(SpeakerType), values[1].Trim());
                string dialogueText = values[2].Trim();

                List<string> options = new List<string>();
                List<int> nextDialogueIndices = new List<int>();

                if (values.Length > 3)
                {
                    for (int i = 3; i < values.Length - 1; i += 2)
                    {
                        options.Add(values[i].Trim());
                        nextDialogueIndices.Add(int.Parse(values[i + 1].Trim()));
                    }
                }

                Dialogue dialogue = new Dialogue(speaker, dialogueText, options, nextDialogueIndices);

                if (!npcDialogues.ContainsKey(npcID))
                {
                    npcDialogues[npcID] = new List<Dialogue>();
                }

                npcDialogues[npcID].Add(dialogue);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("CSV�t�@�C���̓ǂݍ��ݒ��ɃG���[���������܂���: " + e.Message);
        }
    }

    public List<Dialogue> GetDialoguesForNPC(int npcID)
    {
        if (npcDialogues.ContainsKey(npcID))
        {
            return npcDialogues[npcID];
        }
        return new List<Dialogue>();
    }
}
