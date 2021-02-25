namespace FasePractica.Services
{
    public class TenantStorage
    {
        static TenantStorage singleton;

        public static TenantStorage Instance()
        {
            return Instance(null);
        }
        

        public static TenantStorage Instance(string tenant)
        {
            if (singleton == null)
                singleton = new TenantStorage(tenant);
            return singleton;
        }

        public string Tenant { get; private set; }

        private TenantStorage(string tenant)
        {
            Tenant = tenant;
        }

        public static void Remove()
        {
            if (singleton != null)
                singleton = null;
        }
    }
}
