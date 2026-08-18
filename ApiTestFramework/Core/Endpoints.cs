namespace ApiTestFramework.Core
{
    public static class Endpoints
    {
        public const string Users = "/users";
        public const string Posts = "/posts";
        public const string Comments = "/comments";
        public const string Todos = "/todos";
        public const string Orders = "/posts";
        
        public static string UserById(string id) => $"/users/{id}";
        public static string OrderById(string id) => $"/posts/{id}";
    }
}