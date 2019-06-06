using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour {

    private Lesson[] _lessons;

    public Lesson[] Lessons
    {
        get
        {
            if (_lessons == null)
            {
                _lessons = transform.GetComponentsInChildren<Lesson>();
            }

            return _lessons;
        }
    }

    public void SetLesson(LessonModel lesson, int index)
    {
        Lessons[index].FillLesson(lesson.title, lesson.words);
    }

    public void FinishPage()
    {
        for (int i = 0; i < Lessons.Length; i++)
        {
            Lessons[i].transform.GetChild(0).gameObject.SetActive(i == 0);
        }
    }
}
