using System;
using System.Collections;
using System.Text;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
//using Boo.Lang;
using UnityEngine;
using UnityEngine.UI;


public class DisplayLrc : MonoBehaviour
{
    public Text tittleText;
    public Text phraseText0;
    public Text phraseText1;
    public Text phraseText2;

    private AudioSource audioSource; //音乐
    private Encoding enc; //更改歌词字符编码
    private StreamReader lrcStream; //读取歌词
    public System.Collections.Generic.List<string> strList = new System.Collections.Generic.List<string>();//获取到的歌词
    public System.Collections.Generic.List<float> timList = new System.Collections.Generic.List<float>();//获取到的时间

    
    public string lrcPath; //歌词路径
    public string audioPath;//音频路径

    private void Start()
    {

        //Resources中的文件路径 （以后用assetBundle加载）
        //PlaySong("Audio/songs/Adele - Someone Like You.mp3");
    }

    public enum singState{Stop,Playing,Pause }
    singState isDisplaying = singState.Stop;
    public bool PlaySong(string songPath)
    {
        isDisplaying = singState.Stop;
        //提取音频、和LRC文件的路径
        audioPath = songPath;
        lrcPath = Application.dataPath+"/Resources/"+audioPath.Substring(0, audioPath.LastIndexOf('.') + 1) + "lrc";
        
        //检验音频和LRC是否存在，及可以正常打开
        if (!File.Exists(Application.dataPath + "/Resources/" + audioPath) || !File.Exists(lrcPath))
        {
            Debug.Log("Error：file: " + audioPath + " or Lrc not exists!!");
            return false;
        }
        try
        {
            enc = GetEncoding(lrcPath, Encoding.GetEncoding("UTF-8"));
            lrcStream = new StreamReader(lrcPath, enc);
        }
        catch (Exception e)
        {
            Debug.Log("Error: Cant read Lrc File: "+lrcPath+" exception:" + e.Message);
            return false;
        }


        //解析LRC文件，播放音频
        ParseLrcFile();
        if (audioSource!=null)
        {
            audioSource.clip = Resources.Load<AudioClip>( audioPath.Remove(audioPath.LastIndexOf('.')) );//"Audio/songs/Adele - Someone Like You"
        }
        else
        {
            //从自己的component中查找
            audioSource = GetComponent<AudioSource>();
            //如果自己没有audioSource 则创建一个
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
            audioSource.clip = Resources.Load<AudioClip>( audioPath.Remove(audioPath.LastIndexOf('.')) );
        }
        audioSource.Play();
        isDisplaying = singState.Playing;
        return true;
    }

    //打开歌词文件
    public void ParseLrcFile()
    {
        string oneLine = "";
        string tittle=null;
        try
        {
            while (!lrcStream.EndOfStream)
            {
                oneLine = lrcStream.ReadLine() + "\r\n";
                if(tittle == null)
                {
                    tittle = GetTittle(oneLine);
                }
                else
                {
                    Replacestr(oneLine);
                    Time(oneLine);
                }
            }
            lrcStream.Close();
        }
        catch (Exception e)
        {
            Debug.Log("exception: "+ e.Message);
            lrcStream.Close();
        }
        tittleText.text = tittle;
    }

    //返回是否从一行字符串得到题目
    public string GetTittle(string oneLine)
    {
        string sPattner = "(\\[)(t)(i)(:)(.*)(\\])";
        Regex reg = new Regex(sPattner);
        string tmp = reg.Match(oneLine).Value;
        if (tmp == null)
            return null;
        else
            return tmp.Substring(4,tmp.IndexOf(']')-4);
    }

    //去掉除歌词外的其他东西
    public void Replacestr(string inputstr)
    {
        string sPattner = "(\\[)" + "(\\d+)" + "(:)" + "(\\d+)" + "(\\.)" + "(\\d+)" + "(\\])";
        Regex reg = new Regex(sPattner);
        foreach (Match mc in reg.Matches(inputstr))
        {  
            strList.Add(inputstr.Substring(inputstr.IndexOf(mc.Value)+ mc.Value.Length));
        }
    }

