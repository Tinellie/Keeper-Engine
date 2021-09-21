using System;
using System.Collections.Generic;
using System.Text;

namespace SecretArea
{

    
    public class Deserializer
    {
        private static Deserializer ins = new Deserializer();
        public static Deserializer Ins
        {
            get { return ins; }
        }
        private Deserializer() { }


        List<char> bracket = new List<char>(new char[] { '{', '}', '(', ')' });
        List<char> monobracket = new List<char>(new char[] { '\"', '\'' });


        private bool CheckBracket(char lastCh, char ch, ref Stack<short> stack)
        {
            if (lastCh == '\\') return true;
            short index = (short)bracket.IndexOf(ch);
            if (index != -1)
            {
                if (index % 2 == 0) stack.Push(index);
                else if(stack.Count > 0)
                {
                    if (stack.Peek() == index - 1) stack.Pop();
                }
                else Console.WriteLine("GameScriptError, Unexpected bracket");
            }

            index = (short)(-monobracket.IndexOf(ch) - 1);
            if (index != 0)
            {
                if (stack.Count > 0)
                {
                    if (stack.Peek() != index) stack.Push(index);

                    else stack.Pop();
                }
                else stack.Push(index);
            }
            return false;
        }



        public class Expression
        {

        }



        public class ScriptGroup
        {
            List<Script> scriptsInc = new List<Script>();
            string rawScript;

            Stack<short> stack = new Stack<short>();

            public ScriptGroup(string rawScript)
            {
                
                rawScript = rawScript.Substring((rawScript[0] == '{') ? 1 : 0,
                    rawScript.Length - ( (rawScript[0] == '{') ? 1 : 0 ) - ( (rawScript[rawScript.Length - 1] == '}') ? 1 : 0) );

                this.rawScript = rawScript;

                //Stack<short> stack = new Stack<short>();
                string scriptRead = "";
                char lastCh = ' ';
                foreach (char ch in rawScript)
                {
                    
                    Ins.CheckBracket(lastCh, ch, ref stack);


                    if (ch == ';' && stack.Count == 0)
                    {
                        scriptsInc.Add(new Script(scriptRead + ';'));
                        scriptRead = "";
                    }
                    else scriptRead += ch;
                    lastCh = ch;
                }
            }
        }

        public class Script
        {
            KeyWord keyWord;

            List<Para> para = new List<Para>();

            Stack<short> stack = new Stack<short>();

            short paraCount = -1;

            string rawScript;


            public Script(string rawScript)
            {
                this.rawScript = rawScript;

                //Stack<short> stack = new Stack<short>();
                //short paraCount = -1;
                string paraRead = "";

                char lastCh = ' ';
                foreach (char ch in rawScript)
                {

                    Ins.CheckBracket(lastCh, ch, ref stack);

                    if ((ch == ' ' || ch == ';') && stack.Count == 0)
                    {
                        if(paraCount >= 0)
                        {
                            para.Add(new Para(keyWord.paraType[paraCount], paraRead));
                        }
                        else
                        {
                            SetKeyWord(paraRead);
                        }
                        paraCount++;
                        paraRead = "";
                    }
                    else paraRead += ch;

                    lastCh = ch;
                }
            }


            private void SetKeyWord(string keyWord)
            {
                this.keyWord = KeyWordGet.Ins.get(keyWord);
            }


            public class Para
            { 
                public object obj;

                string rawPara;
                Type type;

                public Para(Type type, string rawPara)
                {
                    int index = Ins.bracket.IndexOf(rawPara[0]);
                    if (index != -1 && (index + 1) == Ins.bracket.IndexOf(rawPara[rawPara.Length - 1]))
                    {
                        rawPara = rawPara.Substring(1, rawPara.Length - 2);
                    }

                    this.type = type;
                    this.rawPara = rawPara;
                    switch (type.ToString())
                    {
                        case Utility.namespaceName + ".Deserializer+ScriptGroup":
                            obj = new ScriptGroup(rawPara);
                            break;
                        case "System.Int32":
                            obj = int.Parse(rawPara);
                            break;
                        case "System.String":
                            obj = rawPara;
                            break;
                    }
                }
            }
        }



    }
}
