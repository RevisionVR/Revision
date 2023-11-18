namespace Revision.Service.Validations;

public class EmailValidation
{
    public static bool IsValid(string email)
    {
        if (email.Contains("@") && email.Contains(".") && email.Length > 0)
            return true;

        return false;
    }
}