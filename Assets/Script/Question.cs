using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public Letter letter;
    private bool hiraganaUsed = false;
    private bool katakanaUsed = false;

    public Question (Letter letter)
    {
        this.letter = letter;
    }

    public bool HiraganaUsed()
    {
        return hiraganaUsed;
    }

    public void UseHiragana()
    {
        hiraganaUsed = true;
    }

    public bool KatakanaUsed()
    {
        return katakanaUsed;
    }

    public void UseKatakana()
    {
        katakanaUsed = true;
    }
}
