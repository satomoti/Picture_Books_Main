using System.Collections;
using UnityEngine;
using TMPro;

public class TitleScreenManager : MonoBehaviour
{
    public CanvasGroup background;        // �w�i��CanvasGroup
    public CanvasGroup titleLogo;         // �^�C�g�����S��CanvasGroup
    public CanvasGroup teamName;          // ����`�[������CanvasGroup
    public TextMeshProUGUI startText;     // �uB�{�^���ł͂��߂�vTextMeshPro�I�u�W�F�N�g

    private float fadeDuration = 0.5f;    // �t�F�[�h�C��/�A�E�g�̎���
    private float blinkDuration = 1.5f;   // �_�ł̎�������

    private void Start()
    {
        // �����ݒ�
        background.alpha = 0;
        titleLogo.alpha = 0;
        teamName.alpha = 0;
        startText.alpha = 0;

        // ���o���J�n
        StartCoroutine(ShowTitleSequence());
    }

    private IEnumerator ShowTitleSequence()
    {
        // �w�i���t�F�[�h�C��
        yield return StartCoroutine(FadeIn(background, fadeDuration));

        // �^�C�g�����S���t�F�[�h�C��
        yield return StartCoroutine(FadeIn(titleLogo, fadeDuration));

        // ����`�[�������t�F�[�h�C��
        yield return StartCoroutine(FadeIn(teamName, fadeDuration));

        // �uB�{�^���ł͂��߂�v��_��
        StartCoroutine(BlinkText());
    }

    private IEnumerator FadeIn(CanvasGroup canvasGroup, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    private IEnumerator BlinkText()
    {
        while (true)
        {
            // �e�L�X�g�����X�Ƀt�F�[�h�C��
            float time = 0;
            while (time < blinkDuration / 2)
            {
                startText.alpha = Mathf.Lerp(0.5f, 1f, time / (blinkDuration / 2));
                time += Time.deltaTime;
                yield return null;
            }

            // �e�L�X�g�����X�Ƀt�F�[�h�A�E�g
            time = 0;
            while (time < blinkDuration / 2)
            {
                startText.alpha = Mathf.Lerp(1f, 0.5f, time / (blinkDuration / 2));
                time += Time.deltaTime;
                yield return null;
            }
        }
    }
}
