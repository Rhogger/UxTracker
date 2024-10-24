﻿using UxTracker.Core.Contexts.Account.ValueObjects;
using UxTracker.Core.Contexts.Shared.Entities;

namespace UxTracker.Core.Contexts.Account.Entities;

public class User : Entity
{
    protected User() { }

    public User(string name, Email email, Password password)
    {
        Name = name;
        Email = email;
        Password = password;
        IsActive = true;
    }
    
    public User(string name, Email email, Password password, List<Role> roles)
    {
        Name = name;
        Email = email;
        Password = password;
        Roles = roles;
        IsActive = true;
    }

    public string Name { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public Password Password { get; private set; } = null!;
    public List<Role> Roles { get; set; } = [];
    public bool IsActive { get; private set; } = false;

    public void UpdateName(string name)
    {
        Name = name;
    }
    
    public void UpdatePassword(string plainTextPassword)
    {
        Password = new Password(plainTextPassword);
    }

    public void UpdateStatusAccount() => IsActive = !IsActive;
}