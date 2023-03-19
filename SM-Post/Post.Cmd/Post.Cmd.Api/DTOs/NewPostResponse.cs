using Post.Commom.DTOs;

namespace Post.Cmd.Api.DTOs;

public class NewPostResponse : BaseResponse
{
    public Guid Id { get; set; }
    
}