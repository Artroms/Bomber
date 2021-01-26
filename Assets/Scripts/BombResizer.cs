using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombResizer : MonoBehaviour
{
    [SerializeField] private Vector3 neededSize;
    [SerializeField] private BombSettings bombSettings;
    public List<Transform> toResize = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Resizer());
    }

    private void Reset()
    {
        toResize = new List<Transform>();
        foreach (Transform item in transform)
        {
            toResize.Add(item);
        }
    }

    private IEnumerator Resizer()
    {
        float time = 0;
        while (true)
        {
            for (int i = 0; i < toResize.Count; i++)
            {
                toResize[i].localScale = Vector3.Lerp(Vector3.one, neededSize, time / bombSettings.TimeToExplosion);
            }
            yield return null;
            time += Time.deltaTime;
        }
    }
}
