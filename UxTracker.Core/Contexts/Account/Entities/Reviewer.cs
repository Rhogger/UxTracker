using UxTracker.Core.Contexts.Account.Enums;
using UxTracker.Core.Contexts.Account.ValueObjects;
using UxTracker.Core.Contexts.Review.Entities;

namespace UxTracker.Core.Contexts.Account.Entities;

public class Reviewer: User
{
    protected Reviewer() { }
    public Reviewer(Email email, Password? password, Sex sex, DateTime? birthDate, string? country,
        string? state, string? city) : base(email, password)
    {
        Sex = sex;
        BirthDate = birthDate;
        Country = country;
        State = state;
        City = city;
    }

    public Sex Sex { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public string? Country { get; private set; }
    public string? State { get; private set; }
    public string? City { get; private set; }
    public List<Rate> Reviews { get; private set; } = null!;

    public void UpdateCountry(string? country) => Country = country;
    public void UpdateState(string? state) => State = state;
    public void UpdateCity(string? city) => City = city;
    
    public bool IsNewCountry(string? country) => Country != null && !Country.Equals(country);
    public bool IsNewState(string? state) => State != null && !State.Equals(state);
    public bool IsNewCity(string? city) => City != null && !City.Equals(city);
}