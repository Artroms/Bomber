using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ����� ��� ���������� �������
/// </summary>
public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// ������������� ��� ��������, ��������� �� ��������
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// ������������ ��� �������� ��������� �� ��������
    /// </summary>
    public void UnPause()
    {
        Time.timeScale = 1;
    }

    /// <summary>
    /// ��������� ��������� ������������ �������
    /// </summary>
    public void LoadLastLevel()
    {
        SceneManager.LoadSceneAsync(Levels.Instance.LastLevelName);
    }

    /// <summary>
    /// ���� ������ ����� ���������, �� ����� �� ����� �����������
    /// </summary>
    private void OnDisable()
    {
        UnPause();
    }
}
