using System.Collections;
using UnityEngine;
using TMPro;

public class TextPulse : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public Color normalColor = Color.white;     // �ʏ�\���̐F�i�f�t�H���g�͔��j
    public Color dimmedColor = Color.gray;      // 50%�g�[���_�E���̐F�i�f�t�H���g�̓O���[�j
    public float pulseDuration = 1.5f;          // �p���X�̎�������

    private void Start()
    {
        // TextMeshPro�̃R���|�[�l���g���A�^�b�`����Ă��Ȃ��ꍇ�A�����Ŏ擾����
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        // �R���[�`���ŐF�̃g�[���_�E�����J�n����
        StartCoroutine(PulseText());
    }

    private IEnumerator PulseText()
    {
        while (true)
        {
            // �ʏ�F����g�[���_�E���F�܂ŐF��ω�������
            yield return StartCoroutine(LerpTextColor(normalColor, dimmedColor, pulseDuration / 2));

            // �g�[���_�E���F����ʏ�F�܂ŐF��߂�
            yield return StartCoroutine(LerpTextColor(dimmedColor, normalColor, pulseDuration / 2));
        }
    }

    private IEnumerator LerpTextColor(Color fromColor, Color toColor, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            // �F����`��Ԃŏ��X�ɕω�������
            textMeshPro.color = Color.Lerp(fromColor, toColor, time / duration);
            time += Time.deltaTime;
            yield return null; // 1�t���[���ҋ@
        }

        // �ŏI�I�ɖړI�̐F�ɐݒ肷��
        textMeshPro.color = toColor;
    }
}
