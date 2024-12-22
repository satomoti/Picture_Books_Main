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

        public Dialogue(SpeakerType speaker, string text)
        {
            this.speaker = speaker;
            this.text = text;
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

                Dialogue dialogue = new Dialogue(speaker, dialogueText);

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
