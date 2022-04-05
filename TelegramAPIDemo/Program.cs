using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Telegram.Bot;

namespace TelegramAPIDemo
{
    public class Program
    {
        private static TelegramBotClient botClient;
        public static void Main(string[] args)
        {
            botClient = new TelegramBotClient("5051886466:AAGVAwmfPH42m54eBOHlV6Cnwr5gHn2RwfE");
            
        }
    }
}
