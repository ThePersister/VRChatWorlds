using System.Collections;
using System;
using UnityEngine;

public class JsonToLessonConverter : Singleton<JsonToLessonConverter> {

    [SerializeField]
    private GameObject _lessonWallPrefab;

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
        GameObject lessonWall = GameObject.Instantiate(_lessonWallPrefab, Vector3.zero, Quaternion.identity);
        lessonWall.GetComponent<LessonWall>().CreateUI(lessons);
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
