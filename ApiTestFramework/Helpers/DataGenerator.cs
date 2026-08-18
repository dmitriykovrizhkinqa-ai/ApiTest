using ApiTestFramework.Models;
using Bogus;

namespace ApiTestFramework.Helpers
{
    public static class DataGenerator
    {
        private static readonly Faker Faker = new();

        public static string GetRandomName() => Faker.Name.FullName();
        public static string GetRandomEmail() => Faker.Internet.Email();
        public static string GetRandomPhone() => Faker.Phone.PhoneNumber();
        public static string GetRandomCity() => Faker.Address.City();
        
        // Генерация фейкового пользователя
        public static User GetRandomUser()
        {
            return new User
            {
                Name = GetRandomName(),
                Username = Faker.Internet.UserName(),
                Email = GetRandomEmail(),
                Phone = GetRandomPhone(),
                Website = Faker.Internet.DomainName()
            };
        }

        // Генерация фейкового заказа
        public static Order GetRandomOrder()
        {
            return new Order
            {
                UserId = Faker.Random.Int(1, 100),
                Title = Faker.Lorem.Sentence(5),
                Body = Faker.Lorem.Paragraph(2),
                Completed = Faker.Random.Bool()
            };
        }

        // Генерация списка заказов
        public static Order[] GetRandomOrders(int count = 5)
        {
            var orders = new Order[count];
            for (var i = 0; i < count; i++)
            {
                orders[i] = GetRandomOrder();
            }
            return orders;
        }
    }
}