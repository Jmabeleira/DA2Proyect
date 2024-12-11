using DataAccess.Managers;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests.ReflectionTests
{
    [TestClass]
    public class ReflectionTest
    {
        [TestMethod]
        public void GetAllPromotionsTest()
        {
            Mock<IConfiguration> configurationMock = new Mock<IConfiguration>();

            configurationMock.Setup(c => c["ReflectionPath"]).Returns("C:\\Users\\Joaquin\\Desktop\\ORT\\SEXTO SEMESTRE\\DISEÑO DE APLICACIONES 2\\OBLIGATORIO\\275315_243181_238826\\DA2Project\\PromosDlls\\Release\\net6.0");

            ReflectionManager reflectionManager = new ReflectionManager(configurationMock.Object);

            var promos = reflectionManager.GetAllPromotions();

            Assert.AreEqual(promos.Count, 4);
        }
    }
}
