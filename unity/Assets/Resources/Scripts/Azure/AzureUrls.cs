using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class AzureUrls
{
    public const string GPT_ENDPOINT = "<YOUR_GPT_ENDPOINT>";
    public const string GPT_KEY = "<YOUR_GPT_KEY>";
    public const string GPT_DEPLOY = "<YOUR_GPT_DEPLOY_NAME>";
    public const string SEARCH_URL = "<YOUR_SEARCH_ENDPOINT>";
    public const string SEARCH_KEY = "<YOUR_SEARCH_KEY>";
    public static string GPT_URL
    {
        get
        {
            return string.Format("{0}/openai/deployments/{1}/chat/completions?api-version=2024-02-15-preview", GPT_ENDPOINT, GPT_DEPLOY);
        }
    }

    public const string SPEECH_REGION = "westus2";
    public const string SPEECH_KEY = "";   
 
    public const string LANGUAGE = "ko-KR";

    public static string VOICE_LIST_URL
    {
        get
        {
            return string.Format("https://{0}.tts.speech.microsoft.com/cognitiveservices/voices/list", SPEECH_REGION);
        }
    }

    public static string TTS_TOKEN_URL
    {
        get
        {
            return string.Format("https://{0}.api.cognitive.microsoft.com/sts/v1.0/issueToken", SPEECH_REGION);
        }
    }

    public static string TTS_URL
    {
        get
        {
            return string.Format("https://{0}.tts.speech.microsoft.com/cognitiveservices/v1", SPEECH_REGION);
        }
    }
}
