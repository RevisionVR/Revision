namespace Revision.Service.Validations;

public class EmailValidation
{
    public static bool IsValid(string email)
    {
        if (email.Length < 0 && email.Contains("@") && email.Contains("."))
        {
            return false;
        }
        return true;
    }
}
