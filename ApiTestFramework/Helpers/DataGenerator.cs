using ApiTestFramework.Models;
using Bogus;

namespace ApiTestFramework.Helpers
{
    public static class DataGenerator
    {
        private static readonly Faker _faker = new Faker();

        public static string GetRandomName() => _faker.Name.FullName();
        public static string GetRandomEmail() => _faker.Internet.Email();
        public static string GetRandomPhone() => _faker.Phone.PhoneNumber();
        public static string GetRandomCity() => _faker.Address.City();
        
        // Генерация фейкового пользователя
        public static User GetRandomUser()
        {
            return new User
            {
                Name = GetRandomName(),
                Username = _faker.Internet.UserName(),
                Email = GetRandomEmail(),
                Phone = GetRandomPhone(),
                Website = _faker.Internet.DomainName()
            };
        }

        // Генерация фейкового заказа
        public static Order GetRandomOrder()
        {
            return new Order
            {
                UserId = _faker.Random.Int(1, 100),
                Title = _faker.Lorem.Sentence(5),
                Body = _faker.Lorem.Paragraph(2),
                Completed = _faker.Random.Bool()
            };
        }

        // Генерация списка заказов
        public static Order[] GetRandomOrders(int count = 5)
        {
            var orders = new Order[count];
            for (int i = 0; i < count; i++)
            {
                orders[i] = GetRandomOrder();
            }
            return orders;
        }
    }
}