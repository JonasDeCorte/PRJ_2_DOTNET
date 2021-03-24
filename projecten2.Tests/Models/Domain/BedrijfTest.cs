using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace projecten2.Tests.Models.Domain
{
    public class BedrijfTest
    {
        public Bedrijf _bedrijf;
        private readonly string[] _nummers = { "+32 517 47 89 65", "+32 586 44 88 66", "+32 886 54 89 63" };
        public BedrijfTest()
        {
            
        }
       
       
        [Fact]
        public void NewBedrijfTypeSucces()
        {
            _bedrijf = new Bedrijf();
            Assert.IsType<Bedrijf>(_bedrijf);
        }
        [Fact]
        public void NewBedrijfTypeFail()
        {
            _bedrijf = null;
            Assert.IsNotType<Bedrijf>(_bedrijf);
        }
        [Fact]
        public void NewBedrijfTypeSuccesWithParameters()
        {

            _bedrijf = new Bedrijf("BEEGO", _nummers, "BELGIUM", "OVERAL");
            Assert.IsType<Bedrijf>(_bedrijf);
            Assert.Equal("BEEGO", _bedrijf.Bedrijfsnaam);
            Assert.Equal("BELGIUM", _bedrijf.LandHoofdzetel);
            Assert.Equal("OVERAL", _bedrijf.Straat);
        }
        [Fact]
        public void NewBedrijfTypeFailWithParameters()
        {
            string[] empty = { };
            Assert.Throws<ArgumentException>(() => new Bedrijf(string.Empty, empty, string.Empty, string.Empty));
        }
        [Fact]
        public void NewBedrijfType_Null_Fails()
        {
            Assert.Throws<ArgumentNullException>(() => new Bedrijf(null, null, null,null));
        }
       
    }
}
