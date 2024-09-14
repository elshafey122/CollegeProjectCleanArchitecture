using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProject.XUnitTest.ClassDataAndMemberData
{
    internal class PassDatatoParamsUsingClassData : IEnumerable<Object[]>
    {
        private readonly List<Object[]> data;
        public PassDatatoParamsUsingClassData()
        {
            data = new List<Object[]>()
            {
                new object[]{1},
                new object[]{2},
            };
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class ahmed
    {
        public ahmed()
        {
            
        }
        public static void print()
        {
            Console.WriteLine("AHMED");
        }
    }
}
