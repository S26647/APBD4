using System;

namespace LegacyApp;

public class UserValidator : IUserValidator
{
    public bool Validate(string firstName, string lastName, string email, DateTime birthDate)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
        {
            return false;
        }

        if (!email.Contains("@") && !email.Contains("."))
        {
            return false;
        }
        
        var now = DateTime.Now;
        int age = now.Year - birthDate.Year;
        if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day)) age--;
        if (age < 21)
        {
            return false;
        }

        return true;
    }
}