using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BotNet
{
    public static class Resolver
    {
        private static volatile bool _loaded;

        public static void RegisterDependencyResolver()
        {
            try
            {
                if (!_loaded)
                {
                    AppDomain.CurrentDomain.AssemblyResolve += OnResolve;
                    _loaded = true;
                }
            }
            catch //(Exception ex)
            {
                //Console.WriteLine(ex.ToString());
            }
        }
        private static Assembly OnResolve(object sender, ResolveEventArgs args)
        {

            Assembly execAssembly = Assembly.GetExecutingAssembly();
            string resourceName = String.Format("{0}.{1}.dll",
                execAssembly.GetName().Name,
                new AssemblyName(args.Name).Name);

            using (var stream = execAssembly.GetManifestResourceStream(resourceName))
            {
                int read = 0, toRead = (int)stream.Length;
                byte[] data = new byte[toRead];

                do
                {
                    int n = stream.Read(data, read, data.Length - read);
                    toRead -= n;
                    read += n;
                } while (toRead > 0);

                return Assembly.Load(data);
            }
        }
    }
}
