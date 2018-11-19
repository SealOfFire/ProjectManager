using NLog;
using System;

namespace ProjectManager.Log
{
    public class AMDbLogger
    {
        private static NLog.Logger logger = LogManager.GetLogger("amdbLog");

        #region Trace

        /// <summary>
        /// 开始标记
        /// </summary>
        public static void TraceBegin(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 结束标记
        /// </summary>
        public static void TraceEnd(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 方法开始
        /// </summary>
        public static void TraceMethodBegin()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 方法结束
        /// </summary>
        public static void TraceMethodEnd()
        {
            throw new NotImplementedException();
        }

        public static void Trace(string message)
        {
            logger.Trace(message);
        }

        public static void Trace(string message, double argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(string message, params object[] args)
        {
            logger.Trace(message, args);
        }

        public static void Trace(Exception exception)
        {
            logger.Trace(exception);
        }

        public static void Trace(Exception exception, string message)
        {
            logger.Trace(exception, message);
        }

        #endregion

        #region debug

        public static void Debug(string message)
        {
            logger.Debug(message);
        }

        public static void Debug(string message, double argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(string message, params object[] args)
        {
            logger.Debug(message, args);
        }

        public static void Debug(Exception exception)
        {
            logger.Debug(exception);
        }

        public static void Debug(Exception exception, string message)
        {
            logger.Debug(exception, message);
        }

        #endregion

        #region info

        public static void Info(string message)
        {
            logger.Info(message);
        }

        public static void Info(string message, double argument)
        {
            logger.Info(message, argument);
        }

        public void Info(string message, params object[] args)
        {
            logger.Info(message, args);
        }

        public static void Info(Exception exception)
        {
            logger.Info(exception);
        }

        public static void Info(Exception exception, string message)
        {
            logger.Info(exception, message);
        }

        #endregion

        #region Warn

        public static void Warn(string message)
        {
            logger.Warn(message);
        }

        public static void Warn(string message, double argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(string message, params object[] args)
        {
            logger.Warn(message, args);
        }

        public static void Warn(Exception exception)
        {
            logger.Warn(exception);
        }

        public static void Warn(Exception exception, string message)
        {
            logger.Warn(exception, message);
        }

        #endregion

        #region Error

        public static void Error(string message)
        {
            logger.Error(message);
        }

        public static void Error(string message, double argument)
        {
            logger.Error(message, argument);
        }

        public void Error(string message, params object[] args)
        {
            logger.Error(message, args);
        }

        public static void Error(Exception exception)
        {
            logger.Error(exception);
        }

        public static void Error(Exception exception, string message)
        {
            logger.Error(exception, message);
        }

        #endregion

        #region Fatal

        public static void Fatal(string message)
        {
            logger.Fatal(message);
        }

        public static void Fatal(string message, double argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(string message, params object[] args)
        {
            logger.Fatal(message, args);
        }

        public static void Fatal(Exception exception)
        {
            logger.Fatal(exception);
        }

        public static void Fatal(Exception exception, string message)
        {
            logger.Fatal(exception, message);
        }

        #endregion
    }
}
