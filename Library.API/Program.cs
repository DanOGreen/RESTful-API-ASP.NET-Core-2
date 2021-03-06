﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Library.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        //public static IWebHost BuildWebHost(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .ConfigureAppConfiguration((builderContext, config) =>
        //        {
        //            config.AddJsonFile("appconfig.json", optional: true, reloadOnChange: true);
        //        })
        //        .UseStartup<Startup>()
        //        .UseNLog()
        //        .Build();
    }
}
