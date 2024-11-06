﻿using UnityEngine;
using UnityEngine.UI;

public class LanguageSimpleText : MonoBehaviour
{


    public string[] content;

    private Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }

	// Use this for initialization
	void Start ()
    {
        if (null == content)
            return;

        int mTmpLanguage = PlayerPrefsMgr.mInstance.GetInt(PlayerPrefsKeys.KEY_LANGUAGE, 3);
        if (content.Length < mTmpLanguage)
        {
            mTmpLanguage = content.Length;
        }

        if (null != text)
        {
            text.text = content[mTmpLanguage];
        }
    }
}
