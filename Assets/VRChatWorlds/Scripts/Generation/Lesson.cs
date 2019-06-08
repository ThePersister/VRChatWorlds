using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lesson : MonoBehaviour {

    [SerializeField]
    private Text _lessonTitle;

    [SerializeField]
    private GameObject _lessonWords;

    private Text[] _textSlots;

    public Text[] TextSlots
    {
        get
        {
            if (_textSlots == null)
            {
                _textSlots = _lessonWords.GetComponentsInChildren<Text>();
            }

            return _textSlots;
        }
    }

    public GameObject Words
    {
        get
        {
            return _lessonWords;
        }
    }

    /// <summary>
    /// 5 words fit in each text slots, every lesson has up to 9 text slots.
    /// This code is to fill them automatically.
    /// </summary>
    /// <param name="title">Title of the lesson.</param>
    /// <param name="words">Words within the lesson.</param>
    public void FillLesson(string title, string[] words)
    {
        if (TextSlots.Length == 0)
        {
            Debug.LogWarning("TextSlots count should never be 0, don't fill lesson.");
            return;
        }

        _lessonTitle.text = title;
        this.name = title;

        int slotIndex = 0;
        Text currentSlot = TextSlots[0];
        currentSlot.text += title;
        for (int i = 0; i < words.Length; i++)
        {
            currentSlot.text += "\n" + words[i];

            if (i != 0 && (i + 1) % 5 == 0 && slotIndex < TextSlots.Length - 1)
            {
                slotIndex += 1;
                currentSlot = TextSlots[slotIndex];
            }
        }
    }
}
