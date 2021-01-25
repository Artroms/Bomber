using UnityEngine;

/// <summary>
/// ������ ���������� ������ Vector3
/// </summary>
public static class Vector3Extensions
{
    /// <summary>
    /// ���������� ���������� � ������ ������ ������ 
    /// </summary>
    /// <param name="vec"></param>
    /// <returns></returns>
    public static Vector3 GridRound(this Vector3 vec)
    {
        return new Vector3(Mathf.Floor(vec.x) + 0.5f, Mathf.Floor(vec.y) + 0.5f, Mathf.Floor(vec.z) + 0.5f);
    }

    /// <summary>
    /// ���������� ���������� ������
    /// </summary>
    /// <param name="vec"></param>
    /// <returns></returns>
    public static Vector3 Round(this Vector3 vec)
    {
        return new Vector3(Mathf.Round(vec.x), Mathf.Round(vec.y), Mathf.Round(vec.z));
    }
}
