using LegacyApp;

namespace LegacyAppTests;

public class UserSeriveTests
{
    [Fact]
    public void AddUser_Should_Return_False_When_Email_Without_At_And_Dot()
    {
        string firstName = "John";
        string lastName = "Doe";
        DateTime birthDate = new DateTime(1980, 1, 1);
        int clientId = 1;
        string email = "doe";
        var service = new UserService();

        bool result = service.AddUser(firstName, lastName, email, birthDate, clientId);
        
        Assert.Equal(false, result);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_FirstName_Empty()
    {
        string firstName = null;
        string lastName = "Doe";
        DateTime birthDate = new DateTime(1980, 1, 1);
        int clientId = 1;
        string email = "doe";
        var service = new UserService();

        bool result = service.AddUser(firstName, lastName, email, birthDate, clientId);
        
        Assert.Equal(false, result);
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_Age_Lower_Than_21()
    {
        string firstName = "John";
        string lastName = "Doe";
        DateTime birthDate = new DateTime(2020, 1, 1);
        int clientId = 1;
        string email = "doe@gmail.com";
        var service = new UserService();

        bool result = service.AddUser(firstName, lastName, email, birthDate, clientId);
        
        Assert.Equal(false, result);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Normal_Client_Has_CreditLimit_Under_500()
    {
        
        string firstName = "Jane";
        string lastName = "Kowalski";
        string email = "jane@gmail.com";
        DateTime dateOfBirth = new DateTime(1990, 9, 30);
        int clientId = 1;
        
        ICreditService creditSerice = new FakeCreditService();
        IClientRepository clientRepository = new FakeClientRepository();
        IUserValidator userValidator = new UserValidator();
        UserService userService = new UserService(clientRepository,creditSerice,userValidator);
        bool result = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);
        Assert.Equal(false,result);
    }
    
    [Fact]
    public void AddUser_Should_Return_True_When_Normal_Client_Has_CreditLimit_Over_500()
    {
        
        string firstName = "John";
        string lastName = "Doe";
        DateTime birthDate = new DateTime(1990, 1, 1);
        int clientId = 4;
        string email = "doe@gmail.com";
        ICreditService creditSerice = new FakeCreditService();
        IClientRepository clientRepository = new FakeClientRepository();
        IUserValidator userValidator = new UserValidator();
        UserService userService = new UserService(clientRepository,creditSerice,userValidator);
        
        bool result = userService.AddUser(firstName, lastName, email, birthDate, clientId);
        
        Assert.Equal(true,result);
    }
    [Fact]
    public void AddUser_Should_Return_True_When_Very_Important_Client()
    {
        
        string firstName = "John";
        string lastName = "Doe";
        DateTime birthDate = new DateTime(1990, 1, 1);
        int clientId = 2;
        string email = "doe@gmail.com";
        ICreditService creditSerice = new FakeCreditService();
        IClientRepository clientRepository = new FakeClientRepository();
        IUserValidator userValidator = new UserValidator();
        UserService userService = new UserService(clientRepository,creditSerice,userValidator);
        
        bool result = userService.AddUser(firstName, lastName, email, birthDate, clientId);
        
        Assert.Equal(true,result);
    }
    [Fact]
    public void AddUser_Should_Return_True_When_Important_Client()
    {
        
        string firstName = "John";
        string lastName = "Doe";
        DateTime birthDate = new DateTime(1990, 1, 1);
        int clientId = 3;
        string email = "doe@gmail.com";
        ICreditService creditSerice = new FakeCreditService();
        IClientRepository clientRepository = new FakeClientRepository();
        IUserValidator userValidator = new UserValidator();
        UserService userService = new UserService(clientRepository,creditSerice,userValidator);
        
        bool result = userService.AddUser(firstName, lastName, email, birthDate, clientId);
        
        Assert.Equal(true,result);
    }
}