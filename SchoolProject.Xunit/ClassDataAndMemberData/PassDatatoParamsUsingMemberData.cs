using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProject.XUnitTest.ClassDataAndMemberData
{
    internal class PassDatatoParamsUsingMemberData : IEnumerable<object[]>
    {
        public static IEnumerable<object[]> GetParamData()
        {
            return new List<object[]>
            {
                new object[]{1},
                new object[]{2}
            };
        }
        public IEnumerator<object[]> GetEnumerator()
        {
            return (IEnumerator<object[]>)GetParamData();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
