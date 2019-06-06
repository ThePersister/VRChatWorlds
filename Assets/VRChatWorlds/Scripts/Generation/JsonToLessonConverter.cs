using System.Collections;
using System;
using UnityEngine;

public class JsonToLessonConverter : Singleton<JsonToLessonConverter> {

    [SerializeField]
    private GameObject _pagePrefab;

    private string _jsonText;
    private string _jsonFileName = "LessonWall.json";

    public void GenerateLessonWall()
    {
        StartCoroutine(GenerateWall());
    }

    private IEnumerator GenerateWall()
    {
        yield return RetrieveJsonText();
        LessonModel[] lessonWall = CreateModel();
        CreateWall(lessonWall);
    }

    private IEnumerator RetrieveJsonText()
    {
        WWW data = new WWW(Application.streamingAssetsPath + "/" + _jsonFileName);
        yield return data;

        if (string.IsNullOrEmpty(data.error))
        {
            _jsonText = data.text;
            Debug.Log("Read json: " + _jsonText);
        } else
        {
            Debug.Log("Failed to read json, error: " + data.error);
        }
    }

    private LessonModel[] CreateModel()
    {
        return JsonHelper.FromJson<LessonModel>(_jsonText);
    }

    private void CreateWall(LessonModel[] lessons)
    {
        Debug.Log("Create wall with lessons: " + lessons);
        foreach (LessonModel lesson in lessons)
        {
            Debug.Log("Lesson: " + lesson.title);
            foreach (string word in lesson.words)
            {
                Debug.Log(word);
            }
        }
    }

    [Serializable]
    private struct LessonModel {
        public string title;
        public string[] words;
    }
}
