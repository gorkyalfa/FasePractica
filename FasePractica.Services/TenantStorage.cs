namespace FasePractica.Services
{
    public class TenantStorage
    {
        static TenantStorage singleton;

        public static TenantStorage Instance()
        {
            return Instance(null, null);
        }
        

        public static TenantStorage Instance(string tenant, string institute)
        {
            if (singleton == null)
                singleton = new TenantStorage(tenant, institute);
            return singleton;
        }

        public string Tenant { get; private set; }
        public string Institute { get; private set; }

        private TenantStorage(string tenant, string institute)
        {
            Tenant = tenant;
            Institute = institute;
        }

        public static void Remove()
        {
            if (singleton != null)
                singleton = null;
        }
    }
}
