namespace ImplementDAL.LoggerManager;

public class LoggerManager : ILoggerManager
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

    public void LogDebug(string message)
    {
        Logger.Debug(message);
    }

    public void LogError(string message)
    {
        Logger.Error(message);
    }

    public void LogException(Exception e)
    {
        Logger.Error(e);
    }

    public void LogErrorWithException(string message, Exception e)
    {
        Logger.Error(message);
        Logger.Error(e);
    }

    public void LogInfo(string message)
    {
        Logger.Info(message);
    }

    public void LogWarn(string message)
    {
        Logger.Warn(message);
    }

    public void LogEnter()
    {
        var trace = new StackTrace();
        if (trace.FrameCount <= 1) return;

        var declaringType = trace.GetFrame(1)?.GetMethod()?.DeclaringType;
        if (declaringType != null)
            Logger.Info($"Entering {declaringType.Name}.{trace.GetFrame(1)?.GetMethod()?.Name}");
    }

    public void LogExit()
    {
        var trace = new StackTrace();
        if (trace.FrameCount <= 1) return;

        var declaringType = trace.GetFrame(1)?.GetMethod()?.DeclaringType;
        if (declaringType != null)
            Logger.Info($"Exiting {declaringType.Name}.{trace.GetFrame(1)?.GetMethod()?.Name}");
    }
}