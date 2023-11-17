using HtmlAgilityPack;
using System.Collections.Generic;

namespace Lich.HtmlPack
{
    public class crawlDataHtml
    {
        private readonly HtmlDocument _htmlDocument;

        // Hàm khởi tạo nhận một chuỗi HTML làm đầu vào
        
        public crawlDataHtml(string htmlContent)
        {
            // Tạo một đối tượng HtmlDocument và tải nội dung HTML vào đó
            _htmlDocument = new HtmlDocument();
            _htmlDocument.LoadHtml(htmlContent);
        }
        // Phương thức để lấy tiêu đề của trang HTML
        public string GetTitle()
        {
            return _htmlDocument.DocumentNode.SelectSingleNode("//title")?.InnerHtml;
        }
        // Phương thức để lấy nội dung của một thẻ HTML theo tên thẻ
        public string GetElementContent(string tagName)
        {
            return _htmlDocument.DocumentNode.SelectSingleNode($"//{tagName}")?.InnerHtml;
        }
        // Phương thức để lấy nội dung của một thẻ HTML dựa trên XPath
        public string GetElementContentByXPath(string xPath)
        {
            return _htmlDocument.DocumentNode.SelectSingleNode(xPath)?.InnerHtml;
        }
        // Phương thức để lấy nội dung text của một thẻ HTML dựa trên class
        public string GetTextByClass(string className)
        {
            var node = _htmlDocument.DocumentNode.SelectSingleNode($"//*[contains(@class,'{className}')]");

            // Kiểm tra xem node có tồn tại hay không trước khi lấy text
            return node?.InnerText;
        }
        public List<string> GetTextsByClass(string className)
        {
            var nodes = _htmlDocument.DocumentNode.SelectNodes($"//*[contains(@class, '{className}')]");
            var texts = new List<string>();

            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    texts.Add(node.InnerText);
                }
            }

            return texts;
        }
    }
}
