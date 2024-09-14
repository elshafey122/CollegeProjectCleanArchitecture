using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProject.XUnitTest.MoqTest
{
    internal interface ICarMoqService
    {
        public bool AddCar(Car car);
        public bool RemoveCar(int? id);
        public List<Car> GetAll();
    }
}
