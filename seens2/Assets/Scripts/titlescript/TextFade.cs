using System.Collections;
using UnityEngine;
using TMPro;

public class TextFade : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float pulseDuration = 1.5f;  // �p���X�̎�������

    private void Start()
    {
        // TextMeshPro�̃R���|�[�l���g���A�^�b�`����Ă��Ȃ��ꍇ�A�����Ŏ擾����
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        // �R���[�`���Ńt�F�[�h�C���E�t�F�[�h�A�E�g���J�n
        StartCoroutine(FadeText());
    }

    private IEnumerator FadeText()
    {
        while (true)
        {
            // ���S�\������50%�����Ƀt�F�[�h�A�E�g
            yield return StartCoroutine(LerpTextAlpha(1f, 0.3f, pulseDuration / 2));

            // 50%�������犮�S�\���Ƀt�F�[�h�C��
            yield return StartCoroutine(LerpTextAlpha(0.3f, 1f, pulseDuration / 2));
        }
    }

    private IEnumerator LerpTextAlpha(float fromAlpha, float toAlpha, float duration)
    {
        float time = 0f;
        Color currentColor = textMeshPro.color;

        while (time < duration)
        {
            float alpha = Mathf.Lerp(fromAlpha, toAlpha, time / duration);
            textMeshPro.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            time += Time.deltaTime;
            yield return null;  // 1�t���[���ҋ@
        }

        // �ŏI�I�ɖړI�̃A���t�@�l�ɐݒ肷��
        textMeshPro.color = new Color(currentColor.r, currentColor.g, currentColor.b, toAlpha);
    }
}
