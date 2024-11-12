using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuContoroller : MonoBehaviour
{
    public TextMeshProUGUI[] menuItems;  // ���j���[���� (5��TextMeshProUGUI)
    public string[] sceneNames;           // �e���j���[���ڂɑΉ�����V�[����
    private int selectedIndex = 0;        // ���ݑI������Ă���C���f�b�N�X
    private Coroutine blinkCoroutine;     // �_�ŗp�R���[�`��

    private void Start()
    {
        UpdateMenuDisplay();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // Xbox�R���g���[���[�̍��X�e�B�b�N��Y���̓��͂��擾
        float verticalInput = Input.GetAxis("Vertical"); // "Vertical" �̓f�t�H���g�ŏ㉺�ɐݒ肳��Ă���

        if (verticalInput > 0.5f) // ��ɃX���C�v
        {
            selectedIndex--;
            if (selectedIndex < 0) selectedIndex = menuItems.Length - 1;
            UpdateMenuDisplay();
            // �����͂����Z�b�g���ĘA�����͂�h��
            while (verticalInput > 0.5f)
                verticalInput = Input.GetAxis("Vertical");
        }
        else if (verticalInput < -0.5f) // ���ɃX���C�v
        {
            selectedIndex++;
            if (selectedIndex >= menuItems.Length) selectedIndex = 0;
            UpdateMenuDisplay();
            // �����͂����Z�b�g���ĘA�����͂�h��
            while (verticalInput < -0.5f)
                verticalInput = Input.GetAxis("Vertical");
        }

        if (Input.GetButtonDown("Bbutton")) // B�{�^���Ō���
        {
            // �Ή�����V�[���ɑJ��
            SceneManager.LoadScene(sceneNames[selectedIndex]);
        }
    }

    private void UpdateMenuDisplay()
    {
        // �S�Ẵ��j���[���ڂ̓_�ł��~
        for (int i = 0; i < menuItems.Length; i++)
        {
            StopBlinking(menuItems[i]);
        }

        // �I�����ꂽ���j���[���ڂ̂ݓ_�ŊJ�n
        blinkCoroutine = StartCoroutine(BlinkText(menuItems[selectedIndex]));
    }

    private IEnumerator BlinkText(TextMeshProUGUI text)
    {
        while (true)
        {
            text.alpha = 1f;  // ���S�ɕ\��
            yield return new WaitForSeconds(0.5f);
            text.alpha = 0.5f;  // ������
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void StopBlinking(TextMeshProUGUI text)
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);  // �_�ł��~
            blinkCoroutine = null;
        }
        text.alpha = 1f;  // �������S�ɖ߂�
    }
}

