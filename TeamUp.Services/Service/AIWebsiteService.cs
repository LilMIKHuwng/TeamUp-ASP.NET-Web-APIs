using TeamUp.Core.APIResponse;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Contract.Services.Interface;

namespace TeamUp.Services.Service
{
    public class AIWebsiteService : IAIWebsiteService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey = "AIzaSyAL9QvA5ZmZC0QkkWjzSZsPKFBj07YfZi4";

        public AIWebsiteService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ApiResult<string>> GetWebsiteAIResponseAsync(string question)
        {
            var websiteInfo = @"
                TeamUp là một nền tảng thể thao trực tuyến giúp kết nối người chơi, chủ sân và huấn luyện viên. Website hỗ trợ các tính năng chính như:

                1. Đặt sân thể thao: Người dùng có thể tìm và đặt các sân bóng đá, cầu lông, tennis, v.v.
                2. Tìm người chơi cùng: Kết nối những người có nhu cầu chơi thể thao theo khu vực, thời gian, trình độ.
                3. Thuê huấn luyện viên: Người dùng có thể tìm và thuê các huấn luyện viên chuyên nghiệp theo môn thể thao, lịch trình, giá.
                4. Tạo và quản lý phòng: Cho phép người dùng tạo phòng tập hoặc nhóm luyện tập có quản lý.
                5. Thanh toán điện tử: Tích hợp VNPay để thanh toán đặt sân, thuê huấn luyện viên dễ dàng và an toàn.
                6. Quản lý khu thể thao: Chủ sân có thể đăng ký và quản lý nhiều sân nằm trong một khu thể thao.

                Đối tượng người dùng:
                - Người chơi thể thao (USER)
                - Chủ sân thể thao (OWNER)
                - Huấn luyện viên (COACH)

                Trang web hỗ trợ trải nghiệm thân thiện trên cả máy tính và điện thoại.

                Nếu câu hỏi không liên quan đến chủ đề đặt sân, thuê huấn luyện viên, tạo phòng tập, thanh toán hoặc hệ thống TeamUp, hãy trả lời: 
                'Xin lỗi, tôi chỉ có thể hỗ trợ các câu hỏi liên quan đến nền tảng thể thao TeamUp.'
                ";
            ;

            var prompt = $@"
                Bạn là một AI hỗ trợ khách hàng cho nền tảng thể thao TeamUp.

                Câu hỏi từ người dùng:
                {question}

                Thông tin về website:
                {websiteInfo}

                Hãy trả lời ngắn gọn, chính xác và thân thiện. Nếu câu hỏi không liên quan đến dịch vụ TeamUp, hãy từ chối trả lời một cách lịch sự.
                ";

            return await SendRequestToAI(prompt);
        }

        private async Task<ApiResult<string>> SendRequestToAI(string prompt)
        {
            var requestBody = new
            {
                contents = new[] { new { parts = new object[] { new { text = prompt } } } }
            };

            string apiUrl = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={_apiKey}";
            var httpClient = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                return new ApiErrorResult<string>($"API request failed: {response.StatusCode}");
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            try
            {
                JObject jsonResponse = JObject.Parse(responseContent);
                string generatedText = jsonResponse["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString();
                return new ApiSuccessResult<string>(generatedText ?? "Không có phản hồi từ AI.");
            }
            catch (JsonException)
            {
                return new ApiErrorResult<string>("Lỗi xử lý phản hồi từ AI.");
            }
        }
    }
}
