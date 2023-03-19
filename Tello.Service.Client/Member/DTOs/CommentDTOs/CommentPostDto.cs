using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using Tello.Core.Entities;

namespace Tello.Service.Client.Member.DTOs.CommentDTOs
{
    public class CommentPostDto
    {
        public string Desc { get; set; }
        public int ProductItemId { get; set; }
    }
    public class CommentPostDtoValidatior : AbstractValidator<Comment>
    {
        public CommentPostDtoValidatior()
        {
            RuleFor(x => x.ProductItemId).NotEmpty();
            RuleFor(x => x.Desc).NotEmpty().MaximumLength(200);
        }
    }
}
