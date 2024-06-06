using AdressValidation.Model;

namespace AdressValidation.Service
{
    public interface IUSPSClient
    {
        bool ValidateAddress(AddressModel address);
    }
}
