using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenuUI;  // �|�[�Y��ʂ�UI
    private bool isPaused = false;  // �Q�[�����|�[�Y�����ǂ����̃t���O

    void Update()
    {
        // Xbox�R���g���[���[��Start�{�^���������ꂽ�Ƃ�
        if (Input.GetButtonDown("Start"))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                Pause();
            }
        }
    }

    // �Q�[�����ĊJ���鏈��
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);     // �|�[�Y���j���[���\���ɂ���
        Time.timeScale = 1f;              // �Q�[���̎��Ԃ�ʏ�ɖ߂�
        isPaused = false;                 // �|�[�Y��Ԃ�����
    }

    // �Q�[�����|�[�Y���鏈��
    void Pause()
    {
        pauseMenuUI.SetActive(true);      // �|�[�Y���j���[��\������
        Time.timeScale = 0f;              // �Q�[���̎��Ԃ��~����
        isPaused = true;                  // �|�[�Y��Ԃɂ���
    }
}
