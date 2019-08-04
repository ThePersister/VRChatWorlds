using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TranslateLessonWall : Singleton<TranslateLessonWall> {

    private string _koreanLocale = "ko";
    private string _saveFileNameBase = "LessonWall_";
    private string _saveFileNameExtension = ".json";
    private string _apiKey = "AIzaSyDL1W69AZjHjUdYIZ7JofvYNtGurUossqQ";

    private string _jsonFileName = "LessonWall_en.json";
    private string _retrievedJsonText;
    private string _translatedJsonText;
    private string _latestTranslatedValue;
    private bool _failedTranslation;

    public void TranslateWall()
    {
        StartCoroutine(TranslateToKorean());
    }

    private IEnumerator TranslateToKorean()
    {
        _failedTranslation = false;
        yield return RetrieveJsonText();
        LessonModel[] lessonWall = JsonHelper.FromJson<LessonModel>(_retrievedJsonText);
        yield return TranslateLessons(lessonWall);

        Debug.Log("Write to json text file");
        string filePath = Application.streamingAssetsPath + "/" + _saveFileNameBase + _koreanLocale + _saveFileNameExtension;
        File.WriteAllText(filePath, _translatedJsonText);
        Debug.Log("Translation complete!");
    }

    private IEnumerator RetrieveJsonText()
    {
        WWW data = new WWW(Application.streamingAssetsPath + "/" + _jsonFileName);
        yield return data;

        if (string.IsNullOrEmpty(data.error))
        {
            _retrievedJsonText = data.text;
        }
        else
        {
            Debug.Log("Failed to read json, error: " + data.error);
        }
    }

    private IEnumerator TranslateLessons(LessonModel[] lessons)
    {
        for (int lessonIndex = 0; lessonIndex < lessons.Length; lessonIndex++)
        {
            LessonModel lesson = lessons[lessonIndex];
            Debug.Log("Translate lesson: " + lesson.title);
            yield return Translate.Process(_koreanLocale, lesson.title, OnTranslatedWord, OnTranslationFailure);
            lesson.title = _latestTranslatedValue;

            for (int wordIndex = 0; wordIndex < lesson.words.Length; wordIndex++)
            {
                Debug.Log("Translate word: " + lesson.words[wordIndex]);
                yield return Translate.Process(_koreanLocale, lesson.words[wordIndex], OnTranslatedWord, OnTranslationFailure);
                if (_failedTranslation)
                {
                    yield break;
                }

                lesson.words[wordIndex] = _latestTranslatedValue;
            }
        }
        
        _translatedJsonText = JsonHelper.ToJson<LessonModel>(lessons);
    }

    private void OnTranslatedWord(string translatedWord)
    {
        _latestTranslatedValue = translatedWord;
        Debug.Log("Translated to: " + _latestTranslatedValue);
    }

    private void OnTranslationFailure()
    {
        _failedTranslation = true;
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
