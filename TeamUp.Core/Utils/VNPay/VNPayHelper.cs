using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TeamUp.Core.Utils.VNPay
{
    public static class VNPayHelper
    {
        public static string GeneratePaymentUrl(
            decimal amount,
            string txnRef,
            string orderInfo,
            string vnpUrl,         // ✅ domain của VNPay
            string tmnCode,
            string hashSecret,
            string returnUrl)
        {
            var vnp_Params = new SortedDictionary<string, string>
            {
                { "vnp_Version", "2.1.0" },
                { "vnp_Command", "pay" },
                { "vnp_TmnCode", tmnCode },
                { "vnp_Amount", ((long)Math.Round(amount * 100)).ToString() },
                { "vnp_CurrCode", "VND" },
                { "vnp_TxnRef", txnRef },
                { "vnp_OrderInfo", orderInfo },
                { "vnp_OrderType", "other" },
                { "vnp_Locale", "vn" },
                { "vnp_ReturnUrl", returnUrl },
                { "vnp_IpAddr", "127.0.0.1" },
                { "vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss") }
            };

            string queryString = BuildQueryString(vnp_Params);
            string signData = BuildDataToHash(vnp_Params);
            string vnp_SecureHash = HmacSHA512(hashSecret, signData);

            return $"{vnpUrl}?{queryString}&vnp_SecureHash={vnp_SecureHash}";
        }

        public static VNPayReturnResult ProcessReturn(IQueryCollection vnpParams, string hashSecret)
        {
            var vnpData = new SortedDictionary<string, string>();
            string vnpSecureHash = "";

            foreach (var key in vnpParams.Keys)
            {
                if (key == "vnp_SecureHash" || key == "vnp_SecureHashType")
                {
                    if (key == "vnp_SecureHash")
                        vnpSecureHash = vnpParams[key];
                }
                else if (key.StartsWith("vnp_"))
                {
                    vnpData.Add(key, vnpParams[key]);
                }
            }

            string signData = BuildDataToHash(vnpData);
            string computedHash = HmacSHA512(hashSecret, signData);

            if (computedHash.Equals(vnpSecureHash, StringComparison.InvariantCultureIgnoreCase))
            {
                string responseCode = vnpParams["vnp_ResponseCode"];
                return new VNPayReturnResult
                {
                    IsSuccess = responseCode == "00",
                    Message = responseCode == "00" ? "Thanh toán thành công." : $"VNPay từ chối giao dịch: {responseCode}"
                };
            }

            return new VNPayReturnResult
            {
                IsSuccess = false,
                Message = "Chữ ký không hợp lệ. Có thể bị giả mạo."
            };
        }

        private static string BuildQueryString(SortedDictionary<string, string> data)
        {
            var query = new StringBuilder();
            foreach (var kv in data)
            {
                query.Append(Uri.EscapeDataString(kv.Key) + "=" + Uri.EscapeDataString(kv.Value) + "&");
            }
            return query.ToString().TrimEnd('&');
        }

        private static string BuildDataToHash(SortedDictionary<string, string> data)
        {
            var raw = new StringBuilder();
            foreach (var kv in data)
            {
                raw.Append(kv.Key + "=" + kv.Value + "&");
            }
            return raw.ToString().TrimEnd('&');
        }

        private static string HmacSHA512(string key, string input)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }

    public class VNPayReturnResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
