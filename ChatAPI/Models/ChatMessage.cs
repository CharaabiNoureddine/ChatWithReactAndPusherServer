using System.ComponentModel.DataAnnotations;

namespace ChatWithReact
{
    public class ChatMessage
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public string AuthorTwitterHandle { get; set; }
    }
}