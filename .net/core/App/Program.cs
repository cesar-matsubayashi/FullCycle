using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtGenerator
{
    public class MainClass
    {
        public string Generate(string secret, string issuer, string audience, string sub, string jti, long iat)
        {
            // Convert the Unix timestamp to a .NET DateTime
            DateTimeOffset now = DateTimeOffset.FromUnixTimeSeconds(iat);

            // Create a new JWT security token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Issuer, issuer),
                    new Claim(ClaimTypes.Audience, audience),
                    new Claim(ClaimTypes.Name, sub),
                    new Claim(ClaimTypes.NameIdentifier, jti)
                }),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    SecurityAlgorithms.HmacSha256Signature),
                IssuedAt = now,
                Id = jti
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Get the JWT string
            return tokenHandler.WriteToken(token);
        }
    }
}


// // See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

// // var counter = 0;
// // var max = args.Length is not 0 ? Convert.ToInt32(args[0]) : -1;
// // while (max is -1 || counter < max)
// // {
// //     Console.WriteLine($"Counter: {++counter}");
// //     await Task.Delay(TimeSpan.FromMilliseconds(1_000));
// // }

// // using System;

// // namespace BidimensionalArrayExample
// // {
// //     class Program
// //     {
// //         static void Main(string[] args)
// //         {
//             // Create a 2D array with 3 rows and 4 columns
//             int[,] numbers = new int[3, 4]
//             {
//                 { 1, 2, 3, 4 },
//                 { 5, 6, 7, 8 },
//                 { 9, 10, 11, 12 }
//             };

//             // Display the original array
//             Console.WriteLine("Original array:");
//             PrintArray(numbers);

//             // Modify the array by adding 1 to each element
//             for (int i = 0; i < numbers.GetLength(0); i++)
//             {
//                 for (int j = 0; j < numbers.GetLength(1); j++)
//                 {
//                     numbers[i, j] += 1;
//                 }
//             }

//             // Display the modified array
//             Console.WriteLine("\nModified array:");
//             PrintArray(numbers);
//         //}

//         static void PrintArray(int[,] array)
//         {
//             for (int i = 0; i < array.GetLength(0); i++)
//             {
//                 for (int j = 0; j < array.GetLength(1); j++)
//                 {
//                     Console.Write(array[i, j] + " ");
//                 }
//                 Console.WriteLine();
//             }
//         }
// //     }
// // }