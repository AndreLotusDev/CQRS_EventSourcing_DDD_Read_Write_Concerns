using CQRS.Core.Commands;

namespace Post.Cmd.Api.Commands
{
    public class NewPostCommand : BaseCommand
    {
        //author
        public string Author { get; set; }
        //message
        public string Message { get; set; }
    }
}
