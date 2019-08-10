using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lesson : MonoBehaviour {

    [SerializeField]
    private Text _lessonTitle;

    [SerializeField]
    private GameObject _lessonWordsCanvas;

    private Text[] _textSlots;
    private bool _filled;

    public Text[] TextSlots
    {
        get
        {
            if (_textSlots == null)
            {
                _textSlots = _lessonWordsCanvas.GetComponentsInChildren<Text>();
            }

            return _textSlots;
        }
    }

    public GameObject WordsCanvas
    {
        get
        {
            return _lessonWordsCanvas;
        }
    }

    public bool Filled
    {
        get
        {
            return _filled;
        }
    }

    /// <summary>
    /// 5 words fit in each text slots, every lesson has up to 9 text slots.
    /// This code is to fill them automatically.
    /// </summary>
    /// <param name="title">Title of the lesson.</param>
    /// <param name="words">Words within the lesson.</param>
    public void FillLesson(LessonModel englishLesson, LessonModel koreanLesson)
    {
        if (TextSlots.Length == 0)
        {
            Debug.LogWarning("TextSlots count should never be 0, don't fill lesson.");
            return;
        }

        string title = englishLesson.title;
        string titlePlusKorean = title + "<size=108>(" + koreanLesson.title + ")</size>";
        this.name = title;

        _lessonTitle.text = titlePlusKorean;
        _filled = true;

        int slotIndex = 0;
        Text currentSlot = TextSlots[0];
        currentSlot.text += titlePlusKorean;
        for (int i = 0; i < englishLesson.words.Length; i++)
        {
            var word = englishLesson.words[i] + "<size=124>(" + koreanLesson.words[i] + ")</size>";
            currentSlot.text += "\n" + word;

            if (i != 0 && (i + 1) % 5 == 0 && slotIndex < TextSlots.Length - 1)
            {
                slotIndex += 1;
                currentSlot = TextSlots[slotIndex];
            }
        }
    }
}
