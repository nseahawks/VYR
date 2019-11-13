using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Services
{
    class TaskService
    {
        private static TaskService _instance;

        public static TaskService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TaskService();

                return _instance;
            }
        }
    }
}
