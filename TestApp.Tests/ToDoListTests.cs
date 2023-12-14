using System;

using NUnit.Framework;

using TestApp.Todo;

namespace TestApp.Tests;

[TestFixture]
public class ToDoListTests
{
    private ToDoList _toDoList = null!;
    
    [SetUp]
    public void SetUp()
    {
        this._toDoList = new();
    }
    
    [Test]
    public void Test_AddTask_TaskAddedToToDoList()
    {
        // Arrange
        string nameTask = "my first task";
        DateTime dateTime = DateTime.Today;

        // Act
        this._toDoList.AddTask(nameTask, dateTime);     // Добавяме задачата.

        string result = this._toDoList.DisplayTasks();  // Изписваме задачата.

        // Assert
        Assert.That(result, Does.Contain("To-Do List:"));   // Първи ред съдържа ли това.
        Assert.That(result, Does.Contain($"[ ] {nameTask} - Due:")); // Втори ред съдържа ли това.
    }

    [Test]
    public void Test_CompleteTask_TaskMarkedAsCompleted()
    {
        // Arrange
        string nameTask = "my second task";
        DateTime dateTime = DateTime.Today;

        // Act
        this._toDoList.AddTask(nameTask, dateTime);
        this._toDoList.CompleteTask(nameTask);

        string result = this._toDoList.DisplayTasks();

        // Assert
        Assert.That(result, Does.Contain("To-Do List:"));
        Assert.That(result, Does.Contain($"[✓] {nameTask} - Due:"));
    }

    [Test]
    public void Test_CompleteTask_TaskNotFound_ThrowsArgumentException()
    {
        // Arrange
        string nameTask = "my third task";
        DateTime dateTime = DateTime.Today;

        // Act & Assert
        this._toDoList.AddTask(nameTask , dateTime);
        
        Assert.That(() => this._toDoList.CompleteTask("my first task"), Throws.TypeOf<ArgumentException>());

    }

    [Test]
    public void Test_DisplayTasks_NoTasks_ReturnsEmptyString()
    {
        // Arrange
        string expectedOutput = "To-Do List:";

        // Act
        string result = this._toDoList.DisplayTasks();

        // Assert
        Assert.That(result, Is.EqualTo(expectedOutput));
    }

    [Test]
    public void Test_DisplayTasks_WithTasks_ReturnsFormattedToDoList()
    {
        // Arrange
        string nameTask = "my first task";
        DateTime dateTime = DateTime.Today;
        string nameTask2 = "my second task";
        DateTime dateTime2 = DateTime.Today;
       
        // Act
        this._toDoList.AddTask(nameTask, dateTime);
        this._toDoList.AddTask(nameTask2, dateTime2);
        this._toDoList.CompleteTask(nameTask);

        string result = this._toDoList.DisplayTasks();

        // Assert
        Assert.That(result, Does.Contain("To-Do List:"));
        Assert.That(result, Does.Contain($"[✓] {nameTask} - Due:"));
        Assert.That(result, Does.Contain($"[ ] {nameTask2} - Due:"));
    }
}
