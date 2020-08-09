using Moq;
using Xunit;
using System;
using System.Linq;
using FizzWare.NBuilder;
using System.Threading.Tasks;
using System.Linq.Expressions;
using ToDoApp.BusinessEntity.Model;
using ToDoApp.BusinessLogic.Service;
using ToDoApp.BusinessLogic.Service.Interface;

namespace ToDoApp.Test.ServiceUnitTests
{
    public class TodoTaskServiceUnitTests
    {
        private readonly IToDoTaskService toDoTaskService;
        private readonly Mock<IRepository<ToDoTask>> todoTaskRepositoryMock;

        public TodoTaskServiceUnitTests()
        {
            todoTaskRepositoryMock = new Mock<IRepository<ToDoTask>>();
            toDoTaskService = new ToDoTaskService(todoTaskRepositoryMock.Object);
        }

        [Fact]
        public async Task AddAsync_Should_AddEntity()
        {
            // Arrange
            var entity = Builder<ToDoTask>.CreateNew()
                .With(x => x.Id = 0)
                .With(x => x.IsDone = false)
                .With(x => x.IsDeleted = false)
                .With(x => x.Text = "Test todo text")
                .Build();

            var newEntity = Builder<ToDoTask>.CreateNew()
                .With(x => x.Id = 1)
                .With(x => x.IsDone = false)
                .With(x => x.IsDeleted = false)
                .With(x => x.Text = "Test todo text")
                .Build();

            todoTaskRepositoryMock.Setup(mock => mock.AddAsync(entity))
                .ReturnsAsync(newEntity);

            // Act
            var result = await toDoTaskService.AddAsync(entity).ConfigureAwait(false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test todo text", entity.Text);
        }

        [Fact]
        public async Task UpdateAsync_Should_UpdateEntity()
        {
            // Arrange
            var updatedEntity = Builder<ToDoTask>.CreateNew()
                .With(x => x.Id = 1)
                .With(x => x.IsDone = false)
                .With(x => x.IsDeleted = false)
                .With(x => x.Text = "Test todo text modified")
                .Build();

            todoTaskRepositoryMock.Setup(mock => mock.UpdateAsync(updatedEntity))
                .ReturnsAsync(updatedEntity);

            // Act
            var result = await toDoTaskService.UpdateAsync(updatedEntity).ConfigureAwait(false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test todo text modified", updatedEntity.Text);
        }

        [Fact]
        public async Task SetTodoTaskDoneAsync_Should_SetEntityDone()
        {
            // Arrange
            var entity = Builder<ToDoTask>.CreateNew()
                .With(x => x.Id = 1)
                .With(x => x.IsDeleted = false)
                .With(x => x.IsDone = false)
                .Build();

            todoTaskRepositoryMock.Setup(mock => mock.GetByIdAsync(1))
                .ReturnsAsync(entity);

            todoTaskRepositoryMock.Setup(mock => mock.UpdateAsync(entity))
                .ReturnsAsync(entity);

            // Act
            var result = await toDoTaskService.SetTodoTaskDoneAsync(1).ConfigureAwait(false);

            // Assert
            Assert.NotNull(result);
            Assert.True(entity.IsDone);
        }

        [Fact]
        public async Task DeleteAsync_Should_DeleteEntityLogically()
        {
            // Arrange
            var entity = Builder<ToDoTask>.CreateNew()
                .With(x => x.Id = 1)
                .With(x => x.IsDeleted = false)
                .Build();

            todoTaskRepositoryMock.Setup(mock => mock.GetByIdAsync(1))
                .ReturnsAsync(entity);

            todoTaskRepositoryMock.Setup(mock => mock.UpdateAsync(entity))
                .ReturnsAsync(entity);

            // Act
            var result = await toDoTaskService.DeleteAsync(1).ConfigureAwait(false);

            // Assert
            Assert.NotNull(result);
            Assert.True(entity.IsDeleted);
        }

        [Fact]
        public async Task QueryByTypeAsync_Should_ReturnAllEntities()
        {
            // Arrange
            var entities = Builder<ToDoTask>.CreateListOfSize(6)
                .TheFirst(3)
                .With(x => x.IsDeleted = false)
                .With(x => x.IsDone = false)
                .TheNext(3)
                .With(x => x.IsDeleted = false)
                .With(x => x.IsDone = true)
                .Build();

            todoTaskRepositoryMock.Setup(mock => mock.GetByAsync(It.IsAny<Expression<Func<ToDoTask, bool>>>()))
                .ReturnsAsync(entities.ToList());

            // Act
            var result = await toDoTaskService.QueryByTypeAsync(ListQueryType.All).ConfigureAwait(false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(6, result.Count());
        }

        [Fact]
        public async Task QueryByTypeAsync_Should_ReturnDoneEntities()
        {
            // Arrange
            var entities = Builder<ToDoTask>.CreateListOfSize(3)
                .TheFirst(3)
                .With(x => x.IsDeleted = false)
                .With(x => x.IsDone = true)
                .Build();

            todoTaskRepositoryMock.Setup(mock => mock.GetByAsync(It.IsAny<Expression<Func<ToDoTask, bool>>>()))
                .ReturnsAsync(entities.ToList());

            // Act
            var result = await toDoTaskService.QueryByTypeAsync(ListQueryType.Done).ConfigureAwait(false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.True(result.All(x => x.IsDone == true));
        }

        [Fact]
        public async Task QueryByTypeAsync_Should_ReturnInProgressEntities()
        {
            // Arrange
            var entities = Builder<ToDoTask>.CreateListOfSize(3)
                .TheFirst(3)
                .With(x => x.IsDeleted = false)
                .With(x => x.IsDone = false)
                .Build();

            todoTaskRepositoryMock.Setup(mock => mock.GetByAsync(It.IsAny<Expression<Func<ToDoTask, bool>>>()))
                .ReturnsAsync(entities.ToList());

            // Act
            var result = await toDoTaskService.QueryByTypeAsync(ListQueryType.InProgress).ConfigureAwait(false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.True(result.All(x => x.IsDone == false));
        }
    }
}
