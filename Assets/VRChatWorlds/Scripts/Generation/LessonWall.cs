using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonWall : MonoBehaviour {

    [SerializeField]
    private Transform _pagesHolder;

    [SerializeField]
    private GameObject _pagePrefab;

    public void CreateUI(LessonModel[] lessons)
    {
        GameObject page;
        for (int i = 0; i < lessons.Length; i++)
        {
            page = GameObject.Instantiate(_pagePrefab, Vector3.zero, Quaternion.identity, _pagesHolder);
            Page pageComponent = page.GetComponent<Page>();
            for (int x = 0; x < 5; x++)
            {
                pageComponent.SetLesson(lessons[i], x);
                i++;

                if (i == lessons.Length)
                    break;
            }
            pageComponent.FinishPage();
        }
        SetupPageNavigation();
    }

    private void SetupPageNavigation()
    {
        int totalPageCount = _pagesHolder.childCount;
        for (int i = 0; i < totalPageCount; i++)
        {
            GameObject page = _pagesHolder.GetChild(i).gameObject;
            page.GetComponent<Page>().SetPageButtons(i, totalPageCount);
            page.SetActive(i == 0);
            page.name = "Page" + (i + 1);
        }
    }
}
