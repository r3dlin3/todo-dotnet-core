using ToDoApi.Models;
using ToDoApi.Tests.UnitTests.Services;
using Xunit;

namespace ToDoApi.Tests.UnitTests.Models
{
    public class TodoItemTest
    {
        private readonly TodoItem _todoItem;

        public TodoItemTest()
        {
            _todoItem = new TodoItem();
        }

        [Fact]
        public void ReturnFalseGivenNameNull()
        {
            var result = DataAnnotationsValidator.TryValidate(_todoItem);
            Assert.False(result, "Name should not be null");
        }

        [Fact]
        public void ReturnFalseGivenNameEmpty()
        {
            _todoItem.Name = "";
            var result = DataAnnotationsValidator.TryValidate(_todoItem);
            Assert.False(result, "Name should not be empty");
        }

        [Fact]
        public void ReturnFalseGivenNameWhitSpace()
        {
            _todoItem.Name = "    ";
            var result = DataAnnotationsValidator.TryValidate(_todoItem);
            Assert.False(result, "Name should not be whit space");
        }

        [Fact]
        public void ReturnFalseGivenNameLengthMoreThan10()
        {
            _todoItem.Name = ".Net Core Unit Test";
            var result = DataAnnotationsValidator.TryValidate(_todoItem);
            Assert.False(result, "Name should not be more than 10");
        }

        [Fact]
        public void ReturnFalseGivenNameNotEn()
        {
            _todoItem.Name = ".เนต คอ";
            var result = DataAnnotationsValidator.TryValidate(_todoItem);
            Assert.False(result, "Name should be English");
        }

        [Fact]
        public void ReturnTrueGivenNameEnAndLessThan10()
        {
            _todoItem.Name = "Teera Nai";
            var result = DataAnnotationsValidator.TryValidate(_todoItem);
            Assert.True(result, "Name should be English and less than 10");
        }
    }
}