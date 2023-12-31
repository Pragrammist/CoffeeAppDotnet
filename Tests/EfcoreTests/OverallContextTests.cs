using EFCore.Models;
using FluentAssertions;

namespace EfcoreTests;

[Collection("DbContexts")]
public class OverallContextTests
{
    DbContextsFixutre _dbContexts;
    public OverallContextTests(DbContextsFixutre dbContextsFixture)
    {
        _dbContexts = dbContextsFixture;
    } 

    [Fact]
    public void ContextCreationTest()
    {
        _dbContexts.OverallDbContext.Should().NotBeNull();   
    }

    [Fact]
    public void AddingUserTest()
    {
        AddUserTestData();

        _dbContexts.OverallDbContext.Users.Count().Should().BeGreaterThan(0);
    }

    [Fact]
    public void AddingCoffeeTest()
    {
        AddCoffeeTestData();
        
        _dbContexts.OverallDbContext.Coffees.Count().Should().BeGreaterThan(0);
    }

    [Fact]
    public void AddingCommentTest()
    {
        AddCommentTestData();
        _dbContexts.OverallDbContext.Comments.Count().Should().BeGreaterThan(0);
    }

    [Fact]
    public void AddingRefreshTokenTest()
    {
        AddUserTestData();
        var user = _dbContexts.OverallDbContext.Users.First();
        var token = GetRefreshToken(user);
        _dbContexts.OverallDbContext.Add(token);

        _dbContexts.OverallDbContext.SaveChanges();
        _dbContexts.OverallDbContext.RefreshTokens.Count().Should().BeGreaterThan(0);
    }

    void AddCommentTestData()
    {
        AddUserTestData();


        AddCoffeeTestData();


        var fUser = FirstUser();
        var fCoffe = FirstCoffee();

        _dbContexts.OverallDbContext.Comments.Add(GetCommentModel(fCoffe.Id, fUser.Id));
        _dbContexts.OverallDbContext.SaveChanges();
    }

    
    RefreshToken GetRefreshToken(User user) => new RefreshToken{
      Expires = DateTimeOffset.Now,
      Token = "supertoken",
      User = user

    };
    Coffee GetCoffeeTestData() => new Coffee{
        AvgScore = 4.23f,
        CreatedDate = DateTime.Now,
        Name = "coffeName",
        Note = "note",
        Photos = "phot.jpg;phto2.jpg",
        ScoreCount = 20
    };
    User GetUserTestData() => new User{
        Email = "some@em",
        Login = "eqwef",
        Password = "supperhash",
        CreatedDate = DateTime.Now
    };


    Comment GetCommentModel(int coffeId, int userId) => new Comment{
        CoffeeId = coffeId,
        UserId = userId,
        CreatedDate = DateTime.Now,
        Photos = "photos.png;p.jpg",
        Score = 5,
        Text = "WHOA!"
    };
    void AddCoffeeTestData()
    {
        _dbContexts.OverallDbContext.Coffees.Add(GetCoffeeTestData());
        _dbContexts.OverallDbContext.SaveChanges();
    }

    void AddUserTestData ()
    {
        _dbContexts.OverallDbContext.Users.Add(GetUserTestData());
        _dbContexts.OverallDbContext.SaveChanges();
    }

    User FirstUser() => _dbContexts.OverallDbContext.Users.First();

    Coffee FirstCoffee() => _dbContexts.OverallDbContext.Coffees.First();
}