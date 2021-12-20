using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Validations;
using System.ComponentModel.DataAnnotations;

namespace WebAPILinbis.Test
{
    [TestClass]
    public class FirstCapitalLetterAttributeTests
    {
        [TestMethod]
        public void LowercaseFirstLetter_Error()
        {
            //Preparation
            var firstCapitalLetter = new FirstCapitalLetterAttribute();
            var value = "yosbany";
            var valContext = new ValidationContext(new { Nombre = value });

            //Execution
            var result = firstCapitalLetter.GetValidationResult(value, valContext);

            //Verification
            Assert.AreEqual("La primera letra debe ser mayúscula", result.ErrorMessage);
        }
        [TestMethod]
        public void NullValue_NoError()
        {
            //Preparation
            var firstCapitalLetter = new FirstCapitalLetterAttribute();
            string value = null;
            var valContext = new ValidationContext(new { Nombre = value });

            //Execution
            var result = firstCapitalLetter.GetValidationResult(value, valContext);

            //Verification
            Assert.IsNull(result);
        }
    }
}
