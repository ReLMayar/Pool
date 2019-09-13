using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pool.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        private string fName { get; set; }
        private string lName { get; set; }
        private byte age { get; set; }
        private string numberPhone { get; set; }

        /// <summary>
        /// Наличие или отсутствие мед. справки.
        /// </summary>
        /// 
        public byte MedicalExamination { get; set; }
        /// <summary>
        /// Способ оплаты.
        /// </summary>
        private string payment { get; set; }
      
        public virtual Service Service { get; set; }
        public virtual Subscription Subscription { get; set; }

        [ForeignKey("Service")]
        public int? ServiceId { get; set; }

        [ForeignKey("Subscription ")]
        public int? SubscriptionId { get; set; }

        public string FName
        {
            get => fName;
            set
            {
                if(value != null)
                {
                    fName = value;
                }
                else { throw new ArgumentNullException(nameof(value), "Cannot be null!"); }
            }
        }

        public string LName
        {
            get => lName;
            set
            {
                if (value != null)
                {
                    lName = value;
                }
                else { throw new ArgumentNullException(nameof(value), "Cannot be null!"); }
            }
        }

        public byte Age
        {
            get => age;
            set
            {
                if (value != default)
                {
                    age = value;
                }
                else { throw new ArgumentNullException(nameof(value), "Age cannot be zero!"); }
            }
        }

        public string NumberPhone
        {
            get => numberPhone;
            set
            {
                if (value != null)
                {
                    numberPhone = value;
                }
                else { throw new ArgumentNullException(nameof(value), "Cannot be null!"); }
            }
        }

        public string Payment
        {
            get => payment;
            set
            {
                if (value != null)
                {
                    payment = value;
                }
                else { throw new ArgumentNullException(nameof(value), "Cannot be null!"); }
            }
        }
    }
}
