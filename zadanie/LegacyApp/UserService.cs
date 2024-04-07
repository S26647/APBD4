using System;

namespace LegacyApp
{
    public class UserService
    {
        private IClientRepository _clientRepository;
        private ICreditService _creditService;
        private IUserValidator _userValidator;

        public UserService()
        {
            _clientRepository = new ClientRepository();
            _creditService = new UserCreditService();
            _userValidator = new UserValidator();
        }

        public UserService(IClientRepository clientRepository, ICreditService creditService, IUserValidator userValidator)
        {
            _clientRepository = clientRepository;
            _creditService = creditService;
            _userValidator = userValidator;
        }
        public bool AddUser(string firstName, string lastName, string email, DateTime birthDate, int clientId)
        {
            if (!_userValidator.Validate(firstName, lastName, email, birthDate))
            {
                return false;
            }
            
            
            var client = _clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = birthDate,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                int creditLimit = _creditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                creditLimit *= 2;
                user.CreditLimit = creditLimit;
            }
            else
            {
                user.HasCreditLimit = true;
                int creditLimit = _creditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                user.CreditLimit = creditLimit;
            }

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }
    }
}
