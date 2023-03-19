using CQRS.Core.Commands;

namespace Post.Cmd.Api.Commands
{
    public class EditCommentCommand : BaseCommand
    {
        //CommentId
        public Guid CommentId { get; set; }
        //Comment
        public string Comment { get; set; }
        //Username
        public string Username { get; set; }
    }
}
