using projecten2.Models.Domain;
using projecten2.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace projecten2.Tests.Models.Domain
{
    public class ContractTypeTest
    {   public  ContractType _contractType;
       
        public ContractTypeTest()
        {
         
        } 
        [Fact]
        public void NewContractTypeSucces()
        {
            _contractType = new ContractType();
            Assert.IsType<ContractType>(_contractType);
        }
        [Fact]
        public void NewContractTypeFail()
        {
            _contractType = null; 
           Assert.IsNotType<ContractType>(_contractType);
        }
        [Fact]
        public void NewContractTypeSuccesWithParameters()
        {
            _contractType = new ContractType("test", "open", DateTime.Now, DateTime.Now.AddDays(30), DateTime.Now.AddDays(15), 500.00);          
            Assert.IsType<ContractType>(_contractType);
            Assert.Equal("test", _contractType.Naam);
            Assert.Equal("open", _contractType.Status);
            Assert.Equal(500.00, _contractType.Prijs);
        }
        [Fact]
        public void NewContractTypeFailWithParameters()
        {         
            Assert.Throws<ArgumentException>(() => new ContractType(string.Empty, string.Empty, DateTime.Now, DateTime.Now.AddDays(30), DateTime.Now.AddDays(15), 500.00));    
        }
        [Fact]
        public void NewContractType_Null_Fails()
        {
            Assert.Throws<ArgumentNullException>(() => new ContractType(null,null, DateTime.Now, DateTime.Now.AddDays(30), DateTime.Now.AddDays(15), 500.00));
        }
        [Fact]
        public void NewContractType_Prijs_Fails()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new ContractType("test", "open", DateTime.Now, DateTime.Now.AddDays(30), DateTime.Now.AddDays(15), -500.00));
        }


    }
}
