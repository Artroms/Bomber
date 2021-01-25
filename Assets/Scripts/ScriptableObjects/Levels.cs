using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Levels", menuName = "ScriptableObjects/Levels", order = 2)]
public class Levels: ScriptableObject
{
    static Levels instance = null;
    public static Levels Instance
    {
        get
        {
            if (!instance)
            {
                instance = Resources.Load<Levels>("Settings/Levels");
            }
            return instance;
        }
    }
    public List<string> levels;

    public int LastLevel
    {
        get => PlayerPrefs.GetInt("LastLevel");
        set
        {
            if (value > 0 && value < levels.Count) PlayerPrefs.SetInt("LastLevel", value);
        }
    }

    public string LastLevelName => levels[LastLevel];
}
