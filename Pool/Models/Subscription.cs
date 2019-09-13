using System;
using System.Collections.Generic;

namespace Pool.Models
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }

        /// <summary>
        /// Количество занятий.
        /// </summary>
        private byte quantityOfOccupation { get; set; }

        /// <summary>
        /// Срок.
        /// </summary>
        private string time { get; set; }
        public virtual ICollection<Client> Client { get; set; }

        public byte QuantityOfOccupation
        {
            get => quantityOfOccupation;
            set
            {
                if (value != default)
                {
                    quantityOfOccupation = value;
                }
                else { throw new ArgumentNullException(nameof(value), "Cannot be zero!"); }
            }
        }

        public string Time
        {
            get => time;
            set
            {
                if (value != null)
                {
                    time = value;
                }
                else { throw new ArgumentNullException(nameof(value), "Cannot be null!"); }
            }
        }
    }
}
