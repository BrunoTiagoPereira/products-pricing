using ProductsPricing.Core.Exceptions;
using ProductsPricing.Domain.Users.Entities;

namespace ProductsPricing.Domain.Products.Entities
{
    public class RefinedProduct : Product
    {

        private readonly List<Ingredient> _ingredients;
        public IReadOnlyCollection<Ingredient> Ingredients => _ingredients.AsReadOnly();

        protected RefinedProduct()
        {
            _ingredients = new List<Ingredient>();
        }
        public RefinedProduct(string name, decimal cost, decimal additionalValue, User user) : base(name, cost, additionalValue, user)
        {
            _ingredients = new List<Ingredient>();
        }

        public void AddIngredient(Product dependency)
        {
            if (dependency is null)
            {
                throw new DomainException("O ingrediente não pode ser nulo");
            }

            if (dependency.Id == Id)
            {
                throw new DomainException("Um produto base não pode ter ele mesmo como ingrediente.");
            }

            _ingredients.Add(new Ingredient(this, dependency));
        }
        public void AddIngredients(IEnumerable<Product> dependencies)
        {
            if(dependencies is null)
            {
                throw new DomainException("Os ingredientes não podem ser nulos");
            }

            foreach (var dependency in dependencies)
            {
                AddIngredient(dependency);
            }
        }

        public void RemoveIngredient(Product dependency)
        {
            if(dependency is null)
            {
                throw new DomainException("O ingrediente não pode ser nulo");
            }

            var dependencyIndex = _ingredients.FindIndex(x => x.DependencyId == dependency.Id);

            if(dependencyIndex != -1)
            {
                _ingredients.RemoveAt(dependencyIndex);
            }
            
        }

    }
}