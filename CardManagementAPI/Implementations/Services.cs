using CardManagementAPI.Abstractions;
using CardManagementAPI.EntityConfiguration;
using CardManagementAPI.Models;
using CardManagementAPI.Models._ConfigurationModels;
using CardManagementAPI.Models.EntityDTOs;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CardManagementAPI.Implementations
{
    public class Services : IServices
    {
        private readonly AppDbContext _context;
        private readonly AESConfiguration _settings;
        private readonly IUnitOfWork _unit;
        public Services(AppDbContext context, IOptions<AESConfiguration> settings, IUnitOfWork unit)
        {
            _context = context;
            _settings = settings.Value;
            _unit = unit;
        }
        public async Task<int?> GeneratePinAsync(string customerId)
        {
            Customers customer = _unit.Customers.GetCustomer(customerId);
            if (customer == null)
                return null;
            int newPin = GenerateRandomNo();
            customer.Pin = await Encrypt(newPin);
            _unit.SaveToDB();
            return newPin;
        }
        public async Task<string> ActivateCardAsync(CardDTO cardDTO)
        {
            Customers customer = _unit.Customers.GetCustomer(cardDTO.CustomerID);
            if (customer == null)
                return null;
            if (customer.Pin == null)
                return "Please generate your pin";
            if (customer.Status)
                return "Already Activated";
            int dbPin = await Decrypt(customer.Pin);
            if (dbPin != cardDTO.Pin)
                return "Incorrect Pin";
            customer.Status = true;
            _unit.SaveToDB();
            return "Successfully Activated";
        }
        public async Task<string> DeactivateCardAsync(CardDTO cardDTO)
        {
            Customers customer = _unit.Customers.GetCustomer(cardDTO.CustomerID);
            if (customer == null)
                return null;
            if (!customer.Status)
                return "Already Deactivated";
            int dbPin = await Decrypt(customer.Pin);
            if (dbPin != cardDTO.Pin)
                return "Incorrect Pin";
            customer.Status = false;
            customer.Pin = null;
            _unit.SaveToDB();
            return "Successfully Deactivated";
        }
        public void AddCustomer(CustomerDTO customerDTO)
        {
            Customers customer = new Customers
            {
                CustomerId = Guid.NewGuid().ToString(),
                FirstName = customerDTO.FirstName,
                LastName = customerDTO.LastName
            };
            _context.Add(customer);
            _unit.SaveToDB();
        }
        private Task<string> Encrypt(int pin)
        {
            try
            {
                return Task.Run(() =>
                {
                    byte[] secretkeyByte = Encoding.UTF8.GetBytes(_settings.SecretKey);
                    byte[] publickeybyte = Encoding.UTF8.GetBytes(_settings.PublicKey);
                    byte[] inputbyteArray = Encoding.UTF8.GetBytes(Convert.ToString(pin));
                    using DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                });

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        private Task<int> Decrypt(string encyptText)
        {
            try
            {
                return Task.Run(() =>
                {
                    byte[] privatekeyByte = Encoding.UTF8.GetBytes(_settings.SecretKey);
                    byte[] publickeybyte = Encoding.UTF8.GetBytes(_settings.PublicKey);
                    byte[] inputbyteArray = new byte[encyptText.Replace(" ", "+").Length];
                    inputbyteArray = Convert.FromBase64String(encyptText.Replace(" ", "+"));
                    using DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    return Convert.ToInt32(encoding.GetString(ms.ToArray()));
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        private static int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
    }
}
