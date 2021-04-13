using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UserManagement_GymBookings
{
    internal class AuthorizationFilter : IFilterMetadata
    {
        private AuthorizationPolicy policy;

        public AuthorizationFilter(AuthorizationPolicy policy)
        {
            this.policy = policy;
        }
    }
}