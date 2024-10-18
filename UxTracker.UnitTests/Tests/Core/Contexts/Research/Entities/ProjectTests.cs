using System.Reflection;
using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Research.Enums;
using UxTracker.Core.Contexts.Review.Entities;

namespace UxTracker.UnitTests.Tests.Core.Contexts.Research.Entities;

[TestClass]
public class ProjectTests
{
    private Project _project;
        
    [TestInitialize]
    public void SetUp()
    {
        _project = new Project(
            Guid.NewGuid(), 
            "Test Title", 
            "Test Description", 
            DateTime.UtcNow.AddDays(1), 
            null,
            PeriodType.Daily, 
            5, 
            "hash123",
            new List<Relatory>());
    }

    [TestMethod]
    public void UpdateTitle_Should_Update_Title_If_Status_Not_Finished()
    {
        // Act
        _project.UpdateTitle("New Title");

        // Assert
        Assert.AreEqual("New Title", _project.Title);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "Não pode alterar o título após o fim da pesquisa")]
    public void UpdateTitle_Should_Throw_Exception_If_Status_Is_Finished()
    {
        // Arrange
        typeof(Project).GetProperty("StartDate").SetValue(_project, DateTime.UtcNow.AddDays(-4));
        typeof(Project).GetProperty("EndDate").SetValue(_project, DateTime.UtcNow.AddDays(-1));

        // Act
        _project.UpdateTitle("New Title");
    }

    [TestMethod]
    public void UpdateDescription_Should_Update_Description_If_Status_Not_Finished()
    {
        // Act
        _project.UpdateDescription("New Description");

        // Assert
        Assert.AreEqual("New Description", _project.Description);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "Não pode alterar a descrição após o fim da pesquisa")]
    public void UpdateDescription_Should_Throw_Exception_If_Status_Is_Finished()
    {
        // Arrange
        typeof(Project).GetProperty("StartDate").SetValue(_project, DateTime.UtcNow.AddDays(-4));
        typeof(Project).GetProperty("EndDate").SetValue(_project, DateTime.UtcNow.AddDays(-1));

        // Act
        _project.UpdateDescription("New Description");
    }

    [TestMethod]
    public void UpdateStartDate_Should_Update_StartDate_If_Status_Not_Finished()
    {
        // Act
        var newDate = DateTime.UtcNow.AddDays(2);
        _project.UpdateStartDate(newDate);

        // Assert
        Assert.AreEqual(newDate, _project.StartDate);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "Não pode alterar a data inicial após o fim da pesquisa")]
    public void UpdateStartDate_Should_Throw_Exception_If_Status_Is_Finished()
    {
        // Arrange
        typeof(Project).GetProperty("StartDate").SetValue(_project, DateTime.UtcNow.AddDays(-4));
        typeof(Project).GetProperty("EndDate").SetValue(_project, DateTime.UtcNow.AddDays(-1));

        // Act
        _project.UpdateStartDate(DateTime.UtcNow.AddDays(2));
    }

    [TestMethod]
    public void UpdatePeriodType_Should_Update_PeriodType_If_Status_Not_Finished()
    {
        // Act
        _project.UpdatePeriodType(PeriodType.Weekly);

        // Assert
        Assert.AreEqual(PeriodType.Weekly, _project.PeriodType);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "Não pode alterar o tipo de período após o fim da pesquisa")]
    public void UpdatePeriodType_Should_Throw_Exception_If_Status_Is_Finished()
    {
        // Arrange
        typeof(Project).GetProperty("StartDate").SetValue(_project, DateTime.UtcNow.AddDays(-4));
        typeof(Project).GetProperty("EndDate").SetValue(_project, DateTime.UtcNow.AddDays(-1));

        // Act
        _project.UpdatePeriodType(PeriodType.Weekly);
    }

    [TestMethod]
    public void UpdateSurveyCollections_Should_Update_SurveyCollections_If_Status_Not_Finished()
    {
        // Act
        _project.UpdateSurveyCollections(10);

        // Assert
        Assert.AreEqual(10, _project.SurveyCollections);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "Não pode alterar a quantidade de coleta após o fim da pesquisa")]
    public void UpdateSurveyCollections_Should_Throw_Exception_If_Status_Is_Finished()
    {
        // Arrange
        typeof(Project).GetProperty("StartDate").SetValue(_project, DateTime.UtcNow.AddDays(-4));
        typeof(Project).GetProperty("EndDate").SetValue(_project, DateTime.UtcNow.AddDays(-1));

        // Act
        _project.UpdateSurveyCollections(10);
    }

    [TestMethod]
    public void UpdateConsentTermHash_Should_Update_ConsentTermHash()
    {
        // Act
        _project.UpdateConsentTermHash("newHash");

        // Assert
        Assert.AreEqual("newHash", _project.ConsentTermHash);
    }

    [TestMethod]
    public void UpdateRelatories_Should_Update_Relatories()
    {
        // Arrange
        var newRelatories = new List<Relatory>
        {
            new Relatory(),
            new Relatory()
        };

        // Act
        _project.UpdateRelatories(newRelatories);

        // Assert
        Assert.AreEqual(2, _project.Relatories.Count);
    }
    
    [TestMethod]
    public void SetStatus_Should_Return_NotStarted_When_StartDate_Is_InFuture()
    {
        // Arrange
        var project = new Project(Guid.NewGuid(), "Test", "Test", DateTime.UtcNow.AddDays(1), null, PeriodType.Daily, 5, "hash123", new List<Relatory>());

        // Act
        var status = project.Status;

        // Assert
        Assert.AreEqual(Status.NotStarted, status);
    }

    [TestMethod]
    public void SetStatus_Should_Return_InProgress_When_Current_Date_Is_Between_StartAndEndDate()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(-1);
        var endDate = DateTime.UtcNow.AddDays(1);
        var project = new Project(Guid.NewGuid(), "Test", "Test", startDate,endDate, PeriodType.Daily, 5, "hash123", new List<Relatory>());

        // Act
        var status = project.Status;

        // Assert
        Assert.AreEqual(Status.InProgress, status);
    }

    [TestMethod]
    public void SetStatus_Should_Return_Finished_When_EndDate_Is_InPast()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(-2);
        var endDate = DateTime.UtcNow.AddDays(-1);
        var project = new Project(Guid.NewGuid(), "Test", "Test", startDate, endDate, PeriodType.Daily, 5, "hash123", new List<Relatory>());

        // Act
        var status = project.Status;

        // Assert
        Assert.AreEqual(Status.Finished, status);
    }
    
    [TestMethod]
    public void Constructor_Should_Initialize_All_Properties_Correctly()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var title = "Test Title";
        var description = "Test Description";
        var startDate = DateTime.UtcNow.AddDays(1);
        var periodType = PeriodType.Daily;
        var surveyCollections = 5;
        var consentTermHash = "hash123";
        var relatories = new List<Relatory>();

        // Act
        var project = new Project(userId, title, description, startDate, null, periodType, surveyCollections, consentTermHash, relatories);

        // Assert
        Assert.AreEqual(userId, project.UserId);
        Assert.AreEqual(title, project.Title);
        Assert.AreEqual(description, project.Description);
        Assert.AreEqual(startDate, project.StartDate);
        Assert.AreEqual(periodType, project.PeriodType);
        Assert.AreEqual(surveyCollections, project.SurveyCollections);
        Assert.AreEqual(consentTermHash, project.ConsentTermHash);
        Assert.AreEqual(relatories, project.Relatories);
    }

    [TestMethod]
    public void SetStatus_Should_Set_Correct_Status_When_Not_Started()
    {
        // Arrange
        var project = new Project(Guid.NewGuid(), "Title", "Description", DateTime.UtcNow.AddDays(1), null, PeriodType.Daily, 5, "hash123", new List<Relatory>());

        // Act
        var status = project.Status;

        // Assert
        Assert.AreEqual(Status.NotStarted, status);
    }

    [TestMethod]
    public void SetStatus_Should_Set_Correct_Status_When_In_Progress()
    {
        // Arrange
        var project = new Project(Guid.NewGuid(), "Title", "Description", DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1), PeriodType.Daily, 5, "hash123", new List<Relatory>());

        // Act
        var status = project.Status;

        // Assert
        Assert.AreEqual(Status.InProgress, status);
    }

    [TestMethod]
    public void SetStatus_Should_Set_Correct_Status_When_Finished()
    {
        // Arrange
        var project = new Project(Guid.NewGuid(), "Title", "Description", DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(-1), PeriodType.Daily, 5, "hash123", new List<Relatory>());

        // Act
        var status = project.Status;

        // Assert
        Assert.AreEqual(Status.Finished, status);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "Não pode alterar a data inicial se já iniciaram as avaliações")]
    public void UpdateStartDate_Should_Throw_Exception_If_Have_Deliveries()
    {
        // Arrange
        _project.UpdateStartDate(DateTime.UtcNow.AddDays(-4));
        var review = new Rate(Guid.NewGuid(), _project.Id, 5, "Great Project");
        _project.Reviews.Add(review);
        
        // Act
        _project.UpdateStartDate(DateTime.UtcNow.AddDays(1));
    }

    [TestMethod]
    public void IsNewTitle_Should_Return_True_If_Title_Is_Different()
    {
        // Act
        var result = _project.IsNewTitle("Different Title");

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsNewTitle_Should_Return_False_If_Title_Is_Same()
    {
        // Act
        var result = _project.IsNewTitle("Test Title");

        // Assert
        Assert.IsFalse(result);
    }
}