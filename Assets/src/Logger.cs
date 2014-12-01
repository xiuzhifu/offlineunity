using System;
//using UnityEngine;
internal class Logger
{
	private const string HEAD_LOG = "[INFO] ";
	private const string HEAD_ERROR = "[ERROR] ";
	private const string HEAD_WARNING = "[WARN] ";
	private const string HEAD_EXCEPTION = "[EXCEPTION] ";
	private const bool LOGSTYLE_NORMAL = false;
	public static void LogMsg(string msg)
	{
		Logger.DoLog("[INFO] " + msg);
	}
	public static void LogError(string error)
	{
		Logger.DoLog("[ERROR] " + error);
	}
	public static void LogWarning(string warning)
	{
		Logger.DoLog("[WARN] " + warning);
	}
	public static void LogException(Exception ex)
	{
		string text = string.Concat(new object[]
		{
			"[EXCEPTION] type: ",
			ex.GetType(),
			"\nmsg: ",
			ex.Message,
			"\nstack:\n",
			ex.StackTrace
		});
		for (Exception innerException = ex.InnerException; innerException != null; innerException = innerException.InnerException)
		{
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"\ninner exception:\ntype: ",
				innerException.GetType(),
				"\nmsg: ",
				innerException.Message,
				"\nstack:\n",
				innerException.StackTrace
			});
		}
		Logger.DoLog(text);
	}
	public static void LogException(Exception ex, string msg)
	{
		string text = string.Concat(new object[]
		{
			"[EXCEPTION] type: ",
			ex.GetType(),
			"\nmsg: ",
			ex.Message,
			"\nstack:\n",
			ex.StackTrace
		});
		if (ex.InnerException != null)
		{
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"\ninner exception:\ntype: ",
				ex.InnerException.GetType(),
				"\nmsg: ",
				ex.InnerException.Message,
				"\nstack:\n",
				ex.InnerException.StackTrace
			});
		}
		if (msg != null)
		{
			text = text + "\ncustom msg: " + msg;
		}
		Logger.DoLog(text);
	}
	private static void DoLog(string msg)
	{
		Logger.Console(msg);
	}
	private static void Console(string msg)
	{
		if (false || msg.StartsWith("[INFO] "))
		{
            System.Console.WriteLine(msg);
			//Debug.Log(msg);
		}
		else
		{
			if (msg.StartsWith("[ERROR] "))
			{
                System.Console.WriteLine(msg);
				//Debug.LogError(msg);
			}
			else
			{
				if (msg.StartsWith("[EXCEPTION] "))
				{
                    System.Console.WriteLine(msg);
					//Debug.LogError(msg);
				}
				else
				{
					if (msg.StartsWith("[WARN] "))
					{
                        System.Console.WriteLine(msg);
						//Debug.LogWarning(msg);
					}
				}
			}
		}
	}
}
