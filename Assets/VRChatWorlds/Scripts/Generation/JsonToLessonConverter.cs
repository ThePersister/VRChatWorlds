using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class JsonToLessonConverter : Singleton<JsonToLessonConverter> {

    [SerializeField]
    private GameObject _lessonWallPrefab;

    [SerializeField]
    private GameObject _spawnPoint;

    public class JsonHolder
    {
        public string json;
        public string fileName;

        public JsonHolder(string fileName)
        {
            this.fileName = fileName;
        }
    }

    private JsonHolder _customTranslatedWordsJsonHolder = new JsonHolder("CustomTranslatedWords_en_ko.json");
    private JsonHolder _englishJsonHolder = new JsonHolder("LessonWall_en.json");
    private JsonHolder _koreanJsonHolder = new JsonHolder("LessonWall_ko.json");

    public void GenerateLessonWall()
    {
        StartCoroutine(GenerateWall());
    }

    private IEnumerator GenerateWall()
    {
        yield return RetrieveJsonText(_customTranslatedWordsJsonHolder);
        yield return RetrieveJsonText(_englishJsonHolder);
        yield return RetrieveJsonText(_koreanJsonHolder);

        string nope = @"{
	""words"": {

        ""example1"": ""sweet"",
		""example2"": ""nice""
    }
}";

        string nope2 = @"{
""words"": {
		""Can't"": ""못하다"",
		""Born"": ""태어나다"",
		""What"": ""무엇"",
		""Can"": ""할 수 있다"",
		""Very"": ""매우,아주"",
		""Cry"": ""울다"",
		""Angry"": ""화나다"",
		""Like"": ""마음에 들다"",
		""Never"": ""절대"",
		""Start"": ""시작"",
		""Fine"": ""좋아/벌금"",
		""Mute"": ""묵언"",
		""Brother"": ""오빠,형,"",
		""How-Many"": ""몇 개입니까?"",
		""Single"": ""혼자/단일"",
		""Sister"": ""언니,누나"",
		""City"": ""도시"",
		""From"": ""부터"",
		""Here"": ""여기"",
		""Baby"": ""아기"",
		""Brush-Teeth"": ""치아를 닦는다"",
		""Stand"": ""서다"",
		""With"": ""와 함께"",
		""Favorite"": ""마음에 들다"",
		""Draw"": ""그리다"",
		""What-Kind"": ""어떤 종류"",
		""Aid"": ""지원"",
		""off"": ""쉬다"",
		""on"": ""나타냄"",
		""Lights"": ""빛"",
		""Microwave"": ""전자레인지"",
		""Table"": ""식탁"",
		""Open Book "": "" 열린 책 "",
		""Close Book "": "" 닫은 책"",
		""Pet"": ""애완동물"",
		""Tell"": ""말하다"",
		""Real"": ""진짜"",
		""Anime"": ""일본만화애니"",
		""Miss"": ""놓치다"",
		""To"": ""쪽/으로"",
		""Are"": ""있다,존재하다"",
		""Between"": ""사이에"",
		""Sweet"": ""달콤하다"",
		""Level"": ""수준,정도"",
		""Funny"": ""웃기다,기이하다"",
		""Radiate"": ""내뿜다"",
		""Which"": ""어느쪽"",
		""Mute"": ""음소거"",
		""Hard of Hearing"": ""청각장애 귀가 안들림""
    }
}";

        TranslationOverride translationOverride = JsonConvert.DeserializeObject<TranslationOverride>(nope2);
        LessonModel[] englishLessons = JsonHelper.FromJson<LessonModel>(_englishJsonHolder.json);
        LessonModel[] koreanLessons = JsonHelper.FromJson<LessonModel>(_koreanJsonHolder.json);
        Debug.Log("T, custom translated words: " + translationOverride);
        Debug.Log("T, english lessons: " + englishLessons);
        Debug.Log("T, korean lessons: " + koreanLessons);
        CreateWall(translationOverride, englishLessons, koreanLessons);
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

    private void CreateWall(TranslationOverride translationOverride, LessonModel[] englishLessons, LessonModel[] koreanLessons)
    {
        GameObject lessonWall = GameObject.Instantiate(_lessonWallPrefab, _spawnPoint.transform.position, Quaternion.identity, _spawnPoint.transform);
        lessonWall.GetComponent<LessonWall>().CreateUI(translationOverride, englishLessons, koreanLessons);
        lessonWall.transform.rotation = _spawnPoint.transform.rotation;
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

[Serializable]
public struct TranslationOverride
{
    public Dictionary<string, string> words;
}