using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using VeyraApi.Interfaces;

namespace VeyraApi.Services;

public class GroqService : IHazemService
{
    private readonly HttpClient _http;
    private readonly string _apiKey;
    private const string ApiUrl = "https://api.groq.com/openai/v1/chat/completions";

    public GroqService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _apiKey = config["GROQ_API_KEY"] ?? "";
    }

    public async Task<string> ChatAsync(string message, string sessionId)
    {
        if (string.IsNullOrEmpty(_apiKey))
            return "عذرًا، خدمة حازم غير متاحة حاليًا. تواصل معنا على واتساب: +201035199880";

        var systemPrompt = @"أنت حازم، المساعد العقاري لشركة Veyra Developments. ترد بالعامية المصرية دائمًا.
مهمتك: مساعدة الزوار في اختيار المشروع المناسب، التشطيب، أو الأجهزة الذكية.
- ردودك قصيرة (2-6 أسطر)
- اسأل سؤال واحد فقط كل مرة
- في النهاية اعرض خطوة واضحة: حجز زيارة / طلب عرض سعر / استشارة
- لا تختلق معلومات غير موجودة في بيانات الشركة
رقم الشركة: +201035199880";

        var payload = new
        {
            model = "llama-3.3-70b-versatile",
            messages = new[] {
                new { role = "system", content = systemPrompt },
                new { role = "user", content = message }
            },
            temperature = 0.7,
            max_tokens = 500
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Post, ApiUrl);
        request.Headers.Add("Authorization", $"Bearer {_apiKey}");
        request.Content = content;

        var response = await _http.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        return doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? "...";
    }
}
