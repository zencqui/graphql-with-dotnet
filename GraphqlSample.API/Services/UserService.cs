using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GraphqlSample.API.Models;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GraphqlSample.API.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;
        public UserService(IEventBookingDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.UserCollection);
        }

        public Task<User> Add(User user)
        {
            if (string.IsNullOrWhiteSpace(user.email)) throw new ArgumentNullException(nameof(user.email));
            if (string.IsNullOrWhiteSpace(user.password)) throw new ArgumentNullException(nameof(user.password));

            var existingUser = _users.Find(x => x.email == user.email).FirstOrDefault();

            if (existingUser != null)
            {
                throw new Exception("Existing user!");
            }

            var salt = BCrypt.Net.BCrypt.GenerateSalt(12, 'a');
            var hasedPassword = BCrypt.Net.BCrypt.HashPassword(user.password, salt);
            user.password = hasedPassword;

            _users.InsertOneAsync(user);
            return Task.FromResult(user);
        }

        public Task<AuthData> Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));

            var existingUser = _users.Find(x => x.email == email).FirstOrDefault();

            if (existingUser == null)
                throw new Exception("User not found!");

            var result = BCrypt.Net.BCrypt.Verify(password, existingUser.password);

            if(!result)
                throw new Exception("Password not correct!");

            return Task.FromResult(GenerateAuthenticationResultForUser(existingUser));
        }

        public async Task<User> FindById(string userId)
        {
            return await _users.Find(x => x.Id == userId).SingleOrDefaultAsync();
        }

        private AuthData GenerateAuthenticationResultForUser(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.email),
                    new Claim("id", user.Id)
                }),
                Expires = DateTime.UtcNow.Add(new TimeSpan(1,0,0)),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthData
            {
                UserId = user.Id,
                Token = tokenHandler.WriteToken(token),
                Expiration = tokenDescriptor.Expires
            };
        }

        public async Task<List<User>> All()
        {
            return await _users.Find(new BsonDocument()).ToListAsync(); 
        }
    }
}