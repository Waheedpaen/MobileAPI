

   namespace DataAccessLayer.ILoggerManager;
   public interface ILoggerManager
{
    void LogInfo(string message);
    void LogWarn(string message);
    void LogDebug(string message);
    void LogError(string message);
    void LogException(Exception e);
    void LogErrorWithException(string message, Exception e);
    void LogEnter();
    void LogExit();
}