    //获取字符串的时间
    public void Time(string t)
    {
        string sPattner = "(\\[)" + "(\\d+)" + "(:)" + "(\\d+)" + "(\\.)" + "(\\d+)" + "(\\])";// "(?<t>\\[\\d.*\\]+)(?<q><[^>]*>)(?<w>[^\\[]+\r\n)";
        Regex reg = new Regex(sPattner);
        foreach (Match mc in reg.Matches(t))
        {
            string[] tmpStrs = mc.Value.Split(new char[] { '[', ':', ']' });
            float tmpf = Convert.ToSingle(tmpStrs[1]) * 60 + Convert.ToSingle(tmpStrs[2]);
            timList.Add(tmpf);
        }
    }


    private int x = 0; //获取的歌词当前的行数
    //匹配时间
    private void SuitedTime()
    {
        float tmpf = audioSource.time;
        for (int i = x; i < timList.Count; i++)
        {
            if (timList[i] <= tmpf)
            {
                phraseText0.text = i - 1 >= 0 ? strList[i - 1] : "";
                phraseText1.text = strList[i];
                phraseText2.text = i==timList.Count-1?"": strList[i + 1];
                x = i + 1;
            }
            else
                break;
        }
    }

    public void Pause()
    {
        if (isDisplaying == singState.Playing)
        {
            audioSource.Pause();
            isDisplaying = singState.Pause;
        }
    }

    public bool Play()
    {
        //如果没有添加audioClip 或者成功解析LRC 则返回false
        if (audioSource==null || audioSource.clip==null 
            || timList.Count<=0 || strList.Count<=0 || timList.Count !=strList.Count )
        {
            return false;
        }
        //开始音乐的播放
        if (isDisplaying != singState.Playing)
        {
            audioSource.Play();
            isDisplaying = singState.Playing;
        }
        return false;
    }

    public void Stop()
    {
        if (isDisplaying != singState.Stop)
        {
            audioSource.Stop();
            isDisplaying = singState.Stop;
        }
    }

    private void Update()
    {
        if (isDisplaying == singState.Playing)
        {
            SuitedTime();//匹配当前时间和歌词
            if (audioSource.isPlaying == false)
                isDisplaying = singState.Stop;
        }
    }

    private static Encoding GetEncoding(string file, Encoding defEnc)
    {
        using (var stream = File.OpenRead(file))
        {
            //判断流可读？  
            if (!stream.CanRead)
                return null;
            //字节数组存储BOM  
            var bom = new byte[4];
            //实际读入的长度  
            int readc;
            readc = stream.Read(bom, 0, 4);
            if (readc >= 2)
            {
                if (readc >= 4)
                {
                    //UTF32，Big-Endian  
                    if (CheckBytes(bom, 4, 0x00, 0x00, 0xFE, 0xFF))
                        return new UTF32Encoding(true, true);

                    //UTF32，Little-Endian  
                    if (CheckBytes(bom, 4, 0xFF, 0xFE, 0x00, 0x00))
                        return new UTF32Encoding(false, true);
                }
                //UTF8  
                if (readc >= 3 && CheckBytes(bom, 3, 0xEF, 0xBB, 0xBF))
                    return new UTF8Encoding(true);
                //UTF16，Big-Endian  
                if (CheckBytes(bom, 2, 0xFE, 0xFF))
                    return new UnicodeEncoding(true, true);

                //UTF16，Little-Endian  
                if (CheckBytes(bom, 2, 0xFF, 0xFE))
                    return new UnicodeEncoding(false, true);
            }
            return defEnc;
        }
    }

    private static bool CheckBytes(byte[] bytes, int count, params int[] values)
    {
        for (int i = 0; i < count; i++)
            if (bytes[i] != values[i])
                return false;
        return true;
    }
}
