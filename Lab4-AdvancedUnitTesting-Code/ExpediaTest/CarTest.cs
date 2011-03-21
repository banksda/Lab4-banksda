using System;
using NUnit.Framework;
using Expedia;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestFixture()]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[SetUp()]
		public void SetUp()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[Test()]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [Test()]
        public void TestCarLocation()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();

            String car1 = "Here";
            String car2 = "There";
            using (mocks.Record())
            {
                mockDatabase.getCarLocation(5);
                LastCall.Return(car1);

                mockDatabase.getCarLocation(8);
                LastCall.Return(car2);
            }

            var target = new Car(5);
            target.Database = mockDatabase;

            String result;

            result = target.getCarLocation(5);
            Assert.AreEqual(result, car1);

            result = target.getCarLocation(8);
            Assert.AreEqual(result, car2);
        }

        [Test()]
        public void TestCarMileage()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            int miles = 42;
            mockDatabase.Miles = miles;

            var target = ObjectMother.Saab();
            target.Database = mockDatabase;

            int mileage = target.Mileage;
            Assert.AreEqual(mileage, miles);
        }
	}
}
