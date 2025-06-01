using Jarvis.Extensions;
using Jarvis.Services;

namespace Jarvis.Handlers
{
    public class JarvisApiHandler : DelegatingHandler
    {
        private readonly AuthenticatedUser _authUser;

        public JarvisApiHandler(AuthenticatedUser authUser) 
        {
            _authUser = authUser;
        }

        public JarvisApiHandler(AuthenticatedUser authUser, HttpMessageHandler innerHandler) : base(innerHandler)
        {
            _authUser = authUser;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if(request.RequestUri != null && request.RequestUri.OriginalString.Contains("{{userId}}"))
            {
                if (_authUser.CurrentUser.Identity?.IsAuthenticated ?? false)
                {
                    var userId = _authUser.CurrentUser.GetId();

                    var builder = new UriBuilder(request.RequestUri.OriginalString.Replace("{{userId}}", userId.ToString()));

                    request.RequestUri = builder.Uri;
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
