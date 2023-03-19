using CQRS.Core.Commands;

namespace Post.Cmd.Api.Commands
{
    public class AddCommentCommand : BaseCommand
    {
        //Comment
        public string Comment { get; set; }
        //Username
        public string Username { get; set; }
    }
}
