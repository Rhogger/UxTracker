using System.Text.Json.Serialization;
using UxTracker.Core.Contexts.Account.Enums;
using UxTracker.Core.Contexts.Account.ValueObjects;

namespace UxTracker.Core.Contexts.Account.Entities;

public class Reviewer: User
{
    protected Reviewer() { }
    public Reviewer(Email email, Password password, Sex sex, DateTime birthDate, string country,
        string state, string city) : base(email, password)
    {
        Sex = sex;
        BirthDate = birthDate;
        Country = country;
        State = state;
        City = city;
    }

    public Sex Sex { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string Country { get; private set; }
    public string State { get; private set; }
    public string City { get; private set; }

    public void UpdateCountry(string country) => Country = country;
    public void UpdateState(string state) => State = state;
    public void UpdateCity(string city) => City = city;
    public void UpdateSex(Sex sex) => Sex = sex;
    
    public bool IsNewCountry(string country) => !Country.Equals(country);
    public bool IsNewState(string state) => !State.Equals(state);
    public bool IsNewCity(string city) => !City.Equals(city);
    public bool IsNewSex(Sex sex) => !Sex.Equals(sex);

}