using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public record Address // : ValueObject
    {

        //record bir nov valueobjectdiler eslinde
        //record bir obyektin bütövlükdə sabit/dəyişməz qalmasını təmin edir
        //record bizə dəyişməz obyektlər yaratmağa imkan verir
        //iki record tipin bir-birine beraber olmasi ucun butun propertylerin ve onlarin deyerlerinin bir-birine beraber olmasi lazimdi

        //iki recordu karşılaştırırken bu recordların aynı referansa sahip olması değil aynı dataya sahip olması dikkate alınmakta.
        // iki recordlarin qarsilasdirilmasi zamani onlarin referansini deyil de deyerleri qarsilasdirilir(property)
        //property SET INIT yazirigsa,sadece initializer edilerken yeni,obyekti yaradib datalari daxil ederken set oluna biler;
        //public int MyProperty { get; set; } = default!; <--- non-nullable olacagini deyirik

        //Valueobject yerine record secmeyimizin de sebeblerinden biri,o metodlari istifade etmeden qarsilasdirma ede bilirik


        public String Street { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Country { get; set; }
        public String ZipCode { get; set; }

        public Address()
        {

        }

        public Address(string street, string city, string state, string country, string zipCode)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        //ValueObject istifade etseydik bele olacagdi
        /*protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return ZipCode;
        }*/




    }

}
