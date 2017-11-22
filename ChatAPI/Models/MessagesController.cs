using PusherServer;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

// [EnableCors("*", "*", "*")]
using System.Web.Http.Cors;

namespace ChatWithReact
{
    [EnableCors("*", "*", "*")]
    public class MessagesController : ApiController
    {
        private static List<ChatMessage> messages =
            new List<ChatMessage>()
            {
                new ChatMessage
                {
                    AuthorTwitterHandle = "Pusher",
                    Text = "Hi there! ?"
                },
                new ChatMessage
                {
                    AuthorTwitterHandle = "Pusher",
                    Text = "Welcome to your chat app"
                }
            };

        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(
                HttpStatusCode.OK,
                messages);
        }

        public HttpResponseMessage Post(ChatMessage message)
        {
            if (message == null || !ModelState.IsValid)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    "Invalid input");
            }
            messages.Add(message);

            var pusher = new Pusher(
                "434176",
                "6c60600dbf50171243ce",
                "83bdb644957bc741751e",
                new PusherOptions
                {
                    Cluster = "mt1"
                }
            );

            pusher.TriggerAsync(
                channelName: "messages",
                eventName: "new_message",
                data: new
                {
                    AuthorTwitterHandle = message.AuthorTwitterHandle,
                    Text = message.Text
                }
            );

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}