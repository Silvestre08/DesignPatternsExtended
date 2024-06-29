using MyShop.Domain.Models;

namespace MyShop.Infrastructure.Lazy.Proxies
{
    internal class CustomerProxy : Customer
    {
         public override byte[] ProfilePicture
        {
            get
            {
                if (base.ProfilePicture == null)
                {
                    base.ProfilePicture = ProfilePictureService.GetFor(Name);
                }

                return base.ProfilePicture;
            }
        }
    }
}
