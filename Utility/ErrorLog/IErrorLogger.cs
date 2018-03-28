using System;

namespace Utility.ErrorLog
{
    public interface IErrorLogger
    {
        void ExceptionHandler(Exception ex, string strPolicy, string ModuleName);

        void ExceptionWriteIntoTextFile(Exception ex, string strPolicy, string strQuery, string ModuleName);
    }
}
