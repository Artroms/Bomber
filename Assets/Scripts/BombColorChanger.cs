using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BombColorChanger : MonoBehaviour
{
    [SerializeField] private Color neededColor;
    [SerializeField] private BombSettings bombSettings;
    public List<MeshRenderer> toColor = new List<MeshRenderer>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Resizer());
    }

    private void Reset()
    {
        toColor = GetComponentsInChildren<MeshRenderer>().ToList();
    }

    private IEnumerator Resizer()
    {
        float time = 0;
        var mat = toColor[0].material;
        var col = mat.color;
        for (int i = 0; i < toColor.Count; i++)
        {
            toColor[i].sharedMaterial = mat;
        }
        while (true)
        {
            mat.color = Color.Lerp(col, neededColor, time / bombSettings.TimeToExplosion);
            yield return null;
            time += Time.deltaTime;
        }
    }
}
