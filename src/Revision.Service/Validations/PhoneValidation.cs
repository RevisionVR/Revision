namespace Revision.Service.Validations;

public class PhoneValidation
{
    public static bool IsValid(string phoneNumber)
    {
        if (!(phoneNumber.StartsWith("+998") && phoneNumber.Length == 13))
            return false;

        for (int i = 1; i < phoneNumber.Length; i++)
            if (!char.IsDigit(phoneNumber[i]))
                return false;

        return true;
    }
}