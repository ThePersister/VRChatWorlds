using System.Collections;
using System;
using UnityEngine;

public class JsonToLessonConverter : Singleton<JsonToLessonConverter> {

    [SerializeField]
    private GameObject _lessonWallPrefab;

    public class JsonHolder
    {
        public string json;
        public string fileName;

        public JsonHolder(string fileName)
        {
            this.fileName = fileName;
        }
    }

    private JsonHolder _englishJsonHolder = new JsonHolder("LessonWall_en.json");
    private JsonHolder _koreanJsonHolder = new JsonHolder("LessonWall_ko.json");

    public void GenerateLessonWall()
    {
        StartCoroutine(GenerateWall());
    }

    private IEnumerator GenerateWall()
    {
        yield return RetrieveJsonText(_englishJsonHolder);
        yield return RetrieveJsonText(_koreanJsonHolder);
        LessonModel[] englishLessons = JsonHelper.FromJson<LessonModel>(_englishJsonHolder.json);
        LessonModel[] koreanLessons = JsonHelper.FromJson<LessonModel>(_koreanJsonHolder.json);
        Debug.Log("T, english lessons: " + englishLessons);
        Debug.Log("T, korean lessons: " + koreanLessons);
        CreateWall(englishLessons, koreanLessons);
    }

    private void OnTranslationComplete(string result)
    {
        Debug.Log("Result: " + result);
    }

    private IEnumerator RetrieveJsonText(JsonHolder jsonHolder)
    {
        WWW data = new WWW(Application.streamingAssetsPath + "/" + jsonHolder.fileName);
        yield return data;

        if (string.IsNullOrEmpty(data.error))
        {
            jsonHolder.json = data.text;
        } else
        {
            Debug.Log("Failed to read json, error: " + data.error);
        }
    }

    private void CreateWall(LessonModel[] englishLessons, LessonModel[] koreanLessons)
    {
        GameObject lessonWall = GameObject.Instantiate(_lessonWallPrefab, Vector3.zero, Quaternion.identity);
        lessonWall.GetComponent<LessonWall>().CreateUI(englishLessons, koreanLessons);
    }

    private void DebugLessons(LessonModel[] lessons)
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
}

[Serializable]
public struct LessonModel
{
    public string title;
    public string[] words;
}
