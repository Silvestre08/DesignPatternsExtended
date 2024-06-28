using System;

namespace MyShop.Domain.Models
{
    public class Customer
    {
        private byte[] _profilePicture;

        public Guid CustomerId { get; set; }

        public string Name { get; set; }
        public string ShippingAddress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public byte[] ProfilePicture
        {
            get
            {
                if (_profilePicture == null)
                {
                    _profilePicture = ProfilePictureService.GetFor(Name);
                }
                return _profilePicture;
            }

            set
            {
                _profilePicture = value;
            }
        }

        public Customer()
        {
            CustomerId = Guid.NewGuid();
        }
    }
}
