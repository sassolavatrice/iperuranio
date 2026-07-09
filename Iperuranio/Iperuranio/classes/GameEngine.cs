namespace Gioco
{
    internal class GameEngine
    {
        public Dictionary<string,string> LoginTable{get;set;}
        string currentplayer;
        GameState gameState;
        public GameEngine()
        {
            LogFileManager.Write("application start");
        }
        public void Init()
        {
            LogFileManager.Write("init");
            LoginTable = SaveLoadManager.LoadLoginTable();
            Console.WriteLine("welcome to the game write help for list of commands");            
        }
        public void CloseGame()
        {
            LogFileManager.Write("close game");
        }
        public string CommandParser(string[] arguments)
        {
            if (arguments[0]==null)
            {
                return null;
            }
            switch (arguments[0].ToLower())
            {            
                case "newg":
                    if(arguments.Length<4)
                    {
                        Console.WriteLine("invalid number of arguments");
                        break;
                    }
                    else
                   {
                        NewGameProcedure(arguments[1],arguments[2],arguments[3]);
                        break;
                    }
                case "resg":
                    if(arguments.Length<3)
                    {
                        Console.WriteLine("invalid number of arguments");
                        break;
                    }
                    else if(ResumeGameProcedure(arguments[1],arguments[2])=="startgame")
                    {
                        //StartGame();
							Console.WriteLine("starting the game!");
                        break;
                    }
                    break;
                case "delete":
                    if(arguments.Length<3)
                    {
                        Console.WriteLine("invalid number of arguments");
                        break;
                    }else
                    {
                        if(LoginTable.ContainsKey(arguments[1]) && LoginTable[arguments[1]]==arguments[2])
                        {
                            LoginTable.Remove(arguments[1]);
                            Console.WriteLine("account deleted");
                            LogFileManager.Write("account "+arguments[1]+" deleted");
                        }
                        else
                        {
                            Console.WriteLine("invalid credentials");
                        }
                        break;
                    }
                case "exit":
                    Console.WriteLine("Goodbye");
                    return "exit";
                case "help":
                Console.WriteLine("newg<nickname><password><password> to create new user\n"+
                                               "resg<nickname><password> to login\n"+
                                               "delete<nickname><password> to delete user\n"+
                                               "exit");
                                               break;
                //case "startgame":{return "startgame";}
                default:
                Console.WriteLine("try to write help");
                 break;
            }
            return null;
        }
        void NewGameProcedure(string name,string password1,string password2)
        {
            LogFileManager.Write("new game procedure");
            if(LoginTable==null)LoginTable=new Dictionary<string, string>();
            if(LoginTable.ContainsKey(name)){Console.WriteLine("name already exist");return;}
            if(password1!=password2){Console.WriteLine("password doesnt match please try again"); return;}
            if(password1==password2)LoginTable.Add(name,password1);
            LogFileManager.Write("new game completed");
            Console.WriteLine("New game created "+name);
        }
        string ResumeGameProcedure(string name,string password)
        {
            LogFileManager.Write("resume game procedure");
            if(LoginTable==null){Console.WriteLine("no login available"); return null;}
            if(!LoginTable.ContainsKey(name)){Console.WriteLine("no account available for "+name+"try newg<nickname><password><password>"); return null;}
            if(LoginTable[name]!=password){Console.WriteLine("wrong password try again"); return null;}
            if(LoginTable[name]==password){Console.WriteLine("welcome back "+name); currentplayer=name; return "startgame";}
            return null;
        }   
        //void StartGame()
        //{
        //    gameState = SaveLoadManager.LoadGame(currentplayer);
        //    motoreGioco.elaboraComando(gameState);
        //    Console.Clear();
        //    Console.WriteLine("menu");
        //}
    }
}
