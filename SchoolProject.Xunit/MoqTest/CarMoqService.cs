﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProject.XUnitTest.MoqTest
{
    internal class CarMoqService : ICarMoqService
    {
        public List<Car> carList;
        public CarMoqService(List<Car> cars)
        {
            carList = cars;
        }
        public bool AddCar(Car car)
        {
            carList.Add(car);
            return true;
        }

        public List<Car> GetAll()
        {
            return carList;
        }

        public bool RemoveCar(int? id)
        {
            if (id == null) return false;
            var car = carList.Find(x => x.Id == id);
            if (car == null) return false;
            return carList.Remove(car);
        }
    }
}
