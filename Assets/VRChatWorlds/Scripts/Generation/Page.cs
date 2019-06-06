using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour {

    private Lesson[] _lessons;

    private void Start()
    {
        _lessons = transform.GetComponentsInChildren<Lesson>();
    }
}
