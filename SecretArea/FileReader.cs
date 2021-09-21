using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SecretArea
{
    public class FileReader
    {
        static char[] remove = new char[] { '\n', '\r', '\t' };
        static char[] reStr = new char[] { '\'', '\"' };
        static Dictionary<char, char> reStrDic = new Dictionary<char, char>(
            new KeyValuePair<char, char>[]
            {
                new KeyValuePair<char, char>('\'','c'),
                new KeyValuePair<char, char>('\"','s'),
            }
        );

        public static string Read(string path)
        {

            try
            {
                using (StreamReader sr = new StreamReader(path))//"c:/jamaica.txt"
                {
                    string str = sr.ReadToEnd();
                    return DataProcesser(ref str);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("GameScript could not be read:");
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
            return null;
            
        }


        private static string DataProcesser(ref string str)
        {
            string strNew = "";

            foreach (char ch in remove)
                str = str.Replace(ch.ToString(), "");
            char? lastChar = null;
            char inStr = 'n';
            foreach (char ch in str)
            {

                if (strNew.Length > 0) lastChar = strNew[strNew.Length - 1];
                else lastChar = null;


                if (inStr == 'n')
                {
                    foreach (char reCh in reStr)
                    {
                        if (ch == reCh && lastChar != '\\')
                        {
                            inStr = reCh;
                            break;
                        }
                    }



                    //如果是在开头的' '空格字符 或第一个字符, 那么lastChar == null, 不执行if
                    if (lastChar != null)
                    {
                        //如果是{, 且前面没有空格, 添加一个
                        if (ch == '{' && (lastChar != ' ')) strNew += ' ';


                    }
                    //如果有连续的空格，不会重复添加
                    if (inStr == 'n' && !((lastChar == ' ' || lastChar == ';') && ch == ' '))
                        strNew += ch;
                }
                else if (ch == inStr)
                {
                    if(lastChar != '\\') inStr = 'n';
                    
                }
                else if (reStrDic.ContainsKey(ch)) strNew += reStrDic[ch];

                if (inStr != 'n')
                {
                    strNew += ch;
                }
                
                


            }


            return strNew;
        }
    }
        
    
}
