using Clicks.Yoga.Context;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Clicks.Yoga.Import.ImageStoreAzure
{
    class Program
    {
        private static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new NinjectSettings { AllowNullInjection = true, InjectNonPublic = true }, new YogaNinjectModule());
            var context = kernel.Get<IEntityContext>();
            var store = kernel.Get<IImageStore>();

            foreach (var image in context.Images.ToList())
            {
                var path = Path.Combine(ConfigurationManager.AppSettings["LocalImagePath"] + image.Path, image.Name);

                if (!File.Exists(path))
                {
                    Console.WriteLine("Failed: " + path);
                    continue;
                }

                FileStream file = File.Open(path, FileMode.Open);

                image.Source = file;

                store.SaveImage(image);

                Console.WriteLine("Succeed: " + path);
            }
            Console.ReadLine();
        }
    }
}
