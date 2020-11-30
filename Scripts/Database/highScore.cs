using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highScore : MonoBehaviour
{
    [SerializeField]
    private Text[] _stringBestScoreText;
    private char[] _stringBestScore;
    [SerializeField]
    private Text[] _string2ScoreText;
    private char[] _string2Score;

    [SerializeField]
    private Text[] _string3ScoreText;
    private char[] _string3Score;

    [SerializeField]
    private Text[] _string4ScoreText;
    private char[] _string4Score;

    [SerializeField]
    private Text[] _string5ScoreText;
    private char[] _string5Score;

    private string _changeString;

    private int i;

    [SerializeField]
    private Text[] Scores;
        
    // Start is called before the first frame update
    void Start()
    {
        _changeString = PlayerPrefs.GetString("bestScoreName");
        _stringBestScore = _changeString.ToCharArray();
        
        _changeString = PlayerPrefs.GetString("2ScoreName");
        _string2Score = _changeString.ToCharArray();

        _changeString = PlayerPrefs.GetString("3ScoreName");
        _string3Score = _changeString.ToCharArray();

        _changeString = PlayerPrefs.GetString("4ScoreName");
        _string4Score = _changeString.ToCharArray();

        _changeString = PlayerPrefs.GetString("5ScoreName");
        _string5Score = _changeString.ToCharArray();

        updateChar(_stringBestScore);
        updateChar2(_string2Score);
        updateChar3(_string3Score);
        updateChar4(_string4Score);
        updateChar5(_string5Score);

    }

    public void updateChar(char[] _string)
    {
        int i;

        for (i = 0; i < 5; i++)
        {
            if (_string.Length > i)
            {
                _stringBestScoreText[i].text = _string[i].ToString();
            }
        }

        Scores[0].text = PlayerPrefs.GetInt("bestScore").ToString();
    }
    public void updateChar2(char[] _string)
    {
        int i;
        for (i = 0; i < 5; i++)
        {
            if(_string.Length > i)
            {
                _string2ScoreText[i].text = _string[i].ToString();
            }
        }
        Scores[1].text = PlayerPrefs.GetInt("2Score").ToString();
    }
    public void updateChar3(char[] _string)
    {
        int i;
        for (i = 0; i < 5; i++)
        {
            if(_string.Length > i)
            {
                _string3ScoreText[i].text = _string[i].ToString();
            }
            
        }
        Scores[2].text = PlayerPrefs.GetInt("3Score").ToString();
    }
    public void updateChar4(char[] _string)
    {
        int i;
        for (i = 0; i < 5; i++)
        {
            if (_string.Length > i)
            {
                _string4ScoreText[i].text = _string[i].ToString();
            }
        }
        Scores[3].text = PlayerPrefs.GetInt("4Score").ToString();
    }
    public void updateChar5(char[] _string)
    {
        int i;
        for (i = 0; i < 5; i++)
        {
            if (_string.Length > i)
            {
                _string5ScoreText[i].text = _string[i].ToString();
            }
        }
        Scores[4].text = PlayerPrefs.GetInt("5Score").ToString();
    }
}
