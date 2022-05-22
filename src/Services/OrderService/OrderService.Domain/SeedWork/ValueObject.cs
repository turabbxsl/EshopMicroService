using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderService.Domain.SeedWork
{
    public abstract class ValueObject
    {
        // Referance tipli obyektlerin bir-birileri arasinda bir qarsilasdirma edileceyi zaman ve ya ikisinin esit olub-olmadigi bilgisi kontrol edileceyi zaman referanslarinin mi yoxsa o referans icerisinde tutulan classlarin propertilerinin mi bir-birilerine esit olmasini kontrol edeceyimiz metodlar bu sinif icerisindedir.


        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }

            return ReferenceEquals(left, null) || left.Equals(right);
        }



        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !EqualOperator(left, right);
        }



        protected abstract IEnumerable<object> GetEqualityComponents();


        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }


        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x * y);
        }


        public ValueObject GetCopy()
        {
            return MemberwiseClone() as ValueObject;
        }

    }

}
