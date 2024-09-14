using CollegeProject.XUnitTest.MoqTest;
using FluentAssertions;
using Moq;

namespace CollegeProject.XUnitTest
{
    public class MoqOperationTest
    {
        private readonly Mock<List<Car>> mockCars = new();

        [Fact]
        public void Add_Car()
        {
            var car = new Car()
            {
                Id = 1,
                Color = "red",
                Name = "mercedes"
            };
            CarMoqService carmockservice = new CarMoqService(mockCars.Object);
            var addcar = carmockservice.AddCar(car);
            var carList = carmockservice.GetAll();
            addcar.Should().Be(true);
            carList.Should().HaveCount(1);
            carList.Should().NotBeNull();
        }
    }
}

