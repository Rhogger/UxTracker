using System.Reflection;
using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Research.Enums;
using UxTracker.Core.Contexts.Review.Entities;

namespace UxTracker.UnitTests.Tests.Core.Contexts.Research.Entities;

[TestClass]
public class ProjectTests
{
    private Project? _project;
        
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
            new List<Relatory>
            {
                new Relatory { Title = "Relatório 1" },
                new Relatory { Title = "Relatório 2" }
            });
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
    public void IsNewTitle_Should_Return_False_If_Title_Is_Same()
    {
        // Act
        var result = _project?.IsNewTitle("Test Title");

        // Assert
        Assert.IsFalse(result);
    }
    
    [TestMethod]
    public void IsNewTitle_Should_Return_True_If_Title_Is_New()
    {
        // Act
        var isNewTitle = _project?.IsNewTitle("Another Title");

        // Assert
        Assert.IsTrue(isNewTitle);
    }
    
    [TestMethod]
    public void UpdateTitle_Should_Update_Title_If_Status_Not_Finished()
    {
        // Act
        _project?.UpdateTitle("New Title");

        // Assert
        Assert.AreEqual("New Title", _project?.Title);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "Não pode alterar o título após o fim da pesquisa")]
    public void UpdateTitle_Should_Throw_Exception_If_Status_Is_Finished()
    {
        // Arrange
        typeof(Project).GetProperty("StartDate")?.SetValue(_project, DateTime.UtcNow.AddDays(-4));
        typeof(Project).GetProperty("EndDate")?.SetValue(_project, DateTime.UtcNow.AddDays(-1));

        // Act
        _project?.UpdateTitle("New Title");
    }
    
    [TestMethod]
    public void UpdateDescription_Should_Update_Description_If_Status_Not_Finished()
    {
        // Act
        _project?.UpdateDescription("New Description");

        // Assert
        Assert.AreEqual("New Description", _project?.Description);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "Não pode alterar a descrição após o fim da pesquisa")]
    public void UpdateDescription_Should_Throw_Exception_If_Status_Is_Finished()
    {
        // Arrange
        typeof(Project).GetProperty("StartDate")?.SetValue(_project, DateTime.UtcNow.AddDays(-4));
        typeof(Project).GetProperty("EndDate")?.SetValue(_project, DateTime.UtcNow.AddDays(-1));

        // Act
        _project?.UpdateDescription("New Description");
    }
    
    [TestMethod]
    public void IsNewDescription_Should_Return_True_If_Description_Is_New()
    {
        // Act
        var isNewDescription = _project?.IsNewDescription("Another Description");

        // Assert
        Assert.IsTrue(isNewDescription);
    }

    [TestMethod]
    public void IsNewDescription_Should_Return_False_If_Description_Is_Same()
    {
        // Act
        var isNewDescription = _project?.IsNewDescription("Test Description");

        // Assert
        Assert.IsFalse(isNewDescription);
    }
    
    [TestMethod]
    public void UpdateStartDate_Should_Update_StartDate_If_Status_Not_Finished()
    {
        // Act
        var newDate = DateTime.UtcNow.AddDays(2);
        _project?.UpdateStartDate(newDate);

        // Assert
        Assert.AreEqual(newDate, _project?.StartDate);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "Não pode alterar a data inicial após o fim da pesquisa")]
    public void UpdateStartDate_Should_Throw_Exception_If_Status_Is_Finished()
    {
        // Arrange
        typeof(Project).GetProperty("StartDate")?.SetValue(_project, DateTime.UtcNow.AddDays(-4));
        typeof(Project).GetProperty("EndDate")?.SetValue(_project, DateTime.UtcNow.AddDays(-1));

        // Act
        _project?.UpdateStartDate(DateTime.UtcNow.AddDays(2));
    }
    
    [TestMethod]
    [ExpectedException(typeof(Exception), "Não pode alterar a data inicial se já iniciaram as avaliações")]
    public void UpdateStartDate_Should_Throw_Exception_If_Have_Deliveries()
    {
        // Arrange
        _project?.UpdateStartDate(DateTime.UtcNow.AddDays(-4));
        if (_project != null)
        {
            var review = new Rate(Guid.NewGuid(), _project.Id,0, 5, "Great Project");
            _project.Reviews.Add(review);
        }

        // Act
        _project?.UpdateStartDate(DateTime.UtcNow.AddDays(1));
    }
    
    [TestMethod]
    public void UpdateStartDate_Should_Update_StartDate_If_No_Reviews_Submitted_And_Not_InProgress()
    {
        // Arrange
        var newStartDate = DateTime.UtcNow.AddDays(2);

        // Act
        _project?.UpdateStartDate(newStartDate);

        // Assert
        Assert.AreEqual(newStartDate, _project?.StartDate);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "Não pode alterar a data inicial se já iniciaram as avaliações")]
    public void UpdateStartDate_Should_Throw_Exception_If_Has_Reviews_And_InProgress()
    {
        // Arrange
        _project = new Project(
            Guid.NewGuid(),
            "Test Title",
            "Test Description",
            DateTime.UtcNow.AddDays(-2),
            DateTime.UtcNow.AddDays(1),  
            PeriodType.Daily,
            5,
            "hash123",
            new List<Relatory>()
        );
        
        var reviews = new List<Rate>
        {
            new Rate(Guid.NewGuid(), Guid.NewGuid(), 0, 8, "Great Project")
        };
    
        _project.Reviews.AddRange(reviews);

        // Act & Assert
        _project.UpdateStartDate(DateTime.UtcNow.AddDays(2));
    }

    [TestMethod]
    public void UpdateStartDate_Should_Throw_Exception_If_Project_Already_In_Progress_And_No_Reviews()
    {
        // Arrange
        typeof(Project).GetProperty("StartDate")?.SetValue(_project, DateTime.UtcNow.AddDays(-1));
        typeof(Project).GetProperty("EndDate")?.SetValue(_project, DateTime.UtcNow.AddDays(1));

        // Act & Assert
        try
        {
            _project?.UpdateStartDate(DateTime.UtcNow.AddDays(2));
        }
        catch (Exception ex)
        {
            Assert.AreEqual("Não pode alterar a data inicial após o fim da pesquisa", ex.Message);
        }
    }
    
    [TestMethod]
    public void UpdateStartDate_Should_Not_Throw_Exception_When_Project_Is_Not_InProgress_And_No_Reviews()
    {
        // Arrange
        var newStartDate = DateTime.UtcNow.AddDays(2);

        // Act
        _project?.UpdateStartDate(newStartDate);

        // Assert
        Assert.AreEqual(newStartDate, _project?.StartDate);
    }

    [TestMethod]
    public void IsNewStartDate_Should_Return_True_If_StartDate_Is_Different()
    {
        // Act
        var result = _project.IsNewStartDate(DateTime.UtcNow.AddDays(1));

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsNewStartDate_Should_Return_False_If_StartDate_Is_Same()
    {
        // Act
        var result = _project.IsNewStartDate(_project.StartDate);

        // Assert
        Assert.IsFalse(result);
    }
    
    [TestMethod]
    public void UpdateEndDate_Should_Update_EndDate_If_Not_InProgress_And_No_Reviews_Submitted()
    {
        // Arrange
        var newEndDate = DateTime.UtcNow.AddDays(7);

        // Act
        _project?.UpdateEndDate(newEndDate, _project.StartDate);

        // Assert
        Assert.AreEqual(newEndDate, _project?.EndDate);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "Não pode alterar a data final para menor ou igual a data inicial")]
    public void UpdateEndDate_Should_Throw_Exception_If_EndDate_Is_Less_Than_Or_Equal_To_StartDate()
    {
        // Arrange
        _project = new Project(
            Guid.NewGuid(),
            "Test Title",
            "Test Description",
            DateTime.UtcNow.AddDays(1), 
            DateTime.UtcNow.AddDays(5), 
            PeriodType.Daily,
            5,
            "hash123",
            new List<Relatory>()
        );
        
        var reviews = new List<Rate>
        {
            new Rate(Guid.NewGuid(), Guid.NewGuid(), 0, 8, "Great Project")
        };
        _project.Reviews.AddRange(reviews);

        // Act
        _project.UpdateEndDate(DateTime.UtcNow.AddDays(0), _project.StartDate);
    }

    [TestMethod]
    public void UpdateEndDate_Should_Throw_Exception_If_Project_Already_In_Progress()
    {
        // Arrange
        typeof(Project).GetProperty("StartDate")?.SetValue(_project, DateTime.UtcNow.AddDays(-1));
        typeof(Project).GetProperty("EndDate")?.SetValue(_project, DateTime.UtcNow.AddDays(5));

        // Act & Assert
        try
        {
            _project?.UpdateEndDate(DateTime.UtcNow.AddDays(7), _project.StartDate);
        }
        catch (Exception ex)
        {
            Assert.AreEqual("Não pode alterar a data final após o fim da pesquisa", ex.Message);
        }
    }

    [TestMethod]
    public void UpdateEndDate_Should_Not_Throw_Exception_If_Project_Not_Started_And_No_Reviews()
    {
        // Arrange
        var newEndDate = DateTime.UtcNow.AddDays(7);

        // Act
        _project?.UpdateEndDate(newEndDate, _project.StartDate);

        // Assert
        Assert.AreEqual(newEndDate, _project?.EndDate);
    }

    [TestMethod]
    public void UpdateEndDate_Should_Throw_Exception_If_EndDate_Before_StartDate()
    {
        // Arrange
        var invalidEndDate = DateTime.UtcNow.AddDays(-3);

        // Act & Assert
        try
        {
            _project?.UpdateEndDate(invalidEndDate, DateTime.UtcNow);
        }
        catch (Exception ex)
        {
            Assert.AreEqual("Não pode alterar a data final para menor ou igual a data inicial", ex.Message);
        }
    }

    [TestMethod]
    public void UpdateEndDate_Should_Update_EndDate_To_Valid_Future_Date()
    {
        // Arrange
        var newEndDate = DateTime.UtcNow.AddDays(10);

        // Act
        _project?.UpdateEndDate(newEndDate, _project.StartDate);

        // Assert
        Assert.AreEqual(newEndDate, _project?.EndDate);
    }
    
    [TestMethod]
    public void IsNewEndDate_Should_Return_True_If_EndDate_Is_Different()
    {
        // Act
        var result = _project.IsNewEndDate(DateTime.UtcNow.AddDays(2));

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsNewEndDate_Should_Return_False_If_EndDate_Is_Same()
    {
        // Act
        var result = _project.IsNewEndDate(_project.EndDate);

        // Assert
        Assert.IsFalse(result);
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
    public void SetStatus_Should_Return_InProgress_When_EndDate_Is_Null_And_StartDate_Has_Passed()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(-1);
        var project = new Project(Guid.NewGuid(), "Test", "Test", startDate, null, PeriodType.Daily, 5, "hash123", new List<Relatory>());

        // Act
        var status = project.Status;

        // Assert
        Assert.AreEqual(Status.InProgress, status);
    }

    [TestMethod]
    public void UpdatePeriodType_Should_Update_PeriodType_If_Status_Not_Finished()
    {
        // Act
        if (_project == null) return;
        _project.UpdatePeriodType(PeriodType.Weekly);

        // Assert
        Assert.AreEqual(PeriodType.Weekly, _project.PeriodType);
    }

    [TestMethod]
    public void UpdatePeriodType_Should_Update_If_InProgress_And_No_Evaluations()
    {
        // Arrange
        typeof(Project).GetProperty("StartDate")?.SetValue(_project, DateTime.UtcNow.AddDays(-1)); 
        typeof(Project).GetProperty("EndDate")?.SetValue(_project, DateTime.UtcNow.AddDays(2)); 
        _project = new Project(Guid.NewGuid(), "Test", "Test", DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(2), PeriodType.Daily, 5, "hash123", new List<Relatory>());

        // Act
        _project?.UpdatePeriodType(PeriodType.Weekly);

        // Assert
        Assert.AreEqual(PeriodType.Weekly, _project?.PeriodType); 
    }
    
    [TestMethod]
    [ExpectedException(typeof(Exception), "Não pode alterar o tipo de período se já iniciaram as avaliações")]
    public void UpdatePeriodType_Should_Throw_Exception_If_InProgress_And_Has_Evaluations()
    {
        // Arrange
        var evaluations = new List<Relatory> { new Relatory() }; 
        _project = new Project(
            Guid.NewGuid(), 
            "Test", 
            "Test", 
            DateTime.UtcNow.AddDays(-1),
            DateTime.UtcNow.AddDays(2),
            PeriodType.Daily, 
            5, 
            "hash123", 
            evaluations
        );
        
        _project.Reviews.Add(new Rate(Guid.NewGuid(), Guid.NewGuid(), 0, 8, "Feedback"));

        // Act
        _project.UpdatePeriodType(PeriodType.Weekly);
    }
    
    [TestMethod]
    [ExpectedException(typeof(Exception), "Não pode alterar o tipo de período após o fim da pesquisa")]
    public void UpdatePeriodType_Should_Throw_Exception_If_Status_Is_Finished()
    {
        // Arrange
        typeof(Project).GetProperty("StartDate")?.SetValue(_project, DateTime.UtcNow.AddDays(-4));
        typeof(Project).GetProperty("EndDate")?.SetValue(_project, DateTime.UtcNow.AddDays(-1));

        // Act
        _project?.UpdatePeriodType(PeriodType.Weekly);
    }
    
    [TestMethod]
    public void IsNewPeriodType_Should_Return_True_If_PeriodType_Is_Different()
    {
        // Act
        var result = _project.IsNewPeriodType(PeriodType.Weekly);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsNewPeriodType_Should_Return_False_If_PeriodType_Is_Same()
    {
        // Act
        var result = _project.IsNewPeriodType(PeriodType.Daily);

        // Assert
        Assert.IsFalse(result);
    }
    
    [TestMethod]
    public void UpdateSurveyCollections_Should_Update_SurveyCollections_If_Status_Not_Finished()
    {
        // Act
        _project?.UpdateSurveyCollections(10);

        // Assert
        if (_project != null) Assert.AreEqual(10, _project.SurveyCollections);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "Não pode alterar a quantidade de coleta após o fim da pesquisa")]
    public void UpdateSurveyCollections_Should_Throw_Exception_If_Status_Is_Finished()
    {
        // Arrange
        typeof(Project).GetProperty("StartDate")?.SetValue(_project, DateTime.UtcNow.AddDays(-4));
        typeof(Project).GetProperty("EndDate")?.SetValue(_project, DateTime.UtcNow.AddDays(-1));

        // Act
        _project?.UpdateSurveyCollections(10);
    }
    
    [TestMethod]
    [ExpectedException(typeof(Exception), "A quantidade de coleta informada não pode ser inferior a quantidade máxima de avaliações submetidas por um participante")]
    public void UpdateSurveyCollections_Should_Throw_Exception_If_InProgress_And_Less_Than_LastSurveyCollection()
    {
        // Arrange
        typeof(Project).GetProperty("EndDate")?.SetValue(_project, DateTime.UtcNow.AddDays(1)); 
        typeof(Project).GetProperty("StartDate")?.SetValue(_project, DateTime.UtcNow.AddDays(-10)); 

        var userId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        
        var reviews = new List<Rate>
        {
            new Rate(userId, projectId, 0, 1, "Avaliação de A"),
            new Rate(userId, projectId, 1, 2, "Avaliação de B"),
            new Rate(userId, projectId, 2, 5, "Avaliação de C") 
        };
        _project.Reviews.AddRange(reviews);

        // Act
        _project.UpdateSurveyCollections(2);
    }
    
    [TestMethod]
    public void IsNewSurveyCollections_Should_Return_True_If_SurveyCollections_Is_Different()
    {
        // Arrange
        _project.UpdateSurveyCollections(5);
        int newSurveyCollections = 10; 

        // Act
        var result = _project.IsNewSurveyCollections(newSurveyCollections);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsNewSurveyCollections_Should_Return_False_If_SurveyCollections_Is_Same()
    {
        // Arrange
        int sameSurveyCollections = 5; 
        _project.UpdateSurveyCollections(sameSurveyCollections); 

        // Act
        var result = _project.IsNewSurveyCollections(sameSurveyCollections);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "Não pode alterar o termo após o fim da pesquisa")]
    public void UpdateConsentTermHash_Should_Throw_Exception_If_Project_Is_Finished()
    {
        // Arrange
        typeof(Project).GetProperty("StartDate")?.SetValue(_project, DateTime.UtcNow.AddDays(-5));
        typeof(Project).GetProperty("EndDate")?.SetValue(_project, DateTime.UtcNow.AddDays(-1));

        // Act
        _project.UpdateConsentTermHash("newHash");
    }
    
    [TestMethod]
    [ExpectedException(typeof(Exception), "Não pode alterar o termo se já iniciaram as avaliações")]
    public void UpdateConsentTermHash_Should_Throw_Exception_If_InProgress_And_Has_Deliveries()
    {
        // Arrange
        typeof(Project).GetProperty("EndDate")?.SetValue(_project, DateTime.UtcNow.AddDays(1));
        typeof(Project).GetProperty("StartDate")?.SetValue(_project, DateTime.UtcNow.AddDays(-10));
        
        var reviews = new List<Rate>
        {
            new Rate(Guid.NewGuid(), Guid.NewGuid(), 0, 1, "Avaliação de A"),
        };
        _project.Reviews.AddRange(reviews);

        // Act
        _project.UpdateConsentTermHash("newHash");
    }
    
    [TestMethod]
    public void IsNewConsentTerm_Should_Return_True_If_Hash_Is_Different()
    {
        // Act
        var result = _project.IsNewConsentTerm("newHash");

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsNewConsentTerm_Should_Return_False_If_Hash_Is_Same()
    {
        // Act
        var result = _project.IsNewConsentTerm(_project.ConsentTermHash);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsNewConsentTerm_Should_Return_False_If_Hash_Is_Null_Or_Empty()
    {
        // Act
        var result1 = _project.IsNewConsentTerm(null); 
        var result2 = _project.IsNewConsentTerm(string.Empty); 

        // Assert
        Assert.IsFalse(result1);
        Assert.IsFalse(result2);
    }
    
    [TestMethod]
    public void UpdateRelatories_Should_Update_Relatories()
    {
        // Arrange
        var newRelatories = new List<Relatory>
        {
            new Relatory { Title = "Relatório 1" },
            new Relatory { Title = "Relatório 2" }
        };

        // Act
        _project.UpdateRelatories(newRelatories);

        // Assert
        CollectionAssert.AreEqual(newRelatories, _project.Relatories);
    }

    [TestMethod]
    public void UpdateRelatories_Should_Handle_Null_List()
    {
        // Act
        _project.UpdateRelatories(null);

        // Assert
        Assert.IsNull(_project.Relatories);
    }

    [TestMethod]
    public void IsNewRelatories_Should_Return_True_If_Relatory_Length_Is_Different()
    {
        // Act
        var result = _project.IsNewsRelatories(new List<string> { "1", "2", "3" }); 

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsNewRelatories_Should_Return_False_If_All_Relatories_Match()
    {
        // Arrange
        var relatoryId1 = _project.Relatories[0].Id;
        var relatoryId2 = _project.Relatories[0].Id;

        // Act
        var result = _project.IsNewsRelatories(new List<string> { relatoryId1.ToString(), relatoryId2.ToString() });

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsNewRelatories_Should_Return_True_If_Any_Relatory_Does_Not_Match()
    {
        // Arrange
        var relatoryId = Guid.NewGuid();
        
        // Act
        var result = _project.IsNewsRelatories(new List<string> { relatoryId.ToString() });

        // Assert
        Assert.IsTrue(result);
    }
}