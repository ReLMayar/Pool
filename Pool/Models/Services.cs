using System;
using System.Collections.Generic;

namespace Pool.Models
{
    public class Service
    {
        public int ServiceId { get; set; }

        /// <summary>
        /// Наименование услуги.
        /// </summary>
        private string serviceName { get; set; }
        public virtual ICollection<Client> Client { get; set; }

        public string ServiceName
        {
            get => serviceName;
            set
            {
                if (value != null)
                {
                    serviceName = value;
                }
                else { throw new ArgumentNullException(nameof(value), "Cannot be null!"); }
            }
        }
    }
}
