using ToDoApi.Models;
using ToDoApi.Tests.UnitTests.Services;
using Xunit;

namespace ToDoApi.Tests.UnitTests.Models
{
    public class TodoTest
    {
        private readonly Todo _todo;

        public TodoTest()
        {
            _todo = new Todo();
        }

        [Fact]
        public void ReturnFalseGivenNameNull()
        {
            var result = DataAnnotationsValidator.TryValidate(_todo);
            Assert.False(result, "Name should not be null");
        }

        [Fact]
        public void ReturnFalseGivenNameEmpty()
        {
            _todo.Name = "";
            var result = DataAnnotationsValidator.TryValidate(_todo);
            Assert.False(result, "Name should not be empty");
        }

        [Fact]
        public void ReturnFalseGivenNameWhitSpace()
        {
            _todo.Name = "    ";
            var result = DataAnnotationsValidator.TryValidate(_todo);
            Assert.False(result, "Name should not be whit space");
        }

        [Fact]
        public void ReturnFalseGivenNameLengthMoreThan10()
        {
            _todo.Name = ".Net Core Unit Test";
            var result = DataAnnotationsValidator.TryValidate(_todo);
            Assert.False(result, "Name should not be more than 10");
        }

        [Fact]
        public void ReturnFalseGivenNameNotEn()
        {
            _todo.Name = ".เนต คอ";
            var result = DataAnnotationsValidator.TryValidate(_todo);
            Assert.False(result, "Name should be English");
        }

        [Fact]
        public void ReturnTrueGivenNameEnAndLessThan10()
        {
            _todo.Name = "Teera Nai";
            var result = DataAnnotationsValidator.TryValidate(_todo);
            Assert.True(result, "Name should be English and less than 10");
        }
    }
}