using NUnit.Framework;
using SnowE2E.Test.Helper;

namespace SnowE2E.Test.Test.APITest
{
    [TestFixture]
    public class RequestItemTests
    {
        private string _RITMNumber;

        [Test]
        public void Test_CreateRITM()
        {
            // Arrange
            string catItem = "Samsung Galaxy S7";
            string openedBy = "Abel Tuter";

            // Act
            _RITMNumber = APIHelper.CreateRITM(catItem, openedBy);
            TestDataHelper.CreatedRITMNumbers.Add(_RITMNumber);

            // Assert
            // Verify the RITM is not null
            Assert.That(_RITMNumber, Is.Not.Null);
        }

        
    }
}