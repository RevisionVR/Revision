namespace Revision.Service.Validations;

public class PasswordValidation
{
    //public static string Symbols { get; } = "~`!@#$%^&*()_-+={[}]|\\:;\"'<,>.?/";

    public static (bool IsValid, string Message) IsStrongPassword(string password)
    {
        if (password.Length < 8)
            return (IsValid: false, Message: "Password can not be than 8 characters");

        bool isUpperCaseExists = false;
        bool isLowerCaseExists = false;
        bool isNumberExists = false;
        //bool isCharacterExists = false;

        foreach (var item in password)
        {
            if (char.IsUpper(item))
                isUpperCaseExists = true;

            if (char.IsLower(item))
                isLowerCaseExists = true;

            if (char.IsDigit(item))
                isNumberExists = true;

            /*if (Symbols.Contains(item))
                isCharacterExists = true;*/
        }

        if (!isNumberExists)
            return (IsValid: false, Message: "Password should contain at least one Digit!");

        if (!isUpperCaseExists)
            return (IsValid: false, Message: "Password should contain at least one Upper case!");

        if (!isLowerCaseExists)
            return (IsValid: false, Message: "Password should contain at least one Lower case!");

        /*if (!isCharacterExists)
            return (IsValid: false, Message: "Password should contain at least one Symbol like (#@$%.!)!");*/

        return (IsValid: true, "");
    }
}
