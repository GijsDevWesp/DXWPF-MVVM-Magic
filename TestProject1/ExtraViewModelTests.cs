using Microsoft.VisualStudio.TestTools.UnitTesting;
using Webinar.ViewModels;

namespace TestProject1
{
    [TestClass]
    public class ExtraViewModelTests
    {
        [TestMethod]
        public void ShowErrorMessage()
        {
            // Arrange
            var extraVM = ExtraViewModel.Create();

            // Act
            extraVM.ShowErrorMessage();

            // Assert
            //Assert.
        }
    }
}
