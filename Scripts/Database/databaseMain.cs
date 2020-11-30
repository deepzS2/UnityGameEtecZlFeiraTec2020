using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class databaseMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("",0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void saveData(int _score,string _scoreName)
    {
        if (_score > getInt("bestScore"))
        {

            int i = getInt("bestScore");
            string j = getString("bestScoreName");

            int x = getInt("2Score");
            string z = getString("2ScoreName");

            setInt("2Score",i);
            setString("2ScoreName",j);

            i = getInt("3Score");
            j = getString("3ScoreName");

            setInt("3Score",x);
            setString("3ScoreName", z);

            x = getInt("4Score");
            z = getString("4ScoreName");

            setInt("4Score", i);
            setString("4ScoreName", j);

            i = getInt("5Score");
            j = getString("5ScoreName");

            setInt("5Score", x);
            setString("5ScoreName", z);


            setInt("bestScore", _score);
            setString("bestScoreName",_scoreName);
        }else
        {
            if (_score > getInt("2Score"))
            {
                int x = getInt("2Score");
                string z = getString("2ScoreName");

                int i = getInt("3Score");
                string j = getString("3ScoreName");

                setInt("3Score", x);
                setString("3ScoreName", z);

                x = getInt("4Score");
                z = getString("4ScoreName");

                setInt("4Score", i);
                setString("4ScoreName", j);

                i = getInt("5Score");
                j = getString("5ScoreName");

                setInt("5Score", x);
                setString("5Score", z);

                setInt("2Score", _score);
                setString("2ScoreName", _scoreName);
            }else
            {
                if (_score > getInt("3Score"))
                {
                    int i = getInt("3Score");
                    string j = getString("3ScoreName");

                    int x = getInt("4Score");
                    string z = getString("4ScoreName");

                    setInt("4Score", i);
                    setString("4ScoreName", j);

                    i = getInt("5Score");
                    j = getString("5ScoreName");

                    setInt("5Score", x);
                    setString("5ScoreName", z);

                    setInt("3Score", _score);
                    setString("3ScoreName", _scoreName);
                }else
                {
                    if (_score > getInt("4Score"))
                    {
                        int x = getInt("4Score");
                        string z = getString("4ScoreName");

                        int i = getInt("5Score");
                        string j = getString("5ScoreName");

                        setInt("5Score", x);
                        setString("5ScoreName", z);

                        setInt("4Score", _score);
                        setString("4ScoreName", _scoreName);
                    }else
                    {
                        if (_score > getInt("5Score"))
                        {
                            setInt("5Score", _score);
                            setString("5ScoreName", _scoreName);
                        }
                    }
                }
            }
        }
    }


    public void setInt(string keyName,int Value)
    {
        PlayerPrefs.SetInt(keyName,Value);
    }

    public void setString(string keyName,string Value)
    {
        PlayerPrefs.SetString(keyName,Value);
    }

    public int getInt(string keyName)
    {
        int Value = PlayerPrefs.GetInt(keyName);
        return Value;
    }

    public string getString(string keyName)
    {
        string Value = PlayerPrefs.GetString(keyName);
        return Value;
    }
}
