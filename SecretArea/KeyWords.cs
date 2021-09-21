using System;
using System.Collections.Generic;
using System.Text;

namespace SecretArea
{
    public class KeyWord
    {
        public string name;
        public List<string> names;
        public List<Type> paraType;

        public KeyWord(string name, List<string> names, List<Type> paraType)
        {
            this.name = name;
            this.names = names;
            this.paraType = paraType;
        }

    }

    public class KeyWordGet
    {
        private static KeyWordGet ins = new KeyWordGet();
        public static KeyWordGet Ins
        {
            get { return ins; }
        }



        private List<KeyWord> KeyWordsList = new List<KeyWord>();

        

        public KeyWord get(string keywordGiven)
        {
            foreach (KeyWord keyword in KeyWordsList)
            {
                if (keyword.names.Contains(keywordGiven)) return keyword;
            }

            Console.WriteLine("GameScriptError: Unknown KeyWord");
            return null;
        }





        private KeyWordGet()
        {
            KeyWordsList.Add(new KeyWord(
                "say",
                new List<string>(new string[] {
                    "say",
                    "说"
                }),
                new List<Type>(new Type[1] {
                    typeof(string)
                })
            ));

            KeyWordsList.Add(new KeyWord(
                "if",
                new List<string>(new string[] {
                    "if",
                    "如果"
                }),
                new List<Type>(new Type[1] {
                    typeof(Deserializer.ScriptGroup)
                })
            ));
        }
    }


}
