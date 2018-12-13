﻿using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace AOPTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] a = new string[] { "2", "3" };
            SetMail();
            Console.WriteLine("Hello World!");
        }

        public void PrintText(string Text)
        {
            Console.WriteLine(Text);
        }

        public static void SetMail()
        {
            MailMessage mailMessage = new MailMessage();
            //发件人邮箱地址，方法重载不同，可以根据需求自行选择。
            mailMessage.From = new MailAddress("18702823318@163.com");
            //收件人邮箱地址。
            mailMessage.To.Add(new MailAddress("852227791@qq.com"));
            //邮件标题。
            mailMessage.Subject = "发送邮件测试";
            //邮件内容。
            mailMessage.Body = "这是我给你发送的第一份邮件哦！";

            //实例化一个SmtpClient类。
            SmtpClient client = new SmtpClient();
            //在这里我使用的是qq邮箱，所以是smtp.qq.com，如果你使用的是126邮箱，那么就是smtp.126.com。
            client.Host = "smtp.163.com";
            //使用安全加密连接。
            client.EnableSsl = true;
            //不和请求一块发送。
            client.UseDefaultCredentials = false;
            //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
            client.Credentials = new NetworkCredential("18702823318@163.com", "sdy163");//oxcijljjgkxfbehb
            //发送
            client.Send(mailMessage);
            Console.WriteLine("发送成功！");
            Console.ReadKey();
        }
        }
}
