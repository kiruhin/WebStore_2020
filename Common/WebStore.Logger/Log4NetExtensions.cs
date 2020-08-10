using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace WebStore.Logger
{
    public static class Log4NetExtensions
    {
        public static ILoggerFactory AddLog4Net(
            this ILoggerFactory factory,
            string log4NetConfigFile)
        {
            factory.AddProvider(new Log4NetProvider(log4NetConfigFile));
            return factory;
        }
 
        // второй метод для возможности вызова без параметров
        // этот метод будет вызывать предыдущий с файлом конфигурации по умолчанию
        public static ILoggerFactory AddLog4Net(this ILoggerFactory factory)
        {
            return factory.AddLog4Net("log4net.config");
        }
    }

}
