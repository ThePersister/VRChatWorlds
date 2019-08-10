using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonWall : MonoBehaviour {

    [SerializeField]
    private Transform _pagesHolder;

    [SerializeField]
    private GameObject _pagePrefab;

    private const int lessonsPerPage = 5;

    public void CreateUI(LessonModel[] englishLessons, LessonModel[] koreanLessons)
    {
        GameObject page;
        for (int i = 0; i < englishLessons.Length; i++)
        {
            page = GameObject.Instantiate(_pagePrefab, _pagesHolder.transform.position, Quaternion.identity, _pagesHolder);
            Page pageComponent = page.GetComponent<Page>();
            for (int x = 0; x < lessonsPerPage; x++)
            {
                int clampedKoreanIndex = Mathf.Min(koreanLessons.Length - 1, i);
                pageComponent.SetLesson(englishLessons[i], koreanLessons[clampedKoreanIndex], x);

                if (i == englishLessons.Length - 1)
                {
                    break;
                }
                else if (x < lessonsPerPage - 1)
                {
                    i++;
                }
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
