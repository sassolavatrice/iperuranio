namespace Gioco
{
    internal static class LogFileManager
    {

        public static void Write(string msg)
        {
            File.AppendAllText("log.txt",msg+"\n");
        }


    }
}
