using System;


namespace ApplicationCore.Exceptions
{
    public class WishListNotFoundException : Exception
    {
        

            public WishListNotFoundException(int wishlistId) : base($"No wishlist found with id {wishlistId}")
        {
        }

        protected WishListNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public WishListNotFoundException(string message) : base(message)
        {
        }

        public WishListNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}