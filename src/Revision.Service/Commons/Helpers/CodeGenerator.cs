namespace Revision.Service.Commons.Helpers;

public class CodeGenerator
{
    public static int CodeGeneratorPhoneNumber()
    {
        Random random = new Random();

        return random.Next(10000, 99999);
    }
}
