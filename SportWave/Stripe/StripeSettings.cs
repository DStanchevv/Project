namespace SportWave.Stripe
{
    public class StripeSettings
    {
        public readonly string UrlsHost = Environment.GetEnvironmentVariable("STRIPE_URLS_HOST");
        public readonly string SecretKey = Environment.GetEnvironmentVariable("STRIPE_API_SECRET");
        public readonly string PublicKey = Environment.GetEnvironmentVariable("STRIPE_API_KEY");
    }
}
