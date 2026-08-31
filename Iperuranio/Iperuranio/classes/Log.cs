using System.Reflection;
using log4net;
using log4net.Config;

namespace Gioco;

/// <summary>
/// Wrapper sottile su log4net. Tutto il gioco logga da qui, così se un
/// giorno si cambia libreria si tocca un file solo.
/// La configurazione sta nel file "log" in root (appender su logfile.txt).
/// </summary>
public static class Log
{
	private static ILog logger;
	private static bool ready;

	public static void Init()
	{
		try
		{
			// su .NET Core il repository va passato esplicitamente:
			// non viene più dedotto dall'assembly chiamante
			var repository = LogManager.GetRepository(Assembly.GetEntryAssembly());
			XmlConfigurator.Configure(repository, new FileInfo("log"));
			logger = LogManager.GetLogger(repository.Name, "Iperuranio");
			ready = true;
			Info("=== avvio ===");
		}
		catch (Exception e)
		{
			// il logging non deve mai impedire al gioco di partire
			ready = false;
			Console.WriteLine($"(logging non disponibile: {e.Message})");
		}
	}

	public static void Info(string message)
	{
		if (ready) logger.Info(message);
	}

	public static void Debug(string message)
	{
		if (ready) logger.Debug(message);
	}

	public static void Error(string message, Exception e = null)
	{
		if (!ready) return;
		if (e == null) logger.Error(message);
		else logger.Error(message, e);
	}
}
