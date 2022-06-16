using MakonisTechTest.Core;
using MakonisTechTest.Data;
using MakonisTechTest.Models;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MakonisTechTest.UnitTest
{
    public class PersonServiceTest
    {
        private Mock<IDataContact> mockDataProvider = null;
        private List<PersonViewModel> listOfPerson;
        private IPersonService serviceToTest;

        public PersonServiceTest()
        {
            SetUpMock();
            serviceToTest = new PersonService(mockDataProvider.Object);
        }

        private void SetUpMock()
        {
            listOfPerson = new List<PersonViewModel> { new PersonViewModel { FirstName = "Test", LastName = "Data", Id = Guid.NewGuid() },
            new PersonViewModel { FirstName = "Test1", LastName = "Data1", Id = Guid.NewGuid() } };

            mockDataProvider = new Mock<IDataContact>();
            mockDataProvider.Setup(s => s.Write(null)).Throws<ArgumentNullException>();
            mockDataProvider.Setup(s => s.Read<PersonViewModel>()).ReturnsAsync(listOfPerson);
            mockDataProvider.Setup(s => s.Remove(It.IsAny<Guid>())).ReturnsAsync(true);
        }

        [Fact]
        public void ShouldSetUpRequiredObjectWithMoQ()
        {
            Assert.NotNull(mockDataProvider);
            Assert.NotNull(serviceToTest);
            Assert.NotNull(listOfPerson);
            Assert.NotEmpty(listOfPerson);
        }

        [Fact]
        public async void ShouldReturnListOfPersonsOnRead()
        {
            Assert.NotNull(serviceToTest);
            Assert.NotNull(listOfPerson);
            Assert.NotEmpty(listOfPerson);
            Assert.NotEmpty(await serviceToTest.GetPersonList());
        }

        [Fact]
        public async void ShouldReturnTrueOnRemove()
        {
            Assert.NotNull(serviceToTest);
            Assert.True(await serviceToTest.DeletePerson(Guid.NewGuid()));
        }


        [Fact]
        public async System.Threading.Tasks.Task ShouldThrowArgumentNullExceptionOnWriteWithNull()
        {
            Assert.NotNull(serviceToTest);
            await Assert.ThrowsAsync<ArgumentNullException>(() => serviceToTest.AddPerson(null));
        }
    }
}
