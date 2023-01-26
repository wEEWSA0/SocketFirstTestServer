using SocketFirstTestServer.Engines;

Console.WriteLine("I'm working");

ServerEngine serverEngine = new ServerEngine("127.0.0.1", 34536);

Task.Run(() => serverEngine.StartServer(2));

Console.ReadLine();

Console.WriteLine("Wait until servet turn off");

serverEngine.CloseServerSocket();

Console.ReadLine();

/*
 * На клиенте необходим класс SentData и Command, enum CommandType для пересылки сообщений
 * */