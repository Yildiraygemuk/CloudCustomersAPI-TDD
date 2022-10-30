using CloudCustomersAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.Test.Fixtures
{
    public static class UsersFixture
    {
        public static List<User> GetTestUsers()
        {
            return new List<User>()
            {
                new()
                {
                    Name="Yıldıray Test1",
                    Email="yildiraygemuk@gmail.com",
                    Address = new Address
                    {
                        Street="123 Ana cadde",
                        City="Istanbul",
                        ZipCode="123456"
                    }
                },
                new()
                {
                    Name="Yıldıray Test2",
                    Email="yildiraygemuk2@gmail.com",
                    Address = new Address
                    {
                        Street="123 Ana cadde",
                        City="Istanbul",
                        ZipCode="123456"
                    }
                },
            };
        }
    }
}
