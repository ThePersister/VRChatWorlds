using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lesson : MonoBehaviour {

    private Text[] _textSlots;

    private void Start()
    {
        _textSlots = transform.GetChild(0).GetComponentsInChildren<Text>();
    }

    /// <summary>
    /// 5 words fit in each text slots, every lesson has up to 9 text slots.
    /// This code is to fill them automatically.
    /// </summary>
    /// <param name="title">Title of the lesson.</param>
    /// <param name="words">Words within the lesson.</param>
    public void FillLesson(string title, List<string> words)
    {
        if (_textSlots.Length == 0)
        {
            Debug.LogWarning("TextSlots count should never be 0, don't fill lesson.");
            return;
        }

        int slotIndex = 0;
        Text currentSlot = _textSlots[0];
        currentSlot.text += title;
        for (int i = 0; i < words.Count; i++)
        {
            currentSlot.text += "\n" + words[i];

            if (i != 0 && i % 5 == 0)
            {
                Debug.Log("Test: " + i);
                slotIndex += 1;
                currentSlot = _textSlots[slotIndex];
                currentSlot.text += "\n";
            }
        }
    }
}
