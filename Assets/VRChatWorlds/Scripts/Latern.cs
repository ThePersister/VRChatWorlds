using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Latern : MonoBehaviour
{
    [SerializeField]
    private List<MeshRenderer> _laternMeshes;

    [SerializeField]
    private Light _laternLight;

    [SerializeField]
    private float _minColorElementValue = 0.5f;

    private void Start()
    {
        Color randomColor = new Color(GetRandomColorElement(), GetRandomColorElement(), GetRandomColorElement());
        foreach (MeshRenderer meshRenderer in _laternMeshes)
        {
            meshRenderer.material.color = randomColor;
        }

        _laternLight.color = randomColor;
    }

    private float GetRandomColorElement()
    {
        return Mathf.Max(_minColorElementValue, Random.value);
    }
}
