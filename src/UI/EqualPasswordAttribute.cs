using System.ComponentModel.DataAnnotations;

namespace UI
{
    public class EqualPasswordAttribute : ValidationAttribute
    {
        private string _password;
        public EqualPasswordAttribute(string password) : base("Passowords must be same.")
        {
            _password = password;
        }
        public override bool IsValid(object? value)
        {
            return _password.Equals(value);
        }

    }
}
