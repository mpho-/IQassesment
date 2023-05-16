using ACMEIndustries.Models;

namespace BusinessLogic.Interface
{
    public interface ILookUpManager
    {
        List<Gender>? GetGenders();
    }
}