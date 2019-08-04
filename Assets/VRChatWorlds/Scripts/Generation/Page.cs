using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRCSDK2;

public class Page : MonoBehaviour {

    [SerializeField]
    private Text _pageSelectText;

    [SerializeField]
    private VRC_Trigger _previousButton;

    [SerializeField]
    private VRC_Trigger _nextButton;

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

    public void SetLesson(LessonModel englishLesson, LessonModel koreanLesson, int index)
    {
        Lessons[index].FillLesson(englishLesson, koreanLesson);
    }

    public void FinishPage()
    {
        ShowFirstLesson();
    }

    private void ShowFirstLesson()
    {
        for (int i = 0; i < Lessons.Length; i++)
        {
            Lessons[i].Words.SetActive(i == 0);

            if (!Lessons[i].Filled)
            {
                Lessons[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetPageButtons(int pageIndex, int totalPageCount)
    {
        bool notFirstPage = pageIndex != 0;
        bool notLastPage = pageIndex != totalPageCount - 1;

        _pageSelectText.text = "Page " + (pageIndex + 1) + "/" + totalPageCount;
        _previousButton.gameObject.SetActive(notFirstPage);
        _nextButton.gameObject.SetActive(notLastPage);

        VRC_EventHandler.VrcEvent hideCurrentPageEvent = createSetPageEvent(pageIndex, VRC_EventHandler.VrcBooleanOp.False);

        if (notFirstPage) {
            VRC_EventHandler.VrcEvent showPreviousPageEvent = createSetPageEvent(pageIndex - 1, VRC_EventHandler.VrcBooleanOp.True);
            _previousButton.Triggers[0].Events.Add(hideCurrentPageEvent);
            _previousButton.Triggers[0].Events.Add(showPreviousPageEvent);
        }

        if (notLastPage)
        {
            VRC_EventHandler.VrcEvent showNextPageEvent = createSetPageEvent(pageIndex + 1, VRC_EventHandler.VrcBooleanOp.True);
            _nextButton.Triggers[0].Events.Add(hideCurrentPageEvent);
            _nextButton.Triggers[0].Events.Add(showNextPageEvent);
        }
    }

    private VRC_EventHandler.VrcEvent createSetPageEvent(int pageIndex, VRC_EventHandler.VrcBooleanOp visible) {
        VRC_EventHandler.VrcEvent pageEvent = new VRC_EventHandler.VrcEvent();
        pageEvent.EventType = VRC_EventHandler.VrcEventType.SetGameObjectActive;
        pageEvent.ParameterObject = transform.parent.GetChild(pageIndex).gameObject;
        pageEvent.ParameterBoolOp = visible;
        return pageEvent;
    }
}
