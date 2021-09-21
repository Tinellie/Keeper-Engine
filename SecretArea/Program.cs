using System;
using System.Collections.Generic;
using System.Text;

namespace SecretArea
{

    public class Utility
    {
        /*private static Utility ins = new Utility();
        private Utility() { }
        public static Utility Ins
        {
            get { return ins; }
        }*/

        public const string namespaceName = "SecretArea";

        public static Deserializer.ScriptGroup MainScript;
    }

    class Program
    {


        static void Main(string[] args)
        {
            Utility.MainScript = 
                new Deserializer.ScriptGroup(
                    FileReader.Read(@"D:\_Works\_Foreshow Network Team\SecretArea\SecretArea\GameScript\gameScript.txt")
                );

            Console.WriteLine("test");
        }
    }
}
