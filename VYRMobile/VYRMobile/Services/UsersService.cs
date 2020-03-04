using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Models;

namespace VYRMobile.Services
{
    public class UsersService
    {
        private static UsersService _instance;
        public static UsersService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UsersService();

                return _instance;
            }
        }
        public async Task<List<ApplicationUser>> GetUsers()
        {
            var response = new List<ApplicationUser>
            {
                new ApplicationUser { SupervisorUserId = "10", Name = "Pedro Jimenez"},
                new ApplicationUser { SupervisorUserId = "10", Name = "Jaime Baez"},
                new ApplicationUser { SupervisorUserId = "10", Name = "Rodrigo Garcia"},
                new ApplicationUser { SupervisorUserId = "10", Name = "Carlos Rodriguez"},
                new ApplicationUser { SupervisorUserId = "10", Name = "Jose Vasquez"}
            };

            return response;
        }
    }
}

