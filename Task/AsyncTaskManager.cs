using SCKRM.Installer;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SCKRM
{
    public static class AsyncTaskManager
    {
        public static event Action asyncTaskAdd = () => { };
        public static event Action asyncTaskChange = () => { };
        public static event Action asyncTaskRemove = () => { };

        public static void AsyncTaskAddEventInvoke() => asyncTaskAdd();
        public static void AsyncTaskRemoveEventInvoke() => asyncTaskRemove();
        public static void AsyncTaskChangeEventInvoke() => asyncTaskChange();



        public static List<AsyncTask> asyncTasks { get; } = new List<AsyncTask>();

        public static void AllAsyncTaskCancel(bool onlyAsyncTaskClass = true)
        {
            for (int i = 0; i < asyncTasks.Count; i++)
            {
                AsyncTask asyncTask = asyncTasks[i];
                if (!asyncTask.cantCancel)
                {
                    asyncTask.Remove();
                    i--;
                }
            }
        }
    }

    public class AsyncTask : IRemoveableForce
    {
        public AsyncTask(string name = "", string info = "", bool loop = false, bool cantCancel = false)
        {
            this.name = name;
            this.info = info;
            this.loop = loop;
            this.cantCancel = cantCancel;

            AsyncTaskManager.asyncTasks.Add(this);

            AsyncTaskManager.AsyncTaskAddEventInvoke();
            AsyncTaskManager.AsyncTaskChangeEventInvoke();
        }

        public virtual string name { get; set; }
        public virtual string info { get; set; }
        public virtual bool loop { get; set; }
        public virtual bool cantCancel { get; set; }

        public virtual float progress { get; set; }
        public virtual float maxProgress { get; set; }



        public virtual bool isRemoved { get => isCanceled; }
        public virtual bool isCanceled { get; protected set; }



        public event Action cancelEvent;



        readonly CancellationTokenSource _cancel = new CancellationTokenSource();
        public CancellationToken cancel => _cancel.Token;



        public virtual bool Remove() => Remove(false);

        public virtual bool Remove(bool force)
        {
            if (!isCanceled && (!cantCancel || force))
            {
                isCanceled = true;

                try
                {
                    cancelEvent?.Invoke();
                }
                catch (Exception e)
                {
                    Program.Exception(e);
                }

                AsyncTaskManager.asyncTasks.Remove(this);

                AsyncTaskManager.AsyncTaskChangeEventInvoke();
                AsyncTaskManager.AsyncTaskRemoveEventInvoke();

                _cancel.Cancel();

                return true;
            }

            return false;
        }
    }
}
