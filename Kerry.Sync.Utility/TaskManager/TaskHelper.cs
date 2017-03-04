using System;

namespace Kerry.Sync.Utility.TaskManger
{
    public class TaskHelper
    {
        //private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void taskRunner<TResult>(Func<TResult> task)
        {
            try
            {
                IAsyncResult asyncResult = task.BeginInvoke(null, null);
                while (!asyncResult.IsCompleted)
                {
                    System.Threading.Thread.Sleep(100);
                }
                var result = task.EndInvoke(asyncResult);
            }
            catch (Exception ex)
            {
                //log.Error("[Task Runner][" + task.GetType() + "]" + ex.StackTrace);
            }

        }
        public void taskRunner(Action task)
        {
            try
            {
                IAsyncResult asyncResult = task.BeginInvoke(null, null);
                while (!asyncResult.IsCompleted)
                {
                    System.Threading.Thread.Sleep(100);
                }
                task.EndInvoke(asyncResult);
            }
            catch (Exception ex)
            {
                //log.Error("[Task Runner][" + task.GetType() + "]" + ex.StackTrace);
            }

        }
        public void taskCallBack(IAsyncResult ar)
        {
            //ar.AsyncState
        }
    }
}
