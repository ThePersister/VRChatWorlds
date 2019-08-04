// Credit goes to Grimmdev https://gist.github.com/grimmdev/979877fcdc943267e44c

// We need this for parsing the JSON, unless you use an alternative.
// You will need SimpleJSON if you don't use alternatives.
// It can be gotten hither. http://wiki.unity3d.com/index.php/SimpleJSON
using SimpleJSON;
using UnityEngine;
using System.Collections;

public class Translate
{
    // We have use googles own api built into google Translator.
    public static IEnumerator Process(string targetLang, string sourceText, System.Action<string> result, System.Action onFailed)
    {
        yield return Process("auto", targetLang, sourceText, result, onFailed);
    }

    // Exactly the same as above but allow the user to change from Auto, for when google get's all Jerk Butt-y
    public static IEnumerator Process(string sourceLang, string targetLang, string sourceText, System.Action<string> result, System.Action onFailed)
    {
        string url = "https://translate.googleapis.com/translate_a/single?client=gtx&sl="
            + sourceLang + "&tl=" + targetLang + "&dt=t&q=" + WWW.EscapeURL(sourceText);

        WWW www = new WWW(url);
        yield return new WaitForSeconds(3.0f);
        yield return www;

        if (www.isDone)
        {
            if (string.IsNullOrEmpty(www.error))
            {
                var N = JSONNode.Parse(www.text);
                string translatedText = N[0][0][0];

                result(translatedText);
            }
            else
            {
                Debug.Log("WWW, error: " + www.error);
                onFailed();
            }
        }
    }
}