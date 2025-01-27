using TechSolutions.Domain.Validation;

namespace TechSolutions.Domain.Entities
{
    public sealed class Store
    {
        public Guid Id { get; private set; }
        public string? Name { get; private set; }

        public Store(string name)
        {
            ValidateDomain(name);
        }

        public void Update(string name)
        {
            ValidateDomain(name);
        }

        private void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(name.Length > 100, "O nome deve ter até 100 caracteres");

            Name = name;
        }
    }
}
