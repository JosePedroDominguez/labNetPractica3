using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab.Practica.EF.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Practica.EF.Logic.Tests
{
    [TestClass()]
    public class metodoParaUnitTestTests
    {
        [TestMethod()]
        public void SumaTest()
        {
            // Arrange
            metodoParaUnitTest suma = new metodoParaUnitTest();

            // Act
            int result = suma.CalcularSuma(3, 5);

            // Assert
            Assert.AreEqual(8, result);
        }
    }
}
