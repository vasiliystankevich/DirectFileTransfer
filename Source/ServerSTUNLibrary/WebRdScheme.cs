using System;

namespace TransportProtocolCommonLibrary
{
    public class WebRdScheme
    {
        protected static void Registeration()
        {
            if (!UriParser.IsKnownScheme("webrd"))
                UriParser.Register(new GenericUriParser(GenericUriParserOptions.AllowEmptyAuthority), "webrd", 25000);
        }

        public static void Validation(ref Uri uri)
        {
            Registeration();
            if (uri.Port == -1) uri = new Uri(uri.ToString());
        }
    }
}
