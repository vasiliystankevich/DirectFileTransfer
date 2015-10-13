using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSTUNLibrary
{
    public static class TaskExtention
    {
        public static Task IsCompleted(this Task task)
        {
            if (!task.IsCompleted) task.Wait();
            return task;
        }

        public static T IsCompleted<T>(this Task<T> task)
        {
            if (!task.IsCompleted) task.Wait();
            return task.Result;
        }
    }
}
