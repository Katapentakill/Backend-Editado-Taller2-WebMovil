using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.EntityFrameworkCore;
using project_dotnet7_api.Src.Models;

namespace project_dotnet7_api.Src.Data
{
    public class DataSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DataContext>();

                
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(
                        new Role { Type = "Admin" },
                        new Role { Type = "Usuario" }
                    );
                }

                if (!context.Genders.Any())
                {
                    context.Genders.AddRange(
                        new Gender { Type = "Masculino"},
                        new Gender { Type = "Femenino"},
                        new Gender { Type = "Prefiero no decirlo"},
                        new Gender { Type = "Otros"}
                    ); 
                }

                if (!context.ProductTypes.Any())
                {
                    context.ProductTypes.AddRange(
                        new ProductType { Type = "Tecnología"},
                        new ProductType { Type = "Electrohogar"},
                        new ProductType { Type = "Juguetería"},
                        new ProductType { Type = "Ropa"},
                        new ProductType { Type = "Muebles"},
                        new ProductType { Type = "Comida"},
                        new ProductType { Type = "Libros"}
                    ); 
                }

                context.SaveChanges();

                var generatedRuts = new HashSet<string>();

                if(!context.Users.Any())
                {
                    
                    var user = new User { 
                        Rut = "20416699-4", 
                        Name = "Ignacio Mancilla",
                        Birthday = new DateTime(2000,10,25), 
                        Email = "ignacio.mancilla@gmail.com",
                        GenderId = 1,
                        Password = BCrypt.Net.BCrypt.HashPassword("P4ssw0rd"),
                        IsActive = true,
                        RoleId = 1
                    };

                    generatedRuts.Add(user.Rut);
                    context.Users.Add(user);

                    var faker = new Faker<User>()
                    .RuleFor(u => u.Rut, f => GenerateUniqueRandomRut(generatedRuts))
                    .RuleFor(u => u.Name, f => f.Person.FullName)
                    .RuleFor(u => u.Birthday, f => f.Date.Past(30, DateTime.Now.AddYears(-18)))
                    .RuleFor(u => u.Email, f => f.Internet.Email())
                    .RuleFor(u => u.Password, f => BCrypt.Net.BCrypt.HashPassword("password"))
                    .RuleFor(u => u.IsActive, f => f.Random.Bool())
                    .RuleFor(u => u.RoleId, f => 2)
                    .RuleFor(u => u.GenderId, f => f.Random.Number(1, 4));

                    var users = faker.Generate(20); // Genera 10 usuarios aleatorios

                    context.Users.AddRange(users);
                }

                if(!context.Products.Any())
                {
                    var faker = new Faker<Product>()
                    .RuleFor(u => u.Name, f => f.Commerce.ProductName())
                    .RuleFor(u => u.Price, f => f.Random.Number(1000, 50000))
                    .RuleFor(u => u.Stock, f => f.Random.Number(100, 1000))
                    .RuleFor(u => u.ImgUrl, f => "https://res.cloudinary.com/dvqzbb1rs/image/upload/v1716130625/Taller_IDWM/hgohlblkjf9ucukczyiy.png")
                    .RuleFor(u => u.ImgId,f => "Taller_IDWM/hgohlblkjf9ucukczyiy")
                    .RuleFor(u => u.ProductTypeId, f => f.Random.Number(1, 7));

                    var products = faker.Generate(20); // Genera 10 usuarios aleatorios

                    context.Products.AddRange(products);
                }

                context.SaveChanges();


        
            }

            
        }
    private static string GenerateUniqueRandomRut(HashSet<string> existingRuts)
    {
        string rut;
        do
        {
            rut = GenerateRandomRut();
        } while (existingRuts.Contains(rut));

        existingRuts.Add(rut);
        return rut;
    }

    private static string GenerateRandomRut()
    {
        Random random = new();
        int rutNumber = random.Next(1, 99999999); // Número aleatorio de 7 o 8 dígitos
        int verificator = CalculateRutVerification(rutNumber);
        string verificatorStr = verificator.ToString();
        if(verificator == 10){
            verificatorStr = "k";
        }

        return $"{rutNumber}-{verificatorStr}";
    }

    // Método para calcular el dígito verificador de un RUT
    private static int CalculateRutVerification(int rutNumber)
    {
        int[] coefficients = { 2, 3, 4, 5, 6, 7 };
        int sum = 0;
        int index = 0;

        while (rutNumber != 0)
        {
            sum += rutNumber % 10 * coefficients[index];
            rutNumber /= 10;
            index = (index + 1) % 6;
        }

        int verification = 11 - (sum % 11);
        return verification == 11 ? 0 : verification;
    }
    }
}