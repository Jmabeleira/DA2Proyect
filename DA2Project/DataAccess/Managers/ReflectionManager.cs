using DataAccess.Configuration;
using Domain.Interfaces;
using Domain.Models;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Managers
{
    public class ReflectionManager : IReflectionManager
    {
        private readonly IConfiguration _configuration;

        public ReflectionManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public List<Promotion> GetAllPromotions()
        {
            string dllFolderPath = _configuration[ConfigurationParameter.ReflectionPath.ToString()];

            List<Promotion> promotions = new List<Promotion>();

            if (dllFolderPath is not null)
            {
                string[] dllFiles = Directory.GetFiles(dllFolderPath, "*.dll");

                foreach (string dllFile in dllFiles)
                {
                    Assembly assembly = Assembly.Load(File.ReadAllBytes(dllFile));

                    Type[] types = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Promotion))).ToArray();

                    foreach (Type type in types)
                    {
                        Promotion promotion = (Promotion)Activator.CreateInstance(type);
                        promotions.Add(promotion);
                    }
                }
            }

            return promotions;
        }
    }
}
